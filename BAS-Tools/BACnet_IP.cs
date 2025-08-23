using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.BACnet;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class BACnet_IP : UserControl
    {
        // BACnet communication client
        private BacnetClient _bacnetClient;
        private Thread _bacnetThread;


        // Data storage
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;
        public BACnet_IP()
        {
            InitializeComponent();
            PopulateDefaultValues();
            UpdateAllStates(null, null);
        }

        // --- BACnet Logic ---
        private void EnsureBacnetClientStarted()
        {
            // If the client is already running, do nothing.
            if (_isClientStarted && _bacnetClient != null)
            {
                return;
            }

            // If a previous client exists, dispose it before creating a new one.
            if (_bacnetClient != null)
            {
                _bacnetClient.Dispose();
                _bacnetClient = null;
                Thread.Sleep(200); // Give it a moment to release resources
            }

            try
            {
                Log("Initializing BACnet client...");

                string localIp = "0.0.0.0";

                if (interfaceComboBox.SelectedIndex > 0)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(interfaceComboBox.Text, @"\((.*?)\)");
                    if (match.Success)
                    {
                        // Now we just assign a new value to the existing variable
                        localIp = match.Groups[1].Value;
                    }
                }

                var transport = new BacnetIpUdpProtocolTransport(int.Parse(ipPortComboBox.Text), true, false, 1472, localIp);
                _bacnetClient = new BacnetClient(transport) { Timeout = int.Parse(apduTimeoutComboBox.Text) };


                _bacnetClient.OnIam += OnIamHandler;

                // ... (event subscriptions, thread start, etc.)

                _bacnetThread = new Thread(() =>
                {
                    try { _bacnetClient.Start(); }
                    catch (Exception ex) { Log($"--- BACnet Thread Error: {ex.Message} ---"); }
                })
                {
                    IsBackground = true
                };
                _bacnetThread.Start();
                Thread.Sleep(100);

                _isClientStarted = true; // Set the flag
                Log("BACnet client started.");

                // ... (BBMD registration)
            }
            catch (Exception ex)
            {
                _isClientStarted = false; // Ensure flag is false on failure
                Log($"--- ERROR initializing BACnet client: {ex.Message} ---");
                MessageBox.Show($"Error during BACnet initialization: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnIamHandler(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
            this.Invoke((MethodInvoker)delegate {
                Log($"I-Am received from {adr} (DeviceID: {deviceId})");
                string deviceDisplay = $"{deviceId} ({adr})";

                if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                {
                    var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = adr };
                    deviceTreeView.Nodes.Add(node);
                }
            });
        }

        private void PingButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instanceNumberComboBox.Text))
            {
                MessageBox.Show("Instance number is required for Ping.", "Error");
                return;
            }

            EnsureBacnetClientStarted(); // Use the new method
            if (!_isClientStarted) return;

            uint deviceId = uint.Parse(instanceNumberComboBox.Text);
            Log($"Pinging Device ID: {deviceId}...");

            _bacnetClient.WhoIs(lowLimit: (int)deviceId, highLimit: (int)deviceId);

            _lastPingedDeviceId = deviceId;
            UpdateAllStates(null, null);
        }

        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted(); // Use the new method
            if (!_isClientStarted) return;

            deviceTreeView.Nodes.Clear();
            Log("Discovering devices with global Who-Is broadcast...");
            _bacnetClient.WhoIs();
        }

        private async void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (_lastPingedDeviceId == null)
            {
                MessageBox.Show("Please successfully Ping a device first to select it for object discovery.", "Device Not Selected");
                return;
            }

            EnsureBacnetClientStarted(); // Use the new method
            if (!_isClientStarted) return;

            try
            {
                uint deviceId = _lastPingedDeviceId.Value;
                Log($"Discovering objects for Device {deviceId}...");

                BacnetAddress deviceAddress = await FindDeviceAddressAsync(deviceId);
                if (deviceAddress == null)
                {
                    Log($"--- ERROR: Could not resolve address for Device {deviceId}. It may be offline. ---");
                    return;
                }

                var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                var propertyId = BacnetPropertyIds.PROP_OBJECT_LIST;

                if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId, out IList<BacnetValue> objectList))
                {
                    Log($"--- SUCCESS: Found {objectList.Count} objects. ---");
                    PopulateObjectTree(objectList);
                }
                else
                {
                    Log("--- ERROR: Failed to read object list. ---");
                }
            }
            catch (Exception ex)
            {
                Log($"--- Discover Objects Error: {ex.Message} ---");
            }
        }

        private async void ReadPropertyButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instanceNumberComboBox.Text))
            {
                MessageBox.Show("Instance number is required to read.", "Error");
                return;
            }

            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;

            try
            {
                uint deviceId = uint.Parse(instanceNumberComboBox.Text);
                BacnetAddress deviceAddress = await FindDeviceAddressAsync(deviceId);
                if (deviceAddress == null)
                {
                    Log($"--- ERROR: Device {deviceId} not found. Ping or Discover first. ---");
                    return;
                }

                // This is still hardcoded, but now it will work.
                // A great next step is to get this from the object tree view selection.
                var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                var propertyId = BacnetPropertyIds.PROP_OBJECT_NAME;

                Log($"Reading {propertyId} from Device {deviceId}...");

                // Run the blocking network call on a background thread
                await Task.Run(() =>
                {
                    // The 'out' parameter gets filled with the result
                    if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId, out IList<BacnetValue> values))
                    {
                        // We're on a background thread, so we must use Invoke to log the result
                        Log($"--- SUCCESS: Read ACK for {propertyId} ---");
                        foreach (var val in values)
                        {
                            Log($"  Value: {val.Value}");
                        }
                    }
                    else
                    {
                        Log($"--- ERROR: Failed to read property {propertyId}. No response or error received. ---");
                    }
                });
            }
            catch (Exception ex)
            {
                Log($"--- Read Error: {ex.Message} ---");
            }
        }

        private void Log(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(Log), message);
                return;
            }
            outputTextBox.AppendText(DateTime.Now.ToLongTimeString() + ": " + message + Environment.NewLine);
            outputTextBox.ScrollToCaret();
        }

        private void PopulateDefaultValues()
        {
            bbmdIpComboBox.Text = "172.19.10.102";
            ipPortComboBox.Text = "47808";
            apduTimeoutComboBox.Text = "5000";
            bbmdTtlComboBox.Text = "3600";

            interfaceComboBox.Items.Add("0.0.0.0 (Any)");
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            interfaceComboBox.Items.Add($"{ni.Name} ({ip.Address})");
                        }
                    }
                }
            }
            interfaceComboBox.SelectedIndex = 0;
        }

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            pingButton.Enabled = instanceExists;
            readPropertyButton.Enabled = instanceExists;
            writePropertyButton.Enabled = instanceExists;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                uint deviceId = uint.Parse(e.Node.Name);
                instanceNumberComboBox.Text = deviceId.ToString();
                if (!(e.Node.Tag is BacnetAddress bacnetAddress))
                {
                }
                else
                {
                    // Take the first 4 bytes (the IP address) and join them with "."
                    ipAddressComboBox.Text = string.Join(".", bacnetAddress.adr.Take(4));
                }
                _lastPingedDeviceId = deviceId;
                UpdateAllStates(null, null);
                DiscoverObjectsButton_Click(null, null);
            }
        }

        private void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            objectTreeView.Nodes.Clear();

            var objectGroups = objectList
                .Select(val => (BacnetObjectId)val.Value)
                .GroupBy(objId => objId.type)
                .OrderBy(g => g.Key.ToString());

            foreach (var group in objectGroups)
            {
                var parentNode = new TreeNode(group.Key.ToString());
                objectTreeView.Nodes.Add(parentNode);
                foreach (var objId in group.OrderBy(o => o.instance))
                {
                    var childNode = new TreeNode(objId.instance.ToString()) { Tag = objId };
                    parentNode.Nodes.Add(childNode);
                }
            }
        }

        private async Task<BacnetAddress> FindDeviceAddressAsync(uint deviceId)
        {
            var nodes = deviceTreeView.Nodes.Find(deviceId.ToString(), true);
            if (nodes.Length > 0 && nodes[0].Tag is BacnetAddress adr)
            {
                return adr; // Return the cached address
            }

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            var tcs = new TaskCompletionSource<BacnetAddress>();

            // This handler signature is now corrected to match your library's event
            void handler(BacnetClient sender, BacnetAddress address, uint receivedDeviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
            {
                if (receivedDeviceId == deviceId)
                {
                    // When we get the right response, complete the task
                    tcs.TrySetResult(address);
                }
            }

            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);

            // Asynchronously wait for the task to complete or timeout
            var timeoutTask = Task.Delay(2000);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            _bacnetClient.OnIam -= handler; // Always clean up the handler

            if (completedTask == tcs.Task)
            {
                return await tcs.Task; // Success
            }
            else
            {
                Log($"--- Timeout: No I-Am response from Device {deviceId}. ---");
                return null; // Failure (timeout)
            }
        }

        // This is important for cleanup when the main application closes
        public void Shutdown()
        {
            _bacnetClient?.Dispose();
        }
    }
}