using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class frmChooseSupplier : Form
    {
        int partId;
        int index;
        DataTable dtSuppliers;
        frmDisSupplierOrder formDisSupplierOrder;

        public frmChooseSupplier(int id, int index, frmDisSupplierOrder form)
        {
            this.index = index;
            formDisSupplierOrder = form;
            partId = id;
            InitializeComponent();
        }

        private void frmChooseSupplier_Load(object sender, EventArgs e)
        {
            ClassLibrary.SupplierPart supplierpart = new ClassLibrary.SupplierPart();
            supplierpart.PartId = partId;
            dtSuppliers = supplierpart.GetSupplierPartsByPartId().Tables[0];
            comboBox1.DataSource = dtSuppliers;
            comboBox1.DisplayMember = "SUPPLIER_name";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView selectedSupplier = (DataRowView)comboBox1.SelectedItem;

                int supId = (int)selectedSupplier.Row.ItemArray[0];
                string supName = (string)selectedSupplier.Row.ItemArray[1];
                decimal supPrice = Convert.ToDecimal(selectedSupplier.Row.ItemArray[2]);

                formDisSupplierOrder.PartList2[index - 1].SupplierName = supName;
                formDisSupplierOrder.PartList2[index - 1].SupplierPrice = String.Format("{0:c}", supPrice);
                formDisSupplierOrder.PartList2[index - 1].SupplierID = supId;

                formDisSupplierOrder.RefreshPartList();
                formDisSupplierOrder.CalculateTotalPrice();
            }
            catch (Exception ex) { }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
