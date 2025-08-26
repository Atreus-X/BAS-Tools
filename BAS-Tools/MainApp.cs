using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MainApp.Configuration;

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
            this.clearHistoryToolStripMenuItem.Click += (sender, e) => this.ClearHistoryMenuItem_Click();
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

            // Show the BACnet MS/TP control by default
            ShowProtocolControl("BACnet MS/TP");

            this.FormClosing += MainApp_FormClosing;
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Iterate through all protocol controls and call their Shutdown method if available
            foreach (var control in _protocolControls.Values)
            {
                if (control is IHistorySupport historyControl)
                {
                    historyControl.Shutdown(); // Call Shutdown through the interface
                }
            }
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
            this.Text = $"BAS Tools - {key}"; // Update window title
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

        private void ClearHistoryMenuItem_Click()
        {
            // Clear history for all active protocol controls
            foreach (var control in _protocolControls.Values)
            {
                if (control is IHistorySupport historyControl)
                {
                    historyControl.ClearHistory();
                }
            }
        }
    }

    /// <summary>
    /// Interface for controls that support history management and proper shutdown.
    /// </summary>
    public interface IHistorySupport
    {
        void ClearHistory();
        void Shutdown(); // Added Shutdown to the interface
    }
}