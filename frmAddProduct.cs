using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApplication
{
    public partial class frmAddProduct : Form
    {
        frmDisProducts frmDisProducts;
        DataRow drCustomer;
        DataTable dtParts;
        string imagePath;
        string imageName;

        //save the pictures on the root of the website
        string picturesPath = "../../../../WebApplication2/pics/";

        BindingList<ProductBuildSheet> BOM;
        //-------------------------------------------------------------------
        public frmAddProduct(frmDisProducts form)
        {
            frmDisProducts = form;
            InitializeComponent();
        }

        //-------------------------------------------------------------------
        public frmAddProduct(frmDisProducts form, DataRow row)
        {
            drCustomer = row;
            frmDisProducts = form;
            InitializeComponent();
        }

        //-------------------------------------------------------------------
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            BOM = new BindingList<ProductBuildSheet>();
            //add Parts to the combobox
            ClassLibrary.Part part = new ClassLibrary.Part();

            dtParts = part.GetAll().Tables[0];
            cmbParts.DataSource = dtParts;
            cmbParts.DisplayMember = "PART_name";

            //ADDING MODE
            if (drCustomer == null)
            {


            }
            //EDITING MODE
            else
            {
                try
                {
                    //System.Drawing.Image _image = System.Drawing.Image.FromStream
                    //    (new System.IO.MemoryStream((byte[])drCustomer[6]));

                    //byte[] blob = (byte[])drCustomer[6];
                    //picProductImage.Image = _image;
                    //picProductImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    //picProductImage.Refresh();

                    picProductImage.Image = Image.FromFile(picturesPath + drCustomer[6].ToString());
                    picProductImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    picProductImage.Refresh();
                }
                catch (Exception ex) {}
                

                //display product information
                txtName.Text = drCustomer.ItemArray[1].ToString();
                txtCost.Text = String.Format("{0:c}", drCustomer.ItemArray[2]);
                txtPrice.Text = String.Format("{0:c}", drCustomer.ItemArray[3]);
                txtColor.Text = drCustomer.ItemArray[4].ToString();
                txtDescription.Text = drCustomer.ItemArray[5].ToString();

                //display buildsheet
                ClassLibrary.BuildOfMaterial BuildOfMaterial = new ClassLibrary.BuildOfMaterial();
                BuildOfMaterial.ProductId = Convert.ToInt32(drCustomer.ItemArray[0]);
                DataTable thisTable = BuildOfMaterial.GetAllPartsByProductId().Tables[0];


                foreach (DataRow row in thisTable.Rows)
                {
                    //add to generic list
                    BOM.Add(new ProductBuildSheet
                    {
                        PartID = Convert.ToInt32(row.ItemArray[2]),
                        PartName = row.ItemArray[0].ToString(),
                        Quantity = Convert.ToInt32(row.ItemArray[1]),
                    });
                }

                dataGridView1.DataSource = BOM;
            }
        }
        //-------------------------------------------------------------------
        private void btnAddPartToProduct_Click(object sender, EventArgs e)
        {
            try
            {
                //get product id
                DataRowView selectedPart = (DataRowView)cmbParts.SelectedItem;
                int partId = (int)selectedPart.Row.ItemArray[0];
                string partName = (string)selectedPart.Row.ItemArray[1];
                int quantity = Convert.ToInt32(txtQuantity.Text);

                if (IsNotInList(partId))
                {
                    //add to OrderDetails List
                    BOM.Add(new ProductBuildSheet
                    {
                        PartID = partId,
                        PartName = partName,
                        Quantity = quantity,
                    });

                    dataGridView1.DataSource = BOM;
                }
                else
                {
                    MessageBox.Show("the part is already in the list!");
                }
            }
            catch { }
        }
        //-------------------------------------------------------------------
        private bool IsNotInList(int id)
        {
            foreach (ProductBuildSheet part in BOM)
            {
                if (part.PartID == id)
                {
                    return false;
                }
            }
            return true;
        }
        //-------------------------------------------------------------------
        private void btnRemovePartFromProduct_Click(object sender, EventArgs e)
        {
            try
            {
                //remove from the list
                int id = dataGridView1.CurrentRow.Index;
                BOM.Remove(BOM[id]);
            }
            catch (Exception ex)
            {

            }
        }

        //-------------------------------------------------------------------
        private byte[] SetProtuctImage()
        {
            byte[] picbyte = new byte[0];
            //use filestream object to read the image.
            //read to the full length of image to a byte array.
            //add this byte as an oracle parameter and insert it into database.
            try
            {
                //proceed only when the image has a valid path
                if (imagePath != null)
                {
                    FileStream fs;
                    fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    //a byte array to read the image
                    picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                }
                else
                {
                    //if the image textbox is empty return the same image in the datarow
                    if ((byte[])drCustomer[6] != null)
                    {
                        return (byte[])drCustomer[6];
                    }
                }
            }
            catch (Exception ex)
            {
                byte[] zerobyte = new byte[0];
                return zerobyte;
            }
            return picbyte;
        }

        //-------------------------------------------------------------------
        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            decimal result;
            if (
            (txtName.Text == "") ||
            (txtPrice.Text == "") ||
            (txtCost.Text == "") ||
            (txtDescription.Text == "")
            )
            {
                MessageBox.Show("please fill all the empty fields");
            }
            else if (!decimal.TryParse(txtPrice.Text.Replace("$", ""), out result))
            {
                MessageBox.Show("please enter a valid number for 'price'");
            }
            else if (!decimal.TryParse(txtCost.Text.Replace("$", ""), out result))
            {
                MessageBox.Show("please enter a valid number for 'Cost'");
            }
            else
            {
                try
                {
                    ClassLibrary.Product product = new ClassLibrary.Product();
                    product.Color = txtColor.Text;
                    product.Cost = Convert.ToDecimal(txtCost.Text.Replace("$", ""));
                    product.Description = txtDescription.Text;
                    product.Name = txtName.Text;
                    product.Price = Convert.ToDecimal(txtPrice.Text.Replace("$", ""));



                    //ADDING MODE
                    if (drCustomer == null)
                    {
                        if (imageName != null)
                        {
                            product.Pic = imageName;
                            //copy image to the root directory of the application
                            System.IO.File.Copy(imagePath, picturesPath + imageName, true);
                        }
                        else
                        {
                            product.Pic = "";
                        }
                        //add product
                        int LastAddedId = product.Add();

                        // product buildsheet
                        ClassLibrary.BuildOfMaterial BuildOfMaterial = new ClassLibrary.BuildOfMaterial();
                        BuildOfMaterial.ProductId = LastAddedId;

                        foreach (ProductBuildSheet bom in BOM)
                        {
                            BuildOfMaterial.PartID = bom.PartID;
                            BuildOfMaterial.PartQuantity = bom.Quantity;
                            BuildOfMaterial.Add();
                        }
                    }
                    //EDITING MODE
                    else
                    {
                        //add image
                        if (imageName != null)
                        {
                            product.Pic = imageName;
                            //copy image to the root directory of the application
                            System.IO.File.Copy(imagePath, picturesPath + imageName, true);
                        }
                        else
                        {
                            //use the same image name you got when form was loaded
                            product.Pic = drCustomer.ItemArray[6].ToString();
                        }

                        //update product information
                        product.Id = Convert.ToInt32(drCustomer.ItemArray[0]);
                        product.Update();

                        //update product buildsheet
                        ClassLibrary.BuildOfMaterial BuildOfMaterial = new ClassLibrary.BuildOfMaterial();
                        BuildOfMaterial.ProductId = product.Id;
                        BuildOfMaterial.Delete();

                        foreach (ProductBuildSheet bom in BOM)
                        {
                            BuildOfMaterial.PartID = bom.PartID;
                            BuildOfMaterial.PartQuantity = bom.Quantity;
                            BuildOfMaterial.Add();
                        }
                    }

                    frmDisProducts.DisplayAllProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                Close();
            }
        }
        //-------------------------------------------------------------------
        private void btnBrowse_Click(object sender, EventArgs e)
        { 
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                //specify your own initial 
                fldlg.InitialDirectory = @":D\";
                //this will allow only those file extensions to be added
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                if (fldlg.ShowDialog() == DialogResult.OK)
                {
                    imagePath = fldlg.FileName;
                    Bitmap newimg = new Bitmap(imagePath);
                    picProductImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    picProductImage.Image = (Image)newimg;
                    txtImage.Text = imagePath;

                    imageName = System.IO.Path.GetFileName(imagePath);
                    string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    string fileExt = System.IO.Path.GetExtension(imagePath);
                    
                    int i = 0;

                    while (System.IO.File.Exists(picturesPath + imageName))
                    {
                        i++;
                        imageName = fileNameWithoutExt + "(" + i.ToString() + ")" + fileExt;
                    }

                    //System.IO.File.Copy(imagePath, picturesPath + imageName, true);
                }
                fldlg = null;
            }

            catch (System.ArgumentException ae)
            {
                imagePath = " ";
                MessageBox.Show(ae.Message.ToString());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //-------------------------------------------------------------------


    }

    //-------------------------------------------------------------------
    public class ProductBuildSheet
    {
        //public int ItemNumber { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; }
    }
}
