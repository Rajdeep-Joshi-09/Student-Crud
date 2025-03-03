using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Register.DAL
{
    public class command
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public static DataTable ExecuteQuery(SqlCommand cmd)
        {
            try
            {
                SqlConnection cn = connection.Open_Connection();
                DataTable dt = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;

            }

            finally
            {
                connection.Close_Connection();
            }
        }

        public static int ExecuteNonQuery1(SqlCommand cmd)
        {
            try
            {
                SqlConnection cn = connection.Open_Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
            }
            finally
            {
                connection.Close_Connection();
            }

            return cmd.ExecuteNonQuery();

        }

        public static object ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                SqlConnection cn = connection.Open_Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                return cmd.ExecuteScalar(); 
            }
            finally
            {
                connection.Close_Connection();
            }
        }


    }
}