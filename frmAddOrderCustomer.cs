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
    public partial class frmAddOrderCustomer : Form
    {
        BindingList<OrderDetails> ord;
        Decimal totalPrice;

        DataSet dsCustomer;
        DataSet dsProduct;

        DataTable dtCustomer;
        DataTable dtProduct;

        frmDisOrder frmDisOrder;
        DataRow drOrder;
        bool saveDeliveryDate = false;
        //----------------------------------------------------------------------------------------------
        //constructor 1
        public frmAddOrderCustomer(frmDisOrder form)
        {
            frmDisOrder = form;
            InitializeComponent();
        }
        //----------------------------------------------------------------------------------------------
        //constructor 2
        public frmAddOrderCustomer(frmDisOrder form, DataRow row)
        {
            drOrder = row;
            frmDisOrder = form;
            InitializeComponent();
        }
        //----------------------------------------------------------------------------------------------
        private void MakeOrderUnEditable()
        {
            txtPrice.Enabled = false;
            txtStatus.Enabled = false;
            cmbCustomers.Enabled = false;
            btnAdd.Enabled = false;
            btnRemoveFromOrder.Enabled = false;
            btnSaveToDatabase.Enabled = false;
            cmbProducts.Enabled = false;
            txtQuantity.Enabled = false;
            dataGridView1.Enabled = false;
        }
        //----------------------------------------------------------------------------------------------
        private void frmAddOrderCustomer_Load(object sender, EventArgs e)
        {

            //make order list
            ord = new BindingList<OrderDetails>();

            //add customers to the combobox
            ClassLibrary.Customer customer = new ClassLibrary.Customer();
            dsCustomer = customer.GetAll();
            dtCustomer = dsCustomer.Tables[0];
            cmbCustomers.DataSource = dtCustomer;
            cmbCustomers.DisplayMember = "CUSTOMER_name";
            cmbCustomers.ValueMember = "CUSTOMER_id";

            //add products to the combobox
            ClassLibrary.Product product = new ClassLibrary.Product();
            dsProduct = product.GetAll();
            dtProduct = dsProduct.Tables[0];
            cmbProducts.DataSource = dtProduct;
            cmbProducts.DisplayMember = "PRODUCT_name";

            //ADDING MODE
            if (drOrder == null)
            {
                dateTimePicker1.Enabled = false;

            }
            //EDITING MODE
            else
            {
                

                //if orderid is in the sorder table
                ClassLibrary.SupplierOrder sorder = new ClassLibrary.SupplierOrder();
                sorder.OrderId = Convert.ToInt32(drOrder.ItemArray[0]);
                DataTable dt = sorder.GetSorderByOrderId().Tables[0];

                //disables forms items
                if (dt.Rows.Count != 0)
                {
                    MakeOrderUnEditable();
                    saveDeliveryDate = true;
                    dateTimePicker1.Enabled = true ;
                }

                //select the customer combobox
                string custId = drOrder.ItemArray[1].ToString();
                try
                {
                    cmbCustomers.SelectedIndex = cmbCustomers.FindStringExact(custId);
                }
                catch (Exception ex)
                {

                }
                //display date
                dateTimePicker1.Text = drOrder.ItemArray[5].ToString() ;

                //display total price
                totalPrice = Convert.ToDecimal(drOrder.ItemArray[3]);
                txtPrice.Text = String.Format("{0:c}", totalPrice);
                txtStatus.Text = drOrder.ItemArray[4].ToString();

                //display orderdetails
                ClassLibrary.CustomerOrderDetails orderdetails = new ClassLibrary.CustomerOrderDetails();
                orderdetails.OrderId = Convert.ToInt32(drOrder.ItemArray[0]);

                DataTable thisTable = orderdetails.GetOrderDetailsByOrderId().Tables[0];

                try
                {
                    foreach (DataRow row in thisTable.Rows)
                    {
                        string price = String.Format("{0:c}", Convert.ToDecimal(row.ItemArray[3]));
                        //add to generic list
                        ord.Add(new OrderDetails
                        {
                            ProductID = Convert.ToInt32(row.ItemArray[0]),
                            ProductName = row.ItemArray[1].ToString(),
                            Quantity = Convert.ToInt32(row.ItemArray[2]),
                            Price = price,

                        });
                    }
                    dataGridView1.DataSource = ord;
                }
                catch (Exception ex)
                {
                }
            }
        }
        //----------------------------------------------------------------------------------------------
        private void UpdateOrderDetailes()
        {
            //dtOrderDetails = dsOrderDetails.Tables[0];
            //dataGridView1.DataSource = ord;

        }
        //----------------------------------------------------------------------------------------------
        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            //get product id
            DataRowView selectedProduct = (DataRowView)cmbProducts.SelectedItem;
            int productId = (int)selectedProduct.Row.ItemArray[0];

            try
            {
                int quantity = Convert.ToInt32(txtQuantity.Text);
                decimal price = (decimal)selectedProduct.Row.ItemArray[3];
                decimal total = price * quantity;


                string name = (string)selectedProduct.Row.ItemArray[1];

                if (IsNotInList(productId))
                {
                    //add to OrderDetails List
                    ord.Add(new OrderDetails
                    {
                        ProductID = productId,
                        ProductName = name,
                        Quantity = quantity,
                        Price = String.Format("{0:c}", price),
                    });

                    //calculate total price and display it on the textbox
                    dataGridView1.DataSource = ord;
                    totalPrice += price * quantity;
                    txtPrice.Text = String.Format("{0:c}", totalPrice);
                }
                else
                {
                    MessageBox.Show("the product is already in the list!");
                }
            }catch{}
        }
        //-------------------------------------------------------------------
        private bool IsNotInList(int id)
        {
            foreach (OrderDetails o in ord)
            {
                if (o.ProductID == id)
                {
                    return false;
                }
            }
            return true;
        }
        //-------------------------------------------------------------------
        private void btnRemoveFromOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //reduce the total price and update the text box
                decimal price = Convert.ToDecimal
                    (dataGridView1.CurrentRow.Cells["Price"].Value.ToString().Replace("$",""));
                int quantity = (int)dataGridView1.CurrentRow.Cells["Quantity"].Value;
                totalPrice = totalPrice - (price * quantity);
                txtPrice.Text = String.Format("{0:c}",totalPrice);

                //remove from the list
                int id = dataGridView1.CurrentRow.Index;
                ord.Remove(ord[id]);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            ClassLibrary.CustomerOrder order = new ClassLibrary.CustomerOrder();
            DataRowView selectedCustomer = (DataRowView)cmbCustomers.SelectedItem;

            //add order
            order.CustId = (int)selectedCustomer.Row.ItemArray[0];
            order.OrderDate = Convert.ToDateTime(cmbDate.Text);
            order.TotalPrice = totalPrice;
            order.Status = txtStatus.Text;
            
            //ADDING MODE
            if (drOrder == null)
            {
                
                int lastInsertedId = order.Add();

                //add OrderDetails
                ClassLibrary.CustomerOrderDetails orderdetails = new ClassLibrary.CustomerOrderDetails();

                orderdetails.OrderId = lastInsertedId;
                foreach (OrderDetails o in ord)
                {

                    orderdetails.Price = Convert.ToDecimal(o.Price.ToString().Replace("$", ""));
                    orderdetails.ProductId = o.ProductID;
                    orderdetails.Quantity = o.Quantity;
                    orderdetails.AddOrderDetails();
                }
            }

            //EDITING MODE
            else
            {
                try
                {
                    if (dateTimePicker1.Enabled == true)
                    {
                        order.DateDelivery = Convert.ToDateTime(dateTimePicker1.Text);
                    }
                    else
                    {
                        order.DateDelivery = DateTime.MinValue;
                    }
                    order.Id = Convert.ToInt32(drOrder.ItemArray[0]);
                    order.Update();

                    //add OrderDetails
                    ClassLibrary.CustomerOrderDetails orderdetails = new ClassLibrary.CustomerOrderDetails();
                    orderdetails.OrderId = order.Id;
                    orderdetails.Delete();

                    foreach (OrderDetails o in ord)
                    {
                        orderdetails.ProductId = o.ProductID;
                        orderdetails.Quantity = o.Quantity;
                        orderdetails.AddOrderDetails();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            frmDisOrder.DisplayAllOrders();
            Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            btnSaveToDatabase.Enabled = true;
        }

    }
    //----------------------------------------------------------------------------------------------
    public class OrderDetails
    {
        //public int ItemNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
    }
}
