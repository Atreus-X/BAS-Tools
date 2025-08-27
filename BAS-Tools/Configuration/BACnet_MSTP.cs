using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.Configuration
{
    public partial class BACnet_MSTP : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private HistoryManager _historyManager;
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;
        private System.Windows.Forms.Timer _discoveryTimer;

        public BACnet_MSTP()
        {
            InitializeComponent();
            this.Load += BACnet_MSTP_Load;
        }

        private void BACnet_MSTP_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _historyManager = new HistoryManager("BACnet_MSTP_");

            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();

            // *** CHANGE IS HERE *** Initialize the timer
            _discoveryTimer = new System.Windows.Forms.Timer();
            _discoveryTimer.Interval = 10000; // 10 seconds
            _discoveryTimer.Tick += DiscoveryTimer_Tick;

            _remoteModeRadioButton.Checked = true;
            ModeRadioButton_CheckedChanged(null, null);
        }

        private void WireUpEventHandlers()
        {
            _localModeRadioButton.CheckedChanged += ModeRadioButton_CheckedChanged;
            _remoteModeRadioButton.CheckedChanged += ModeRadioButton_CheckedChanged;

            anyNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;
            localNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;
            listNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;

            serialPortComboBox.Leave += (s, args) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            baudRateComboBox.Leave += (s, args) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            maxMastersComboBox.Leave += (s, args) => SaveComboBoxEntry(maxMastersComboBox, "maxMasters");
            maxInfoFramesComboBox.Leave += (s, args) => SaveComboBoxEntry(maxInfoFramesComboBox, "maxInfoFrames");
            instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
            networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(networkNumberComboBox, "networkNumber");
            apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdPortComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdPortComboBox, "bbmdPort");
            bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");

            startDiscoveryButton.Click += StartDiscoveryButton_Click;
            cancelDiscoveryButton.Click += CancelDiscoveryButton_Click;
            pingButton.Click += PingButton_Click;
            discoverObjectsButton.Click += DiscoverObjectsButton_Click;
            readPropertyButton.Click += ReadPropertyButton_Click;
            clearLogButton.Click += ClearLogButton_Click;
            deviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            instanceNumberComboBox.TextChanged += UpdateAllStates;
        }

        private void NetworkFilter_CheckedChanged(object sender, EventArgs e)
        {
            networkNumberComboBox.Visible = listNetworkRadioButton.Checked;
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            Log("Log cleared.");
        }

        private void ModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            localModePanel.Visible = _localModeRadioButton.Checked;
            remoteModePanel.Visible = _remoteModeRadioButton.Checked;
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
                IBacnetTransport transport;
                int apduTimeout = 5000;
                if (_remoteModeRadioButton.Checked && int.TryParse(apduTimeoutComboBox.Text, out int parsedTimeout))
                {
                    apduTimeout = parsedTimeout;
                }

                if (_localModeRadioButton.Checked)
                {
                    Log("Initializing BACnet MS/TP client (Local)...");
                    var portName = serialPortComboBox.Text;
                    var baudRate = int.Parse(baudRateComboBox.Text);
                    byte nodeAddress = 0;
                    var maxMasters = byte.Parse(maxMastersComboBox.Text);
                    var maxInfoFrames = byte.Parse(maxInfoFramesComboBox.Text);
                    transport = new BacnetMstpProtocolTransport(portName, baudRate, nodeAddress, maxMasters, maxInfoFrames);
                }
                else // Remote Mode
                {
                    Log("Initializing BACnet/IP client for remote MS/TP (BBMD)...");
                    string localIp = "0.0.0.0";
                    if (localInterfaceComboBox.SelectedIndex > 0 && localInterfaceComboBox.SelectedItem != null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(localInterfaceComboBox.SelectedItem.ToString(), @"\((.*?)\)");
                        if (match.Success) localIp = match.Groups[1].Value;
                    }

                    int bbmdPort = 47808;
                    if (int.TryParse(bbmdPortComboBox.Text, out int parsedPort))
                    {
                        bbmdPort = parsedPort;
                    }

                    Log("Transport 'donotfragment' flag set to FALSE.");
                    transport = new BacnetIpUdpProtocolTransport(bbmdPort, false, false, 1472, localIp);
                }

                _bacnetClient = new BacnetClient(transport) { Timeout = apduTimeout };
                _bacnetClient.OnIam += OnIamHandler;

                _bacnetClient.Start();
                Log("BACnet client transport started.");

                if (_remoteModeRadioButton.Checked && !string.IsNullOrWhiteSpace(bbmdIpComboBox.Text))
                {
                    int bbmdPort = 47808;
                    if (int.TryParse(bbmdPortComboBox.Text, out int parsedPort))
                    {
                        bbmdPort = parsedPort;
                    }

                    if (!short.TryParse(bbmdTtlComboBox.Text, out short ttl))
                    {
                        Log("--- ERROR: Invalid BBMD TTL value. ---");
                        return;
                    }
                    Log($"Attempting to register as Foreign Device with BBMD at {bbmdIpComboBox.Text}:{bbmdPort} with TTL {ttl}...");
                    _bacnetClient.RegisterAsForeignDevice(bbmdIpComboBox.Text, ttl, bbmdPort);
                    Log("Foreign Device Registration message sent.");

                    Log("Waiting 2 seconds for BBMD to process registration...");
                    Thread.Sleep(2000);
                }

                _isClientStarted = true;
                Log("BACnet client initialization complete.");
            }
            catch (Exception ex)
            {
                _isClientStarted = false;
                Log($"--- ERROR initializing BACnet client: {ex.Message} ---");
                MessageBox.Show($"Error during BACnet initialization: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ushort> ParseNetworkNumbers(string text)
        {
            var networks = new List<ushort>();
            if (string.IsNullOrWhiteSpace(text)) return networks;

            foreach (var part in text.Split(','))
            {
                try
                {
                    var trimmedPart = part.Trim();
                    if (trimmedPart.Contains('-'))
                    {
                        var range = trimmedPart.Split('-');
                        if (range.Length == 2 && ushort.TryParse(range[0], out ushort start) && ushort.TryParse(range[1], out ushort end) && start <= end)
                        {
                            for (ushort i = start; i <= end; i++)
                            {
                                networks.Add(i);
                            }
                        }
                    }
                    else if (ushort.TryParse(trimmedPart, out ushort netNum))
                    {
                        networks.Add(netNum);
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error parsing network number '{part}': {ex.Message}");
                }
            }
            return networks.Distinct().ToList();
        }

        private void OnIamHandler(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
            Log($"--- I-AM HANDLER FIRED --- from {adr}, Device ID: {deviceId}, Vendor: {vendorId}");

            this.Invoke((MethodInvoker)delegate
            {
                bool shouldAddDevice = false;

                if (anyNetworkRadioButton.Checked)
                {
                    shouldAddDevice = true;
                }
                else if (localNetworkRadioButton.Checked)
                {
                    if (_localModeRadioButton.Checked || adr.net == 0)
                    {
                        shouldAddDevice = true;
                    }
                }
                else if (listNetworkRadioButton.Checked)
                {
                    var targetNetworks = ParseNetworkNumbers(networkNumberComboBox.Text);
                    if (targetNetworks.Contains(adr.net))
                    {
                        shouldAddDevice = true;
                    }
                }

                if (shouldAddDevice)
                {
                    string deviceDisplay = $"{deviceId} ({adr})";
                    if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                    {
                        Log($"Adding new device to tree: {deviceDisplay}");
                        var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = adr };
                        deviceTreeView.Nodes.Add(node);
                        discoveryStatusLabel.Text = $"Found: {deviceTreeView.Nodes.Count}";
                    }
                    else
                    {
                        Log($"Device already in tree: {deviceDisplay}");
                    }
                }
                else
                {
                    Log($"Skipping device {deviceId} on network {adr.net} (Filter does not match).");
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

        private void StartDiscoveryButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;

            deviceTreeView.Nodes.Clear();
            Log("Discovering devices...");

            startDiscoveryButton.Enabled = false;
            cancelDiscoveryButton.Visible = true;
            discoveryStatusLabel.Text = "Found: 0";
            discoveryStatusLabel.Visible = true;

            _discoveryTimer.Start();
            Log("Sending Who-Is broadcast for discovery.");
            _bacnetClient.WhoIs();
        }


        private void CancelDiscoveryButton_Click(object sender, EventArgs e)
        {
            DiscoveryTimer_Tick(sender, e); // Stop the timer and reset the UI
        }

        private void DiscoveryTimer_Tick(object sender, EventArgs e)
        {
            _discoveryTimer.Stop();
            Log("Discovery finished.");
            startDiscoveryButton.Enabled = true;
            cancelDiscoveryButton.Visible = false;
            discoveryStatusLabel.Visible = false;
        }

        private async void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (_lastPingedDeviceId == null)
            {
                MessageBox.Show("Please successfully Ping a device first to select it for object discovery.", "Device Not Selected");
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
            outputTextBox.AppendText(DateTime.Now.ToLongTimeString() + ": " + message + Environment.NewLine);
            outputTextBox.ScrollToCaret();
        }

        private void PopulateDefaultValues()
        {
            serialPortComboBox.Items.Clear();
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            if (serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;

            baudRateComboBox.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            maxMastersComboBox.Items.AddRange(Enumerable.Range(1, 127).Select(i => i.ToString()).ToArray());
            maxInfoFramesComboBox.Items.AddRange(Enumerable.Range(1, 10).Select(i => i.ToString()).ToArray());

            localInterfaceComboBox.Items.Clear();
            localInterfaceComboBox.Items.Add("0.0.0.0 (Any)");
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localInterfaceComboBox.Items.Add($"{ni.Name} ({ip.Address})");
                        }
                    }
                }
            }
            if (localInterfaceComboBox.Items.Count > 0)
            {
                localInterfaceComboBox.SelectedIndex = 0;
            }

            apduTimeoutComboBox.Items.AddRange(new object[] { "3000", "5000", "10000" });
            bbmdPortComboBox.Items.AddRange(new object[] { "47808" });
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
            if (e.Node != null && uint.TryParse(e.Node.Name, out uint deviceId))
            {
                instanceNumberComboBox.Text = deviceId.ToString();
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
                return adr;
            }

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            var tcs = new TaskCompletionSource<BacnetAddress>();
            void handler(BacnetClient s, BacnetAddress a, uint d, uint m, BacnetSegmentations seg, ushort v)
            {
                if (d == deviceId) tcs.TrySetResult(a);
            }
            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);
            var timeoutTask = Task.Delay(2000);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);
            _bacnetClient.OnIam -= handler;
            if (completedTask == tcs.Task) return await tcs.Task;

            Log($"--- Timeout: No I-Am response from Device {deviceId}. ---");
            return null;
        }

        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(serialPortComboBox, "serialPort");
            PopulateComboBoxWithHistory(baudRateComboBox, "baudRate");
            PopulateComboBoxWithHistory(maxMastersComboBox, "maxMasters");
            PopulateComboBoxWithHistory(maxInfoFramesComboBox, "maxInfoFrames");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");
            if (bbmdIpComboBox != null) PopulateComboBoxWithHistory(bbmdIpComboBox, "bbmdIp");
            if (networkNumberComboBox != null) PopulateComboBoxWithHistory(networkNumberComboBox, "networkNumber");
            if (apduTimeoutComboBox != null) PopulateComboBoxWithHistory(apduTimeoutComboBox, "apduTimeout");
            if (bbmdPortComboBox != null) PopulateComboBoxWithHistory(bbmdPortComboBox, "bbmdPort");
            if (bbmdTtlComboBox != null) PopulateComboBoxWithHistory(bbmdTtlComboBox, "bbmdTtl");


            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (networkNumberComboBox != null && string.IsNullOrEmpty(networkNumberComboBox.Text)) networkNumberComboBox.Text = "1";
            if (apduTimeoutComboBox != null && string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "5000";
            if (bbmdPortComboBox != null && string.IsNullOrEmpty(bbmdPortComboBox.Text)) bbmdPortComboBox.Text = "47808";
            if (bbmdTtlComboBox != null && string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;

            if (comboBox.Name == "localInterfaceComboBox") return;

            bool isSpecialComboBox = comboBox == serialPortComboBox;
            if (!isSpecialComboBox) comboBox.Items.Clear();

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                if (isSpecialComboBox)
                {
                    foreach (var item in historyList.Where(h => !comboBox.Items.Contains(h)))
                        comboBox.Items.Insert(0, item);
                }
                else
                {
                    comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                }
                comboBox.Text = historyList.First();
            }
            else if (isSpecialComboBox && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text) && comboBox.Name != "localInterfaceComboBox")
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
            serialPortComboBox.Items.Clear();
            baudRateComboBox.Items.Clear();
            maxMastersComboBox.Items.Clear();
            maxInfoFramesComboBox.Items.Clear();
            instanceNumberComboBox.Items.Clear();
            bbmdIpComboBox.Items.Clear();
            networkNumberComboBox.Items.Clear();
            apduTimeoutComboBox.Items.Clear();
            bbmdPortComboBox.Items.Clear();
            bbmdTtlComboBox.Items.Clear();


            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}