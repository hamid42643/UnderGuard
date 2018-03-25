using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class SupplierPart
    {
        int id;
        int partId;
        int supplierId;
        decimal price;
        DataAccess da;
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int PartId
        {
            get { return partId; }
            set { partId = value; }
        }

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }


        public SupplierPart()
        {
            da = new DataAccess();
        }

        public int Add()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@PARTSUPPLIER_id", Id),
                new SqlParameter("@PART_id", PartId) ,
                new SqlParameter("@SUPPLIER_id", SupplierId),
                new SqlParameter("@PARTSUPPLIER_cost", Price),
                };

            param2[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertPARTSUPPLIER", param2);

        }

        public int Delete()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@SUPPLIER_id", SupplierId),
                };

            return da.ExecSP(CommandType.StoredProcedure, "usp_DeletePARTSUPPLIERsBySUPPLIER_id", param2);
        }

        public int DeleteSupplierPartsByPartId()
        {
            SqlParameter[] param2 = new SqlParameter[] {
                new SqlParameter("@PART_id", PartId),
                };

            return da.ExecSP(CommandType.StoredProcedure, "usp_DeletePARTSUPPLIERsByPART_id", param2);
        }


        public DataSet GetSupplierPartsBySupplierId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SUPPLIER_id", SupplierId),
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("SUPPLIERPART", "usp_SelectPARTSUPPLIERsBySUPPLIER_id", param);
        }


        public DataSet GetSupplierPartsByPartId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PART_id", PartId),
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("SUPPLIERPART", "usp_SelectPARTSUPPLIERsByPART_id", param);
        }

        public DataSet GetSupplierPartsId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PARTSUPPLIER_id", Id),
            new SqlParameter("@PART_id", PartId),
            new SqlParameter("@SUPPLIER_id", SupplierId),
            };
            param[0].Direction = ParameterDirection.Output;
            return da.GetDataSet("SUPPLIERPART", "usp_SelectPARTSUPPLIERId", param);
        }

    }
}
