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
    public partial class frmDisSOrderDetail : Form
    {
        DataRow drRow;

        public frmDisSOrderDetail(DataRow row)
        {
            drRow = row;
            InitializeComponent();
        }

        private void frmDisSOrderDetail_Load(object sender, EventArgs e)
        {
            try
            {
                int sorderid = (int)drRow.ItemArray[0];
                ClassLibrary.SupplierOrderDetails sorderdetails = new ClassLibrary.SupplierOrderDetails();
                sorderdetails.SorderId = sorderid;
                dataGridView1.DataSource = sorderdetails.GetSOrderdetailBySOrderId().Tables[0];

                dataGridView1.Columns["ORDERDETAIL_price"].DefaultCellStyle.Format = "c";
                dataGridView1.Columns["PARTSUPPLIER_cost"].DefaultCellStyle.Format = "c";

                dataGridView1.Columns[0].HeaderText = "Part name";
                dataGridView1.Columns[1].HeaderText = "Part Price";
                dataGridView1.Columns[2].HeaderText = "Order Quantity";
                dataGridView1.Columns[3].HeaderText = "Total Price";
                dataGridView1.Columns[4].HeaderText = "Supplier Name";
                dataGridView1.Columns[5].HeaderText = "Delivery date";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
