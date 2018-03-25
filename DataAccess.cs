using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Collections;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace ClassLibrary
{
	public class DataAccess
	{

        SqlTransaction transaction;

        public SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }
		
		private static SqlConnection connection = new SqlConnection
            ("Data Source=LAPTOP2\\SQLEXPRESS;initial catalog=UnderGuard;integrated security=SSPI;");
		public DataAccess()
		{
			
		}

		private SqlConnection GetConnection()
		{
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                return connection;
            }
            else
            {
                return connection;
            }
		}

		
		public int ExecSP(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
			{

            SqlConnection connection = GetConnection();

			SqlCommand cmd = new SqlCommand();

			PrepareCommand(cmd, (SqlTransaction) null, commandType, commandText, commandParameters);

            cmd.ExecuteNonQuery();

            connection.Close();

            //int i = (int)cmd.Parameters[0].Value;

            return (int)cmd.Parameters[0].Value;

		}

		
		public System.Data.DataSet GetDataSet(String tableName, string commandText, params SqlParameter[] commandParameters)
			{
			
			//create a command and prepare it for execution
			SqlCommand cmd = new SqlCommand();
			System.Data.DataSet ds = new System.Data.DataSet();
			SqlDataAdapter da;
			GetConnection();
			PrepareCommand(cmd, (SqlTransaction) null, CommandType.StoredProcedure, commandText, commandParameters);
			
			//create the DataAdapter & DataSet
			da = new SqlDataAdapter(cmd);
			
			//fill the DataSet using default values for DataTable names, etc.
			da.Fill(ds, tableName);
			connection.Close();
			//detach the SqlParameters from the command object, so they can be used again
			cmd.Parameters.Clear();
			
			//return the dataset
			return ds;
			
			
		} //ExecuteDataset





        public int ExecuteTransaction(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = GetConnection();

            SqlCommand cmd = new SqlCommand();

            transaction = connection.BeginTransaction();

            PrepareCommand(cmd, (SqlTransaction)null, commandType, commandText, commandParameters);

            cmd.ExecuteNonQuery();

            return (int)cmd.Parameters[0].Value;

        }

		


		private static void PrepareCommand(SqlCommand command, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
			{
			
			//if the provided connection is not open, we will open it
			//If connection.State.Closed = True Then
			//    connection.Open()
			//End If
			
			//associate the connection with the command
			
			command.Connection = connection;
			
			//set the command text (stored procedure name or SQL statement)
			command.CommandText = commandText;
			
			//if we were provided a transaction, assign it.
			if (!(transaction == null))
			{
                command.Transaction = connection.BeginTransaction("SampleTransaction");
			}
			
			//set the command type
			command.CommandType = commandType;
			
			//attach the command parameters if they are provided
			if (!(commandParameters == null))
			{
				AttachParameters(command, commandParameters);
			}
			
			return;
		} //PrepareCommand
		



		private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
		{
			SqlParameter p;
			foreach (SqlParameter tempLoopVar_p in commandParameters)
			{
				p = tempLoopVar_p;
				//check for derived output value with no value assigned
				//If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
				//    p.Value = Nothing
				//End If

				command.Parameters.Add(p);
			}
		} //AttachParameters

	}
	
}
