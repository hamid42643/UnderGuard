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
    public partial class frmAddCustomer : Form
    {

        DataRow drCustomer;
        frmDisCustomer disCustForm;

        public frmAddCustomer(frmDisCustomer form)
        {
            disCustForm = form;
            InitializeComponent();
        }

        public frmAddCustomer(DataRow drCus, frmDisCustomer form)
        {
            disCustForm = form;
            drCustomer = drCus;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (
            (txtCUSTOMER_name.Text == "") ||
            (txtCUSTOMER_phone.Text == "") ||
            (txtCUSTOMER_address.Text == "") ||
            (txtCUSTOMER_email.Text == "")
            )
            {
                MessageBox.Show("please fill all the empty fields");
            }
            else if (!Regex.IsMatch(txtCUSTOMER_email.Text, "^[\\w-]+@([\\w-]+\\.)+[\\w-]+$"))
            {
                MessageBox.Show("please enter a valid email address");
            }
            else if (!Regex.IsMatch(txtCUSTOMER_phone.Text, "(^(\\+?\\-? *[0-9]+)([,0-9 ]*)([0-9 ])*$)|(^ *$)"))
            {
                MessageBox.Show("please enter a valid phone number");
            }
            else
            {

                ClassLibrary.Customer customer = new ClassLibrary.Customer();
                customer.Name = txtCUSTOMER_name.Text;
                customer.Phone = txtCUSTOMER_phone.Text;
                customer.Address = txtCUSTOMER_address.Text;
                customer.Email = txtCUSTOMER_email.Text;

                //add mode
                if (drCustomer == null)
                {
                    int id = customer.Add();
                    //MessageBox.Show(id.ToString());
                }

                //edit mode
                else
                {
                    customer.Id = Convert.ToInt32(drCustomer.ItemArray[0]);
                    int id = customer.Update();
                    //MessageBox.Show(id.ToString());
                }

                //refresh the disCustomer form
                disCustForm.displayAllCustomers();

                Close();
            }
        }

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {


            //add mode
            if (drCustomer == null)
            {

            }

            //edit mode
            else
            {
                txtCUSTOMER_name.Text = drCustomer.ItemArray[1].ToString();
                txtCUSTOMER_phone.Text = drCustomer.ItemArray[2].ToString();
                txtCUSTOMER_email.Text = drCustomer.ItemArray[3].ToString();
                txtCUSTOMER_address.Text = drCustomer.ItemArray[4].ToString();

            }
        }
    }
}
