using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            manualReadWriteButton.Click += ManualReadWriteButton_Click;
            clearLogButton.Click += ClearLogButton_Click;
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
                    var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "Segmentation", segmentation } };
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

        private void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (_lastPingedDeviceId.HasValue)
            {
                var node = deviceTreeView.Nodes.Find(_lastPingedDeviceId.Value.ToString(), true).FirstOrDefault();
                if (node != null)
                {
                    LoadDeviceDetails(node);
                }
            }
            else
            {
                MessageBox.Show("Please select a device from the list first.", "Device Not Selected");
            }
        }

        private async void LoadDeviceDetails(TreeNode selectedNode)
        {
            if (selectedNode == null || selectedNode.Tag == null || selectedNode.Tag.ToString() == "NETWORK_NODE") return;

            uint deviceId = uint.Parse(selectedNode.Name);
            BacnetAddress deviceAddress = await FindDeviceAddressAsync(deviceId);
            if (deviceAddress == null)
            {
                Log($"--- ERROR: Could not resolve address for Device {deviceId}. It may be offline. ---");
                return;
            }

            var deviceInfo = selectedNode.Tag as Dictionary<string, object>;
            var segmentation = (BacnetSegmentations)deviceInfo["Segmentation"];
            var old_segments = _bacnetClient.MaxSegments;
            if (segmentation == BacnetSegmentations.SEGMENTATION_NONE)
            {
                _bacnetClient.MaxSegments = BacnetMaxSegments.MAX_SEG0;
            }
            try
            {
                if (deviceInfo.ContainsKey("SupportsReadPropertyMultiple") && (bool)deviceInfo["SupportsReadPropertyMultiple"])
                {
                    Log("Device supports ReadPropertyMultiple. Using it to discover objects.");
                    var propertyReferences = new List<BacnetPropertyReference>
                    {
                        new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_OBJECT_LIST, BACNET_ARRAY_ALL)
                    };
                    var request = new BacnetReadAccessSpecification(new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), propertyReferences);
                    if (_bacnetClient.ReadPropertyMultipleRequest(deviceAddress, new List<BacnetReadAccessSpecification> { request }, out IList<BacnetReadAccessResult> results))
                    {
                        var values = results.SelectMany(r => r.values.SelectMany(v => v.value)).Where(v => v.Value != null).ToList();
                        PopulateObjectTree(values);
                    }
                }
                else
                {
                    Log("Device does not support ReadPropertyMultiple. Using ReadProperty for object list.");
                    if (_bacnetClient.ReadPropertyRequest(deviceAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objectList))
                    {
                        PopulateObjectTree(objectList);
                    }
                    else
                    {
                        Log("--- ERROR: Failed to read object list. ---");
                    }
                }
            }
            finally
            {
                _bacnetClient.MaxSegments = old_segments;
            }
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
                DiscoverObjectsButton_Click(null, null);
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

        private void ManualReadWriteButton_Click(object sender, EventArgs e)
        {
            using (var form = new ManualReadWriteForm(deviceTreeView.Nodes))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var deviceNode = form.SelectedDeviceNode;
                    var adr = (deviceNode.Tag as Dictionary<string, object>)["Address"] as BacnetAddress;
                    var objectId = form.SelectedObject;
                    var propertyId = form.SelectedProperty;

                    if (form.IsReadOperation)
                    {
                        if (_bacnetClient.ReadPropertyRequest(adr, objectId, propertyId, out IList<BacnetValue> values))
                        {
                            string valuesStr = string.Join(", ", values.Select(v => v.Value?.ToString() ?? "null"));
                            MessageBox.Show($"Read Success:\n{valuesStr}", "Read Result");
                            Log($"Manual Read Success on {objectId}, Property {propertyId}: {valuesStr}");
                        }
                        else
                        {
                            MessageBox.Show("Read failed.", "Read Result");
                            Log($"Manual Read Failed on {objectId}, Property {propertyId}");
                        }
                    }
                    else
                    {
                        var valueString = form.ValueToWrite;
                        var priority = form.WritePriority;

                        BacnetValue bacnetValue;
                        if (bool.TryParse(valueString, out bool boolVal))
                            bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_BOOLEAN, boolVal);
                        else if (uint.TryParse(valueString, out uint uintVal))
                            bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_UNSIGNED_INT, uintVal);
                        else if (float.TryParse(valueString, out float floatVal))
                            bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, floatVal);
                        else
                            bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_CHARACTER_STRING, valueString);

                        _bacnetClient.WritePriority = priority;
                        if (_bacnetClient.WritePropertyRequest(adr, objectId, propertyId, new[] { bacnetValue }))
                        {
                            MessageBox.Show("Write successful.", "Write Result");
                            Log($"Manual Write Success on {objectId}, Property {propertyId}, Value {valueString}, Priority {priority}");
                        }
                        else
                        {
                            MessageBox.Show("Write failed.", "Write Result");
                            Log($"Manual Write Failed on {objectId}, Property {propertyId}, Value {valueString}, Priority {priority}");
                        }
                    }
                }
            }
        }
    }
}