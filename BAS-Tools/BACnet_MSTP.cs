using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class BACnet_MSTP : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private Thread _bacnetThread;
        private HistoryManager _historyManager;
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;
        private System.Windows.Forms.Timer _discoveryTimer;

        private Panel _localPanel;
        private Panel _remotePanel;
        private ComboBox _bbmdIpComboBox;
        private ComboBox _networkNumberComboBox;
        private ComboBox _localInterfaceComboBox;

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
            SetupDynamicUI();
            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();

            _discoveryTimer = new System.Windows.Forms.Timer();
            _discoveryTimer.Interval = 10000; // 10 seconds
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
        }

        private void SetupDynamicUI()
        {
            if (settingsPanel == null) return;

            _localPanel = new Panel { Dock = DockStyle.Fill };
            _remotePanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            _localPanel.Controls.Add(this.mstpLayout);

            _localInterfaceComboBox = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            _bbmdIpComboBox = new ComboBox { Dock = DockStyle.Fill };
            _networkNumberComboBox = new ComboBox { Dock = DockStyle.Fill };

            var remoteLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 2 };
            remoteLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            remoteLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            remoteLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            remoteLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            remoteLayout.Controls.Add(new Label { Text = "Local Interface:", Anchor = AnchorStyles.Left, AutoSize = true }, 0, 0);
            remoteLayout.SetColumnSpan(_localInterfaceComboBox, 3);
            remoteLayout.Controls.Add(_localInterfaceComboBox, 1, 0);
            remoteLayout.Controls.Add(new Label { Text = "BBMD IP:", Anchor = AnchorStyles.Left, AutoSize = true }, 0, 1);
            remoteLayout.Controls.Add(_bbmdIpComboBox, 1, 1);
            remoteLayout.Controls.Add(new Label { Text = "Network #:", Anchor = AnchorStyles.Left, AutoSize = true }, 2, 1);
            remoteLayout.Controls.Add(_networkNumberComboBox, 3, 1);
            _remotePanel.Controls.Add(remoteLayout);

            settingsPanel.Controls.Add(_localPanel);
            settingsPanel.Controls.Add(_remotePanel);
        }

        private void WireUpEventHandlers()
        {
            if (_localModeRadioButton != null) _localModeRadioButton.CheckedChanged += ModeRadioButton_CheckedChanged;

            serialPortComboBox.Leave += (s, args) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            baudRateComboBox.Leave += (s, args) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            maxMastersComboBox.Leave += (s, args) => SaveComboBoxEntry(maxMastersComboBox, "maxMasters");
            maxInfoFramesComboBox.Leave += (s, args) => SaveComboBoxEntry(maxInfoFramesComboBox, "maxInfoFrames");
            instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            if (_bbmdIpComboBox != null) _bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(_bbmdIpComboBox, "bbmdIp");
            if (_networkNumberComboBox != null) _networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(_networkNumberComboBox, "networkNumber");
            if (_localInterfaceComboBox != null) _localInterfaceComboBox.Leave += (s, args) => SaveComboBoxEntry(_localInterfaceComboBox, "localInterface");

            startDiscoveryButton.Click += StartDiscoveryButton_Click;
            cancelDiscoveryButton.Click += CancelDiscoveryButton_Click;
            pingButton.Click += PingButton_Click;
            discoverObjectsButton.Click += DiscoverObjectsButton_Click;
            readPropertyButton.Click += ReadPropertyButton_Click;
            deviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            instanceNumberComboBox.TextChanged += UpdateAllStates;
        }

        private void ModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _localPanel.Visible = _localModeRadioButton.Checked;
            _remotePanel.Visible = !_localModeRadioButton.Checked;
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
                    if (_localInterfaceComboBox.SelectedIndex > 0 && _localInterfaceComboBox.SelectedItem != null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(_localInterfaceComboBox.SelectedItem.ToString(), @"\((.*?)\)");
                        if (match.Success) localIp = match.Groups[1].Value;
                    }
                    transport = new BacnetIpUdpProtocolTransport(0, false, false, 1472, localIp);
                }

                _bacnetClient = new BacnetClient(transport);
                _bacnetClient.OnIam += OnIamHandler;

                _bacnetThread = new Thread(() => {
                    try
                    {
                        _bacnetClient.Start();
                    }
                    catch (Exception ex)
                    {
                        Log($"--- BACnet Thread Error: {ex.Message} ---");
                    }
                })
                { IsBackground = true };
                _bacnetThread.Start();
                Thread.Sleep(500);

                if (!_localModeRadioButton.Checked && !string.IsNullOrWhiteSpace(_bbmdIpComboBox.Text))
                {
                    _bacnetClient.RegisterAsForeignDevice(_bbmdIpComboBox.Text, 60);
                    Log($"Registered as Foreign Device with BBMD at {_bbmdIpComboBox.Text}");
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
            this.Invoke((MethodInvoker)delegate
            {
                Log($"I-Am received from {adr} (DeviceID: {deviceId})");
                string deviceDisplay = $"{deviceId} ({adr})";

                if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                {
                    var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = adr };
                    deviceTreeView.Nodes.Add(node);
                    discoveryStatusLabel.Text = $"Found: {deviceTreeView.Nodes.Count}";
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

            _localInterfaceComboBox.Items.Add("0.0.0.0 (Any)");
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            _localInterfaceComboBox.Items.Add($"{ni.Name} ({ip.Address})");
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
            if (_bbmdIpComboBox != null) PopulateComboBoxWithHistory(_bbmdIpComboBox, "bbmdIp");
            if (_networkNumberComboBox != null) PopulateComboBoxWithHistory(_networkNumberComboBox, "networkNumber");
            if (_localInterfaceComboBox != null) PopulateComboBoxWithHistory(_localInterfaceComboBox, "localInterface");

            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (_networkNumberComboBox != null && string.IsNullOrEmpty(_networkNumberComboBox.Text)) _networkNumberComboBox.Text = "1";
            if (_localInterfaceComboBox != null && string.IsNullOrEmpty(_localInterfaceComboBox.Text) && _localInterfaceComboBox.Items.Count > 0)
                _localInterfaceComboBox.SelectedIndex = 0;
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;
            bool isSpecialComboBox = comboBox == serialPortComboBox || comboBox == _localInterfaceComboBox;
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
            if (comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text;
            }
        }

        public void ClearHistory()
        {
            _historyManager.ClearHistory();
            serialPortComboBox.Items.Clear();
            baudRateComboBox.Items.Clear();
            maxMastersComboBox.Items.Clear();
            maxInfoFramesComboBox.Items.Clear();
            instanceNumberComboBox.Items.Clear();
            _bbmdIpComboBox.Items.Clear();
            _networkNumberComboBox.Items.Clear();
            _localInterfaceComboBox.Items.Clear();

            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}