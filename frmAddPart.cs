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
    public partial class frmAddPart : Form
    {
        DataRow drRow;
        frmDisParts frmDisParts;
        frmDisSupplierOrder formDisSupplierOrder;
        DataTable dtSupplier;
        BindingList<SupplierList> supplerlist;
        int id;

        //constructor 1 takes a datarow to edit
        public frmAddPart(DataRow drRowToEdit, frmDisParts form)
        {
            frmDisParts = form;
            drRow = drRowToEdit;
            InitializeComponent();
        }

        public frmAddPart(DataRow drRowToEdit, frmDisSupplierOrder form)
        {
            formDisSupplierOrder = form;
            drRow = drRowToEdit;
            InitializeComponent();

        }

        public frmAddPart(frmDisParts form)
        {
            frmDisParts = form;
            InitializeComponent();
        }


        //-------------------------------------------------------------------------------------------------
        private void frmAddPart_Load(object sender, EventArgs e)
        {
            supplerlist = new BindingList<SupplierList>(); 

            //add parts to the combobox
            ClassLibrary.Supplier supplier = new ClassLibrary.Supplier();
            dtSupplier = supplier.GetAll().Tables[0];
            cmbSuppliers.DataSource = dtSupplier;
            cmbSuppliers.DisplayMember = "SUPPLIER_name";



            if (drRow == null)
            {

            }
            else
            {
                txtName.Text = drRow.ItemArray[1].ToString();
                txtColor.Text = drRow.ItemArray[2].ToString();

                ClassLibrary.SupplierPart supplierpart = new ClassLibrary.SupplierPart();
                supplierpart.PartId = Convert.ToInt32(drRow.ItemArray[0]);
                DataTable thisTable = supplierpart.GetSupplierPartsByPartId().Tables[0];
                foreach (DataRow row in thisTable.Rows)
                {
                    //add to generic list
                    supplerlist.Add(new SupplierList
                    {
                        SupplierID = Convert.ToInt32(row.ItemArray[0]),
                        SupplierName = row.ItemArray[1].ToString(),
                        price = String.Format("{0:c}", row.ItemArray[2]),
                    });
                }
                dataGridView1.DataSource = supplerlist;
            }
        }
        //-------------------------------------------------------------------------------------------------
        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {

            if (
            (txtName.Text == "")
            )
            {
                MessageBox.Show("please fill all the empty fields");
            }

            else
            {
                try
                {
                    //ADD MODE
                    if (drRow == null)
                    {
                        ClassLibrary.Part part = new ClassLibrary.Part();

                        part.Name = txtName.Text;
                        part.Color = txtColor.Text;
                        int lastInsertedId = part.Add();

                        //add Suppliers
                        ClassLibrary.SupplierPart supplierPart = new ClassLibrary.SupplierPart();
                        supplierPart.PartId = lastInsertedId;
                        foreach (SupplierList s in supplerlist)
                        {
                            supplierPart.SupplierId = s.SupplierID;
                            supplierPart.Price = Convert.ToDecimal(s.price.Replace("$", ""));
                            supplierPart.Add();
                        }
                    }

                    //EDIT MODE
                    else if (drRow != null)
                    {
                        ClassLibrary.Part part = new ClassLibrary.Part();
                        part.Id = Convert.ToInt32(drRow.ItemArray[0]);
                        part.Name = txtName.Text;
                        part.Color = txtColor.Text;
                        part.Update();

                        //save supplier list to the database
                        ClassLibrary.SupplierPart supplierPart = new ClassLibrary.SupplierPart();
                        supplierPart.PartId = part.Id;
                        supplierPart.DeleteSupplierPartsByPartId();

                        foreach (SupplierList s in supplerlist)
                        {
                            supplierPart.SupplierId = s.SupplierID;
                            supplierPart.Price = Convert.ToDecimal(s.price.Replace("$", ""));
                            supplierPart.Add();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                if (formDisSupplierOrder != null)
                {
                    //formDisSupplierOrder.DisplayOrderParts();
                }
                else
                {
                    frmDisParts.DisplayAllParts();
                }
                
                Close();
            }

        }
        //-------------------------------------------------------------------
        private bool IsNotInList(int id)
        {
            foreach (SupplierList supplier in supplerlist)
            {
                if (supplier.SupplierID == id)
                {
                    return false;
                }
            }
            return true;
        }
        //-------------------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //get product id
                DataRowView selectedProduct = (DataRowView)cmbSuppliers.SelectedItem;
                int supplierId = (int)selectedProduct.Row.ItemArray[0];
                string supplierName = selectedProduct.Row.ItemArray[1].ToString();

                string cost = String.Format("{0:c}", Convert.ToDecimal(txtPrice.Text));

                if (IsNotInList(supplierId))
                {
                    //add to OrderDetails List
                    supplerlist.Add(new SupplierList
                    {
                        SupplierID = supplierId,
                        SupplierName = supplierName,
                        price = cost,
                    });


                    dataGridView1.DataSource = supplerlist;
                }
                else
                {
                    MessageBox.Show("supllier is already in the list");
                }
            }
            catch { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            try
            {
                //remove from the list
                int id = dataGridView1.CurrentRow.Index;
                supplerlist.Remove(supplerlist[id]);
            }
            catch (Exception ex)
            {

            }
        }
        //-------------------------------------------------------------------------------------------------

        public class SupplierList
        {
            public int SupplierID { get; set; }
            public string SupplierName { get; set; }
            public string price { get; set; }
        }

    }
}
