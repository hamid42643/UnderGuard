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
    public partial class frmDisOrder : Form
    {
        DataTable dtOrders;

        public frmDisOrder()
        {
            InitializeComponent();
        }

        private void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            frmAddOrderCustomer f = new frmAddOrderCustomer(this);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        public void DisplayAllOrders()
        {
            try
            {
                ClassLibrary.CustomerOrder order = new ClassLibrary.CustomerOrder();
                dtOrders = order.GetAllOrders().Tables[0];
                dataGridView1.DataSource = dtOrders;
                dataGridView1.Columns["ORDER_totalprice"].DefaultCellStyle.Format = "c";
                
                //set the product_id column as primary key so you can search the table later
                dtOrders.PrimaryKey = new DataColumn[] { dtOrders.Columns["ORDER_id"] };
                dataGridView1.DataSource = dtOrders;

                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "Date";
                dataGridView1.Columns[3].HeaderText = "Total Price";
                dataGridView1.Columns[4].HeaderText = "Status";
                dataGridView1.Columns[5].HeaderText = "Delivery Date";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //get selected row id and send it to the addorder form
                object id = dataGridView1.CurrentRow.Cells["ORDER_id"].Value;
                DataRow selectedRow = dtOrders.Rows.Find(id);

                frmAddOrderCustomer f = new frmAddOrderCustomer(this, selectedRow);
                f.MdiParent = this.MdiParent;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            catch (Exception ex) { }
        }

        private void frmDisOrder_Load(object sender, EventArgs e)
        {
            DisplayAllOrders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.CustomerOrder order = new ClassLibrary.CustomerOrder();

                object id = dataGridView1.CurrentRow.Cells["ORDER_id"].Value;
                DataRow selectedRow = dtOrders.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("Are you sure you want to delete \nselected order with id of " + id,
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    order.Id = Convert.ToInt32(id);
                    order.Delete();
                    DisplayAllOrders();
                }
                else if (dlgResult == DialogResult.No)
                {

                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cant delete the record! Please delete supplier order first!",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnSendToSupplier_Click(object sender, EventArgs e)
        {
            try
            {

                //get selected row id and send it to the supplier form
                object id = dataGridView1.CurrentRow.Cells["ORDER_id"].Value;
                DataRow selectedRow = dtOrders.Rows.Find(id);

                //if orderid is in the sorder table
                ClassLibrary.SupplierOrder sorder = new ClassLibrary.SupplierOrder();
                sorder.OrderId = Convert.ToInt32(id);
                DataTable dt = sorder.GetSorderByOrderId().Tables[0];

                //if customer order hasnt been sent to suppliers before
                if (dt.Rows.Count == 0)
                {
                    frmDisSupplierOrder f = new frmDisSupplierOrder(this, selectedRow);
                    f.MdiParent = this.MdiParent;
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.Show();
                }
                else
                {
                    DialogResult dlgResult = MessageBox.Show
                    ("the order has already been sent to the suppliers!",
                    "Continue?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { }
        }


    }
}
