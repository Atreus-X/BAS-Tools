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
    public partial class BACnet_IP : BACnetControlBase
    {
        private string _lastBBMDIp = "";
        private System.Windows.Forms.Timer _discoveryTimer;

        protected override TreeView DeviceTreeView => deviceTreeView;
        protected override TreeView ObjectTreeView => objectTreeView;
        protected override DataGridView PropertiesDataGridView => propertiesDataGridView;
        protected override RichTextBox OutputTextBox => outputTextBox;
        protected override Button TogglePollingButton => togglePollingButton;
        protected override NumericUpDown ReadIntervalNumericUpDown => readIntervalNumericUpDown;
        protected override ComboBox WritePriorityComboBox => writePriorityComboBox;

        public BACnet_IP()
        {
            InitializeComponent();
            this.Load += BACnet_IP_Load;
        }

        private void BACnet_IP_Load(object sender, EventArgs e)
        {
            BaseLoad("BACnet_IP_");
            _discoveryTimer = new System.Windows.Forms.Timer { Interval = 10000 };
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
        }

        private void DiscoveryTimer_Tick(object sender, EventArgs e)
        {
            _discoveryTimer.Stop();
            Log("Discovery finished.");
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    discoverButton.Enabled = true;
                    DeviceTreeView.Enabled = true;
                    objectDiscoveryProgressBar.Visible = false;
                    objectDiscoveryProgressBar.Style = ProgressBarStyle.Blocks;
                    discoveryStatusLabel.Visible = false;
                });
            }
        }

        protected override void UpdateAllStates(object sender, EventArgs e)
        {
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            pingButton.Enabled = instanceExists;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        protected override void WireUpProtocolSpecificEventHandlers()
        {
            this.instanceNumberComboBox.TextChanged += this.UpdateAllStates;
            this.discoverButton.Click += this.DiscoverButton_Click;
            this.pingButton.Click += this.PingButton_Click;
            this.discoverObjectsButton.Click += this.DiscoverObjectsButton_Click;
            this.manualReadWriteButton.Click += base.ManualReadWriteButton_Click;
            this.clearLogButton.Click += base.ClearLogButton_Click;
            this.cancelActionButton.Click += this.CancelActionButton_Click;
            this.cancelDiscoveryButton.Click += this.CancelDiscoveryButton_Click;

            networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(networkNumberComboBox, "networkNumber");
            ipAddressComboBox.Leave += (s, args) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            ipPortComboBox.Leave += (s, args) => SaveComboBoxEntry(ipPortComboBox, "ipPort");
            apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdIpComboBox.Leave += (s, args) => {
                SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
                EnsureBacnetClientStarted();
            };
            bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");

            expandAllButton.Click += (s, e) => deviceTreeView.ExpandAll();
            collapseAllButton.Click += (s, e) => deviceTreeView.CollapseAll();
            clearBrowserButton.Click += (s, e) => {
                deviceTreeView.Nodes.Clear();
                objectTreeView.Nodes.Clear();
            };
        }
        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
        private void CancelDiscoveryButton_Click(object sender, EventArgs e)
        {
            DiscoveryTimer_Tick(sender, e);
        }

        private async void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode != null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var progress = new Progress<Tuple<int, int>>(value =>
                {
                    objectDiscoveryProgressBar.Maximum = value.Item2;
                    objectDiscoveryProgressBar.Value = value.Item1;
                    objectCountLabel.Text = $"{value.Item1}/{value.Item2}";
                });

                objectDiscoveryProgressBar.Value = 0;
                objectDiscoveryProgressBar.Visible = true;
                objectCountLabel.Text = "0/0";
                objectCountLabel.Visible = true;
                cancelActionButton.Enabled = true;

                try
                {
                    await LoadDeviceDetails(deviceTreeView.SelectedNode, _cancellationTokenSource.Token, progress);
                }
                catch (OperationCanceledException)
                {
                    Log("Object discovery cancelled.");
                }
                catch (Exception ex)
                {
                    Log($"Error during object discovery: {ex.Message}");
                }
                finally
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            objectDiscoveryProgressBar.Visible = false;
                            objectCountLabel.Visible = false;
                            cancelActionButton.Enabled = false;
                        });
                    }
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
            else
            {
                MessageBox.Show("Please select a device from the list first.", "Device Not Selected");
            }
        }
        private void EnsureBacnetClientStarted()
        {
            string currentBBMDIp = bbmdIpComboBox.Text.Trim();

            if (_isClientStarted && _bacnetClient != null && _lastBBMDIp == currentBBMDIp) return;
            if (_bacnetClient != null)
            {
                _bacnetClient.Dispose();
                _bacnetClient = null;
                _isClientStarted = false;
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

                var transport = new BacnetIpUdpProtocolTransport(int.Parse(ipPortComboBox.Text), false, false, 1472, localIp);
                _bacnetClient = new BacnetClient(transport) { Timeout = int.Parse(apduTimeoutComboBox.Text) };
                _bacnetClient.OnIam += OnIamHandler;

                _bacnetClient.Start();
                Log("BACnet client transport started.");

                if (!string.IsNullOrWhiteSpace(currentBBMDIp))
                {
                    int bbmdPort = int.Parse(ipPortComboBox.Text);
                    if (!short.TryParse(bbmdTtlComboBox.Text, out short ttl))
                    {
                        Log("--- ERROR: Invalid BBMD TTL value. ---");
                        return;
                    }
                    Log($"Attempting to register as Foreign Device with BBMD at {currentBBMDIp}:{bbmdPort} with TTL {ttl}...");
                    _bacnetClient.RegisterAsForeignDevice(currentBBMDIp, ttl, bbmdPort);
                    Log("Foreign Device Registration message sent.");
                    Thread.Sleep(2000);
                }

                _isClientStarted = true;
                _lastBBMDIp = currentBBMDIp;
                Log("BACnet client initialization complete.");
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
            Log($"OnIam from {adr} for device {deviceId}. Segmentation support: {segmentation}");
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    ushort deviceNetwork = (adr.RoutedSource != null) ? adr.RoutedSource.net : adr.net;
                    string macAddress = adr.ToString(true);
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
                        var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "VendorName", vendorName }, { "MAC", macAddress }, { "Segmentation", segmentation }, { "VendorId", vendorId } };
                        string deviceText = $"(Name not read) ({macAddress}) ({deviceId}) ({vendorName})";
                        TreeNode deviceNode = new TreeNode(deviceText) { Name = deviceId.ToString(), Tag = deviceInfo };
                        networkNode.Nodes.Add(deviceNode);
                        networkNode.Expand();
                        ReadDeviceName(deviceNode, deviceId, adr);

                        int deviceCount = 0;
                        foreach (TreeNode netNode in deviceTreeView.Nodes)
                        {
                            deviceCount += netNode.Nodes.Count;
                        }
                        discoveryStatusLabel.Text = $"Found: {deviceCount}";
                    }
                }
                catch (Exception ex) { Log($"Error in OnIamHandler: {ex.Message}"); }
            });
        }

        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;
            deviceTreeView.Nodes.Clear();
            DeviceTreeView.Enabled = false;
            discoverButton.Enabled = false;
            Log("Sending Who-Is broadcast...");

            objectDiscoveryProgressBar.Style = ProgressBarStyle.Marquee;
            objectDiscoveryProgressBar.Visible = true;
            discoveryStatusLabel.Text = "Found: 0";
            discoveryStatusLabel.Visible = true;

            string bbmdIp = bbmdIpComboBox.Text.Trim();
            ushort.TryParse(networkNumberComboBox.Text, out ushort net);

            if (!string.IsNullOrWhiteSpace(bbmdIp))
            {
                int bbmdPort = int.Parse(ipPortComboBox.Text);
                _bacnetClient.RemoteWhoIs(bbmdIp, bbmdPort, -1, -1, net == 0 ? (ushort)0xFFFF : net);
            }
            else
            {
                var adr = _bacnetClient.Transport.GetBroadcastAddress();
                adr.net = net;
                _bacnetClient.WhoIs(-1, -1, adr);
            }
            _discoveryTimer.Start();
        }

        private void PingButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instanceNumberComboBox.Text) || string.IsNullOrWhiteSpace(ipAddressComboBox.Text))
            {
                MessageBox.Show("IP Address and Instance number are required for Ping.", "Error");
                return;
            }
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;
            uint deviceId = uint.Parse(instanceNumberComboBox.Text);
            Log($"Pinging Device ID: {deviceId}...");

            string bbmdIp = bbmdIpComboBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(bbmdIp))
            {
                int bbmdPort = int.Parse(ipPortComboBox.Text);
                ushort.TryParse(networkNumberComboBox.Text, out ushort net);
                _bacnetClient.RemoteWhoIs(bbmdIp, bbmdPort, (int)deviceId, (int)deviceId, net == 0 ? (ushort)0xFFFF : net);
            }
            else
            {
                try
                {
                    var port = ushort.Parse(ipPortComboBox.Text);
                    var adr = new BacnetAddress(BacnetAddressTypes.IP, ipAddressComboBox.Text, port);
                    _bacnetClient.WhoIs(lowLimit: (int)deviceId, highLimit: (int)deviceId, adr: adr);
                }
                catch (Exception ex)
                {
                    Log($"--- ERROR during Ping: {ex.Message} ---");
                    MessageBox.Show($"Error during Ping: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || e.Node.Tag.ToString() == "NETWORK_NODE")
            {
                _lastPingedDeviceId = null;
                UpdateAllStates(null, null);
                objectTreeView.Nodes.Clear();
                return;
            }

            _lastPingedDeviceId = uint.Parse(e.Node.Name);
            UpdateAllStates(null, null);
            DiscoverObjectsButton_Click(sender, e);
        }

        protected override void PopulateDefaultValues()
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

        protected override void LoadHistory()
        {
            PopulateComboBoxWithHistory(ipAddressComboBox, "ipAddress");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");
            PopulateComboBoxWithHistory(ipPortComboBox, "ipPort");
            PopulateComboBoxWithHistory(apduTimeoutComboBox, "apduTimeout");
            PopulateComboBoxWithHistory(bbmdIpComboBox, "bbmdIp");
            PopulateComboBoxWithHistory(bbmdTtlComboBox, "bbmdTtl");
            PopulateComboBoxWithHistory(networkNumberComboBox, "networkNumber");

            if (string.IsNullOrEmpty(ipAddressComboBox.Text)) ipAddressComboBox.Text = "192.168.1.200";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (string.IsNullOrEmpty(ipPortComboBox.Text)) ipPortComboBox.Text = "47808";
            if (string.IsNullOrEmpty(apduTimeoutComboBox.Text)) apduTimeoutComboBox.Text = "5000";
            if (string.IsNullOrEmpty(bbmdIpComboBox.Text)) bbmdIpComboBox.Text = "";
            if (string.IsNullOrEmpty(bbmdTtlComboBox.Text)) bbmdTtlComboBox.Text = "3600";
            if (string.IsNullOrEmpty(networkNumberComboBox.Text)) networkNumberComboBox.Text = "0";
        }

        public override void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();
            ipAddressComboBox?.Items.Clear();
            instanceNumberComboBox?.Items.Clear();
            ipPortComboBox?.Items.Clear();
            apduTimeoutComboBox?.Items.Clear();
            bbmdIpComboBox?.Items.Clear();
            bbmdTtlComboBox?.Items.Clear();
            networkNumberComboBox?.Items.Clear();
            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet/IP history cleared.");
        }
    }
}

