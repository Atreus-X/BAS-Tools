using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.BACnet;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainApp.BACnet;
using static System.IO.BACnet.Serialize.ASN1;

namespace MainApp.BACnet
{
    public abstract partial class BACnetControlBase : UserControl, IHistorySupport
    {
        protected BacnetClient _bacnetClient;
        protected HistoryManager _historyManager;
        protected uint? _lastPingedDeviceId;
        protected bool _isClientStarted = false;
        protected readonly object _bacnetLock = new object();
        protected readonly System.Windows.Forms.Timer _propertyPollingTimer;
        protected BacnetObjectId _selectedObjectId;
        protected CancellationTokenSource _cancellationTokenSource;

        // Abstract properties have been changed to virtual properties with default null implementations
        protected virtual TreeView DeviceTreeView => null;
        protected virtual TreeView ObjectTreeView => null;
        protected virtual DataGridView PropertiesDataGridView => null;
        protected virtual RichTextBox OutputTextBox => null;
        protected virtual Button TogglePollingButton => null;
        protected virtual NumericUpDown ReadIntervalNumericUpDown => null;
        protected virtual ComboBox WritePriorityComboBox => null;

        public BACnetControlBase()
        {
            InitializeComponent();
            _propertyPollingTimer = new System.Windows.Forms.Timer();
        }

        // Common Load logic
        protected void BaseLoad(string historyPrefix)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            GlobalLogger.Register(OutputTextBox);
            _historyManager = new HistoryManager(historyPrefix);

            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpCommonEventHandlers();
            WireUpProtocolSpecificEventHandlers();
            InitializeContextMenu();

            _propertyPollingTimer.Tick += PropertyPollingTimer_Tick;
        }

