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
    public partial class frmDisSOrder : Form
    {
        DataTable dtSOrder;

        public frmDisSOrder()
        {
            InitializeComponent();
        }

        private void frmDisSOrder_Load(object sender, EventArgs e)
        {
            DisplayAllSOrders();
        }

        public void DisplayAllSOrders()
        {
            ClassLibrary.SupplierOrder sorder = new ClassLibrary.SupplierOrder();
            dtSOrder = sorder.GetAll().Tables[0];

            dataGridView1.DataSource = dtSOrder;

            //set the parts id column as primary key so you can search the table later
            dtSOrder.PrimaryKey = new DataColumn[] { dtSOrder.Columns["SORDER_id"] };
            dataGridView1.Columns["SORDER_totalprice"].DefaultCellStyle.Format = "c";

            dataGridView1.Columns["ORDER_id"].HeaderText = "Customer Order";
            dataGridView1.Columns["SORDER_id"].HeaderText = "ID";
            dataGridView1.Columns["SORDER_date"].HeaderText = "Date";
            dataGridView1.Columns["SORDER_totalprice"].HeaderText = "Total Parts Price";
            dataGridView1.Columns["SORDER_status"].HeaderText = "Status";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                object id = dataGridView1.CurrentRow.Cells["SORDER_id"].Value;
                DataRow selectedRow = dtSOrder.Rows.Find(id);

                frmDisSOrderDetail f = new frmDisSOrderDetail(selectedRow);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            catch (Exception ex) { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.SupplierOrder sorder = new ClassLibrary.SupplierOrder();

                object id = dataGridView1.CurrentRow.Cells["SORDER_id"].Value;
                DataRow selectedRow = dtSOrder.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("Are you sure you want to delete \nselected order with id of " + id,
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    sorder.Id = Convert.ToInt32(id);
                    sorder.Delete();
                    DisplayAllSOrders();
                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cant delete the record!",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
