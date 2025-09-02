using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO.BACnet;

namespace MainApp
{
    public partial class ManualReadWriteForm : Form
    {
        public TreeNode SelectedDeviceNode { get; private set; }
        public BacnetObjectId SelectedObject { get; private set; }
        public BacnetPropertyIds SelectedProperty { get; private set; }
        public string ValueToWrite { get; private set; }
        public uint WritePriority { get; private set; }
        public bool IsReadOperation { get; private set; }
        private bool _isManualMode = false;

        public ManualReadWriteForm(TreeNodeCollection deviceNodes)
        {
            InitializeComponent();
            PopulateDeviceComboBox(deviceNodes);
            PopulateObjectComboBox();
            PopulatePropertyComboBox();
            PopulatePriorityComboBox();
        }

        private void PopulateDeviceComboBox(TreeNodeCollection deviceNodes)
        {
            foreach (TreeNode node in deviceNodes)
            {
                // Assuming child nodes are devices
                foreach (TreeNode deviceNode in node.Nodes)
                {
                    cmbDevices.Items.Add(deviceNode);
                }
            }
            cmbDevices.DisplayMember = "Text";
            if (cmbDevices.Items.Count > 0)
                cmbDevices.SelectedIndex = 0;
        }

        private void PopulateObjectComboBox()
        {
            cmbObjectType.Items.AddRange(Enum.GetNames(typeof(BacnetObjectTypes)).Where(s => s != "MAX_BACNET_OBJECT_TYPE").ToArray());
            cmbObjectType.SelectedIndex = 0;
        }

        private void PopulatePropertyComboBox()
        {
            cmbProperty.Items.AddRange(Enum.GetNames(typeof(BacnetPropertyIds)).Where(s => s != "MAX_BACNET_PROPERTY_ID").ToArray());
            cmbProperty.SelectedIndex = 0;
        }

        private void PopulatePriorityComboBox()
        {
            for (int i = 1; i <= 16; i++)
            {
                cmbPriority.Items.Add(i.ToString());
            }
            cmbPriority.SelectedIndex = 7; // Default to priority 8
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            _isManualMode = !_isManualMode;
            if (_isManualMode)
            {
                lblObjectType.Visible = false;
                cmbObjectType.Visible = false;
                lblInstance.Visible = false;
                txtInstance.Visible = false;
                lblProperty.Visible = false;
                cmbProperty.Visible = false;
                lblManualInput.Visible = true;
                txtManualInput.Visible = true;
                btnManual.Text = "Standard";
            }
            else
            {
                lblObjectType.Visible = true;
                cmbObjectType.Visible = true;
                lblInstance.Visible = true;
                txtInstance.Visible = true;
                lblProperty.Visible = true;
                cmbProperty.Visible = true;
                lblManualInput.Visible = false;
                txtManualInput.Visible = false;
                btnManual.Text = "Manual";
            }
        }

        private bool ParseInputs()
        {
            if (cmbDevices.SelectedItem == null)
            {
                MessageBox.Show("Please select a device.", "Input Error");
                return false;
            }
            SelectedDeviceNode = (TreeNode)cmbDevices.SelectedItem;

            if (_isManualMode)
            {
                try
                {
                    string[] parts = txtManualInput.Text.Split(';');
                    if (parts.Length != 3) throw new Exception();
                    var objType = (BacnetObjectTypes)Enum.Parse(typeof(BacnetObjectTypes), parts[0], true);
                    var instance = uint.Parse(parts[1]);
                    SelectedObject = new BacnetObjectId(objType, instance);
                    SelectedProperty = (BacnetPropertyIds)uint.Parse(parts[2]);
                }
                catch
                {
                    MessageBox.Show("Invalid format for Manual Input. Please use 'OBJECT_TYPE;instance;property_id'.", "Input Error");
                    return false;
                }
            }
            else
            {
                if (!uint.TryParse(txtInstance.Text, out uint instance))
                {
                    MessageBox.Show("Invalid instance number.", "Input Error");
                    return false;
                }
                var objType = (BacnetObjectTypes)Enum.Parse(typeof(BacnetObjectTypes), cmbObjectType.SelectedItem.ToString());
                SelectedObject = new BacnetObjectId(objType, instance);
                SelectedProperty = (BacnetPropertyIds)Enum.Parse(typeof(BacnetPropertyIds), cmbProperty.SelectedItem.ToString());
            }

            ValueToWrite = txtValue.Text;
            WritePriority = uint.Parse(cmbPriority.SelectedItem.ToString());
            return true;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (ParseInputs())
            {
                IsReadOperation = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (ParseInputs())
            {
                IsReadOperation = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}