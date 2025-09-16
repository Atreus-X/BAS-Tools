using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BAS_Tools;
using MainApp.BACnet;
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

            // --- Set the form's starting position and size ---
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.Width = Screen.PrimaryScreen.Bounds.Width / 2;

            // --- Wire up menu item event handlers ---
            this.exitToolStripMenuItem.Click += (sender, e) => this.Close();
            this.clearHistoryToolStripMenuItem.Click += (sender, e) => this.ClearHistoryMenuItem_Click();
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.bacnetIPToolStripMenuItem.Click += new System.EventHandler(this.BacnetIpMenuItem_Click);
            this.bacnetMSTPLocalToolStripMenuItem.Click += new System.EventHandler(this.BacnetMstpLocalMenuItem_Click);
            this.bacnetMSTPRemoteToolStripMenuItem.Click += new System.EventHandler(this.BacnetMstpRemoteMenuItem_Click);
            this.modbusTCPToolStripMenuItem.Click += new System.EventHandler(this.ModbusTcpMenuItem_Click);
            this.modbusRTUToolStripMenuItem.Click += new System.EventHandler(this.ModbusRtuMenuItem_Click);
            // --- End of new code ---

            // Create instances of our protocol controls
            _protocolControls = new Dictionary<string, UserControl>
            {
                { "BACnet/IP", new BACnet_IP() },
                { "BACnet MS/TP Local", new BACnet_MSTP_Local() },
                { "BACnet MS/TP Remote", new BACnet_MSTP_Remote() },
                { "Modbus TCP/IP", new Modbus_IP() },
                { "Modbus RTU", new Modbus_RTU() }
            };

            // Add all controls to the main panel and set them up
            foreach (var control in _protocolControls.Values)
            {
                mainContentPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }

            // Show the last used control, or default to "BACnet MS/TP Remote"
            string lastControl = BAS_Tools.Properties.Settings.Default.LastConfiguration;
            if (!string.IsNullOrEmpty(lastControl) && _protocolControls.ContainsKey(lastControl))
            {
                ShowProtocolControl(lastControl);
            }
            else
            {
                ShowProtocolControl("BACnet MS/TP Remote");
            }


            this.FormClosing += MainApp_FormClosing;
            this.KeyDown += MainApp_KeyDown; // Add KeyDown event handler
            this.KeyPreview = true; // Set KeyPreview to true
        }

        private void MainApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                // Find the active control and trigger the discover button
                var activeControl = mainContentPanel.Controls.OfType<UserControl>().FirstOrDefault(c => c.Visible);
                if (activeControl != null)
                {
                    Button discoverButton = null;
                    if (activeControl is BACnet_IP)
                        discoverButton = activeControl.Controls.Find("discoverButton", true).FirstOrDefault() as Button;
                    else if (activeControl is BACnet_MSTP_Local || activeControl is BACnet_MSTP_Remote)
                        discoverButton = activeControl.Controls.Find("startDiscoveryButton", true).FirstOrDefault() as Button;

                    if (discoverButton != null && discoverButton.Enabled)
                    {
                        discoverButton.PerformClick();
                    }
                }
            }
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

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
            var selectedControl = _protocolControls[key];
            selectedControl.Visible = true;
            this.Text = $"BAS Tools - {key}"; // Update window title
            BAS_Tools.Properties.Settings.Default.LastConfiguration = key;
            BAS_Tools.Properties.Settings.Default.Save();


            // Re-register the logger for the active control
            if (selectedControl is BACnetControlBase bacnetControl)
            {
                var rtb = bacnetControl.GetOutputTextBox();
                if (rtb != null)
                {
                    GlobalLogger.Register(rtb);
                }
            }
        }
        // --- Menu Item Click Handlers ---
        private void BacnetIpMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet/IP");
        }

        private void BacnetMstpLocalMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet MS/TP Local");
        }

        private void BacnetMstpRemoteMenuItem_Click(object sender, EventArgs e)
        {
            ShowProtocolControl("BACnet MS/TP Remote");
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
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
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