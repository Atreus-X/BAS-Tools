using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.BACnet.Serialize;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainApp.BACnet;
using static System.IO.BACnet.Serialize.ASN1;
using static System.IO.BACnet.Serialize.Services;

namespace MainApp.Configuration
{
    public partial class BACnet_MSTP_Remote : BACnetControlBase
    {
        private readonly System.Windows.Forms.Timer _discoveryTimer;
        private string _lastBBMD_IP = "";
        private List<ushort> _networksToScan = new List<ushort>();

        // Implement abstract properties from the base class
        protected override TreeView DeviceTreeView => deviceTreeView;
        protected override TreeView ObjectTreeView => objectTreeView;
        protected override DataGridView PropertiesDataGridView => propertiesDataGridView;
        protected override RichTextBox OutputTextBox => outputTextBox;
        protected override Button TogglePollingButton => togglePollingButton;
        protected override NumericUpDown ReadIntervalNumericUpDown => readIntervalNumericUpDown;
        protected override ComboBox WritePriorityComboBox => writePriorityComboBox;

        public BACnet_MSTP_Remote()
        {
            InitializeComponent();
            this.Load += BACnet_MSTP_Remote_Load;
            _discoveryTimer = new System.Windows.Forms.Timer { Interval = 10000 };
        }

        private void BACnet_MSTP_Remote_Load(object sender, EventArgs e)
        {
            BaseLoad("BACnet_MSTP_Remote_");
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
            anyNetworkRadioButton.Checked = true;
            NetworkFilter_CheckedChanged(null, null);
        }

