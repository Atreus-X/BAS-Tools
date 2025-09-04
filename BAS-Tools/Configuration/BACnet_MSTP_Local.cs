using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainApp.BACnet;
using static System.IO.BACnet.Serialize.ASN1;

namespace MainApp.Configuration
{
    public partial class BACnet_MSTP_Local : BACnetControlBase
    {
        private System.Windows.Forms.Timer _discoveryTimer;

        // Implement abstract properties from the base class
        protected override TreeView DeviceTreeView => deviceTreeView;
        protected override TreeView ObjectTreeView => objectTreeView;
        protected override DataGridView PropertiesDataGridView => propertiesDataGridView;
        protected override RichTextBox OutputTextBox => outputTextBox;
        protected override Button TogglePollingButton => togglePollingButton;
        protected override NumericUpDown ReadIntervalNumericUpDown => readIntervalNumericUpDown;
        protected override ComboBox WritePriorityComboBox => writePriorityComboBox;

        public BACnet_MSTP_Local()
        {
            InitializeComponent();
            this.Load += BACnet_MSTP_Local_Load;
        }

        private void BACnet_MSTP_Local_Load(object sender, EventArgs e)
        {
            BaseLoad("BACnet_MSTP_Local_");
            _discoveryTimer = new System.Windows.Forms.Timer { Interval = 10000 };
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
        }

        protected override void WireUpProtocolSpecificEventHandlers()
        {
            serialPortComboBox.Leave += (s, args) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            baudRateComboBox.Leave += (s, args) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            maxMastersComboBox.Leave += (s, args) => SaveComboBoxEntry(maxMastersComboBox, "maxMasters");
            maxInfoFramesComboBox.Leave += (s, args) => SaveComboBoxEntry(maxInfoFramesComboBox, "maxInfoFrames");

            startDiscoveryButton.Click += StartDiscoveryButton_Click;
            cancelDiscoveryButton.Click += CancelDiscoveryButton_Click;
            pingButton.Click += PingButton_Click;
            discoverObjectsButton.Click += DiscoverObjectsButton_Click;
            manualReadWriteButton.Click += base.ManualReadWriteButton_Click;
            clearLogButton.Click += base.ClearLogButton_Click;
            cancelActionButton.Click += CancelActionButton_Click;
        }

        private void CancelActionButton_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private new void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode != null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var progress = new Progress<int>(value =>
                {
                    objectDiscoveryProgressBar.Value = value;
                    objectCountLabel.Text = $"Found {value}%";
                });

                objectDiscoveryProgressBar.Visible = true;
                objectCountLabel.Visible = true;
                cancelActionButton.Enabled = true;

                try
                {
                    LoadDeviceDetails(deviceTreeView.SelectedNode, _cancellationTokenSource.Token, progress);
                }
                finally
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        if (this.IsHandleCreated && !_cancellationTokenSource.IsCancellationRequested)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                objectDiscoveryProgressBar.Visible = false;
                                objectCountLabel.Visible = false;
                                cancelActionButton.Enabled = false;
                            });
                        }
                    });
                }
            }
            else
            {
                MessageBox.Show("Please select a device from the list first.", "Device Not Selected");
            }
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
                Log("Initializing BACnet MS/TP client (Local)...");
                var portName = serialPortComboBox.Text;
                var baudRate = int.Parse(baudRateComboBox.Text);
                byte nodeAddress = 0;
                var maxMasters = byte.Parse(maxMastersComboBox.Text);
                var maxInfoFrames = byte.Parse(maxInfoFramesComboBox.Text);
                var transport = new BacnetMstpProtocolTransport(portName, baudRate, nodeAddress, maxMasters, maxInfoFrames);

                _bacnetClient = new BacnetClient(transport) { Timeout = 5000 };
                _bacnetClient.OnIam += OnIamHandler;

                _bacnetClient.Start();
                Log("BACnet client transport started.");

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

        private void OnIamHandler(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
            Log($"--- I-AM HANDLER FIRED --- from {adr}, Device ID: {deviceId}, Vendor: {vendorId}");

            this.Invoke((MethodInvoker)delegate
            {
                string deviceDisplay = $"{deviceId} ({adr})";
                if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                {
                    Log($"Adding new device to tree: {deviceDisplay}");
                    var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "Segmentation", segmentation }, { "VendorId", vendorId } };
                    var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = deviceInfo };
                    deviceTreeView.Nodes.Add(node);
                    discoveryStatusLabel.Text = $"Found: {deviceTreeView.Nodes.Count}";
                    ReadDeviceName(node, deviceId, adr);
                }
                else
                {
                    Log($"Device already in tree: {deviceDisplay}");
                }
            });
        }

        private void PingButton_Click(object sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode == null)
            {
                MessageBox.Show("Please select a device to ping.", "Error");
                return;
            }

            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;

            uint deviceId = uint.Parse(deviceTreeView.SelectedNode.Name);
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
            DiscoveryTimer_Tick(sender, e);
        }

        private void DiscoveryTimer_Tick(object sender, EventArgs e)
        {
            _discoveryTimer.Stop();
            Log("Discovery finished.");
            startDiscoveryButton.Enabled = true;
            cancelDiscoveryButton.Visible = false;
            discoveryStatusLabel.Visible = false;
        }

        protected override void PopulateDefaultValues()
        {
            serialPortComboBox.Items.Clear();
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            if (serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;

            baudRateComboBox.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            maxMastersComboBox.Items.AddRange(Enumerable.Range(1, 127).Select(i => i.ToString()).ToArray());
            maxInfoFramesComboBox.Items.AddRange(Enumerable.Range(1, 10).Select(i => i.ToString()).ToArray());
        }

        protected override void UpdateAllStates(object sender, EventArgs e)
        {
            bool deviceSelected = deviceTreeView.SelectedNode != null;
            pingButton.Enabled = deviceSelected;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        protected override void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && uint.TryParse(e.Node.Name, out uint deviceId))
            {
                _lastPingedDeviceId = deviceId;
                UpdateAllStates(null, null);
            }
        }

        protected override void LoadHistory()
        {
            PopulateComboBoxWithHistory(serialPortComboBox, "serialPort");
            PopulateComboBoxWithHistory(baudRateComboBox, "baudRate");
            PopulateComboBoxWithHistory(maxMastersComboBox, "maxMasters");
            PopulateComboBoxWithHistory(maxInfoFramesComboBox, "maxInfoFrames");

            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
        }

        public override void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();
            serialPortComboBox.Items.Clear();
            baudRateComboBox.Items.Clear();
            maxMastersComboBox.Items.Clear();
            maxInfoFramesComboBox.Items.Clear();

            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP Local history cleared.");
        }
    }
}

