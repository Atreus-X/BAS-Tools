using System.Windows.Forms;

// Ensure the namespace is consistent
namespace MainApp
{
    public partial class Modbus_IP : UserControl
    {
        public Modbus_IP()
        {
            InitializeComponent();
            var label = new Label { Text = "BACnet MS/TP Configuration Not Implemented", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
            this.Controls.Add(label);
        }
    }
}