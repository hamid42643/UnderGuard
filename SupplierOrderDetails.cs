using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class SupplierOrderDetails
    {
        int id;
        //int partsupplierId;
        int sorderId;
        int quantity;
        DateTime dateDelivery;
        decimal price;
        int partId;

        public int PartId
        {
            get { return partId; }
            set { partId = value; }
        }
        int supplierId;

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //public int PartsupplierId
        //{
        //    get { return partsupplierId; }
        //    set { partsupplierId = value; }
        //}

        public int SorderId
        {
            get { return sorderId; }
            set { sorderId = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public DateTime DateDelivery
        {
            get { return dateDelivery; }
            set { dateDelivery = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        DataAccess da;

        public SupplierOrderDetails()
        {
            da = new DataAccess();
        }


        public int Add()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SORDERDETAIL_id",Id),
            new SqlParameter("@SORDER_id", SorderId) ,
            new SqlParameter("@PART_id", PartId),
            new SqlParameter("@SUPPLIER_id", SupplierId),
            new SqlParameter("@ORDERDETAIL_quantity", Quantity),
            new SqlParameter("@SORDERDETAIL_deliverytime", DBNull.Value),
            new SqlParameter("@ORDERDETAIL_price", Price),
            };

            param[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertSORDERDETAIL", param);
        }

        public DataSet GetSOrderdetailBySOrderId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SORDER_id", SorderId),
            };

            return da.GetDataSet("SOrderDetails", "usp_SelectSORDERDETAILsBySORDER_id", param);
        }
    }
}
