using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private HistoryManager _historyManager;

        // Data storage
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;

        public BACnet_IP()
        {
            InitializeComponent();
            // Defer initialization to the Load event to make it designer-safe
            this.Load += new System.EventHandler(this.BACnet_IP_Load);
        }

        private void BACnet_IP_Load(object sender, EventArgs e)
        {
            // Prevent initialization code from running in the Visual Studio Designer
            bool inDesignMode = this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            if (inDesignMode)
            {
                return;
            }

            // Wrap initialization in a try-catch block to prevent designer crashes
            try
            {
                _historyManager = new HistoryManager("BACnet_IP_");
                PopulateDefaultValues();
                LoadHistory();
                UpdateAllStates(null, null);

                // Wire up event handlers here, instead of in the designer, to make the form designer-safe.
                this.deviceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DeviceTreeView_AfterSelect);
                this.instanceNumberComboBox.TextChanged += new System.EventHandler(this.UpdateAllStates);
                this.discoverButton.Click += new System.EventHandler(this.DiscoverButton_Click);
                this.pingButton.Click += new System.EventHandler(this.PingButton_Click);
                this.discoverObjectsButton.Click += new System.EventHandler(this.DiscoverObjectsButton_Click);
                this.readPropertyButton.Click += new System.EventHandler(this.ReadPropertyButton_Click);


                // Wire up event handlers to save history
                ipAddressComboBox.Leave += (s, args) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
                instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
                ipPortComboBox.Leave += (s, args) => SaveComboBoxEntry(ipPortComboBox, "ipPort");
                apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
                bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
                bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");
                interfaceComboBox.Leave += (s, args) => SaveComboBoxEntry(interfaceComboBox, "interface");
            }
            catch (Exception ex)
            {
                // This catch block will prevent the designer from crashing if an error occurs during load.
                Console.WriteLine("Error during BACnet_IP_Load, likely in designer: " + ex.Message);
            }
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
                if (_historyManager != null)
                {
                    _historyManager.AddEntry("interface", interfaceComboBox.Text);
                }


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
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.Invoke((MethodInvoker)delegate {
                Log($"I-Am received from {adr} (DeviceID: {deviceId})");
                string deviceDisplay = $"{deviceId} ({adr})";

                if (deviceTreeView != null && !deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
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

            if (deviceTreeView != null) deviceTreeView.Nodes.Clear();
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

                var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                var propertyId = BacnetPropertyIds.PROP_OBJECT_NAME;

                Log($"Reading {propertyId} from Device {deviceId}...");

                await Task.Run(() =>
                {
                    if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId, out IList<BacnetValue> values))
                    {
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
            if (outputTextBox != null)
            {
                outputTextBox.AppendText(DateTime.Now.ToLongTimeString() + ": " + message + Environment.NewLine);
                outputTextBox.ScrollToCaret();
            }
        }

        private void PopulateDefaultValues()
        {
            if (interfaceComboBox == null) return;

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
            if (instanceNumberComboBox == null) return;
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            if (pingButton != null) pingButton.Enabled = instanceExists;
            if (readPropertyButton != null) readPropertyButton.Enabled = instanceExists;
            if (writePropertyButton != null) writePropertyButton.Enabled = instanceExists;
            if (discoverObjectsButton != null) discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                uint deviceId = uint.Parse(e.Node.Name);
                if (instanceNumberComboBox != null) instanceNumberComboBox.Text = deviceId.ToString();
                if (e.Node.Tag is BacnetAddress bacnetAddress)
                {
                    if (ipAddressComboBox != null) ipAddressComboBox.Text = string.Join(".", bacnetAddress.adr.Take(4));
                }
                _lastPingedDeviceId = deviceId;
                UpdateAllStates(null, null);
                DiscoverObjectsButton_Click(null, null);
            }
        }

        private void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            if (objectTreeView == null) return;
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
            if (ipAddressComboBox != null && string.IsNullOrEmpty(ipAddressComboBox.Text)) ipAddressComboBox.Text = "192.168.1.200";
            if (instanceNumberComboBox != null && string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (ipPortComboBox != null && string.IsNullOrEmpty(ipPortComboBox.Text)) ipPortComboBox.Text = "47808";
            if (apduTimeoutComboBox != null && string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "5000";
            if (bbmdIpComboBox != null && string.IsNullOrEmpty(bbmdIpComboBox.Text)) bbmdIpComboBox.Text = "172.19.10.102";
            if (bbmdTtlComboBox != null && string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;

            if (interfaceComboBox != null && comboBox.Name != interfaceComboBox.Name)
            {
                comboBox.Items.Clear();
            }

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                if (interfaceComboBox != null && comboBox.Name == interfaceComboBox.Name)
                {
                    foreach (var item in historyList.Where(h => !comboBox.Items.Contains(h)))
                    {
                        comboBox.Items.Insert(0, item);
                    }
                }
                else
                {
                    comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                }

                comboBox.Text = historyList.First();
            }
            else if (interfaceComboBox != null && comboBox.Name == interfaceComboBox.Name && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (_historyManager != null && comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text;
            }
        }

        public void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();

            ipAddressComboBox?.Items.Clear();
            instanceNumberComboBox?.Items.Clear();
            ipPortComboBox?.Items.Clear();
            apduTimeoutComboBox?.Items.Clear();
            bbmdIpComboBox?.Items.Clear();
            bbmdTtlComboBox?.Items.Clear();
            interfaceComboBox?.Items.Clear();

            PopulateDefaultValues();
            if (interfaceComboBox != null && interfaceComboBox.Items.Count > 0)
            {
                interfaceComboBox.SelectedIndex = 0;
            }

            Log("BACnet/IP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}
