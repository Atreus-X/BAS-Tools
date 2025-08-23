using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MainApp
{
    public partial class MainApp : Form
    {
        // A dictionary to hold our protocol user controls
        private readonly Dictionary<string, UserControl> _protocolControls;

        public MainApp()
        {
            InitializeComponent();

            // --- Wire up menu item event handlers ---
            this.exitToolStripMenuItem.Click += (sender, e) => this.Close();
            this.bacnetIPToolStripMenuItem.Click += new System.EventHandler(this.BacnetIpMenuItem_Click);
            this.bacnetMSTPToolStripMenuItem.Click += new System.EventHandler(this.BacnetMstpMenuItem_Click);
            this.modbusTCPToolStripMenuItem.Click += new System.EventHandler(this.ModbusTcpMenuItem_Click);
            this.modbusRTUToolStripMenuItem.Click += new System.EventHandler(this.ModbusRtuMenuItem_Click);
            // --- End of new code ---

            // Create instances of our protocol controls
            _protocolControls = new Dictionary<string, UserControl>
            {
                { "BACnet/IP", new BACnet_IP() },
                { "BACnet MS/TP", new BACnet_MSTP() },
                { "Modbus TCP/IP", new Modbus_IP() },
                { "Modbus RTU", new Modbus_RTU() }
            };

            // Add all controls to the main panel and set them up
            foreach (var control in _protocolControls.Values)
            {
                mainContentPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }

            // Show the BACnet/IP control by default
            ShowProtocolControl("BACnet/IP");

            this.FormClosing += MainApp_FormClosing;
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Find the BACnet/IP control and shut it down
            if (_protocolControls.TryGetValue("BACnet/IP", out UserControl bacnetIpControl))
            {
                // We cast it to its specific type to access the Shutdown method
                (bacnetIpControl as BACnet_IP)?.Shutdown();
            }

            // You can add similar logic for other controls here as you build them
            // if (_protocolControls.TryGetValue("BACnet MS/TP", out UserControl bacnetMstpControl))
            // {
            //     (bacnetMstpControl as BACnet_MSTP)?.Shutdown();
            // }
        }
        private void ShowProtocolControl(string key)
        {
            if (!_protocolControls.ContainsKey(key)) return;

            // Hide all other controls
            foreach (var control in _protocolControls.Values)
            {
                control.Visible = false;
            }

            // Show the selected control
            _protocolControls[key].Visible = true;
            this.Text = $"BACnet Tools - {key}"; // Update window title
        }

        // --- Menu Item Click Handlers ---
        private void BacnetIpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet/IP");
        }

        private void BacnetMstpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet MS/TP");
        }

        private void ModbusTcpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("Modbus TCP/IP");
        }

        private void ModbusRtuMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("Modbus RTU");
        }
    }
}