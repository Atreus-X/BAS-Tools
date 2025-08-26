using FluentModbus;
using System;
using System.Buffers;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp
{
    public partial class Modbus_RTU : UserControl, IHistorySupport
    {
        private ModbusRtuClient _modbusClient;
        private HistoryManager _historyManager;
        private bool _isConnected = false;

        public Modbus_RTU()
        {
            InitializeComponent();
            this.Load += Modbus_RTU_Load;
        }

        private void Modbus_RTU_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _historyManager = new HistoryManager("Modbus_RTU_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateConnectionState(false);
            WireUpEventHandlers();
        }

        private void WireUpEventHandlers()
        {
            serialPortComboBox.Leave += (s, args) => SaveComboBoxEntry(serialPortComboBox, "serialPort");
            baudRateComboBox.Leave += (s, args) => SaveComboBoxEntry(baudRateComboBox, "baudRate");
            dataBitsComboBox.Leave += (s, args) => SaveComboBoxEntry(dataBitsComboBox, "dataBits");
            parityComboBox.Leave += (s, args) => SaveComboBoxEntry(parityComboBox, "parity");
            stopBitsComboBox.Leave += (s, args) => SaveComboBoxEntry(stopBitsComboBox, "stopBits");
            unitIdComboBox.Leave += (s, args) => SaveComboBoxEntry(unitIdComboBox, "unitId");
            startAddressTextBox.Leave += (s, args) => SaveTextBoxEntry(startAddressTextBox, "startAddress");
            quantityTextBox.Leave += (s, args) => SaveTextBoxEntry(quantityTextBox, "quantity");
            writeValueTextBox.Leave += (s, args) => SaveTextBoxEntry(writeValueTextBox, "writeValue");

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
        }

        private void PopulateDefaultValues()
        {
            serialPortComboBox.Items.Clear();
            serialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            if (serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;

            baudRateComboBox.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            dataBitsComboBox.Items.AddRange(new object[] { "7", "8" });
            parityComboBox.Items.AddRange(Enum.GetNames(typeof(Parity)));
            stopBitsComboBox.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            unitIdComboBox.Items.AddRange(new object[] { "1" });

            baudRateComboBox.Text = "9600";
            dataBitsComboBox.Text = "8";
            parityComboBox.Text = Parity.None.ToString();
            stopBitsComboBox.Text = StopBits.One.ToString();
            unitIdComboBox.Text = "1";
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
                var portName = serialPortComboBox.Text;
                var baudRate = int.Parse(baudRateComboBox.Text);
                var parity = (Parity)Enum.Parse(typeof(Parity), parityComboBox.Text);
                var stopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBitsComboBox.Text);

                _modbusClient = new ModbusRtuClient
                {
                    BaudRate = baudRate,
                    Parity = parity,
                    StopBits = stopBits
                };

                await Task.Run(() => _modbusClient.Connect(portName, ModbusEndianness.LittleEndian));

                Log($"Connected to Modbus RTU server on {portName} at {baudRate} baud.");
                UpdateConnectionState(true);

                _historyManager.AddEntry("serialPort", portName);
                _historyManager.AddEntry("baudRate", baudRate.ToString());
                _historyManager.AddEntry("dataBits", dataBitsComboBox.Text);
                _historyManager.AddEntry("parity", parity.ToString());
                _historyManager.AddEntry("stopBits", stopBits.ToString());
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
                _modbusClient.Close();
                Log("Disconnected from Modbus RTU server.");
            }
            UpdateConnectionState(false);
        }

        private async void ReadCoilsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead("Read Coils", async (startAddress, quantity, unitId) =>
                await _modbusClient.ReadCoilsAsync(unitId, startAddress, quantity), "bool");
        }

        private async void ReadDiscreteInputsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead("Read Discrete Inputs", async (startAddress, quantity, unitId) =>
                await _modbusClient.ReadDiscreteInputsAsync(unitId, startAddress, quantity), "bool");
        }

        private async void ReadHoldingRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead<ushort>("Read Holding Registers", async (startAddress, quantity, unitId) =>
                await _modbusClient.ReadHoldingRegistersAsync<ushort>(unitId, startAddress, quantity), "ushort");
        }

        private async void ReadInputRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusRead<ushort>("Read Input Registers", async (startAddress, quantity, unitId) =>
                await _modbusClient.ReadInputRegistersAsync<ushort>(unitId, startAddress, quantity), "ushort");
        }

        private async void WriteSingleCoilButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Single Coil", async (startAddress, value, unitId) =>
            {
                await _modbusClient.WriteSingleCoilAsync(unitId, startAddress, bool.Parse(value));
                return true;
            }, "bool");
        }

        private async void WriteSingleRegisterButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Single Register", async (startAddress, value, unitId) =>
            {
                await _modbusClient.WriteSingleRegisterAsync(unitId, startAddress, ushort.Parse(value));
                return true;
            }, "ushort");
        }

        private async void WriteMultipleCoilsButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Multiple Coils", async (startAddress, value, unitId) =>
            {
                var values = value.Split(',').Select(x => bool.Parse(x.Trim())).ToArray();
                await _modbusClient.WriteMultipleCoilsAsync(unitId, startAddress, values);
                return true;
            }, "bool[]");
        }

        private async void WriteMultipleRegistersButton_Click(object sender, EventArgs e)
        {
            await ExecuteModbusWrite("Write Multiple Registers", async (startAddress, value, unitId) =>
            {
                var values = value.Split(',').Select(x => ushort.Parse(x.Trim())).ToArray();
                await _modbusClient.WriteMultipleRegistersAsync(unitId, startAddress, values);
                return true;
            }, "ushort[]");
        }

        private async Task ExecuteModbusRead(string operationName, Func<int, int, byte, Task<Memory<byte>>> readFunction, string valueType)
        {
            if (!_isConnected) return;
            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var quantity = int.Parse(quantityTextBox.Text);
                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Quantity={quantity}");
                var memoryValues = await readFunction(startAddress, quantity, unitId);
                var boolValues = new bool[quantity];
                int bitIndex = 0;
                for (int i = 0; i < memoryValues.Length; i++)
                {
                    byte b = memoryValues.Span[i];
                    for (int j = 0; j < 8 && bitIndex < quantity; j++)
                    {
                        boolValues[bitIndex++] = ((b >> j) & 1) == 1;
                    }
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
            if (!_isConnected) return;
            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var quantity = int.Parse(quantityTextBox.Text);
                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Quantity={quantity}");
                var memoryValues = await readFunction(startAddress, quantity, unitId);
                Log($"--- SUCCESS: {operationName} Result ({valueType}): ---");
                Log(string.Join(", ", memoryValues.ToArray()));
            }
            catch (Exception ex)
            {
                Log($"--- ERROR during {operationName}: {ex.Message} ---");
                MessageBox.Show($"Modbus Read Error ({operationName}): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ExecuteModbusWrite(string operationName, Func<int, string, byte, Task<bool>> writeFunction, string valueType)
        {
            if (!_isConnected) return;
            try
            {
                var unitId = byte.Parse(unitIdComboBox.Text);
                var startAddress = int.Parse(startAddressTextBox.Text);
                var writeValue = writeValueTextBox.Text;
                Log($"Performing {operationName}: Unit ID={unitId}, Start Address={startAddress}, Value(s)='{writeValue}'");
                if (await writeFunction(startAddress, writeValue, unitId))
                {
                    Log($"--- SUCCESS: {operationName} completed. ---");
                }
            }
            catch (FormatException)
            {
                Log($"--- ERROR: Invalid value format for {operationName}. ---");
                MessageBox.Show($"Invalid value format for {operationName}.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            PopulateComboBoxWithHistory(serialPortComboBox, "serialPort");
            PopulateComboBoxWithHistory(baudRateComboBox, "baudRate");
            PopulateComboBoxWithHistory(dataBitsComboBox, "dataBits");
            PopulateComboBoxWithHistory(parityComboBox, "parity");
            PopulateComboBoxWithHistory(stopBitsComboBox, "stopBits");
            PopulateComboBoxWithHistory(unitIdComboBox, "unitId");
            PopulateTextBoxWithHistory(startAddressTextBox, "startAddress");
            PopulateTextBoxWithHistory(quantityTextBox, "quantity");
            PopulateTextBoxWithHistory(writeValueTextBox, "writeValue");

            if (string.IsNullOrEmpty(serialPortComboBox.Text) && serialPortComboBox.Items.Count > 0)
                serialPortComboBox.SelectedIndex = 0;
            if (string.IsNullOrEmpty(baudRateComboBox.Text)) baudRateComboBox.Text = "9600";
            if (string.IsNullOrEmpty(dataBitsComboBox.Text)) dataBitsComboBox.Text = "8";
            if (string.IsNullOrEmpty(parityComboBox.Text)) parityComboBox.Text = Parity.None.ToString();
            if (string.IsNullOrEmpty(stopBitsComboBox.Text)) stopBitsComboBox.Text = StopBits.One.ToString();
            if (string.IsNullOrEmpty(unitIdComboBox.Text)) unitIdComboBox.Text = "1";
            if (string.IsNullOrEmpty(startAddressTextBox.Text)) startAddressTextBox.Text = "0";
            if (string.IsNullOrEmpty(quantityTextBox.Text)) quantityTextBox.Text = "1";
        }

        private void PopulateComboBoxWithHistory(ComboBox comboBox, string key)
        {
            if (comboBox.Name != "serialPortComboBox")
            {
                comboBox.Items.Clear();
            }

            var historyList = _historyManager.GetHistoryForPrefixedKey(key);
            if (historyList.Any())
            {
                if (comboBox.Name == "serialPortComboBox")
                {
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
            else if (comboBox.Name == "serialPortComboBox" && comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
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
            _historyManager.ClearHistory();
            serialPortComboBox.Items.Clear();
            baudRateComboBox.Items.Clear();
            dataBitsComboBox.Items.Clear();
            parityComboBox.Items.Clear();
            stopBitsComboBox.Items.Clear();
            unitIdComboBox.Items.Clear();
            startAddressTextBox.Clear();
            quantityTextBox.Clear();
            writeValueTextBox.Clear();
            PopulateDefaultValues();
            Log("Modbus RTU history cleared.");
        }

        public void Shutdown()
        {
            _historyManager.SaveHistory();
            if (_modbusClient != null && _modbusClient.IsConnected)
            {
                _modbusClient.Close();
            }
            _isConnected = false;
        }
    }
}