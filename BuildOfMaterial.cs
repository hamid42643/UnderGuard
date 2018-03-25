using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ClassLibrary
{
    public class BuildOfMaterial
    {
        DataAccess da;

        int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        int partID;

        public int PartID
        {
            get { return partID; }
            set { partID = value; }
        }

        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        int partQuantity;

        public int PartQuantity
        {
            get { return partQuantity; }
            set { partQuantity = value; }
        }

        

        public BuildOfMaterial()
        {
            da = new DataAccess();
        }


        public int Add()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@BOM_id",Id),
            new SqlParameter("@PRODUCT_id", ProductId) ,
            new SqlParameter("@PART_id", PartID),
            new SqlParameter("@BOM_partquantity", partQuantity),
            };

            param[0].Direction = ParameterDirection.Output;
            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertBOM", param);

        }


        public DataSet GetAllPartsByProductId()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id", ProductId) ,
            };

            return da.GetDataSet("GetAllPartsRelated", "usp_SelectBOMsByPRODUCT_id", param);
        }



        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PRODUCT_id", ProductId) ,
            };


            return da.ExecSP(CommandType.StoredProcedure, "usp_DeleteBOMsByPRODUCT_id", param);

        }
    }

}
