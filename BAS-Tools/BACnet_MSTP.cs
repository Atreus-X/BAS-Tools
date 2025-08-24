using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Net;
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

        // New UI Controls for connection mode selection
        private RadioButton _localModeRadioButton;
        private RadioButton _remoteModeRadioButton;
        private Panel _localPanel;
        private Panel _remotePanel;
        private ComboBox _bbmdIpComboBox;
        private ComboBox _networkNumberComboBox;
        private ComboBox _localInterfaceComboBox;

        public BACnet_MSTP()
        {
            InitializeComponent();
            // Defer initialization to the Load event to make it designer-safe
            this.Load += new System.EventHandler(this.BACnet_MSTP_Load);
        }

        private void BACnet_MSTP_Load(object sender, EventArgs e)
        {
            // This is the most robust way to prevent code from running in the Visual Studio Designer.
            bool inDesignMode = this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            if (inDesignMode)
            {
                return;
            }

            _historyManager = new HistoryManager("BACnet_MSTP_");

            // Reconfigure the UI for connection mode selection
            SetupUI();

            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);

            // Wire up event handlers for history saving, with null checks for safety
            if (serialPortComboBox != null) serialPortComboBox.Leave += (s, args) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            if (baudRateComboBox != null) baudRateComboBox.Leave += (s, args) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            if (maxMastersComboBox != null) maxMastersComboBox.Leave += (s, args) => SaveComboBoxEntry(maxMastersComboBox, "maxMasters");
            if (maxInfoFramesComboBox != null) maxInfoFramesComboBox.Leave += (s, args) => SaveComboBoxEntry(maxInfoFramesComboBox, "maxInfoFrames");
            if (instanceNumberComboBox != null) instanceNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
            if (_bbmdIpComboBox != null) _bbmdIpComboBox.Leave += (s, args) => SaveComboBoxEntry(_bbmdIpComboBox, "bbmdIp");
            if (_networkNumberComboBox != null) _networkNumberComboBox.Leave += (s, args) => SaveComboBoxEntry(_networkNumberComboBox, "networkNumber");
            if (_localInterfaceComboBox != null) _localInterfaceComboBox.Leave += (s, args) => SaveComboBoxEntry(_localInterfaceComboBox, "localInterface");
        }

        private void SetupUI()
        {
            // Find the existing configuration GroupBox by its type and position in the control hierarchy.
            GroupBox configFrame = null;
            if (this.Controls.Count > 0 && this.Controls[0] is Panel mainPanel)
            {
                configFrame = mainPanel.Controls.OfType<GroupBox>().FirstOrDefault();
            }

            // More robustly find the TableLayoutPanel within the GroupBox.
            if (configFrame != null && configFrame.Controls.OfType<TableLayoutPanel>().FirstOrDefault() is TableLayoutPanel existingLayout)
            {
                configFrame.Text = "BACnet MS/TP Configuration";
                configFrame.Height = 200;

                existingLayout.Visible = false; // Hide the old layout panel

                // Create panels for local and remote settings
                _localPanel = new Panel { Dock = DockStyle.Fill };
                _remotePanel = new Panel { Dock = DockStyle.Fill, Visible = false };

                // Move existing controls to the local panel
                while (existingLayout.Controls.Count > 0)
                {
                    _localPanel.Controls.Add(existingLayout.Controls[0]);
                }
                _localPanel.Controls.Add(existingLayout);
                existingLayout.Visible = true;


                // Create controls for remote (BBMD) settings
                _localInterfaceComboBox = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Name = "_localInterfaceComboBox" };
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

                // Create Radio buttons for mode selection
                _localModeRadioButton = new RadioButton { Text = "Local (COM Port)", Checked = true, AutoSize = true, Location = new System.Drawing.Point(15, 25) };
                _remoteModeRadioButton = new RadioButton { Text = "Remote (BBMD)", AutoSize = true, Location = new System.Drawing.Point(160, 25) };
                _localModeRadioButton.CheckedChanged += ModeRadioButton_CheckedChanged;

                // Add new controls to the main config frame
                configFrame.Controls.Add(_localModeRadioButton);
                configFrame.Controls.Add(_remoteModeRadioButton);

                var settingsPanel = new Panel { Location = new System.Drawing.Point(10, 50), Size = new System.Drawing.Size(configFrame.Width - 20, 140), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
                settingsPanel.Controls.Add(_localPanel);
                settingsPanel.Controls.Add(_remotePanel);
                configFrame.Controls.Add(settingsPanel);
            }
        }

        private void ModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_localPanel != null) _localPanel.Visible = _localModeRadioButton.Checked;
            if (_remotePanel != null) _remotePanel.Visible = !_localModeRadioButton.Checked;
        }

        // --- BACnet Logic ---
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

                if (!_localModeRadioButton.Checked && !string.IsNullOrWhiteSpace(_bbmdIpComboBox.Text))
                {
                    _bacnetClient.RegisterAsForeignDevice(_bbmdIpComboBox.Text, 60);
                    Log($"Registered as Foreign Device with BBMD at {_bbmdIpComboBox.Text}");
                }

                _bacnetClient.OnIam += OnIamHandler;

                // The BACnet client's Start() method is blocking and must be run on a background thread.
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
            this.Invoke((MethodInvoker)delegate {
                Log($"I-Am received from {adr} (DeviceID: {deviceId})");
                string deviceDisplay = $"{deviceId} ({adr})";

                if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                {
                    var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = adr };
                    deviceTreeView.Nodes.Add(node);
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

        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            EnsureBacnetClientStarted();
            if (!_isClientStarted) return;

            deviceTreeView.Nodes.Clear();
            Log("Discovering devices with Who-Is broadcast...");

            // In both local and remote (BBMD) modes, a general Who-Is is sufficient.
            // The BBMD is responsible for forwarding the broadcast to the remote networks.
            _bacnetClient.WhoIs();
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
            // Add null checks to prevent crashes if controls are not initialized.
            // This is a defensive measure against potential designer/runtime inconsistencies.

            // Local defaults
            if (serialPortComboBox != null)
            {
                serialPortComboBox.Items.Clear();
                serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
                if (serialPortComboBox.Items.Count > 0)
                    serialPortComboBox.SelectedIndex = 0;
            }

            if (baudRateComboBox != null)
            {
                baudRateComboBox.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            }

            if (maxMastersComboBox != null)
            {
                maxMastersComboBox.Items.AddRange(Enumerable.Range(1, 127).Select(i => i.ToString()).ToArray());
            }

            if (maxInfoFramesComboBox != null)
            {
                maxInfoFramesComboBox.Items.AddRange(Enumerable.Range(1, 10).Select(i => i.ToString()).ToArray());
            }

            // Remote defaults
            if (_localInterfaceComboBox != null)
            {
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
        }

        private void UpdateAllStates(object sender, EventArgs e)
        {
            if (instanceNumberComboBox == null) return;
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
                return adr; // Return the cached address
            }

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            var tcs = new TaskCompletionSource<BacnetAddress>();

            void handler(BacnetClient sender, BacnetAddress address, uint receivedDeviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
            {
                if (receivedDeviceId == deviceId)
                {
                    tcs.TrySetResult(address);
                }
            }

            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);

            var timeoutTask = Task.Delay(2000);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            _bacnetClient.OnIam -= handler; // Always clean up the handler

            if (completedTask == tcs.Task)
            {
                return await tcs.Task; // Success
            }
            else
            {
                Log($"--- Timeout: No I-Am response from Device {deviceId}. ---");
                return null; // Failure (timeout)
            }
        }

        // --- History Management ---
        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(serialPortComboBox, "serialPort");
            PopulateComboBoxWithHistory(baudRateComboBox, "baudRate");
            PopulateComboBoxWithHistory(maxMastersComboBox, "maxMasters");
            PopulateComboBoxWithHistory(maxInfoFramesComboBox, "maxInfoFrames");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");
            PopulateComboBoxWithHistory(_bbmdIpComboBox, "bbmdIp");
            PopulateComboBoxWithHistory(_networkNumberComboBox, "networkNumber");
            PopulateComboBoxWithHistory(_localInterfaceComboBox, "localInterface");

            // Set default values if history is empty or not present
            if (serialPortComboBox != null && string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (baudRateComboBox != null && string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (maxMastersComboBox != null && string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (maxInfoFramesComboBox != null && string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
            if (instanceNumberComboBox != null && string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
            if (_networkNumberComboBox != null && string.IsNullOrEmpty(_networkNumberComboBox.Text)) _networkNumberComboBox.Text = "1";
            if (_localInterfaceComboBox != null && string.IsNullOrEmpty(_localInterfaceComboBox.Text) && _localInterfaceComboBox.Items.Count > 0)
                _localInterfaceComboBox.SelectedIndex = 0;
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox == null) return; // Guard against null controls

            // Determine if the ComboBox is one of the special ones that preserves system-populated items.
            bool isSpecialComboBox = (serialPortComboBox != null && comboBox == serialPortComboBox) ||
                                     (_localInterfaceComboBox != null && comboBox == _localInterfaceComboBox);

            if (!isSpecialComboBox)
            {
                comboBox.Items.Clear();
            }

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                if (isSpecialComboBox)
                {
                    // For special combo boxes, add history items without duplicating existing system items.
                    foreach (var item in historyList.Where(h => !comboBox.Items.Contains(h)))
                    {
                        comboBox.Items.Insert(0, item);
                    }
                }
                else
                {
                    comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                }
                comboBox.Text = historyList.First();
            }
            else if (isSpecialComboBox && comboBox.Items.Count > 0)
            {
                // Default to the first item if no history exists for special combo boxes.
                comboBox.SelectedIndex = 0;
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (comboBox != null && !string.IsNullOrWhiteSpace(comboBox.Text))
            {
                if (_historyManager != null)
                {
                    _historyManager.AddEntry(key, comboBox.Text);
                    PopulateComboBoxWithHistory(comboBox, key);
                    comboBox.Text = comboBox.Text;
                }
            }
        }

        public void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();

            // Clear all combo box items and text boxes using null-conditional operator for safety
            if (serialPortComboBox != null) serialPortComboBox.Items.Clear();
            if (baudRateComboBox != null) baudRateComboBox.Items.Clear();
            if (maxMastersComboBox != null) maxMastersComboBox.Items.Clear();
            if (maxInfoFramesComboBox != null) maxInfoFramesComboBox.Items.Clear();
            if (instanceNumberComboBox != null) instanceNumberComboBox.Items.Clear();
            if (_bbmdIpComboBox != null) _bbmdIpComboBox.Items.Clear();
            if (_networkNumberComboBox != null) _networkNumberComboBox.Items.Clear();
            if (_localInterfaceComboBox != null) _localInterfaceComboBox.Items.Clear();

            PopulateDefaultValues();
            LoadHistory(); // Reload defaults
            Log("BACnet MS/TP history cleared.");
        }

        public void Shutdown()
        {
            if (_historyManager != null) _historyManager.SaveHistory();
            if (_bacnetClient != null) _bacnetClient.Dispose();
        }
    }
}
