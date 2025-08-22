using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.BACnet;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BacnetToolsCSharp
{
    public partial class BACnet_IP : UserControl
    {
        // BACnet communication client
        private BacnetClient _bacnetClient;
        private Thread _bacnetThread;

        // Data storage
        private uint? _lastPingedDeviceId = null;

        public BACnet_IP()
        {
            InitializeComponent();
            PopulateDefaultValues();
            UpdateAllStates(null, null);
        }

        // --- BACnet Logic ---

        private void StartBacnetClient()
        {
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
                if (interfaceComboBox.SelectedIndex > 0)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(interfaceComboBox.Text, @"\((.*?)\)");
                    if (match.Success)
                    {
                        localIp = match.Groups[1].Value;
                    }
                }

                var transport = new BacnetIpUdpProtocolTransport(int.Parse(ipPortComboBox.Text), useWildcardAddress: true, localIpAddress: localIp);
                int apduTimeout = int.Parse(apduTimeoutComboBox.Text);
                _bacnetClient = new BacnetClient(transport) { Timeout = apduTimeout };

                _bacnetClient.OnIam += OnIamHandler;
                _bacnetClient.OnReadPropertyAck += OnReadPropertyAckHandler;

                _bacnetThread = new Thread(() => {
                    try { _bacnetClient.Start(); }
                    catch (Exception ex) { Log($"--- BACnet Thread Error: {ex.Message} ---"); }
                });
                _bacnetThread.IsBackground = true;
                _bacnetThread.Start();

                Log("BACnet client started.");

                if (!string.IsNullOrWhiteSpace(bbmdIpComboBox.Text))
                {
                    ushort ttl = ushort.Parse(bbmdTtlComboBox.Text);
                    Log($"Registering as Foreign Device to {bbmdIpComboBox.Text} with TTL {ttl}s...");
                    _bacnetClient.RegisterAsForeignDevice(bbmdIpComboBox.Text, ttl);
                }
            }
            catch (Exception ex)
            {
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

        private void OnReadPropertyAckHandler(BacnetClient sender, BacnetAddress adr, BacnetObjectId objectId, BacnetPropertyReference property, IList<BacnetValue> values)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Log($"--- Read ACK from {adr} for {objectId.type}/{objectId.instance} -> {property.propertyIdentifier} ---");
                foreach (var val in values)
                {
                    Log($"  Value: {val.Value}");
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

            StartBacnetClient();
            if (_bacnetClient == null) return;

            uint deviceId = uint.Parse(instanceNumberComboBox.Text);
            Log($"Pinging Device ID: {deviceId}...");

            _bacnetClient.WhoIs(lowLimit: (int)deviceId, highLimit: (int)deviceId);

            _lastPingedDeviceId = deviceId;
            UpdateAllStates(null, null);
        }

        private void DiscoverButton_Click(object sender, EventArgs e)
        {
            StartBacnetClient();
            if (_bacnetClient == null) return;

            deviceTreeView.Nodes.Clear();
            Log("Discovering devices with global Who-Is broadcast...");
            _bacnetClient.WhoIs();
        }

        private void DiscoverObjectsButton_Click(object sender, EventArgs e)
        {
            if (_lastPingedDeviceId == null)
            {
                MessageBox.Show("Please successfully Ping a device first to select it for object discovery.", "Device Not Selected");
                return;
            }

            StartBacnetClient();
            if (_bacnetClient == null) return;

            try
            {
                uint deviceId = _lastPingedDeviceId.Value;
                Log($"Discovering objects for Device {deviceId}...");

                BacnetAddress deviceAddress = FindDeviceAddress(deviceId);
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

        private void ReadPropertyButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instanceNumberComboBox.Text))
            {
                MessageBox.Show("Instance number is required to read.", "Error");
                return;
            }

            StartBacnetClient();
            if (_bacnetClient == null) return;

            try
            {
                uint deviceId = uint.Parse(instanceNumberComboBox.Text);
                BacnetAddress deviceAddress = FindDeviceAddress(deviceId);
                if (deviceAddress == null)
                {
                    Log($"--- ERROR: Device {deviceId} not found. Ping or Discover first. ---");
                    return;
                }

                var objectId = new BacnetObjectId(BacnetObjectTypes.OBJECT_DEVICE, deviceId);
                var propertyId = BacnetPropertyIds.PROP_OBJECT_NAME;

                Log($"Reading {propertyId} from Device {deviceId}...");
                _bacnetClient.ReadPropertyRequest(deviceAddress, objectId, propertyId);
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
            bbmdIpComboBox.Text = "172.19.10.102";
            ipPortComboBox.Text = "47808";
            apduTimeoutComboBox.Text = "5000";
            bbmdTtlComboBox.Text = "3600";

            interfaceComboBox.Items.Add("0.0.0.0 (Any)");
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
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
            interfaceComboBox.SelectedIndex = 0;
        }

        private void UpdateAllStates(object sender, EventArgs e)
        {
            bool instanceExists = !string.IsNullOrWhiteSpace(instanceNumberComboBox.Text);
            pingButton.Enabled = instanceExists;
            readPropertyButton.Enabled = instanceExists;
            writePropertyButton.Enabled = instanceExists;
            discoverObjectsButton.Enabled = _lastPingedDeviceId.HasValue;
        }

        private void DeviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                uint deviceId = uint.Parse(e.Node.Name);
                instanceNumberComboBox.Text = deviceId.ToString();
                ipAddressComboBox.Text = (e.Node.Tag as BacnetAddress)?.adr;
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

        private BacnetAddress FindDeviceAddress(uint deviceId)
        {
            var nodes = deviceTreeView.Nodes.Find(deviceId.ToString(), true);
            if (nodes.Length > 0 && nodes[0].Tag is BacnetAddress adr)
            {
                return adr;
            }

            Log($"Address for Device {deviceId} not cached. Sending targeted WhoIs...");
            BacnetAddress deviceAddress = null;
            var foundEvent = new ManualResetEvent(false);

            EventHandler<BacnetClient.IamEventArgs> handler = (s, args) =>
            {
                if (args.DeviceId == deviceId)
                {
                    deviceAddress = args.Address;
                    foundEvent.Set();
                }
            };

            _bacnetClient.OnIam += handler;
            _bacnetClient.WhoIs((int)deviceId, (int)deviceId);

            foundEvent.WaitOne(2000);
            _bacnetClient.OnIam -= handler;

            return deviceAddress;
        }

        // This is important for cleanup when the main application closes
        public void Shutdown()
        {
            _bacnetClient?.Dispose();
        }
    }
}