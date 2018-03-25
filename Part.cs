using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ClassLibrary
{
    public class Part
    {
        int SupplierId;

        DataAccess da;
        int id;
        String name;
        String color;
        String picture;

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

        public String Color
        {
            get { return color; }
            set { color = value; }
        }

        public Part()
        {
            da = new DataAccess();
        }


        public int Add()
        {

            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PART_id",Id),
            new SqlParameter("@PART_name", Name) ,
            new SqlParameter("@PART_color", Color)
            };

            param[0].Direction = ParameterDirection.Output;

            int i = da.ExecSP(CommandType.StoredProcedure, "usp_InsertPART", param);

            return i;
           
        }

        public int Update()
        {

            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PART_id",Id),
            new SqlParameter("@PART_name", Name) ,
            new SqlParameter("@PART_color", Color)
            };

            int i = da.ExecSP(CommandType.StoredProcedure, "usp_UpdatePART", param);

            return i;

        }


        public DataSet GetAll()
        {

            SqlParameter[] param = new SqlParameter[] {
            };

            return da.GetDataSet("PARTS", "usp_SelectPARTsAll", param);
        }


        public void Delete()
        {
            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PART_id",Id),
            };

            param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, "usp_DeletePART", param);
        }


        public DataSet Get()
        {

            SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@PART_id",Id),
            };

            return da.GetDataSet("PART", "usp_SelectPART", param);
        }
    }
}
