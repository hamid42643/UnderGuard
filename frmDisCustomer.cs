using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication
{
    public partial class frmDisCustomer : Form
    {
        DataTable dtCustomers;
        

        public frmDisCustomer()
        {
            InitializeComponent();
        }

        private void frmDisCustomer_Load(object sender, EventArgs e)
        {

            displayAllCustomers();

        }

        public void displayAllCustomers()
        {
            ClassLibrary.Customer customer = new ClassLibrary.Customer();

            dtCustomers = customer.GetAll().Tables[0];

            //set the customer_id column as primary key so you can search the table later
            dtCustomers.PrimaryKey = new DataColumn[] { dtCustomers.Columns["CUSTOMER_id"] };
            dataGridView1.DataSource = dtCustomers;

            dataGridView1.Columns["CUSTOMER_id"].HeaderText = "ID";
            dataGridView1.Columns["CUSTOMER_name"].HeaderText = "Name";
            dataGridView1.Columns["CUSTOMER_phone"].HeaderText = "Phone";
            dataGridView1.Columns["CUSTOMER_email"].HeaderText = "Email";
            dataGridView1.Columns["CUSTOMER_address"].HeaderText = "Address";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                object id = dataGridView1.CurrentRow.Cells["CUSTOMER_id"].Value;

                DataRow selectedRow = dtCustomers.Rows.Find(id);

                frmAddCustomer f = new frmAddCustomer(selectedRow, this);
                f.MdiParent = this.MdiParent;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            catch (Exception ex) { }
            //Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.Customer customer = new ClassLibrary.Customer();

                object id = dataGridView1.CurrentRow.Cells["CUSTOMER_id"].Value;
                DataRow selectedRow = dtCustomers.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("Are you sure you want to delete selected customer and all of his orders",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    customer.Id = Convert.ToInt32(id);
                    customer.Delete();
                    displayAllCustomers();
                }
                else if (dlgResult == DialogResult.No)
                {
                    // No, stop
                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cant delete the record!",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmAddCustomer f = new frmAddCustomer(this);
            f.MdiParent = this.MdiParent;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }


    }
}
