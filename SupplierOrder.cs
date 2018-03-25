using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class SupplierOrder:Order
    {
        DataAccess da;
        int orderId;

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public SupplierOrder()
        {
            da = new DataAccess();
        }

        public int Add()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SORDER_id",Id),
            new SqlParameter("@SORDER_date", OrderDate) ,
            new SqlParameter("@SORDER_totalprice", TotalPrice),
            new SqlParameter("@SORDER_status", Status),
            new SqlParameter("@ORDER_id", OrderId),
            };

            param[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertSORDER", param);
        }

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SORDER_id",Id),
            };

            return da.ExecSP(CommandType.StoredProcedure, "usp_DeleteSORDER", param);
        }

        public DataSet GetAll()
        {

            SqlParameter[] param = new SqlParameter[] {
            };

            return da.GetDataSet("SORDERS", "usp_SelectSORDERsAll", param);
        }


        public DataSet GetSorderByOrderId()
        {
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@ORDER_id", OrderId),
            };
            return da.GetDataSet("SORDERS", "usp_SelectSORDERByOrderId", param);
        }
    }
}
