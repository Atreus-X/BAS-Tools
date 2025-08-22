using System.Windows.Forms;
namespace BacnetToolsCSharp
{
    public partial class BACnet_MSTP : UserControl
    {
        public BACnet_MSTP()
        {
            InitializeComponent();
            var label = new Label { Text = "BACnet MS/TP Configuration Not Implemented", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
            this.Controls.Add(label);
        }
    }
}