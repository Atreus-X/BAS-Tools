using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentModbus; // Make sure FluentModbus NuGet package is installed
using System.Buffers; // Required for ReadOnlySequence<T> and Memory<T>

namespace MainApp
{
    public partial class Modbus_IP : UserControl, IHistorySupport
    {
        private ModbusTcpClient _modbusClient;
        private readonly HistoryManager _historyManager;
        private bool _isConnected = false;

        public Modbus_IP()
        {
            InitializeComponent();
            _historyManager = new HistoryManager("Modbus_IP_");
            PopulateDefaultValues();
            LoadHistory();
            UpdateConnectionState(false);

            // Wire up event handlers for history saving
            ipAddressComboBox.Leave += (sender, e) => SaveComboBoxEntry(ipAddressComboBox, "ipAddress");
            portComboBox.Leave += (sender, e) => SaveComboBoxEntry(portComboBox, "port");
            unitIdComboBox.Leave += (sender, e) => SaveComboBoxEntry(unitIdComboBox, "unitId");
            startAddressTextBox.Leave += (sender, e) => SaveTextBoxEntry(startAddressTextBox, "startAddress");
            quantityTextBox.Leave += (sender, e) => SaveTextBoxEntry(quantityTextBox, "quantity");
            writeValueTextBox.Leave += (sender, e) => SaveTextBoxEntry(writeValueTextBox, "writeValue");

            // Wire up button click handlers
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
            // Default values if history is empty
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
                var ipAddress = ipAddressComboBox.Text;
                var port = int.Parse(portComboBox.Text);

                // Initialize ModbusTcpClient with default constructor
                _modbusClient = new ModbusTcpClient();
                // Set endianness BEFORE connecting
                _modbusClient.Endianness = ModbusEndianness.LittleEndian;
                // Connect using the IP address and port
                await Task.Run(() => _modbusClient.Connect(ipAddress, port));

                Log($"Connected to Modbus TCP/IP server at {ipAddress}:{port}.");
                UpdateConnectionState(true);

                // Save successful connection details to history
                _historyManager.AddEntry("ipAddress", ipAddress);
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
            // Call the overload for boolean reads
            await ExecuteModbusRead("Read Coils", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadCoilsAsync(unitId, startAddress, quantity);
            }, "bool");
        }

        private async void ReadDiscreteInputsButton_Click(object sender, EventArgs e)
        {
            // Call the overload for boolean reads
            await ExecuteModbusRead("Read Discrete Inputs", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadDiscreteInputsAsync(unitId, startAddress, quantity);
            }, "bool");
        }

        private async void ReadHoldingRegistersButton_Click(object sender, EventArgs e)
        {
            // Call the generic overload for ushort reads
            await ExecuteModbusRead<ushort>("Read Holding Registers", async (startAddress, quantity, unitId) =>
            {
                return await _modbusClient.ReadHoldingRegistersAsync<ushort>(unitId, startAddress, quantity);
            }, "ushort");
        }

        private async void ReadInputRegistersButton_Click(object sender, EventArgs e)
        {
            // Call the generic overload for ushort reads
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

        /// <summary>
        /// Executes a Modbus read operation for boolean values (coils, discrete inputs) and logs the result.
        /// Handles conversion from Memory<byte> to bool[].
        /// </summary>
        /// <param name="operationName">The name of the operation for logging.</param>
        /// <param name="readFunction">The asynchronous function to perform the Modbus read, returning Memory<byte>.</param>
        /// <param name="valueType">A string representation of the value type for logging.</param>
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

                // The read function now returns Memory<byte>
                var memoryValues = await Task.Run(() => readFunction(startAddress, quantity, unitId));

                // Convert Memory<byte> to bool[]
                bool[] boolValues = new bool[quantity]; // The quantity refers to the number of coils/inputs, not bytes
                int bitIndex = 0;
                for (int i = 0; i < memoryValues.Length; i++)
                {
                    byte b = memoryValues.Span[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if (bitIndex < quantity) // Ensure we don't go out of bounds for the requested quantity
                        {
                            boolValues[bitIndex] = ((b >> j) & 1) == 1;
                            bitIndex++;
                        }
                        else
                        {
                            break; // Stop if we've read the requested quantity
                        }
                    }
                    if (bitIndex >= quantity) break;
                }

                Log($"--- SUCCESS: {operationName} Result ({valueType}): ---");
                Log(string.Join(", ", boolValues.Take(quantity))); // Only log the requested quantity
            }
            catch (Exception ex)
            {
                Log($"--- ERROR during {operationName}: {ex.Message} ---");
                MessageBox.Show($"Modbus Read Error ({operationName}): {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Executes a Modbus read operation for generic types (e.g., ushort) and logs the result.
        /// </summary>
        /// <typeparam name="T">The type of data to read (e.g., ushort).</typeparam>
        /// <param name="operationName">The name of the operation for logging.</param>
        /// <param name="readFunction">The asynchronous function to perform the Modbus read, returning Memory<T>.</param>
        /// <param name="valueType">A string representation of the value type for logging.</param>
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

                // The read function now returns Memory<T>
                var memoryValues = await Task.Run(() => readFunction(startAddress, quantity, unitId));

                // Convert Memory<T> to an array for easier logging
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

        // --- History Management ---
        private void LoadHistory()
        {
            PopulateComboBoxWithHistory(ipAddressComboBox, "ipAddress");
            PopulateComboBoxWithHistory(portComboBox, "port");
            PopulateComboBoxWithHistory(unitIdComboBox, "unitId");
            PopulateTextBoxWithHistory(startAddressTextBox, "startAddress");
            PopulateTextBoxWithHistory(quantityTextBox, "quantity");
            PopulateTextBoxWithHistory(writeValueTextBox, "writeValue");

            // Set default values if history is empty
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
                comboBox.Text = comboBox.Text; // Restore text after repopulating
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
            ipAddressComboBox.Items.Clear();
            portComboBox.Items.Clear();
            unitIdComboBox.Items.Clear();
            startAddressTextBox.Clear();
            quantityTextBox.Clear();
            writeValueTextBox.Clear();
            PopulateDefaultValues(); // Reset to defaults after clearing
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
