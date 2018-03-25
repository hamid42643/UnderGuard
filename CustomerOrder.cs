using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class CustomerOrder:Order
    {

        //int productId;
        int custId;
        int lastOrderId;
        int quantity;
        private DateTime dateDelivery;

        public DateTime DateDelivery
        {
            get { return dateDelivery; }
            set { dateDelivery = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int LastOrderId
        {
            get { return lastOrderId; }
            set { lastOrderId = value; }
        } 

        public int CustId
        {
            get { return custId; }
            set { custId = value; }
        }

        //public int ProductId
        //{
        //    get { return productId; }
        //    set { productId = value; }
        //}

        public CustomerOrder()
        {
            da = new DataAccess();
        }

        public DataSet GetAllOrders()
        {
            SqlParameter[] param = new SqlParameter[] {
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("Orders", "usp_SelectORDERsAll", param);
        }


        public int Add()
        {

            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@ORDER_id",Id),
            new SqlParameter("@CUSTOMER_id", CustId) ,
            new SqlParameter("@ORDER_date", OrderDate),
            new SqlParameter("@ORDER_totalprice", TotalPrice),
            new SqlParameter("@ORDER_status", Status) 
            };

            param[0].Direction = ParameterDirection.Output;

            return LastOrderId = da.ExecSP(CommandType.StoredProcedure, "usp_InsertORDER", param);

        }

        public int Update()
        {
            SqlParameter[] param;
            if(DateDelivery == DateTime.MinValue)
            {
                param = new SqlParameter[] {
                new SqlParameter("@ORDER_id",Id),
                new SqlParameter("@CUSTOMER_id", CustId) ,
                new SqlParameter("@ORDER_date", OrderDate),
                new SqlParameter("@ORDER_totalprice", TotalPrice),
                new SqlParameter("@ORDER_deliveryDate", DBNull.Value),
                new SqlParameter("@ORDER_status", Status) 
                };
            }
            else
            {
                param = new SqlParameter[] {
                new SqlParameter("@ORDER_id",Id),
                new SqlParameter("@CUSTOMER_id", CustId) ,
                new SqlParameter("@ORDER_date", OrderDate),
                new SqlParameter("@ORDER_totalprice", TotalPrice),
                new SqlParameter("@ORDER_deliveryDate", dateDelivery),
                new SqlParameter("@ORDER_status", Status) 
                };
            }


            return LastOrderId = da.ExecSP(CommandType.StoredProcedure, "usp_UpdateORDER", param);
        }

        public int UpdateOrderStatus()
        {

            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@ORDER_id",Id),
            new SqlParameter("@ORDER_status", Status) 
            };

            return LastOrderId = da.ExecSP(CommandType.StoredProcedure, "usp_UpdateORDERStatus", param);

        }


        public int Delete()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@ORDER_id", Id),
                };

            return da.ExecSP(CommandType.StoredProcedure, "usp_DeleteORDER", param2);
        }

        //this method gets all parts in an order but it groups together parts 
        //with same ids and sums up the quantity
        public DataSet GetAllPartsByOrderId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@ORDER_id",Id),
            };

            return da.GetDataSet("AllPartsByOrderId", "usp_SelectPartsALLByORDER_id", param);
        }
    }
}
