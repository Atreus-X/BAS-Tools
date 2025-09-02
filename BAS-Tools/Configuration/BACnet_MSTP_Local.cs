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
    public partial class BACnet_MSTP_Local : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private HistoryManager _historyManager;
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;
        private readonly object _bacnetLock = new object();
        private System.Windows.Forms.Timer _discoveryTimer;
        private readonly System.Windows.Forms.Timer _propertyPollingTimer;
        private BacnetObjectId _selectedObjectId;

        public BACnet_MSTP_Local()
        {
            InitializeComponent();
            _propertyPollingTimer = new System.Windows.Forms.Timer();
            this.Load += BACnet_MSTP_Local_Load;
        }

        private void BACnet_MSTP_Local_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            GlobalLogger.Register(outputTextBox);
            _historyManager = new HistoryManager("BACnet_MSTP_Local_");

            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();

            _discoveryTimer = new System.Windows.Forms.Timer { Interval = 10000 };
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
            _propertyPollingTimer.Tick += PropertyPollingTimer_Tick;

        }

        private void WireUpEventHandlers()
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
            deviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            objectTreeView.AfterSelect += ObjectTreeView_AfterSelect;
            propertiesDataGridView.CellEndEdit += propertiesDataGridView_CellEndEdit;
            togglePollingButton.Click += TogglePollingButton_Click;
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

        private void ReadDeviceName(TreeNode deviceNode, uint deviceId, BacnetAddress adr)
        {
            Task.Run(() =>
            {
                string finalDeviceName = deviceId.ToString();
                string errorText = null;
                bool supportsReadPropertyMultiple = false;
                try
                {
                    lock (_bacnetLock)
                    {
                        var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                        if (_bacnetClient.ReadPropertyRequest(adr, objectId, BacnetPropertyIds.PROP_OBJECT_NAME, out IList<BacnetValue> values) && values?.Count > 0)
                        {
                            finalDeviceName = values[0].Value.ToString();
                        }
                        else
                        {
                            errorText = " (Name not available)";
                        }

                        // Check for ReadPropertyMultiple support
                        if (_bacnetClient.ReadPropertyRequest(adr, objectId, BacnetPropertyIds.PROP_PROTOCOL_SERVICES_SUPPORTED, out values) && values?.Count > 0)
                        {
                            var servicesSupported = (BacnetBitString)values[0].Value;
                            if (servicesSupported.value.Length > 1 && (servicesSupported.value[1] & 0x40) > 0) // Bit 14 is ReadPropertyMultiple
                            {
                                supportsReadPropertyMultiple = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error reading name for device {deviceId}: {ex.Message}");
                    errorText = " (Error reading name)";
                }
                finally
                {
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            var deviceInfo = deviceNode.Tag as Dictionary<string, object>;
                            deviceInfo["SupportsReadPropertyMultiple"] = supportsReadPropertyMultiple;
                            deviceNode.Text = $"{finalDeviceName} ({adr}) ({deviceId}){errorText ?? ""}";
                        });
                    }
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

                var deviceInfo = deviceTreeView.Nodes.Find(deviceId.ToString(), true).First().Tag as Dictionary<string, object>;
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
                            new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_OBJECT_NAME, BACNET_ARRAY_ALL),
                            new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_OBJECT_TYPE, BACNET_ARRAY_ALL),
                            new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_OBJECT_IDENTIFIER, BACNET_ARRAY_ALL)
                        };
                        var request = new BacnetReadAccessSpecification(new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), propertyReferences);
                        if (_bacnetClient.ReadPropertyMultipleRequest(deviceAddress, new List<BacnetReadAccessSpecification> { request }, out IList<BacnetReadAccessResult> results))
                        {
                            Log($"--- SUCCESS: Found {results.Count} objects. ---");
                            var values = results.SelectMany(r => r.values.Select(v => v.value.FirstOrDefault())).Where(v => v.Value != null).ToList();
                            PopulateObjectTree(values);
                        }
                    }
                    else
                    {
                        Log("Device does not support ReadPropertyMultiple. Using ReadProperty for object list.");
                        if (_bacnetClient.ReadPropertyRequest(deviceAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objectList))
                        {
                            Log($"--- SUCCESS: Found {objectList.Count} objects. ---");
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
            catch (Exception ex)
            {
                Log($"--- Discover Objects Error: {ex.Message} ---");
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

            baudRateComboBox.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            maxMastersComboBox.Items.AddRange(Enumerable.Range(1, 127).Select(i => i.ToString()).ToArray());
            maxInfoFramesComboBox.Items.AddRange(Enumerable.Range(1, 10).Select(i => i.ToString()).ToArray());
        }

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool deviceSelected = deviceTreeView.SelectedNode != null;
            pingButton.Enabled = deviceSelected;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && uint.TryParse(e.Node.Name, out uint deviceId))
            {
                _lastPingedDeviceId = deviceId;
                UpdateAllStates(null, null);
                DiscoverObjectsButton_Click(null, null);
            }
        }

        private void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            objectTreeView.Nodes.Clear();
            if (objectList == null) return;

            var objectGroups = objectList
                .Where(v => v.Value is BacnetObjectId)
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
            if (nodes.Length > 0 && nodes[0].Tag is Dictionary<string, object> deviceInfo)
            {
                return deviceInfo["Address"] as BacnetAddress;
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

            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;

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
            if (comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text))
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

            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP Local history cleared.");
        }

        public void Shutdown()
        {
            _propertyPollingTimer?.Stop();
            _propertyPollingTimer?.Dispose();
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }

        private void ObjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _propertyPollingTimer.Stop();
            togglePollingButton.Text = "Start Polling";
            propertiesDataGridView.Rows.Clear();

            if (e.Node?.Tag is BacnetObjectId objectId)
            {
                _selectedObjectId = objectId;
                togglePollingButton.Enabled = true;
                Log($"Selected object: {objectId}");
                if (readIntervalNumericUpDown.Value > 0)
                {
                    _propertyPollingTimer.Interval = (int)readIntervalNumericUpDown.Value;
                    PropertyPollingTimer_Tick(null, null);
                    _propertyPollingTimer.Start();
                    togglePollingButton.Text = "Stop Polling";
                }
            }
            else
            {
                togglePollingButton.Enabled = false;
            }
        }

        private void TogglePollingButton_Click(object sender, EventArgs e)
        {
            if (_propertyPollingTimer.Enabled)
            {
                _propertyPollingTimer.Stop();
                togglePollingButton.Text = "Start Polling";
                Log("Property polling stopped.");
            }
            else
            {
                if (readIntervalNumericUpDown.Value > 0)
                {
                    _propertyPollingTimer.Interval = (int)readIntervalNumericUpDown.Value;
                    PropertyPollingTimer_Tick(null, null);
                    _propertyPollingTimer.Start();
                    togglePollingButton.Text = "Stop Polling";
                    Log($"Property polling started with interval {_propertyPollingTimer.Interval}ms.");
                }
            }
        }

        private async void PropertyPollingTimer_Tick(object sender, EventArgs e)
        {
            if (_bacnetClient == null || !_isClientStarted || _lastPingedDeviceId == null || _selectedObjectId.type == BacnetObjectTypes.MAX_BACNET_OBJECT_TYPE)
                return;

            try
            {
                BacnetAddress adr = await FindDeviceAddressAsync(_lastPingedDeviceId.Value);
                if (adr == null) return;

                if (_bacnetClient.ReadPropertyRequest(adr, _selectedObjectId, BacnetPropertyIds.PROP_ALL, out IList<BacnetValue> values))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        propertiesDataGridView.Rows.Clear();
                        var propValues = values.Select(v => v.Value).OfType<BacnetPropertyValue>();
                        foreach (var prop in propValues)
                        {
                            string propName = ((BacnetPropertyIds)prop.property.propertyIdentifier).ToString();
                            string propValue = prop.value.FirstOrDefault().Value?.ToString() ?? "{empty}";
                            propertiesDataGridView.Rows.Add(propName, propValue);
                        }
                    });
                }
                else
                {
                    Log($"Failed to read properties for {_selectedObjectId}.");
                }
            }
            catch (Exception ex)
            {
                Log($"Error polling properties: {ex.Message}");
                if (_propertyPollingTimer.Enabled)
                {
                    _propertyPollingTimer.Stop();
                    togglePollingButton.Text = "Start Polling";
                }
            }
        }

        private async void propertiesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = propertiesDataGridView.Rows[e.RowIndex];
                var propertyName = row.Cells[0].Value.ToString();
                var newValue = row.Cells[1].Value.ToString();

                Log($"Attempting to write {newValue} to {propertyName} on {_selectedObjectId}");

                if (Enum.TryParse<BacnetPropertyIds>(propertyName, out var propertyId))
                {
                    try
                    {
                        BacnetAddress adr = await FindDeviceAddressAsync(_lastPingedDeviceId.Value);
                        if (adr == null) return;

                        // This is a simplified example. You'll need to determine the correct BacnetApplicationTags based on the property.
                        // For now, we'll try to parse it as a float, which is common for analog values.
                        if (float.TryParse(newValue, out float floatValue))
                        {
                            var bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, floatValue);
                            _bacnetClient.WritePriority = uint.Parse(writePriorityComboBox.SelectedItem.ToString().Split(' ')[0]);
                            _bacnetClient.WritePropertyRequest(adr, _selectedObjectId, propertyId, new[] { bacnetValue });
                            Log("Write request sent.");
                        }
                        else
                        {
                            Log("Could not parse new value as a float. Write request not sent.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Error writing property: {ex.Message}");
                    }
                }
            }
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

                        // Simple type inference, can be improved
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