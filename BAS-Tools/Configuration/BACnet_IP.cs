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

namespace MainApp.Configuration
{
    public partial class BACnet_IP : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private HistoryManager _historyManager;
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;

        public BACnet_IP()
        {
            InitializeComponent();
            this.Load += BACnet_IP_Load;
        }

        private void BACnet_IP_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _historyManager = new HistoryManager("BACnet_IP_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();
        }

        private void WireUpEventHandlers()
        {
            this.deviceTreeView.AfterSelect += this.DeviceTreeView_AfterSelect;
            this.instanceNumberComboBox.TextChanged += this.UpdateAllStates;
            this.discoverButton.Click += this.DiscoverButton_Click;
            this.pingButton.Click += this.PingButton_Click;
            this.discoverObjectsButton.Click += this.DiscoverObjectsButton_Click;
            this.readPropertyButton.Click += this.ReadPropertyButton_Click;
            this.clearLogButton.Click += this.ClearLogButton_Click;

            ipAddressComboBox.Leave += (s, args) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            ipPortComboBox.Leave += (s, args) => SaveComboBoxEntry(ipPortComboBox, "ipPort");
            apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
            bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            Log("Log cleared.");
        }

        private void EnsureBacnetClientStarted()
        {
            if (_isClientStarted && _bacnetClient != null) return;
            if (_bacnetClient != null)
            {
                _bacnetClient.Dispose();
                _bacnetClient = null;
                Thread.Sleep(200);
            }

            try
            {
                Log("Initializing BACnet client...");
                string localIp = "0.0.0.0";
                if (interfaceComboBox.SelectedIndex > 0 && interfaceComboBox.SelectedItem != null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(interfaceComboBox.SelectedItem.ToString(), @"\((.*?)\)");
                    if (match.Success) localIp = match.Groups[1].Value;
                }

                var transport = new BacnetIpUdpProtocolTransport(int.Parse(ipPortComboBox.Text), true, false, 1472, localIp);
                _bacnetClient = new BacnetClient(transport) { Timeout = int.Parse(apduTimeoutComboBox.Text) };
                _bacnetClient.OnIam += OnIamHandler;

                _bacnetClient.Start();

                if (!string.IsNullOrWhiteSpace(bbmdIpComboBox.Text))
                {
                    int bbmdPort = int.Parse(ipPortComboBox.Text);
                    short ttl;
                    if (!short.TryParse(bbmdTtlComboBox.Text, out ttl))
                    {
                        Log("--- ERROR: Invalid BBMD TTL value. ---");
                        return;
                    }
                    _bacnetClient.RegisterAsForeignDevice(bbmdIpComboBox.Text, ttl, bbmdPort);
                    Log($"Registered as Foreign Device with BBMD at {bbmdIpComboBox.Text}:{bbmdPort} with TTL {ttl}");
                }

                _isClientStarted = true;
                Log("BACnet client started.");
            }
            catch (Exception ex)
            {
                _isClientStarted = false;
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
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;
            uint deviceId = uint.Parse(instanceNumberComboBox.Text);
            Log($"Pinging Device ID: {deviceId}...");
            _bacnetClient.WhoIs(lowLimit: (int)deviceId, highLimit: (int)deviceId);
            _lastPingedDeviceId = deviceId;
            UpdateAllStates(null, null);
        }

        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;
            deviceTreeView.Nodes.Clear();
            Log("Discovering devices with global Who-Is broadcast...");
            _bacnetClient.WhoIs();
        }

        private async void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (_lastPingedDeviceId == null)
            {
                MessageBox.Show("Please successfully Ping a device first.", "Device Not Selected");
                return;
            }
            EnsureBacnetClientStarted();
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
                if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objectList))
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
                await Task.Run(() => {
                    if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId, out IList<BacnetValue> values))
                    {
                        Log($"--- SUCCESS: Read ACK for {propertyId} ---");
                        foreach (var val in values) Log($"  Value: {val.Value}");
                    }
                    else
                    {
                        Log($"--- ERROR: Failed to read property {propertyId}. ---");
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
            interfaceComboBox.Items.Clear();
            interfaceComboBox.Items.Add("0.0.0.0 (Any)");
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
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
            if (interfaceComboBox.Items.Count > 0)
            {
                interfaceComboBox.SelectedIndex = 0;
            }
            bbmdTtlComboBox.Items.AddRange(new object[] { "60", "3600" });
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
            if (nodes.Length > 0 && nodes[0].Tag is BacnetAddress adr) return adr;

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            var tcs = new TaskCompletionSource<BacnetAddress>();
            void handler(BacnetClient s, BacnetAddress a, uint d, uint m, BacnetSegmentations seg, ushort v)
            {
                if (d == deviceId) tcs.TrySetResult(a);
            }
            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);
            var timeoutTask = Task.Delay(2000);
            if (await Task.WhenAny(tcs.Task, timeoutTask) == tcs.Task)
            {
                _bacnetClient.OnIam -= handler;
                return await tcs.Task;
            }
            _bacnetClient.OnIam -= handler;
            Log($"--- Timeout: No I-Am response from Device {deviceId}. ---");
            return null;
        }

        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(ipAddressComboBox, "ipAddress");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");
            PopulateComboBoxWithHistory(ipPortComboBox, "ipPort");
            PopulateComboBoxWithHistory(apduTimeoutComboBox, "apduTimeout");
            PopulateComboBoxWithHistory(bbmdIpComboBox, "bbmdIp");
            PopulateComboBoxWithHistory(bbmdTtlComboBox, "bbmdTtl");

            if (string.IsNullOrEmpty(ipAddressComboBox.Text)) ipAddressComboBox.Text = "192.168.1.200";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (string.IsNullOrEmpty(ipPortComboBox.Text)) ipPortComboBox.Text = "47808";
            if (string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "5000";
            if (string.IsNullOrEmpty(bbmdIpComboBox.Text)) bbmdIpComboBox.Text = "";
            if (string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;

            if (comboBox.Name == "interfaceComboBox") return;

            comboBox.Items.Clear();

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                comboBox.Text = historyList.First();
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (_historyManager != null && comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text) && comboBox.Name != "interfaceComboBox")
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
            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet/IP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}