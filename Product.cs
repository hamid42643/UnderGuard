using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ClassLibrary
{
    public class Product
    {
        int id;
        String name;
        Decimal cost;
        Decimal price;
        String color;
        String description;
        string pic;
        DataAccess da;

        //int partQuantity;
        //int partID;
        //public int PartQuantity
        //{
        //    get { return partQuantity; }
        //    set { partQuantity = value; }
        //}

        //public int PartID
        //{
        //    get { return partID; }
        //    set { partID = value; }
        //}
        public string Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }        
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
       
        public Decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }        

        public Decimal Price
        {
            get { return price; }
            set { price = value; }
        }   

        public String Color
        {
            get { return color; }
            set { color = value; }
        }
        
        public String Description
        {
            get { return description; }
            set { description = value; }
        }


        public Product()
        {
            da = new DataAccess();
        }
        public int Add()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id",Id),
            new SqlParameter("@PRODUCT_name", name) ,
            new SqlParameter("@PRODUCT_cost", cost),
            new SqlParameter("@PRODUCT_price", price),
            new SqlParameter("@PRODUCT_color", color),
            new SqlParameter("@PRODUCT_description", description),
            new SqlParameter("@PRODUCT_picture", pic),
            };

            param[0].Direction = ParameterDirection.Output;

            int i = da.ExecSP(CommandType.StoredProcedure, "usp_InsertPRODUCT", param);

            return i;
        }

        public int Update()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id",Id),
            new SqlParameter("@PRODUCT_name", name) ,
            new SqlParameter("@PRODUCT_cost", cost),
            new SqlParameter("@PRODUCT_price", price),
            new SqlParameter("@PRODUCT_color", color),
            new SqlParameter("@PRODUCT_description", description),
            new SqlParameter("@PRODUCT_picture", pic)
            };

            return da.ExecSP(CommandType.StoredProcedure, "usp_updatePRODUCT", param);
        }

        public DataSet GetAll()
        {

            SqlParameter[] param = new SqlParameter[] {
            };

            return da.GetDataSet("Product", "usp_SelectPRODUCTsAll", param);
        }



        public List<Product> WebGetAll()
        {
            SqlParameter[] param = new SqlParameter[] {
            };
            List<Product> prod = new List<Product>();

            DataSet ds = da.GetDataSet("Product", "usp_SelectPRODUCTsAll", param);

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                prod.Add(new Product
                {
                    Id = Convert.ToInt32(row.ItemArray[0]),
                    name = Convert.ToString(row.ItemArray[1]),
                    cost = Convert.ToInt32(row.ItemArray[2]),
                    price = Convert.ToInt32(row.ItemArray[3]),
                    color = Convert.ToString(row.ItemArray[4]),
                    description = Convert.ToString(row.ItemArray[5]),
                    pic = Convert.ToString(row.ItemArray[6])
                });
            }
            return prod;
        }

        public DataSet Get()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id",Id),

            };

            return da.GetDataSet("Product", "usp_SelectPRODUCT", param);
        }

        public void Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id",Id),
            };

            param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, "usp_DeletePRODUCT", param);
        }


    }
}
