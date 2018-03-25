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
    public partial class frmDisProducts : Form
    {
        DataTable dtProducts;

        public frmDisProducts()
        {
            InitializeComponent();
        }

        private void frmDisProducts_Load(object sender, EventArgs e)
        {
            DisplayAllProducts();
        }

        public void DisplayAllProducts()
        {
            ClassLibrary.Product product = new ClassLibrary.Product();
            dtProducts = product.GetAll().Tables[0];
            dataGridView1.DataSource = dtProducts;

            //set the product_id column as primary key so you can search the table later
            dtProducts.PrimaryKey = new DataColumn[] { dtProducts.Columns["PRODUCT_id"] };
            dataGridView1.DataSource = dtProducts;

            dataGridView1.Columns["PRODUCT_picture"].Visible = false;
            dataGridView1.Columns["PRODUCT_cost"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["PRODUCT_price"].DefaultCellStyle.Format = "c";

            dataGridView1.Columns["PRODUCT_id"].HeaderText = "ID";
            dataGridView1.Columns["PRODUCT_name"].HeaderText = "Name";
            dataGridView1.Columns["PRODUCT_cost"].HeaderText = "Cost";
            dataGridView1.Columns["PRODUCT_price"].HeaderText = "Price";
            dataGridView1.Columns["PRODUCT_color"].HeaderText = "Color";
            dataGridView1.Columns["PRODUCT_description"].HeaderText = "Description";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmAddProduct f = new frmAddProduct(this);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                object id = dataGridView1.CurrentRow.Cells["PRODUCT_id"].Value;
                DataRow selectedRow = dtProducts.Rows.Find(id);

                frmAddProduct f = new frmAddProduct(this, selectedRow);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            catch (Exception ex) { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.Product product = new ClassLibrary.Product();

                object id = dataGridView1.CurrentRow.Cells["PRODUCT_id"].Value;
                DataRow selectedRow = dtProducts.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("Are you sure you want to delete \nselected product and it's builtsheet?",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    product.Id = Convert.ToInt32(id);
                    product.Delete();
                    DisplayAllProducts();
                }
                else if (dlgResult == DialogResult.No)
                {
                    // No, stop
                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cannot delete the record!\n the product is in an order, please delete the order first",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
