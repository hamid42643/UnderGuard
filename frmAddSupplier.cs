using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication
{
    public partial class frmAddSupplier : Form
    {

        DataRow drSupllier;
        frmDisSupplier frmDisSupplier;
        DataTable dtPart;

        BindingList<PartList> parts;
        //-----------------------------------------------------------------------------------------------

        public frmAddSupplier(frmDisSupplier form)
        {
            frmDisSupplier = form ;
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //this constructor recieves a datarow for editinf purposes
        public frmAddSupplier(DataRow row, frmDisSupplier form)
        {
            drSupllier = row;
            frmDisSupplier = form;

            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        private void frmAddSupplier_Load(object sender, EventArgs e)
        {
            //make parts list
            parts = new BindingList<PartList>();

            //add parts to the combobox
            ClassLibrary.Part part = new ClassLibrary.Part();
            dtPart = part.GetAll().Tables[0];

            if (drSupllier == null)
            {


            }
            else
            {
                txtName.Text = drSupllier.ItemArray[1].ToString();
                txtFailiureRate.Text = drSupllier.ItemArray[2].ToString();
                txtPhone.Text = drSupllier.ItemArray[3].ToString();
                txtEmail.Text = drSupllier.ItemArray[4].ToString();
                txtAddress.Text = drSupllier.ItemArray[5].ToString();
                txtPostalCode.Text = drSupllier.ItemArray[6].ToString();
                txtProvince.Text = drSupllier.ItemArray[6].ToString();


                ClassLibrary.SupplierPart supplierpart = new ClassLibrary.SupplierPart();

                supplierpart.SupplierId = Convert.ToInt32(drSupllier.ItemArray[0]);
                DataTable thisTable = supplierpart.GetSupplierPartsBySupplierId().Tables[0];

                foreach (DataRow row in thisTable.Rows)
                {
                    //add to generic list
                    parts.Add(new PartList
                    {
                        partID = Convert.ToInt32(row.ItemArray[0]),
                        partName = row.ItemArray[1].ToString(),
                        price = Convert.ToInt32(row.ItemArray[2]),
                    });
                }
                dataGridView1.DataSource = parts;

            }
        }
        //-----------------------------------------------------------------------------------------------
        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            if (
            (txtName.Text == "") ||
            (txtPhone.Text == "") ||
            (txtEmail.Text == "") ||
            (txtAddress.Text == "")||
            (txtProvince.Text == "")||
            (txtPostalCode.Text == "")
            )
            {
                MessageBox.Show("please fill all the empty fields");
            }
            else if (!Regex.IsMatch(txtPostalCode.Text, "^\\D{1}\\d{1}\\D{1}-\\d{1}\\D{1}\\d{1}$"))
            {
                MessageBox.Show("please enter the correct postal code formatting! A0A-0A0");
            }
            else if (!Regex.IsMatch(txtPhone.Text, "(^(\\+?\\-? *[0-9]+)([,0-9 ]*)([0-9 ])*$)|(^ *$)"))
            {
                MessageBox.Show("please enter a valid phone number");
            }
            else
            {
                ClassLibrary.Supplier supplier = new ClassLibrary.Supplier();
                supplier.Name = txtName.Text;
                supplier.Address = txtAddress.Text;
                supplier.Phone = txtPhone.Text;
                supplier.PostalCode = txtPostalCode.Text;
                supplier.Province = txtProvince.Text;
                supplier.Email = txtEmail.Text;
                if (txtFailiureRate.Text != "")
                {
                    supplier.TimesFailed = Convert.ToInt32(txtFailiureRate.Text);
                }
                //ADD MODE
                if (drSupllier == null)
                {
                    //add supplier
                    int lastInsertedId = supplier.Add();

                }

                //EDIT MODE
                else
                {
                    //update supplier information
                    int supid = Convert.ToInt32(drSupllier.ItemArray[0]);
                    supplier.SupId = supid;
                    supplier.Update();

                }

                //refresh the disCustomer form
                frmDisSupplier.displayAllSuppliers();
                Close();
            }
        }
        //-----------------------------------------------------------------------------------------------

    }
    public class PartList
    {
        public int partID { get; set; }
        public string partName { get; set; }
        public decimal price { get; set; }
    }

}
