using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary
{
    public class Supplier : Contact
    {
        int timesFailed;
        int supId;

        DataAccess da;

        public Supplier()
        {
            da = new DataAccess();
        }

        public int TimesFailed
        {
            get { return timesFailed; }
            set { timesFailed = value; }
        }

        public int SupId
        {
            get { return supId; }
            set { supId = value; }
        }

        public int Add()
        {
            Id = 0;
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SUPPLIER_id",SupId),
            new SqlParameter("@SUPPLIER_name", name) ,
            new SqlParameter("@SUPPLIER_phone", phone),
            new SqlParameter("@SUPPLIER_email", email),
            new SqlParameter("@SUPPLIER_address", address),
            new SqlParameter("@SUPPLIER_postalcode", PostalCode),
            new SqlParameter("@SUPPLIER_province", Province) ,
            new SqlParameter("@SUPPLIER_timesfailed", TimesFailed) ,

            };

            param[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertSUPPLIER", param);

        }


        
        public int Update()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SUPPLIER_id",SupId),
            new SqlParameter("@SUPPLIER_name", name) ,
            new SqlParameter("@SUPPLIER_phone", phone),
            new SqlParameter("@SUPPLIER_email", email),
            new SqlParameter("@SUPPLIER_address", address),
            new SqlParameter("@SUPPLIER_postalcode", PostalCode),
            new SqlParameter("@SUPPLIER_province", Province) ,
            new SqlParameter("@SUPPLIER_timesfailed", TimesFailed) ,
            };

            return da.ExecSP(CommandType.StoredProcedure, "usp_UpdateSUPPLIER", param);

        }


        public DataSet GetAll()
        {

            SqlParameter[] param = new SqlParameter[] {
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("Supplier", "usp_SelectSUPPLIERsAll", param);
        }

        public void Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@SUPPLIER_id",Id),
            };

            param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, "usp_DeleteSUPPLIER", param);
        }

    }
}
