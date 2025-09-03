using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.IO.BACnet.Serialize.ASN1;

namespace MainApp.Configuration
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

        // Abstract properties for UI controls that derived classes must provide.
        protected abstract TreeView DeviceTreeView { get; }
        protected abstract TreeView ObjectTreeView { get; }
        protected abstract DataGridView PropertiesDataGridView { get; }
        protected abstract RichTextBox OutputTextBox { get; }
        protected abstract Button TogglePollingButton { get; }
        protected abstract NumericUpDown ReadIntervalNumericUpDown { get; }
        protected abstract ComboBox WritePriorityComboBox { get; }

        public BACnetControlBase()
        {
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

            _propertyPollingTimer.Tick += PropertyPollingTimer_Tick;
        }

        private void WireUpCommonEventHandlers()
        {
            DeviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            ObjectTreeView.AfterSelect += ObjectTreeView_AfterSelect;
            PropertiesDataGridView.CellEndEdit += propertiesDataGridView_CellEndEdit;
            TogglePollingButton.Click += TogglePollingButton_Click;
        }

        // Abstract methods for protocol-specific implementation
        protected abstract void PopulateDefaultValues();
        protected abstract void LoadHistory();
        protected abstract void UpdateAllStates(object sender, EventArgs e);
        protected abstract void WireUpProtocolSpecificEventHandlers();
        public abstract void ClearHistory();

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

        protected void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            ObjectTreeView.Nodes.Clear();
            if (objectList == null) return;

            var objectGroups = objectList
                .Where(v => v.Value is BacnetObjectId)
                .Select(val => (BacnetObjectId)val.Value)
                .GroupBy(objId => objId.type)
                .OrderBy(g => g.Key.ToString());

            ObjectTreeView.BeginUpdate();
            try
            {
                foreach (var group in objectGroups)
                {
                    var parentNode = new TreeNode(group.Key.ToString());
                    ObjectTreeView.Nodes.Add(parentNode);
                    foreach (var objId in group.OrderBy(o => o.instance))
                    {
                        var childNode = new TreeNode(objId.instance.ToString()) { Tag = objId };
                        parentNode.Nodes.Add(childNode);
                    }
                }
            }
            finally
            {
                ObjectTreeView.EndUpdate();
            }
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

                if (_bacnetClient.ReadPropertyRequest(adr, _selectedObjectId, BacnetPropertyIds.PROP_ALL, out IList<BacnetValue> values))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        PropertiesDataGridView.Rows.Clear();
                        var propValues = values.Select(v => v.Value).OfType<BacnetPropertyValue>();
                        foreach (var prop in propValues)
                        {
                            string propName = ((BacnetPropertyIds)prop.property.propertyIdentifier).ToString();
                            string propValue = prop.value.FirstOrDefault().Value?.ToString() ?? "{empty}";
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
    }
}