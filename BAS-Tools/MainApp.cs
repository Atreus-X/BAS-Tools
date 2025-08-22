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
        private void bacnetIpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet/IP");
        }

        private void bacnetMstpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet MS/TP");
        }

        private void modbusTcpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("Modbus TCP/IP");
        }

        private void modbusRtuMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("Modbus RTU");
        }
    }
}