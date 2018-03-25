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
    public partial class frmDisSupplierOrder_Parts : Form
    {
        frmDisSupplierOrder formDisSupplierOrder;

        public frmDisSupplierOrder_Parts(frmDisSupplierOrder form)
        {
            formDisSupplierOrder = form;
            InitializeComponent();
        }

        private void frmDisSupplierOrder_Parts_Load(object sender, EventArgs e)
        {
            dataGridViewParts.DataSource = formDisSupplierOrder.PartList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
