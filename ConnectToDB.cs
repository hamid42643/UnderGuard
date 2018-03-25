using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ClassLibrary
{
    class ConnectToDB
    {

        public void SqlConnection()
        {
            // Create an empty SqlConnection object. 
            using (SqlConnection con = new SqlConnection())
            {
                // Configure the SqlConnection object's connection string. 
                con.ConnectionString =
                    @"Data Source=.\sqlexpress;" + // local SQL Server instance 
                    "Database=UnderGuard;" +        // the sample Northwind DB 
                    "Integrated Security=SSPI";    // integrated Windows security 

                // Open the database connection. 
                con.Open();
                // Display information about the connection. 
                if (con.State == ConnectionState.Open)
                {
                    Console.WriteLine("SqlConnection Information:");
                    Console.WriteLine("  Connection State = " + con.State);
                    Console.WriteLine("  Connection String = " +
                        con.ConnectionString);
                    Console.WriteLine("  Database Source = " + con.DataSource);
                    Console.WriteLine("  Database = " + con.Database);
                    Console.WriteLine("  Server Version = " + con.ServerVersion);
                    Console.WriteLine("  Workstation Id = " + con.WorkstationId);
                    Console.WriteLine("  Timeout = " + con.ConnectionTimeout);
                    Console.WriteLine("  Packet Size = " + con.PacketSize);
                }
                else
                {
                    Console.WriteLine("SqlConnection failed to open.");
                    Console.WriteLine("  Connection State = " + con.State);
                }
                // At the end of the using block Dispose() calls Close(). 
            }


        }

    }
}
