using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ClassLibrary
{
    public class Customer : Contact
    {

        DataAccess da;


        public Customer()
        {
            da = new DataAccess();
        }


        public int Add()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@CUSTOMER_id",Id),
            new SqlParameter("@CUSTOMER_name", Name) ,
            new SqlParameter("@CUSTOMER_phone", Phone),
            new SqlParameter("@CUSTOMER_email", Email),
            new SqlParameter("@CUSTOMER_address", Address) 
            };

            param[0].Direction = ParameterDirection.Output;

            return da.ExecSP(CommandType.StoredProcedure, "usp_InsertCUSTOMER", param);

        }

        public int Update()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@CUSTOMER_id",Id),
            new SqlParameter("@CUSTOMER_name", Name) ,
            new SqlParameter("@CUSTOMER_phone", Phone),
            new SqlParameter("@CUSTOMER_email", Email),
            new SqlParameter("@CUSTOMER_address", Address) 
            };

            param[0].Direction = ParameterDirection.Input;
            return da.ExecSP(CommandType.StoredProcedure, "usp_UpdateCUSTOMER", param);
        }

        public void Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@CUSTOMER_id",Id),
            };

            param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, "usp_DeleteCUSTOMER", param);
        }

        public DataSet GetAll()
        {

            SqlParameter[] param = new SqlParameter[] {
            };

            //return da.SelectItems(CommandType.StoredProcedure, "usp_SelectCUSTOMERsAll", param);
            return da.GetDataSet("Customer" , "usp_SelectCUSTOMERsAll", param);
        }
    }
}
