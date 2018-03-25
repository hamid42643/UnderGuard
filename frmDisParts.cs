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
    public partial class frmDisParts : Form
    {
        DataTable dtParts;
        BindingSource bsSource;

        public frmDisParts()
        {
            bsSource = new BindingSource();

            InitializeComponent();
        }

        private void frmDisParts_Load(object sender, EventArgs e)
        {
            DisplayAllParts();
        }

        public void DisplayAllParts()
        {
            ClassLibrary.Part part = new ClassLibrary.Part();
            dtParts = part.GetAll().Tables[0];

            bsSource.DataSource = dtParts;

            //set the parts id column as primary key so you can search the table later
            dtParts.PrimaryKey = new DataColumn[] { dtParts.Columns["PART_id"] };

            dataGridView1.DataSource = dtParts;

            dataGridView1.Columns["PART_id"].HeaderText = "ID";
            dataGridView1.Columns["PART_name"].HeaderText = "Name";
            dataGridView1.Columns["PART_color"].HeaderText = "Color";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                object id = dataGridView1.CurrentRow.Cells["PART_id"].Value;

                DataRow selectedRow = dtParts.Rows.Find(id);

                frmAddPart f = new frmAddPart(selectedRow, this);
                f.MdiParent = this.MdiParent;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            catch (Exception ex) { }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddPart f = new frmAddPart(this);
            f.MdiParent = this.MdiParent;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.Part part = new ClassLibrary.Part();

                object id = dataGridView1.CurrentRow.Cells["PART_id"].Value;
                DataRow selectedRow = dtParts.Rows.Find(id);

                DialogResult dlgResult = MessageBox.Show
                    ("this will delete the part and all the information related to the part",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dlgResult == DialogResult.Yes)
                {
                    part.Id = Convert.ToInt32(id);
                    part.Delete();
                    DisplayAllParts();
                }
            }
            catch (Exception ex)
            {
                DialogResult dlgResult = MessageBox.Show
    ("Cant delete the part! you are ordering this part from a supplier, please delete the supplier order first",
    "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }


    }
}
