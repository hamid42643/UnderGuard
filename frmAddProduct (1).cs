using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainForm
{
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClassLibrary.Product pr = new ClassLibrary.Product();

            pr.AddProduct(txtName.Text, Convert.ToDecimal(txtCost.Text), Convert.ToDecimal(txtPrice.Text),
                txtColor.Text, txtDescription.Text);
        }
    }
}