        protected override void WireUpProtocolSpecificEventHandlers()
        {
            anyNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;
            localNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;
            listNetworkRadioButton.CheckedChanged += NetworkFilter_CheckedChanged;
            bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
            networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(networkNumberComboBox, "networkNumber");
            apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdPortComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdPortComboBox, "bbmdPort");
            bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");
            startDiscoveryButton.Click += StartDiscoveryButton_Click;
            cancelDiscoveryButton.Click += CancelDiscoveryButton_Click;
            discoverObjectsButton.Click += DiscoverObjectsButton_Click;
            manualReadWriteButton.Click += base.ManualReadWriteButton_Click;
            clearLogButton.Click += base.ClearLogButton_Click;
            cancelActionButton.Click += CancelActionButton_Click;
            expandAllButton.Click += (s, args) => deviceTreeView.ExpandAll();
            collapseAllButton.Click += (s, args) => deviceTreeView.CollapseAll();
            clearBrowserButton.Click += (s, e) => {
                deviceTreeView.Nodes.Clear();
                objectTreeView.Nodes.Clear();
            };
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                Log("Cancelling current operation...");
                _cancellationTokenSource.Cancel();
            }
        }

        private void OnIamHandler(BacnetClient _sender, BacnetAddress adr, uint deviceId, uint _maxApdu, BacnetSegmentations _segmentation, ushort vendorId)
        {
            ushort deviceNetwork = (adr.RoutedSource != null) ? adr.RoutedSource.net : adr.net;

            if (listNetworkRadioButton.Checked && _networksToScan.Any() && !_networksToScan.Contains(deviceNetwork))
            {
                return;
            }

            Log($"OnIam from {adr} for device {deviceId}. Segmentation support: {_segmentation}");
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    string macAddress = (adr.RoutedSource != null && adr.RoutedSource.adr != null) ? string.Join(":", adr.RoutedSource.adr.Select(b => b.ToString("X2"))) : "N/A";
                    string vendorName = BacnetVendorInfo.GetVendorName(vendorId);
                    string networkNodeKey = $"NET-{deviceNetwork}";
                    TreeNode networkNode = deviceTreeView.Nodes.Cast<TreeNode>().FirstOrDefault(n => n.Name == networkNodeKey);
                    if (networkNode == null)
                    {
                        networkNode = new TreeNode($"Network {deviceNetwork}") { Name = networkNodeKey, Tag = "NETWORK_NODE" };
                        deviceTreeView.Nodes.Add(networkNode);
                    }
                    if (!networkNode.Nodes.ContainsKey(deviceId.ToString()))
                    {
                        var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "VendorName", vendorName }, { "MAC", macAddress }, { "Segmentation", _segmentation }, { "VendorId", vendorId } };
                        string deviceText = $"(Name not read) ({macAddress}) ({deviceId}) ({vendorName})";
                        TreeNode deviceNode = new TreeNode(deviceText) { Name = deviceId.ToString(), Tag = deviceInfo };
                        networkNode.Nodes.Add(deviceNode);
                        networkNode.Expand();
                        ReadDeviceName(deviceNode, deviceId, adr);
                    }
                }
                catch (Exception ex) { Log($"Error in OnIamHandler: {ex.Message}"); }
            });
        }

        protected override void DeviceTreeView_AfterSelect(object _sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || e.Node.Tag.ToString() == "NETWORK_NODE")
            {
                _lastPingedDeviceId = null;
                UpdateAllStates(null, null);
                objectTreeView.Nodes.Clear();
                return;
            }
            if (rediscoverDeviceCheckBox.Checked)
            {
                uint deviceIdToRediscover = uint.Parse(e.Node.Name);
                Log($"Rediscovering device {deviceIdToRediscover}...");
                e.Node.Remove();
                _bacnetClient.WhoIs((int)deviceIdToRediscover, (int)deviceIdToRediscover);
                return;
            }
            _lastPingedDeviceId = uint.Parse(e.Node.Name);
            UpdateAllStates(null, null);
        }

        private new void DiscoverObjectsButton_Click(object _sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode != null && deviceTreeView.SelectedNode.Tag != null && deviceTreeView.SelectedNode.Tag.ToString() != "NETWORK_NODE")
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var progress = new Progress<int>(value =>
                {
                    objectDiscoveryProgressBar.Value = value;
                    objectCountLabel.Text = $"Found {value}%";
                });
                LoadDeviceDetails(deviceTreeView.SelectedNode, _cancellationTokenSource.Token, progress);
            }
            else
            {
                MessageBox.Show("Please select a device from the list first.", "Device Not Selected");
            }
        }

        private void NetworkFilter_CheckedChanged(object _sender, EventArgs _e)
        {
            networkNumberComboBox.Visible = listNetworkRadioButton.Checked;
        }

        private void EnsureBacnetClientStarted()
        {
            string currentBBMD_IP = bbmdIpComboBox.Text.Trim();
            if (_bacnetClient != null && currentBBMD_IP == _lastBBMD_IP) return;

            if (_bacnetClient != null)
            {
                _bacnetClient.Dispose();
                _bacnetClient = null;
                Thread.Sleep(200);
            }

            try
            {
                int apduTimeout = 25000;
                if (int.TryParse(apduTimeoutComboBox.Text, out int parsedTimeout)) apduTimeout = parsedTimeout;

                Log("Initializing BACnet/IP client...");
                string localIp = "0.0.0.0";
                if (localInterfaceComboBox.SelectedIndex > 0 && localInterfaceComboBox.SelectedItem != null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(localInterfaceComboBox.SelectedItem.ToString(), @"\((.*?)\)");
                    if (match.Success) localIp = match.Groups[1].Value;
                }

                int localPort = 47808;
                if (int.TryParse(bbmdPortComboBox.Text, out int parsedBBMDPort)) localPort = parsedBBMDPort;

                var transport = new BacnetIpUdpProtocolTransport(localPort, false, true, 1472, localIp);
                Log($"Transport created on local port {localPort}.");
                _bacnetClient = new BacnetClient(transport) { Timeout = apduTimeout };
                _bacnetClient.OnIam += OnIamHandler;
                _bacnetClient.Start();
                Log("BACnet client transport started.");

                _lastBBMD_IP = currentBBMD_IP;

                if (!string.IsNullOrWhiteSpace(currentBBMD_IP))
                {
                    if (!short.TryParse(bbmdTtlComboBox.Text, out short ttl))
                    {
                        Log("--- ERROR: Invalid BBMD TTL value. ---");
                        return;
                    }
                    Log($"Registering as Foreign Device with BBMD at {currentBBMD_IP}:{localPort} with TTL {ttl}s...");
                    _bacnetClient.RegisterAsForeignDevice(currentBBMD_IP, ttl, localPort);
                    Log("Foreign Device Registration message sent.");
                    Thread.Sleep(2000);
                }
                Log("BACnet client initialization complete.");
            }
            catch (Exception ex) { Log($"--- ERROR initializing BACnet client: {ex.Message} ---"); }
        }

        private void StartDiscoveryButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (_bacnetClient == null) return;

            deviceTreeView.Nodes.Clear();
            objectTreeView.Nodes.Clear();
            Log("Discovering devices...");

            startDiscoveryButton.Enabled = false;
            cancelDiscoveryButton.Visible = true;
            discoveryStatusLabel.Visible = true;
            _discoveryTimer.Start();

            string bbmdIp = bbmdIpComboBox.Text.Trim();
            int.TryParse(bbmdPortComboBox.Text, out int bbmdPort);

            if (listNetworkRadioButton.Checked && !string.IsNullOrWhiteSpace(bbmdIp))
            {
                _networksToScan = ParseNetworkNumbers(networkNumberComboBox.Text);
                if (_networksToScan.Any())
                {
                    foreach (var netNum in _networksToScan)
                    {
                        Log($"Sending Who-Is for remote network {netNum} via BBMD {bbmdIp}:{bbmdPort}.");
                        _bacnetClient.RemoteWhoIs(bbmdIp, bbmdPort, -1, -1, netNum);
                    }
                }
                else
                {
                    Log($"Sending global Who-Is via BBMD {bbmdIp}:{bbmdPort}.");
                    _bacnetClient.RemoteWhoIs(bbmdIp, bbmdPort, -1, -1, 0xFFFF);
                }
            }
            else
            {
                _networksToScan.Clear();
                Log("Sending local Who-Is broadcast.");
                _bacnetClient.WhoIs(-1, -1, _bacnetClient.Transport.GetBroadcastAddress());
            }
        }
        private List<ushort> ParseNetworkNumbers(string text)
        {
            var networks = new List<ushort>();
            if (string.IsNullOrWhiteSpace(text)) return networks;
            foreach (var part in text.Split(','))
            {
                var trimmedPart = part.Trim();
                if (trimmedPart.Contains('-'))
                {
                    var range = trimmedPart.Split('-');
                    if (range.Length == 2 && ushort.TryParse(range[0], out ushort start) && ushort.TryParse(range[1], out ushort end) && start <= end)
                    {
                        for (ushort i = start; i <= end; i++) networks.Add(i);
                    }
                }
                else if (ushort.TryParse(trimmedPart, out ushort netNum)) networks.Add(netNum);
            }
            return networks;
        }

        private void CancelDiscoveryButton_Click(object _sender, EventArgs _e)
        {
            DiscoveryTimer_Tick(_sender, _e);
        }

        private void DiscoveryTimer_Tick(object _sender, EventArgs _e)
        {
            _discoveryTimer.Stop();
            Log("Discovery finished.");
            startDiscoveryButton.Enabled = true;
            cancelDiscoveryButton.Visible = false;
            discoveryStatusLabel.Visible = false;
            _networksToScan.Clear();
        }

        protected override void PopulateDefaultValues()
        {
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
            if (localInterfaceComboBox.Items.Count > 0) localInterfaceComboBox.SelectedIndex = 0;
            apduTimeoutComboBox.Items.AddRange(new object[] { "3000", "5000", "10000", "25000" });
            bbmdPortComboBox.Items.AddRange(new object[] { "47808" });
            bbmdTtlComboBox.Items.AddRange(new object[] { "60", "3600" });
        }

        protected override void UpdateAllStates(object sender, EventArgs e)
        {
            bool deviceSelected = _lastPingedDeviceId.HasValue;
            discoverObjectsButton.Enabled = deviceSelected;
        }

        protected override void LoadHistory()
        {
            PopulateComboBoxWithHistory(bbmdIpComboBox, "bbmdIp");
            PopulateComboBoxWithHistory(networkNumberComboBox, "networkNumber");
            PopulateComboBoxWithHistory(apduTimeoutComboBox, "apduTimeout");
            PopulateComboBoxWithHistory(bbmdPortComboBox, "bbmdPort");
            PopulateComboBoxWithHistory(bbmdTtlComboBox, "bbmdTtl");

            if (string.IsNullOrEmpty(networkNumberComboBox.Text)) networkNumberComboBox.Text = "1";
            if (string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "25000";
            if (string.IsNullOrEmpty(bbmdPortComboBox.Text)) bbmdPortComboBox.Text = "47808";
            if (string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
        }

        public override void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();
            bbmdIpComboBox.Items.Clear();
            networkNumberComboBox.Items.Clear();
            apduTimeoutComboBox.Items.Clear();
            bbmdPortComboBox.Items.Clear();
            bbmdTtlComboBox.Items.Clear();
            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP Remote history cleared.");
        }
    }
}

