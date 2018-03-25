using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;

namespace WindowsFormsApplication
{
    public partial class frmDisSupplierOrder : Form
    {
        DataTable dtParts;
        frmDisOrder formDisOrder;
        DataRow drOrders;
        DataTable dtOrderdetail;
        DataTable dtPart;
        BindingList<PartsInTheOrder2> partList2;
        BindingList<PartsInTheOrder> partList;
        decimal totalOrderPrice;
        int ordId;

        public BindingList<PartsInTheOrder> PartList
        {
            get { return partList; }
            set { partList = value; }
        }
        //-----------------------------------------------------------------------------------------------------
        public BindingList<PartsInTheOrder2> PartList2
        {
            get { return partList2; }
            set { partList2 = value; }
        }
        //-----------------------------------------------------------------------------------------------------
        public frmDisSupplierOrder(frmDisOrder form, DataRow row)
        {
            formDisOrder = form;
            drOrders = row;
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------------
        private void frmDisSupplierOrder_Load(object sender, EventArgs e)
        {
            PartList = new BindingList<PartsInTheOrder>();
            PartList2 = new BindingList<PartsInTheOrder2>();

            ordId = Convert.ToInt32(drOrders.ItemArray[0]);
            ClassLibrary.CustomerOrderDetails orderdetails = new ClassLibrary.CustomerOrderDetails();

            //display all products in the order
            orderdetails.OrderId = ordId;
            dtOrderdetail = orderdetails.GetOrderDetailsByOrderId().Tables[0];
            dataGridViewProducts.DataSource = dtOrderdetail;
            dataGridViewProducts.Columns["PRODUCT_price"].DefaultCellStyle.Format = "c";

            //display all parts in an order
            ClassLibrary.CustomerOrder cusOrder = new ClassLibrary.CustomerOrder();

            cusOrder.Id = ordId;
            dtParts = cusOrder.GetAllPartsByOrderId().Tables[0];

            ClassLibrary.BuildOfMaterial bom = new ClassLibrary.BuildOfMaterial();
            DataTable thisTable = new DataTable();

            foreach (DataRow row in dtOrderdetail.Rows)
            {
                bom.ProductId = Convert.ToInt32(row.ItemArray[0]);
                thisTable = bom.GetAllPartsByProductId().Tables[0];

                foreach (DataRow row2 in thisTable.Rows)
                {
                    PartList.Add(new PartsInTheOrder
                    {
                        ProductName = Convert.ToString(row.ItemArray[1]),
                        PartID = Convert.ToString(row2.ItemArray[2]),
                        PartName = Convert.ToString(row2.ItemArray[0]),
                        PartQuantity = Convert.ToString(row2.ItemArray[1]),
                        productQuantity = Convert.ToString(row.ItemArray[2]),
                        totalQuantity = 
                        Convert.ToString(Convert.ToInt32(row.ItemArray[2]) * Convert.ToInt32(row2.ItemArray[1])),
                    });
                }

                //an empty row
                PartList.Add(new PartsInTheOrder
                {
                    ProductName = "",
                });
            }

            //dataGridViewParts.DataSource = PartList;

            //display parts2 list
            DisplayOrderParts();
        }
        //-----------------------------------------------------------------------------------------------------
        public void DisplayOrderParts()
        {
            dtPart = new DataTable();
            ClassLibrary.Part part = new ClassLibrary.Part();
            int i = 1;

            foreach (DataRow row in dtParts.Rows)
            {
                part.Id = Convert.ToInt32(row.ItemArray[0]);
                dtPart = part.Get().Tables[0];

                foreach (DataRow row2 in dtPart.Rows)
                {
                    PartList2.Add(new PartsInTheOrder2
                    {
                        item = i++,
                        PartID = Convert.ToInt32(row2.ItemArray[0]),
                        PartName = Convert.ToString(row2.ItemArray[1]),
                        PartQuantity = Convert.ToInt32(row.ItemArray[1]),
                    });
                }

            }

            dataGridViewParts2.DataSource = PartList2;

        }
        //-----------------------------------------------------------------------------------------------------
        public void RefreshPartList()
        {
            //add an remove a row to refresh the list
            PartList2.Add(new PartsInTheOrder2
            {
                item = 55,
            });


            PartList2.Remove(PartList2[partList2.IndexOf(partList2.Last())]);
        }
        //-----------------------------------------------------------------------------------------------------
        private void btnEditPart_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridViewParts2.CurrentRow.Cells["PartID"].Value;

                ClassLibrary.Part part = new ClassLibrary.Part();
                part.Id = id;
                DataTable thisTable = part.Get().Tables[0];
                DataRow[] rows = thisTable.Select();

                foreach (DataRow row in rows)
                {
                    frmAddPart f = new frmAddPart(row, this);
                    f.MdiParent = this.MdiParent;
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.Show();
                }
            }
            catch (Exception ex) { }
        }
        //-----------------------------------------------------------------------------------------------------
        private void btnSupplierChooser_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridViewParts2.CurrentRow.Cells["PartID"].Value;
                int index = (int)dataGridViewParts2.CurrentRow.Cells["item"].Value;
                ClassLibrary.Part part = new ClassLibrary.Part();
                part.Id = id;
                DataTable thisTable = part.Get().Tables[0];
                DataRow[] rows = thisTable.Select();

