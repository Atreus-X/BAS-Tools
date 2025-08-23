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
    public partial class BACnet_IP : UserControl, IHistorySupport
    {
        // BACnet communication client
        private BacnetClient _bacnetClient;
        private Thread _bacnetThread;
        private readonly HistoryManager _historyManager; // Use the dedicated HistoryManager

        // Data storage
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;

        public BACnet_IP()
        {
            InitializeComponent();
            _historyManager = new HistoryManager("BACnet_IP_"); // Initialize with a unique prefix
            PopulateDefaultValues();
            LoadHistory(); // Load history after populating default network interfaces
            UpdateAllStates(null, null);

            // Wire up event handlers to save history on text changes for ComboBoxes
            // We'll use Leave event for better control over when history is saved
            ipAddressComboBox.Leave += (sender, e) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            instanceNumberComboBox.Leave += (sender, e) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            ipPortComboBox.Leave += (sender, e) => SaveComboBoxEntry(ipPortComboBox, "ipPort");
            apduTimeoutComboBox.Leave += (sender, e) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdIpComboBox.Leave += (sender, e) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
            bbmdTtlComboBox.Leave += (sender, e) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");
            interfaceComboBox.Leave += (sender, e) => SaveComboBoxEntry(interfaceComboBox, "interface"); // Save selected interface
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

                if (interfaceComboBox.SelectedIndex > 0 && interfaceComboBox.SelectedItem != null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(interfaceComboBox.SelectedItem.ToString(), @"\((.*?)\)");
                    if (match.Success)
                    {
                        localIp = match.Groups[1].Value;
                    }
                }

                // Save the currently selected interface to history
                _historyManager.AddEntry("interface", interfaceComboBox.Text);


                var transport = new BacnetIpUdpProtocolTransport(int.Parse(ipPortComboBox.Text), true, false, 1472, localIp);
                _bacnetClient = new BacnetClient(transport) { Timeout = int.Parse(apduTimeoutComboBox.Text) };


                _bacnetClient.OnIam += OnIamHandler;

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
            // Always add network interfaces, as they are system-dependent
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
                if (e.Node.Tag is BacnetAddress bacnetAddress)
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

            void handler(BacnetClient sender, BacnetAddress address, uint receivedDeviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
            {
                if (receivedDeviceId == deviceId)
                {
                    tcs.TrySetResult(address);
                }
            }

            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);

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

        // --- History Management ---
        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(ipAddressComboBox, "ipAddress");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");
            PopulateComboBoxWithHistory(ipPortComboBox, "ipPort");
            PopulateComboBoxWithHistory(apduTimeoutComboBox, "apduTimeout");
            PopulateComboBoxWithHistory(bbmdIpComboBox, "bbmdIp");
            PopulateComboBoxWithHistory(bbmdTtlComboBox, "bbmdTtl");
            PopulateComboBoxWithHistory(interfaceComboBox, "interface"); // Load selected interface

            // Set default values if history is empty or not present
            if (string.IsNullOrEmpty(ipAddressComboBox.Text)) ipAddressComboBox.Text = "192.168.1.200";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (string.IsNullOrEmpty(ipPortComboBox.Text)) ipPortComboBox.Text = "47808";
            if (string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "5000";
            if (string.IsNullOrEmpty(bbmdIpComboBox.Text)) bbmdIpComboBox.Text = "172.19.10.102";
            if (string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
            // For interfaceComboBox, if history is empty, it will default to "0.0.0.0 (Any)" due to PopulateDefaultValues.
            // If there's a history entry, it will select that.
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            comboBox.Items.Clear(); // Clear existing items (except for interfaceCombo which is system specific)

            // Only clear if it's not the interfaceComboBox
            if (comboBox.Name != interfaceComboBox.Name)
            {
                comboBox.Items.Clear();
            }

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                // If it's the interfaceComboBox, ensure system-detected interfaces are still present
                if (comboBox.Name == interfaceComboBox.Name)
                {
                    // Add history items, but ensure no duplicates with system items
                    foreach (var item in historyList.Where(h => !comboBox.Items.Contains(h)))
                    {
                        comboBox.Items.Insert(0, item); // Add to top
                    }
                }
                else
                {
                    comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                }

                // Set the text to the last entered value
                comboBox.Text = historyList.First();
            }
            else if (comboBox.Name == interfaceComboBox.Name && comboBox.Items.Count > 0)
            {
                // Select "0.0.0.0 (Any)" if no specific interface was previously saved
                comboBox.SelectedIndex = 0;
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (!string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                // After saving, re-populate to update the dropdown list with the new/moved item
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text; // Restore the current text after repopulating
            }
        }

        public void ClearHistory()
        {
            _historyManager.ClearHistory();
            ipAddressComboBox.Items.Clear();
            instanceNumberComboBox.Items.Clear();
            ipPortComboBox.Items.Clear();
            apduTimeoutComboBox.Items.Clear();
            bbmdIpComboBox.Items.Clear();
            bbmdTtlComboBox.Items.Clear();

            // Re-populate system interfaces after clearing history
            interfaceComboBox.Items.Clear();
            PopulateDefaultValues();
            interfaceComboBox.SelectedIndex = 0; // Select "0.0.0.0 (Any)"

            Log("BACnet/IP history cleared.");
        }

        // This is important for cleanup when the main application closes
        public void Shutdown()
        {
            _historyManager.SaveHistory(); // Ensure history is saved on shutdown
            _bacnetClient?.Dispose();
        }
    }
}
