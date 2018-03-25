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
    public partial class frmDisSupplier : Form
    {
        DataTable dtSuppliers;

        public frmDisSupplier()
        {
            InitializeComponent();
        }

        private void frmDisSupplier_Load(object sender, EventArgs e)
        {
            displayAllSuppliers();
        }

        public void displayAllSuppliers()
        {
            ClassLibrary.Supplier supplier = new ClassLibrary.Supplier();

            dtSuppliers = supplier.GetAll().Tables[0];

            //set the customer_id column as primary key so you can search the table later
            dtSuppliers.PrimaryKey = new DataColumn[] { dtSuppliers.Columns["SUPPLIER_id"] };
            dataGridView1.DataSource = dtSuppliers;

            dataGridView1.Columns["SUPPLIER_id"].HeaderText = "ID";
            dataGridView1.Columns["SUPPLIER_name"].HeaderText = "Name";
            dataGridView1.Columns["SUPPLIER_timesfailed"].HeaderText = "Failed";
            dataGridView1.Columns["SUPPLIER_phone"].HeaderText = "Phone";
            dataGridView1.Columns["SUPPLIER_email"].HeaderText = "Email";
            dataGridView1.Columns["SUPPLIER_address"].HeaderText = "Address";
            dataGridView1.Columns["SUPPLIER_postalcode"].HeaderText = "Postalcode";
            dataGridView1.Columns["SUPPLIER_province"].HeaderText = "Province";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                object id = dataGridView1.CurrentRow.Cells["SUPPLIER_id"].Value;

                DataRow selectedRow = dtSuppliers.Rows.Find(id);

                frmAddSupplier f = new frmAddSupplier(selectedRow, this);
                f.MdiParent = this.MdiParent;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            catch (Exception ex) { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.Supplier supplier = new ClassLibrary.Supplier();

                object id = dataGridView1.CurrentRow.Cells["SUPPLIER_id"].Value;
                DataRow selectedRow = dtSuppliers.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("Are you sure you want to delete \nselected supplier with id of " + id,
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    supplier.Id = Convert.ToInt32(id);
                    supplier.Delete();
                    displayAllSuppliers();
                }
                else if (dlgResult == DialogResult.No)
                {
                    // No, stop
                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cant delete the record!\n you are ordering a part from this supplier. please delete the order first",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddSupplier f = new frmAddSupplier(this);
            f.MdiParent = this.MdiParent;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}