                foreach (DataRow row in rows)
                {
                    frmChooseSupplier f = new frmChooseSupplier(id, index, this);
                    f.MdiParent = this.MdiParent;
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.Show();
                }
            }
            catch (Exception ex) { }
        }
        //-----------------------------------------------------------------------------------------------------
        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.CustomerOrder customerorder = new ClassLibrary.CustomerOrder();
                ClassLibrary.SupplierPart supplierpart = new ClassLibrary.SupplierPart();
                ClassLibrary.SupplierOrder sorder = new ClassLibrary.SupplierOrder();
                ClassLibrary.SupplierOrderDetails sorderdetail = new ClassLibrary.SupplierOrderDetails();

                if (CheckSuppliersExists(supplierpart) == true)
                {
                    //update customer order status to 'sent to suppliers'
                    customerorder.Id = ordId;
                    customerorder.Status = "Sent To Suppliers";
                    customerorder.UpdateOrderStatus();

                    //add sorder
                    sorder.OrderId = ordId;
                    sorder.OrderDate = DateTime.Today;
                    sorder.Status = "";
                    sorder.TotalPrice = totalOrderPrice;
                    int lastInsertId = sorder.Add();

                    foreach (PartsInTheOrder2 item in partList2)
                    { 
                        //supplierpart.PartId = item.PartID;
                        //supplierpart.SupplierId = item.SupplierID;
                        //DataTable thisTable = supplierpart.GetSupplierPartsId().Tables[0];
                        //int supplierpartId = (int)thisTable.Rows[0].ItemArray[0];

                        //add sorder details 
                        
                        sorderdetail.PartId = item.PartID;
                        sorderdetail.SupplierId = item.SupplierID;

                        sorderdetail.SorderId = lastInsertId;
                        sorderdetail.Price = 
                            item.PartQuantity * Convert.ToDecimal(item.SupplierPrice.Replace("$", ""));
                        sorderdetail.Quantity = item.PartQuantity;
                        sorderdetail.DateDelivery = DateTime.MinValue;
                        sorderdetail.Add();

                    }
                    MessageBox.Show("an order has been made successfully!",
                    "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formDisOrder.DisplayAllOrders();
                    Close();
                }
            }
            catch (Exception ex) { }

        }
        //-----------------------------------------------------------------------------------------------------
        public void CalculateTotalPrice()
        {
            decimal prices = 0;
            foreach (PartsInTheOrder2 part in partList2)
            {
                if (part.SupplierPrice != null)
                {
                    decimal price = Convert.ToDecimal(part.SupplierPrice.Replace("$", ""));
                    prices = prices + (price*part.PartQuantity);
                }
            }
            totalOrderPrice = prices;
            txtTotalPrice.Text = String.Format("{0:c}", totalOrderPrice);
        }
        //-----------------------------------------------------------------------------------------------------
        private bool CheckSuppliersExists(SupplierPart supplierpart)
        {
            //check to find out a supplier exists for all parts in the list
            foreach (PartsInTheOrder2 item in partList2)
            {
                supplierpart.PartId = item.PartID;
                supplierpart.SupplierId = item.SupplierID;
                DataTable dt = supplierpart.GetSupplierPartsId().Tables[0];

                if (dt.Rows.Count == 0)
                {
                    DialogResult dlgResult = MessageBox.Show
                        ("Please choose a supplier for all the parts in the order",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        //-----------------------------------------------------------------------------------------------------

        private void btn_OrderDetails_Click(object sender, EventArgs e)
        {
            
            frmDisSupplierOrder_Parts f = new frmDisSupplierOrder_Parts(this);
            f.MdiParent = this.MdiParent;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
        //-----------------------------------------------------------------------------------------------------

    }
    public class PartsInTheOrder
    {
        public string ProductName { get; set; }
        public string PartID { get; set; }
        public string PartName { get; set; }
        public string PartQuantity { get; set; }
        public string productQuantity { get; set; }
        public string totalQuantity { get; set; }
    }
    //in this dublicate parts are combined
    public class PartsInTheOrder2
    {
        public int item { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public int PartQuantity { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPrice { get; set; }
        public int SupplierID { get; set; }
    }
}
