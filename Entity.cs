using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ClassLibrary
{
    public class Entity
    {
        protected int id;
        protected SqlParameter[] param;
        protected String sprocedureName;
        DataAccess da;


        public Entity()
        {
            da = new DataAccess();

        }

        public virtual int Add()
        {
            //first parameter is always the stored proceudure output
            Param[0].Direction = ParameterDirection.Output;

            int id = da.ExecSP(CommandType.StoredProcedure, SProcedureName , Param);

            return id;

        }

        public virtual void Update()
        {
            Param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, SProcedureName, Param);
        }

        public virtual void Delete()
        {
            Param[0].Direction = ParameterDirection.Input;
            da.ExecSP(CommandType.StoredProcedure, SProcedureName, Param);

        }



        //public virtual ArrayList GetAll()
        //{
        //    da = new DataAccess();
        //    return da.SelectItems(CommandType.StoredProcedure, SProcedureName, Param);
        //}

        public SqlParameter[] Param
        {
            get { return param; }
            set { param = value; }
        }

        public String SProcedureName
        {
            get { return sprocedureName; }
            set { sprocedureName = value; }
        }
    }
}
