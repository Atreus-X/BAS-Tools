using FluentModbus;
using System;
using System.Buffers;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.Configuration
{
    public partial class Modbus_IP : UserControl, IHistorySupport
    {
        private ModbusTcpClient _modbusClient;
        private HistoryManager _historyManager;
        private bool _isConnected = false;

        public Modbus_IP()
        {
            InitializeComponent();
            this.Load += Modbus_IP_Load;
        }

        private void Modbus_IP_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _historyManager = new HistoryManager("Modbus_IP_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateConnectionState(false);
            WireUpEventHandlers();
        }

        private void WireUpEventHandlers()
        {
            ipAddressComboBox.Leave += (sender, e) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            portComboBox.Leave += (sender, e) => SaveComboBoxEntry(portComboBox, "port");
            unitIdComboBox.Leave += (sender, e) => SaveComboBoxEntry(unitIdComboBox, "unitId");
            startAddressTextBox.Leave += (sender, e) => SaveTextBoxEntry(startAddressTextBox, "startAddress");
            quantityTextBox.Leave += (sender, e) => SaveTextBoxEntry(quantityTextBox, "quantity");
            writeValueTextBox.Leave += (sender, e) => SaveTextBoxEntry(writeValueTextBox, "writeValue");

            connectButton.Click += ConnectButton_Click;
            disconnectButton.Click += DisconnectButton_Click;
            readCoilsButton.Click += ReadCoilsButton_Click;
            readDiscreteInputsButton.Click += ReadDiscreteInputsButton_Click;
            readHoldingRegistersButton.Click += ReadHoldingRegistersButton_Click;
            readInputRegistersButton.Click += ReadInputRegistersButton_Click;
            writeSingleCoilButton.Click += WriteSingleCoilButton_Click;
            writeSingleRegisterButton.Click += WriteSingleRegisterButton_Click;
            writeMultipleCoilsButton.Click += WriteMultipleCoilsButton_Click;
            writeMultipleRegistersButton.Click += WriteMultipleRegistersButton_Click;
            clearLogButton.Click += ClearLogButton_Click;
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            Log("Log cleared.");
        }

        private void PopulateDefaultValues()
        {
            portComboBox.Items.AddRange(new object[] { "502" });
            unitIdComboBox.Items.AddRange(new object[] { "1" });
            startAddressTextBox.Text = "0";
            quantityTextBox.Text = "1";
        }

        private void UpdateConnectionState(bool connected)
        {
            _isConnected = connected;
            connectButton.Enabled = !connected;
            disconnectButton.Enabled = connected;
            readCoilsButton.Enabled = connected;
            readDiscreteInputsButton.Enabled = connected;
            readHoldingRegistersButton.Enabled = connected;
            readInputRegistersButton.Enabled = connected;
            writeSingleCoilButton.Enabled = connected;
            writeSingleRegisterButton.Enabled = connected;
            writeMultipleCoilsButton.Enabled = connected;
            writeMultipleRegistersButton.Enabled = connected;
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IPAddress.TryParse(ipAddressComboBox.Text, out var ipAddress))
                {
                    MessageBox.Show("Invalid IP address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(portComboBox.Text, out int port))
                {
                    MessageBox.Show("Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var endpoint = new IPEndPoint(ipAddress, port);
                _modbusClient = new ModbusTcpClient();

                await Task.Run(() => _modbusClient.Connect(endpoint, ModbusEndianness.LittleEndian));

                Log($"Connected to Modbus TCP/IP server at {ipAddress}:{port}.");
                UpdateConnectionState(true);

                _historyManager.AddEntry("ipAddress", ipAddress.ToString());
                _historyManager.AddEntry("port", port.ToString());
            }
            catch (Exception ex)
            {
                Log($"--- ERROR connecting: {ex.Message} ---");
                MessageBox.Show($"Connection Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateConnectionState(false);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (_modbusClient != null && _modbusClient.IsConnected)
            {
                _modbusClient.Disconnect();
                Log("Disconnected from Modbus TCP/IP server.");
            }
            UpdateConnectionState(false);
        }

        private async void ReadCoilsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead("Read Coils", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadCoilsAsync(unitId, startAddress, quantity);
            }, "bool");
        }

        private async void ReadDiscreteInputsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead("Read Discrete Inputs", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadDiscreteInputsAsync(unitId, startAddress, quantity);
            }, "bool");
        }

        private async void ReadHoldingRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead<ushort>("Read Holding Registers", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadHoldingRegistersAsync<ushort>(unitId, startAddress, quantity);
            }, "ushort");
        }

        private async void ReadInputRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead<ushort>("Read Input Registers", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadInputRegistersAsync<ushort>(unitId, startAddress, quantity);
            }, "ushort");
        }

        private async void WriteSingleCoilButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Single Coil", async (startAddress, value, unitId) =>
            {
                bool coilValue = bool.Parse(value); // Expect "True" or "False"
                await _modbusClient.WriteSingleCoilAsync(unitId, startAddress, coilValue);
                return true;
            }, "bool");
        }

        private async void WriteSingleRegisterButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Single Register", async (startAddress, value, unitId) =>
            {
                ushort registerValue = ushort.Parse(value);
                await _modbusClient.WriteSingleRegisterAsync(unitId, startAddress, registerValue);
                return true;
            }, "ushort");
        }

        private async void WriteMultipleCoilsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Multiple Coils", async (startAddress, value, unitId) =>
            {
                bool[] coilValues = value.Split(',').Select(x => bool.Parse(x.Trim())).ToArray();
                await _modbusClient.WriteMultipleCoilsAsync(unitId, startAddress, coilValues);
                return true;
            }, "bool[]");
        }

        private async void WriteMultipleRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Multiple Registers", async (startAddress, value, unitId) =>
            {
                ushort[] registerValues = value.Split(',').Select(x => ushort.Parse(x.Trim())).ToArray();
                await _modbusClient.WriteMultipleRegistersAsync(unitId, startAddress, registerValues);
                return true;
            }, "ushort[]");
        }

        private async Task ExecuteModbusRead(string operationName, Func<int, int, byte, Task<Memory<byte>>> readFunction, string valueType)
        {
            if (!_isConnected)
            {
                MessageBox.Show("Not connected to Modbus server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var quantity = int.Parse(quantityTextBox.Text);

                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Quantity={quantity}");

                var memoryValues = await Task.Run(() => readFunction(startAddress, quantity, unitId));

                bool[] boolValues = new bool[quantity];
                int bitIndex = 0;
                for (int i = 0; i < memoryValues.Length; i++)
                {
                    byte b = memoryValues.Span[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if (bitIndex < quantity)
                        {
                            boolValues[bitIndex] = ((b >> j) & 1) == 1;
                            bitIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (bitIndex >= quantity) break;
                }

                Log($"--- SUCCESS: {operationName} Result ({valueType}): ---");
                Log(string.Join(", ", boolValues.Take(quantity)));
            }
            catch (Exception ex)
            {
                Log($"--- ERROR during {operationName}: {ex.Message} ---");
                MessageBox.Show($"Modbus Read Error ({operationName}): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ExecuteModbusRead<T>(string operationName, Func<int, int, byte, Task<Memory<T>>> readFunction, string valueType) where T : unmanaged
        {
            if (!_isConnected)
            {
                MessageBox.Show("Not connected to Modbus server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var quantity = int.Parse(quantityTextBox.Text);

                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Quantity={quantity}");

                var memoryValues = await Task.Run(() => readFunction(startAddress, quantity, unitId));

                var valuesArray = memoryValues.ToArray();

                Log($"--- SUCCESS: {operationName} Result ({valueType}): ---");
                Log(string.Join(", ", valuesArray));
            }
            catch (Exception ex)
            {
                Log($"--- ERROR during {operationName}: {ex.Message} ---");
                MessageBox.Show($"Modbus Read Error ({operationName}): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ExecuteModbusWrite(string operationName, Func<int, string, byte, Task<bool>> writeFunction, string valueType)
        {
            if (!_isConnected)
            {
                MessageBox.Show("Not connected to Modbus server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var writeValue = writeValueTextBox.Text;

                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Value(s)='{writeValue}' (Expected Type: {valueType})");

                bool success = await Task.Run(() => writeFunction(startAddress, writeValue, unitId));

                if (success)
                {
                    Log($"--- SUCCESS: {operationName} completed. ---");
                }
                else
                {
                    Log($"--- WARNING: {operationName} might not have completed successfully. ---");
                }
            }
            catch (FormatException)
            {
                Log($"--- ERROR: Invalid value format for {operationName}. Expected type: {valueType}. ---");
                MessageBox.Show($"Invalid value format for {operationName}. Please enter a value compatible with {valueType}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Log($"--- ERROR during {operationName}: {ex.Message} ---");
                MessageBox.Show($"Modbus Write Error ({operationName}): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(ipAddressComboBox, "ipAddress");
            PopulateComboBoxWithHistory(portComboBox, "port");
            PopulateComboBoxWithHistory(unitIdComboBox, "unitId");
            PopulateTextBoxWithHistory(startAddressTextBox, "startAddress");
            PopulateTextBoxWithHistory(quantityTextBox, "quantity");
            PopulateTextBoxWithHistory(writeValueTextBox, "writeValue");

            if (string.IsNullOrEmpty(ipAddressComboBox.Text)) ipAddressComboBox.Text = "127.0.0.1";
            if (string.IsNullOrEmpty(portComboBox.Text)) portComboBox.Text = "502";
            if (string.IsNullOrEmpty(unitIdComboBox.Text)) unitIdComboBox.Text = "1";
            if (string.IsNullOrEmpty(startAddressTextBox.Text)) startAddressTextBox.Text = "0";
            if (string.IsNullOrEmpty(quantityTextBox.Text)) quantityTextBox.Text = "1";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            comboBox.Items.Clear();
            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                comboBox.Items.AddRange(historyList.Cast<object>().ToArray());
                comboBox.Text = historyList.First();
            }
        }

        private void PopulateTextBoxWithHistory(TextBox textBox, string key)
        {
            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                textBox.Text = historyList.First();
            }
        }

        private void SaveComboBoxEntry(ComboBox comboBox, string key)
        {
            if (!string.IsNullOrWhiteSpace(comboBox.Text))
            {
                _historyManager.AddEntry(key, comboBox.Text);
                PopulateComboBoxWithHistory(comboBox, key);
                comboBox.Text = comboBox.Text;
            }
        }

        private void SaveTextBoxEntry(TextBox textBox, string key)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                _historyManager.AddEntry(key, textBox.Text);
            }
        }

        public void ClearHistory()
        {
            if (_historyManager == null) return;
            _historyManager.ClearHistory();
            ipAddressComboBox.Items.Clear();
            portComboBox.Items.Clear();
            unitIdComboBox.Items.Clear();
            startAddressTextBox.Clear();
            quantityTextBox.Clear();
            writeValueTextBox.Clear();
            PopulateDefaultValues();
            Log("Modbus TCP/IP history cleared.");
        }

        public void Shutdown()
        {
            _historyManager.SaveHistory();
            if (_modbusClient != null && _modbusClient.IsConnected)
            {
                _modbusClient.Disconnect();
            }
            _isConnected = false;
        }
    }
}