using MainApp;
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
using static System.IO.BACnet.Serialize.Services;

namespace MainApp.Configuration
{
    public partial class BACnet_IP : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private HistoryManager _historyManager;
        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;
        private readonly object _bacnetLock = new object();

        public BACnet_IP()
        {
            InitializeComponent();
            this.Load += BACnet_IP_Load;
        }

        private void BACnet_IP_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            GlobalLogger.Register(outputTextBox);
            _historyManager = new HistoryManager("BACnet_IP_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);
            WireUpEventHandlers();
        }

        private void WireUpEventHandlers()
        {
            this.deviceTreeView.AfterSelect += this.DeviceTreeView_AfterSelect;
            this.instanceNumberComboBox.TextChanged += this.UpdateAllStates;
            this.discoverButton.Click += this.DiscoverButton_Click;
            this.pingButton.Click += this.PingButton_Click;
            this.discoverObjectsButton.Click += this.DiscoverObjectsButton_Click;
            this.readPropertyButton.Click += this.ReadPropertyButton_Click;
            this.clearLogButton.Click += this.ClearLogButton_Click;
            this.networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(networkNumberComboBox, "networkNumber");
            ipAddressComboBox.Leave += (s, args) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            ipPortComboBox.Leave += (s, args) => SaveComboBoxEntry(ipPortComboBox, "ipPort");
            apduTimeoutComboBox.Leave += (s, args) => SaveComboBoxEntry(apduTimeoutComboBox, "apduTimeout");
            bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdIpComboBox, "bbmdIp");
            bbmdTtlComboBox.Leave += (s, args) => SaveComboBoxEntry(bbmdTtlComboBox, "bbmdTtl");

            expandAllButton.Click += (s, e) => deviceTreeView.ExpandAll();
            collapseAllButton.Click += (s, e) => deviceTreeView.CollapseAll();
            clearBrowserButton.Click += (s, e) => {
                deviceTreeView.Nodes.Clear();
                objectTreeView.Nodes.Clear();
            };
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

                if (!string.IsNullOrWhiteSpace(bbmdIpComboBox.Text))
                {
                    int bbmdPort = int.Parse(ipPortComboBox.Text);
                    if (!short.TryParse(bbmdTtlComboBox.Text, out short ttl))
                    {
                        Log("--- ERROR: Invalid BBMD TTL value. ---");
                        return;
                    }
                    Log($"Attempting to register as Foreign Device with BBMD at {bbmdIpComboBox.Text}:{bbmdPort} with TTL {ttl}...");
                    _bacnetClient.RegisterAsForeignDevice(bbmdIpComboBox.Text, ttl, bbmdPort);
                    Log("Foreign Device Registration message sent.");
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

        private void OnIamHandler(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
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
                        var deviceInfo = new Dictionary<string, object> { { "Address", adr }, { "VendorName", vendorName }, { "MAC", macAddress } };
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
                            deviceNode.Text = $"{finalDeviceName} ({deviceInfo["MAC"]}) ({deviceId}) ({deviceInfo["VendorName"]}){errorText ?? ""}";
                        });
                    }
                }
            });
        }
        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;
            deviceTreeView.Nodes.Clear();
            Log("Sending Who-Is broadcast...");

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

            string bbmdIp = bbmdIpComboBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(bbmdIp))
            {
                int bbmdPort = int.Parse(ipPortComboBox.Text);
                ushort.TryParse(networkNumberComboBox.Text, out ushort net);
                _bacnetClient.RemoteWhoIs(bbmdIp, bbmdPort, (int)deviceId, (int)deviceId, net == 0 ? (ushort)0xFFFF : net);
            }
            else
            {
                _bacnetClient.WhoIs(lowLimit: (int)deviceId, highLimit: (int)deviceId);
            }
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
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
            LoadDeviceDetails(e.Node);
        }

        private void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (deviceTreeView.SelectedNode != null)
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
            if (deviceInfo == null) return;
            BacnetAddress deviceAddress = deviceInfo["Address"] as BacnetAddress;

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
                        if (_bacnetClient.ReadPropertyRequest(deviceAddress, new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId), BacnetPropertyIds.PROP_OBJECT_LIST, out IList<BacnetValue> objectList))
                        {
                            Log($"--- SUCCESS: Found {objectList.Count} objects. ---");
                            this.Invoke((MethodInvoker)delegate { PopulateObjectTree(objectList); });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"--- ERROR reading object list for device {deviceId}: {ex.Message} ---");
                }
                finally
                {
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
                await Task.Run(() => {
                    lock (_bacnetLock)
                    {
                        if (_bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId, out IList<BacnetValue> values))
                        {
                            Log($"--- SUCCESS: Read ACK for {propertyId} ---");
                            foreach (var val in values) Log($"  Value: {val.Value}");
                        }
                        else
                        {
                            Log($"--- ERROR: Failed to read property {propertyId}. ---");
                        }
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

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            pingButton.Enabled = instanceExists;
            readPropertyButton.Enabled = instanceExists;
            writePropertyButton.Enabled = instanceExists;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        private void PopulateObjectTree(IList<BacnetValue> objectList)
        {
            objectTreeView.Nodes.Clear();
            if (objectList == null) return;

            objectDiscoveryProgressBar.Value = 0;
            objectDiscoveryProgressBar.Maximum = objectList.Count;
            objectCountLabel.Text = $"Found 0 of {objectList.Count}";

            var objectGroups = objectList
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

        private void LoadHistory()
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

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null || comboBox.Name == "interfaceComboBox") return;
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
            if (_historyManager != null && comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text) && comboBox.Name != "interfaceComboBox")
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

        public void Shutdown()
        {
            _historyManager?.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}