        private void InitializeContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            var exportMenuItem = new ToolStripMenuItem("Export to CSV");
            exportMenuItem.Click += ExportMenuItem_Click;
            contextMenu.Items.Add(exportMenuItem);
            DeviceTreeView.ContextMenuStrip = contextMenu;
            ObjectTreeView.ContextMenuStrip = contextMenu;
        }

        private void ExportMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var contextMenu = menuItem?.Owner as ContextMenuStrip;
            var treeView = contextMenu?.SourceControl as TreeView;

            if (treeView == null) return;

            var selectedNode = treeView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Please select a node to export.", "No Node Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                sfd.FileName = $"{selectedNode.Text.Split(' ')[0]}_export.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var csv = new StringBuilder();
                        if (treeView == DeviceTreeView)
                        {
                            csv.AppendLine("Network,Device ID,Device Name,MAC Address,Vendor");
                            if (selectedNode.Tag?.ToString() == "NETWORK_NODE")
                            {
                                foreach (TreeNode deviceNode in selectedNode.Nodes)
                                {
                                    ExtractDeviceData(deviceNode, selectedNode.Text, csv);
                                }
                            }
                            else
                            {
                                ExtractDeviceData(selectedNode, selectedNode.Parent.Text, csv);
                            }
                        }
                        else if (treeView == ObjectTreeView)
                        {
                            csv.AppendLine("Object Type,Instance,Object Name");
                            if (selectedNode.Parent == null) // Group node
                            {
                                foreach (TreeNode objectNode in selectedNode.Nodes)
                                {
                                    ExtractObjectData(objectNode, csv);
                                }
                            }
                            else // Individual object node
                            {
                                ExtractObjectData(selectedNode, csv);
                            }
                        }

                        File.WriteAllText(sfd.FileName, csv.ToString());
                        MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExtractDeviceData(TreeNode deviceNode, string networkName, StringBuilder csv)
        {
            if (deviceNode.Tag is Dictionary<string, object> deviceInfo)
            {
                var deviceId = deviceNode.Name;
                var deviceName = deviceNode.Text.Split('(')[0].Trim();
                var mac = deviceInfo.ContainsKey("MAC") ? deviceInfo["MAC"].ToString() : "N/A";
                var vendor = deviceInfo.ContainsKey("VendorName") ? deviceInfo["VendorName"].ToString() : "N/A";
                csv.AppendLine($"{networkName},{deviceId},{deviceName},{mac},{vendor}");
            }
        }

        private void ExtractObjectData(TreeNode objectNode, StringBuilder csv)
        {
            if (objectNode.Tag is BacnetObjectId oid)
            {
                var objectType = objectNode.Parent.Text;
                var instance = oid.instance;
                var objectName = objectNode.Text;
                csv.AppendLine($"{objectType},{instance},{objectName}");
            }
        }

        private void WireUpCommonEventHandlers()
        {
            DeviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            ObjectTreeView.AfterSelect += ObjectTreeView_AfterSelect;
            PropertiesDataGridView.CellEndEdit += propertiesDataGridView_CellEndEdit;
            TogglePollingButton.Click += TogglePollingButton_Click;
        }

        // Abstract methods for protocol-specific implementation
        protected virtual void PopulateDefaultValues() { }
        protected virtual void LoadHistory() { }
        protected virtual void UpdateAllStates(object sender, EventArgs e) { }
        protected virtual void WireUpProtocolSpecificEventHandlers() { }
        public virtual void ClearHistory() { }

        protected void ReadDeviceName(TreeNode deviceNode, uint deviceId, BacnetAddress adr)
        {
            Task.Run(() =>
            {
                string finalDeviceName = deviceId.ToString();
                string errorText = null;
                bool supportsReadPropertyMultiple = false;
                var deviceInfo = deviceNode.Tag as Dictionary<string, object>;
                var segmentation = (BacnetSegmentations)deviceInfo["Segmentation"];
                var old_segments = _bacnetClient.MaxSegments;
                if (segmentation == BacnetSegmentations.SEGMENTATION_NONE)
                {
                    _bacnetClient.MaxSegments = BacnetMaxSegments.MAX_SEG0;
                }
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

                        if (_bacnetClient.ReadPropertyRequest(adr, objectId, BacnetPropertyIds.PROP_PROTOCOL_SERVICES_SUPPORTED, out values) && values?.Count > 0)
                        {
                            var servicesSupported = (BacnetBitString)values[0].Value;
                            if (servicesSupported.value.Length > 1 && (servicesSupported.value[1] & 0x40) > 0)
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
                    _bacnetClient.MaxSegments = old_segments;
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            deviceInfo["SupportsReadPropertyMultiple"] = supportsReadPropertyMultiple;
                            deviceNode.Text = $"{finalDeviceName} ({deviceInfo["MAC"]}) ({deviceId}) ({deviceInfo["VendorName"]}){errorText ?? ""}";
                        });
                    }
                }
            });
        }

        protected async Task PopulateObjectTree(IList<BacnetValue> objectList)
        {
            ObjectTreeView.Nodes.Clear();
            if (objectList == null) return;
            ushort vendorId = 0;
            if (_lastPingedDeviceId.HasValue)
            {
                var node = DeviceTreeView.Nodes.Find(_lastPingedDeviceId.Value.ToString(), true).FirstOrDefault();
                if (node != null && node.Tag is Dictionary<string, object> deviceInfo)
                {
                    vendorId = (ushort)deviceInfo["VendorId"];
                }
            }

            var deviceAddress = await FindDeviceAddressAsync(_lastPingedDeviceId.Value);
            if (deviceAddress == null) return;

            var objectGroups = objectList
                .Where(v => v.Value is BacnetObjectId)
                .Select(val => (BacnetObjectId)val.Value)
                .GroupBy(objId => BACnetObjectFactory.GetBacnetObjectInfo(objId.type, vendorId).Group)
                .OrderBy(g => g.Key.ToString());

            ObjectTreeView.BeginUpdate();
            try
            {
                foreach (var group in objectGroups)
                {
                    var parentNode = new TreeNode(group.Key);
                    ObjectTreeView.Nodes.Add(parentNode);

                    var objectDetails = new List<Tuple<uint, string, BacnetObjectId>>();

                    var tasks = group.Select(async objId =>
                    {
                        string name = await GetObjectNameAsync(deviceAddress, objId);
                        lock (objectDetails)
                        {
                            objectDetails.Add(Tuple.Create(objId.instance, name, objId));
                        }
                    });
                    await Task.WhenAll(tasks);


                    foreach (var detail in objectDetails.OrderBy(d => d.Item1)) // Sort by instance
                    {
                        var childNode = new TreeNode(detail.Item2) { Tag = detail.Item3 };
                        parentNode.Nodes.Add(childNode);
                    }
                }
            }
            finally
            {
                ObjectTreeView.EndUpdate();
            }
        }

        private Task<string> GetObjectNameAsync(BacnetAddress adr, BacnetObjectId objectId)
        {
            return Task.Run(() => {
                lock (_bacnetLock)
                {
                    if (_bacnetClient.ReadPropertyRequest(adr, objectId, BacnetPropertyIds.PROP_OBJECT_NAME, out IList<BacnetValue> values) && values?.Count > 0)
                    {
                        return values[0].Value.ToString();
                    }
                    else
                    {
                        var objectTypeInfo = BACnetObjectFactory.GetBacnetObjectInfo(objectId.type, 0);
                        return $"{objectTypeInfo.Label} {objectId.instance}";
                    }
                }
            });
        }

        protected async Task<BacnetAddress> FindDeviceAddressAsync(uint deviceId)
        {
            foreach (TreeNode networkNode in DeviceTreeView.Nodes)
            {
                foreach (TreeNode deviceNode in networkNode.Nodes)
                {
                    if (deviceNode.Name == deviceId.ToString() && deviceNode.Tag is Dictionary<string, object> deviceInfo)
                    {
                        return deviceInfo["Address"] as BacnetAddress;
                    }
                }
            }

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            var tcs = new TaskCompletionSource<BacnetAddress>();
            void handler(BacnetClient _s, BacnetAddress a, uint d, uint _m, BacnetSegmentations _seg, ushort _v)
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

        protected void TogglePollingButton_Click(object sender, EventArgs e)
        {
            if (_propertyPollingTimer.Enabled)
            {
                _propertyPollingTimer.Stop();
                TogglePollingButton.Text = "Start Polling";
                Log("Property polling stopped.");
            }
            else
            {
                if (ReadIntervalNumericUpDown.Value > 0)
                {
                    _propertyPollingTimer.Interval = (int)ReadIntervalNumericUpDown.Value;
                    PropertyPollingTimer_Tick(null, null);
                    _propertyPollingTimer.Start();
                    TogglePollingButton.Text = "Stop Polling";
                    Log($"Property polling started with interval {_propertyPollingTimer.Interval}ms.");
                }
            }
        }

        protected async void PropertyPollingTimer_Tick(object sender, EventArgs e)
        {
            if (_bacnetClient == null || !_isClientStarted || _lastPingedDeviceId == null || _selectedObjectId.type == BacnetObjectTypes.MAX_BACNET_OBJECT_TYPE)
                return;

            try
            {
                BacnetAddress adr = await FindDeviceAddressAsync(_lastPingedDeviceId.Value);
                if (adr == null) return;

                var deviceNode = DeviceTreeView.Nodes.Find(_lastPingedDeviceId.Value.ToString(), true).FirstOrDefault();
                var deviceInfo = deviceNode?.Tag as Dictionary<string, object>;
                bool supportsRpm = deviceInfo != null && deviceInfo.ContainsKey("SupportsReadPropertyMultiple") && (bool)deviceInfo["SupportsReadPropertyMultiple"];

                List<BacnetPropertyValue> allProperties = new List<BacnetPropertyValue>();
                bool success = false;

                if (supportsRpm)
                {
                    Log($"Attempting to read all properties for {_selectedObjectId} using ReadPropertyMultiple with PROP_ALL.");
                    var propertyReferences = new List<BacnetPropertyReference> { new BacnetPropertyReference((uint)BacnetPropertyIds.PROP_ALL, BACNET_ARRAY_ALL) };
                    var request = new BacnetReadAccessSpecification(_selectedObjectId, propertyReferences);

                    if (_bacnetClient.ReadPropertyMultipleRequest(adr, new List<BacnetReadAccessSpecification> { request }, out IList<BacnetReadAccessResult> results))
                    {
                        if (results != null && results.Count > 0 && results[0].values.Any(p => p.value.All(v => !(v.Value is BacnetError))))
                        {
                            allProperties.AddRange(results[0].values);
                            success = true;
                            Log($"Successfully read {allProperties.Count} properties using ReadPropertyMultiple.");
                        }
                        else
                        {
                            Log("ReadPropertyMultiple with PROP_ALL returned no values or an error.");
                        }
                    }
                    else
                    {
                        Log("ReadPropertyMultiple with PROP_ALL failed.");
                    }
                }

                if (!success)
                {
                    Log("Falling back to reading property list and then individual properties one by one.");
                    if (_bacnetClient.ReadPropertyRequest(adr, _selectedObjectId, BacnetPropertyIds.PROP_PROPERTY_LIST, out IList<BacnetValue> propertyListValues))
                    {
                        if (propertyListValues != null && propertyListValues.Count > 0)
                        {
                            Log($"Found {propertyListValues.Count} properties in the property list.");
                            foreach (var propValue in propertyListValues)
                            {
                                var propId = (BacnetPropertyIds)(uint)propValue.Value;
                                if (_bacnetClient.ReadPropertyRequest(adr, _selectedObjectId, propId, out IList<BacnetValue> values))
                                {
                                    var prop = new BacnetPropertyValue
                                    {
                                        property = new BacnetPropertyReference((uint)propId, BACNET_ARRAY_ALL),
                                        value = values
                                    };
                                    allProperties.Add(prop);
                                }
                                else
                                {
                                    Log($"Failed to read property: {propId}");
                                }
                            }
                            success = true;
                        }
                        else
                        {
                            Log("PROP_PROPERTY_LIST was empty.");
                        }
                    }
                    else
                    {
                        Log("Failed to read PROP_PROPERTY_LIST.");
                    }
                }


                if (success)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        PropertiesDataGridView.Rows.Clear();
                        foreach (var prop in allProperties.OrderBy(p => p.property.propertyIdentifier))
                        {
                            string propName = ((BacnetPropertyIds)prop.property.propertyIdentifier).ToString();
                            string propValue = prop.value.Count > 0 && prop.value.First().Value != null ? prop.value.First().Value.ToString() : "{empty}";
                            PropertiesDataGridView.Rows.Add(propName, propValue);
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
                    TogglePollingButton.Text = "Start Polling";
                }
            }
        }


        protected async void propertiesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var row = PropertiesDataGridView.Rows[e.RowIndex];
                var propertyName = row.Cells[0].Value.ToString();
                var newValue = row.Cells[1].Value.ToString();

                Log($"Attempting to write {newValue} to {propertyName} on {_selectedObjectId}");

                if (Enum.TryParse<BacnetPropertyIds>(propertyName, out var propertyId))
                {
                    try
                    {
                        BacnetAddress adr = await FindDeviceAddressAsync(_lastPingedDeviceId.Value);
                        if (adr == null) return;

                        if (float.TryParse(newValue, out float floatValue))
                        {
                            var bacnetValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, floatValue);
                            _bacnetClient.WritePriority = uint.Parse(WritePriorityComboBox.SelectedItem.ToString().Split(' ')[0]);
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

        protected void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
                return;
            }
            OutputTextBox.AppendText($"{DateTime.Now.ToLongTimeString()}: {message}{Environment.NewLine}");
            OutputTextBox.ScrollToCaret();
        }

        protected void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return;
            comboBox.Items.Clear();
            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                comboBox.Text = historyList.First();
            }
        }

        protected void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (_historyManager != null && comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text;
            }
        }

        public virtual void Shutdown()
        {
            _propertyPollingTimer?.Stop();
            _propertyPollingTimer?.Dispose();
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }

        protected virtual void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e) { }
        protected virtual void ObjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _propertyPollingTimer.Stop();
            TogglePollingButton.Text = "Start Polling";
            PropertiesDataGridView.Rows.Clear();

            if (e.Node?.Tag is BacnetObjectId objectId)
            {
                _selectedObjectId = objectId;
                TogglePollingButton.Enabled = true;
                Log($"Selected object: {objectId}");
                if (ReadIntervalNumericUpDown.Value > 0)
                {
                    _propertyPollingTimer.Interval = (int)ReadIntervalNumericUpDown.Value;
                    PropertyPollingTimer_Tick(null, null);
                    _propertyPollingTimer.Start();
                    TogglePollingButton.Text = "Stop Polling";
                }
            }
            else
            {
                TogglePollingButton.Enabled = false;
            }
        }

        protected void ClearLogButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Clear();
            Log("Log cleared.");
        }

        protected void ManualReadWriteButton_Click(object sender, EventArgs e)
        {
            using (var form = new ManualReadWriteForm(DeviceTreeView.Nodes))
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

        protected Task LoadDeviceDetails(TreeNode selectedNode, CancellationToken cancelToken, IProgress<Tuple<int, int>> progress)
        {
            return Task.Run(async () =>
            {
                if (selectedNode == null || selectedNode.Tag == null || selectedNode.Tag.ToString() == "NETWORK_NODE") return;

                uint deviceId = uint.Parse(selectedNode.Name);
                var deviceInfo = selectedNode.Tag as Dictionary<string, object>;
                if (deviceInfo == null)
                {
                    Log($"Error: deviceInfo is null for device {deviceId}");
                    return;
                }
                BacnetAddress deviceAddress = deviceInfo["Address"] as BacnetAddress;

                this.Invoke((MethodInvoker)delegate {
                    ObjectTreeView.Nodes.Clear();
                    DeviceTreeView.Enabled = false;
                    ObjectTreeView.Enabled = false;
                });

                try
                {
                    var objectList = await Task.Run(() =>
                    {
                        try
                        {
                            var segmentation = (BacnetSegmentations)deviceInfo["Segmentation"];
                            var old_segments = _bacnetClient.MaxSegments;
                            if (segmentation == BacnetSegmentations.SEGMENTATION_NONE)
                            {
                                _bacnetClient.MaxSegments = BacnetMaxSegments.MAX_SEG0;
                            }

                            try
                            {
                                Log($"Requesting object list for Device {deviceId}...");
                                var oList = new List<BacnetValue>();
                                var deviceOid = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);

                                if (_bacnetClient.ReadPropertyRequest(deviceAddress, deviceOid, BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> countValue, array_index: 0))
                                {
                                    uint count = (uint)countValue[0].Value;
                                    Log($"Device {deviceId} has {count} objects. Reading them individually.");
                                    for (uint i = 1; i <= count; i++)
                                    {
                                        if (cancelToken.IsCancellationRequested)
                                        {
                                            cancelToken.ThrowIfCancellationRequested();
                                        }

                                        if (_bacnetClient.ReadPropertyRequest(deviceAddress, deviceOid, BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objIdValue, array_index: i))
                                        {
                                            oList.Add(objIdValue[0]);
                                            progress.Report(new Tuple<int, int>((int)i, (int)count));
                                        }
                                        else
                                        {
                                            Log($"--- WARNING: Failed to read object at index {i} from device {deviceId}. ---");
                                        }
                                    }
                                }
                                else
                                {
                                    Log($"--- ERROR: Failed to read object list size for device {deviceId}. ---");
                                }
                                return oList;
                            }
                            finally
                            {
                                _bacnetClient.MaxSegments = old_segments;
                            }
                        }
                        catch (Exception ex) when (!(ex is OperationCanceledException))
                        {
                            Log($"--- ERROR reading object list for device {deviceId}: {ex.Message} ---");
                            return null;
                        }
                    }, cancelToken);

                    if (objectList != null && objectList.Any())
                    {
                        Log($"--- SUCCESS: Found {objectList.Count} objects. ---");
                        if (!this.IsDisposed && this.IsHandleCreated)
                        {
                            await PopulateObjectTree(objectList);
                        }
                    }
                    else if (objectList != null)
                    {
                        Log($"--- ERROR: Failed to read any objects for device {deviceId}. ---");
                    }
                }
                finally
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            DeviceTreeView.Enabled = true;
                            ObjectTreeView.Enabled = true;
                        });
                    }
                }
            });
        }
    }
}

