using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class CustomerOrderDetails
    {
        int id;


        int productId;


        int orderId;


        int quantity;


        decimal price;


        DateTime dateDelivery;

        DataAccess da;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public CustomerOrderDetails()
        {
            da = new DataAccess();
        }

        public int AddOrderDetails()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@ORDERDETAIL_id", Id),
                new SqlParameter("@PRODUCT_id", ProductId) ,
                new SqlParameter("@ORDER_id", OrderId),
                new SqlParameter("@ORDERDETAIL_quantity", Quantity),
                new SqlParameter("@ORDERDETAIL_deliverytime", DBNull.Value),
                new SqlParameter("@ORDERDETAIL_price", Price),
                };

            param2[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertORDERDETAIL", param2);

        }


        public DataSet GetOrderDetailsByOrderId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@ORDER_id", OrderId),
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("OrderDetails", "usp_SelectORDERDETAILsByORDER_id", param);
        }

        public int Delete()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@ORDER_id", OrderId),
                };

            return da.ExecSP(CommandType.StoredProcedure, "usp_DeleteORDERDETAILsByORDER_id", param2);
        }
    }
}
