using System;
using System.Collections.Generic;
using System.IO.BACnet;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class BACnet_MSTP : UserControl, IHistorySupport
    {
        private BacnetClient _bacnetClient;
        private Thread _bacnetThread;
        private readonly HistoryManager _historyManager;

        private uint? _lastPingedDeviceId = null;
        private bool _isClientStarted = false;

        public BACnet_MSTP()
        {
            InitializeComponent();
            _historyManager = new HistoryManager("BACnet_MSTP_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateAllStates(null, null);

            // Wire up event handlers for history saving
            serialPortComboBox.Leave += (sender, e) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            baudRateComboBox.Leave += (sender, e) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            dataBitsComboBox.Leave += (sender, e) => SaveComboBoxEntry(dataBitsComboBox, "dataBits");
            maxMastersComboBox.Leave += (sender, e) => SaveComboBoxEntry(maxMastersComboBox, "maxMasters");
            maxInfoFramesComboBox.Leave += (sender, e) => SaveComboBoxEntry(maxInfoFramesComboBox, "maxInfoFrames");
            instanceNumberComboBox.Leave += (sender, e) => SaveComboBoxEntry(instanceNumberComboBox, "instanceNumber");
        }

        // --- BACnet Logic ---
        private void EnsureBacnetClientStarted()
        {
            if (_isClientStarted && _bacnetClient != null)
            {
                return;
            }

            if (_bacnetClient != null)
            {
                _bacnetClient.Dispose();
                _bacnetClient = null;
                Thread.Sleep(200);
            }

            try
            {
                Log("Initializing BACnet MS/TP client...");

                var portName = serialPortComboBox.Text;
                var baudRate = int.Parse(baudRateComboBox.Text);
                var dataBits = int.Parse(dataBitsComboBox.Text); // Kept for history, but not passed to transport directly
                var maxMasters = byte.Parse(maxMastersComboBox.Text);
                var maxInfoFrames = byte.Parse(maxInfoFramesComboBox.Text);
                byte nodeAddress = 0; // Default MSTP node address for this tool

                // Save settings to history
                _historyManager.AddEntry("serialPort", portName);
                _historyManager.AddEntry("baudRate", baudRate.ToString());
                _historyManager.AddEntry("dataBits", dataBits.ToString());
                _historyManager.AddEntry("maxMasters", maxMasters.ToString());
                _historyManager.AddEntry("maxInfoFrames", maxInfoFrames.ToString());


                // Corrected constructor call for BacnetMstpProtocolTransport
                // Assuming a constructor that takes portName, baudRate, nodeAddress, maxMasters, maxInfoFrames
                // DataBits, Parity, StopBits are often handled internally by the MSTP transport
                var transport = new BacnetMstpProtocolTransport(portName, baudRate, nodeAddress, maxMasters, maxInfoFrames);
                _bacnetClient = new BacnetClient(transport);

                _bacnetClient.OnIam += OnIamHandler;
                _bacnetClient.OnWhoIs += OnWhoIsHandler; // Add WhoIs handler for MSTP

                _bacnetThread = new Thread(() =>
                {
                    try { _bacnetClient.Start(); }
                    catch (Exception ex) { Log($"--- BACnet MSTP Thread Error: {ex.Message} ---"); }
                })
                {
                    IsBackground = true
                };
                _bacnetThread.Start();
                Thread.Sleep(100);

                _isClientStarted = true;
                Log("BACnet MS/TP client started.");
            }
            catch (Exception ex)
            {
                _isClientStarted = false;
                Log($"--- ERROR initializing BACnet MS/TP client: {ex.Message} ---");
                MessageBox.Show($"Error during BACnet MS/TP initialization: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnIamHandler(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Log($"I-Am received from {adr} (DeviceID: {deviceId})");
                string deviceDisplay = $"{deviceId} ({adr})";

                if (!deviceTreeView.Nodes.ContainsKey(deviceId.ToString()))
                {
                    var node = new TreeNode(deviceDisplay) { Name = deviceId.ToString(), Tag = adr };
                    deviceTreeView.Nodes.Add(node);
                }
            });
        }

        private void OnWhoIsHandler(BacnetClient sender, BacnetAddress adr, int lowLimit, int highLimit)
        {
            // For MSTP, we typically expect I-Am responses after a Who-Is,
            // but this handler can be used for logging Who-Is requests if needed.
            this.Invoke((MethodInvoker)delegate
            {
                Log($"Who-Is received from {adr} (Limits: {lowLimit}-{highLimit})");
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
            Log("Discovering devices with global Who-Is broadcast...");
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

                // Placeholder: In a real scenario, this would come from objectTreeView selection
                var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                var propertyId = BacnetPropertyIds.PROP_OBJECT_NAME; // Example property

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
            // Populate Serial Ports
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            if (serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;

            // Populate Baud Rates
            baudRateComboBox.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            baudRateComboBox.Text = "38400"; // Common MSTP baud rate

            // Populate Data Bits
            dataBitsComboBox.Items.AddRange(new object[] { "7", "8" });
            dataBitsComboBox.Text = "8";

            // Populate Max Masters
            maxMastersComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127" });
            maxMastersComboBox.Text = "127";

            // Populate Max Info Frames
            maxInfoFramesComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            maxInfoFramesComboBox.Text = "1";
        }

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            pingButton.Enabled = instanceExists;
            readPropertyButton.Enabled = instanceExists;
            writePropertyButton.Enabled = instanceExists;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue; // Only enable if a device has been pinged
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
            PopulateComboBoxWithHistory(dataBitsComboBox, "dataBits");
            PopulateComboBoxWithHistory(maxMastersComboBox, "maxMasters");
            PopulateComboBoxWithHistory(maxInfoFramesComboBox, "maxInfoFrames");
            PopulateComboBoxWithHistory(instanceNumberComboBox, "instanceNumber");

            // Set default values if history is empty or not present
            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "38400";
            if (string.IsNullOrEmpty(dataBitsComboBox.Text)) dataBitsComboBox.Text = "8";
            if (string.IsNullOrEmpty(maxMastersComboBox.Text)) maxMastersComboBox.Text = "127";
            if (string.IsNullOrEmpty(maxInfoFramesComboBox.Text)) maxInfoFramesComboBox.Text = "1";
            if (string.IsNullOrEmpty(instanceNumberComboBox.Text)) instanceNumberComboBox.Text = "100";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            // For serialPortComboBox, system-detected ports are already added in PopulateDefaultValues.
            // We need to preserve them and add history if they are not duplicates.
            if (comboBox.Name != serialPortComboBox.Name)
            {
                comboBox.Items.Clear(); // Clear existing items for other comboboxes
            }

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                if (comboBox.Name == serialPortComboBox.Name)
                {
                    // Add history items, but ensure no duplicates with system items
                    foreach (var item in historyList.Where(h => !comboBox.Items.Contains(h)))
                    {
                        comboBox.Items.Insert(0, item); // Add to top
                    }
                }
                else
                {
                    comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                }
                comboBox.Text = historyList.First(); // Set to the most recent value
            }
            else if (comboBox.Name == serialPortComboBox.Name && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0; // Select the first available serial port
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (!string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text; // Restore the current text after repopulating
            }
        }

        public void ClearHistory()
        {
            _historyManager.ClearHistory();
            serialPortComboBox.Items.Clear();
            baudRateComboBox.Items.Clear();
            dataBitsComboBox.Items.Clear();
            maxMastersComboBox.Items.Clear();
            maxInfoFramesComboBox.Items.Clear();
            instanceNumberComboBox.Items.Clear();

            // Re-populate system serial ports
            PopulateDefaultValues();

            Log("BACnet MS/TP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager.SaveHistory();
            _bacnetClient?.Dispose();
        }
    }
}
