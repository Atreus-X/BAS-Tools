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
using MainApp;
using static System.IO.BACnet.Serialize.ASN1;
using static System.IO.BACnet.Serialize.Services;

namespace MainApp.Configuration
{
    public partial class BACnet_MSTP_Remote : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private readonly HistoryManager _historyManager;
        private uint? _lastPingedDeviceId;
        private readonly System.Windows.Forms.Timer _discoveryTimer;
        private string _lastBBMD_IP = "";
        private readonly object _bacnetLock = new object();
        private CancellationTokenSource _cancellationTokenSource = null;
        private bool _isClientStarted = false;
        private readonly System.Windows.Forms.Timer _propertyPollingTimer;
        private BacnetObjectId _selectedObjectId;

        public BACnet_MSTP_Remote()
        {
            InitializeComponent();
            _propertyPollingTimer = new System.Windows.Forms.Timer();
            this.Load += BACnet_MSTP_Remote_Load;
            _historyManager = new HistoryManager("BACnet_MSTP_Remote_");
            _discoveryTimer = new System.Windows.Forms.Timer { Interval = 10000 };
        }

        private void BACnet_MSTP_Remote_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            GlobalLogger.Register(outputTextBox);
            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();
            _discoveryTimer.Tick += DiscoveryTimer_Tick;
            _propertyPollingTimer.Tick += PropertyPollingTimer_Tick;
            anyNetworkRadioButton.Checked = true;
            NetworkFilter_CheckedChanged(null, null);
        }

        private void WireUpEventHandlers()
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
            manualReadWriteButton.Click += ManualReadWriteButton_Click;
            clearLogButton.Click += ClearLogButton_Click;
            cancelActionButton.Click += CancelActionButton_Click;
            deviceTreeView.AfterSelect += DeviceTreeView_AfterSelect;
            objectTreeView.AfterSelect += ObjectTreeView_AfterSelect;
            propertiesDataGridView.CellEndEdit += propertiesDataGridView_CellEndEdit;
            togglePollingButton.Click += TogglePollingButton_Click;
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
            Log($"OnIam from {adr} for device {deviceId}. Segmentation support: {_segmentation}");
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    ushort deviceNetwork = (adr.RoutedSource != null) ? adr.RoutedSource.net : adr.net;
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
                        var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "VendorName", vendorName }, { "MAC", macAddress }, { "Segmentation", _segmentation } };
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
                            deviceNode.Text = $"{finalDeviceName} ({deviceInfo["MAC"]}) ({deviceId}) ({deviceInfo["VendorName"]}){errorText ?? ""}";
                        });
                    }
                }
            });
        }
        private void DeviceTreeView_AfterSelect(object _sender, TreeViewEventArgs e)
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
            LoadDeviceDetails(e.Node);
        }

        private void DiscoverObjectsButton_Click(object _sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode != null && deviceTreeView.SelectedNode.Tag != null && deviceTreeView.SelectedNode.Tag.ToString() != "NETWORK_NODE")
            {
                LoadDeviceDetails(deviceTreeView.SelectedNode);
            }
            else
            {
                MessageBox.Show("Please select a device from the list first.", "Device Not Selected");
            }
        }

        private void LoadDeviceDetails(TreeNode selectedNode)
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

            BacnetSegmentations segmentation = BacnetSegmentations.SEGMENTATION_BOTH; // Default to supported
            if (deviceInfo.ContainsKey("Segmentation"))
            {
                segmentation = (BacnetSegmentations)deviceInfo["Segmentation"];
            }
            else
            {
                Log($"Warning: Segmentation support not found for device {deviceId}. Assuming none.");
                segmentation = BacnetSegmentations.SEGMENTATION_NONE;
            }
            Log($"Device {deviceId} segmentation support: {segmentation}");

            var old_segments = _bacnetClient.MaxSegments;
            if (segmentation == BacnetSegmentations.SEGMENTATION_NONE)
            {
                _bacnetClient.MaxSegments = BacnetMaxSegments.MAX_SEG0;
            }
            Log($"BacnetClient.MaxSegments set to: {_bacnetClient.MaxSegments}");


            this.Invoke((MethodInvoker)delegate {
                objectTreeView.Nodes.Clear();
                objectDiscoveryProgressBar.Visible = true;
                objectCountLabel.Visible = true;
                objectCountLabel.Text = "Reading details...";
                objectDiscoveryProgressBar.Value = 0;
            });

            Task.Run(() =>
            {
                try
                {
                    lock (_bacnetLock)
                    {
                        Log($"Requesting object list for Device {deviceId}...");
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
                                this.Invoke((MethodInvoker)delegate { PopulateObjectTree(values); });
                            }
                        }
                        else
                        {
                            Log("Device does not support ReadPropertyMultiple. Using ReadProperty for object list.");
                            if (_bacnetClient.ReadPropertyRequest(deviceAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objectList))
                            {
                                Log($"--- SUCCESS: Found {objectList.Count} objects. ---");
                                this.Invoke((MethodInvoker)delegate { PopulateObjectTree(objectList); });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"--- ERROR reading object list for device {deviceId}: {ex.Message} ---");
                }
                finally
                {
                    // Restore the original segmentation setting
                    _bacnetClient.MaxSegments = old_segments;

                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            objectDiscoveryProgressBar.Visible = false;
                            objectCountLabel.Visible = false;
                        });
                    }
                }
            });
        }

        private async void ObjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (rediscoverObjectCheckBox.Checked && e.Node != null && e.Node.Tag is BacnetObjectId objectId && _lastPingedDeviceId.HasValue)
            {
                uint deviceId = _lastPingedDeviceId.Value;
                Log($"Rediscovering object {objectId.type}:{objectId.instance} on Device {deviceId}");
                try
                {
                    BacnetAddress deviceAddress = await FindDeviceAddressAsync(deviceId);
                    if (deviceAddress == null) return;

                    await Task.Run(() =>
                    {
                        lock (_bacnetLock)
                        {
                            if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, BacnetPropertyIds.PROP_ALL, out IList<BacnetValue> values))
                            {
                                Log($"--- SUCCESS: Read all properties for {objectId.type}:{objectId.instance} ---");
                                foreach (var val in values) Log($"  - {val}");
                            }
                            else
                            {
                                Log($"--- ERROR: Failed to read properties for {objectId.type}:{objectId.instance}. ---");
                            }
                        }
                    });
                }
                catch (Exception ex) { Log($"--- Read Error: {ex.Message} ---"); }
            }
            else
            {
                _propertyPollingTimer.Stop();
                togglePollingButton.Text = "Start Polling";
                propertiesDataGridView.Rows.Clear();

                if (e.Node?.Tag is BacnetObjectId bacnetObjectId)
                {
                    _selectedObjectId = bacnetObjectId;
                    togglePollingButton.Enabled = true;
                    Log($"Selected object: {bacnetObjectId}");
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
        }

        private void NetworkFilter_CheckedChanged(object _sender, EventArgs _e)
        {
            networkNumberComboBox.Visible = listNetworkRadioButton.Checked;
        }

        private void ClearLogButton_Click(object _sender, EventArgs _e)
        {
            outputTextBox.Clear();
            Log("Log cleared.");
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
                List<ushort> networksToScan = ParseNetworkNumbers(networkNumberComboBox.Text);
                if (networksToScan.Any())
                {
                    foreach (var netNum in networksToScan)
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

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool deviceSelected = _lastPingedDeviceId.HasValue;
            discoverObjectsButton.Enabled = deviceSelected;
        }

        private async Task<BacnetAddress> FindDeviceAddressAsync(uint deviceId)
        {
            foreach (TreeNode networkNode in deviceTreeView.Nodes)
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

        private void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            objectTreeView.Nodes.Clear();
            if (objectList == null) return;

            objectDiscoveryProgressBar.Value = 0;
            objectDiscoveryProgressBar.Maximum = objectList.Count;
            objectCountLabel.Text = $"Found 0 of {objectList.Count}";

            var objectGroups = objectList
                .Where(v => v.Value is BacnetObjectId)
                .Select(val => (BacnetObjectId)val.Value)
                .GroupBy(objId => objId.type)
                .OrderBy(g => g.Key.ToString());

            int count = 0;
            objectTreeView.BeginUpdate();
            try
            {
                foreach (var group in objectGroups)
                {
                    var parentNode = new TreeNode(group.Key.ToString());
                    objectTreeView.Nodes.Add(parentNode);
                    foreach (var objId in group.OrderBy(o => o.instance))
                    {
                        var childNode = new TreeNode(objId.instance.ToString()) { Tag = objId };
                        parentNode.Nodes.Add(childNode);
                        count++;
                    }
                    this.Invoke((MethodInvoker)delegate {
                        objectDiscoveryProgressBar.Value = count;
                        objectCountLabel.Text = $"Found {count} of {objectList.Count}";
                    });
                }
            }
            finally
            {
                objectTreeView.EndUpdate();
                objectDiscoveryProgressBar.Value = count;
                objectCountLabel.Text = $"Found {count} of {objectList.Count}";
            }
        }

        private void LoadHistory()
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

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null || comboBox.Name == "localInterfaceComboBox") return;
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
            bbmdIpComboBox.Items.Clear();
            networkNumberComboBox.Items.Clear();
            apduTimeoutComboBox.Items.Clear();
            bbmdPortComboBox.Items.Clear();
            bbmdTtlComboBox.Items.Clear();
            PopulateDefaultValues();
            LoadHistory();
            Log("BACnet MS/TP Remote history cleared.");
        }

        public void Shutdown()
        {
            _cancellationTokenSource?.Cancel();
            _propertyPollingTimer?.Stop();
            _propertyPollingTimer?.Dispose();
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
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