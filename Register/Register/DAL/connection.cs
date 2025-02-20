using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.DAL
{
    public class connection
    {
        public static SqlConnection Create_Connection()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString);
            return cn;
        }

        public static SqlConnection Open_Connection()
        {
            SqlConnection cn = Create_Connection();
            cn.Open();
            return cn;
        }

        public static void Close_Connection()
        {
            SqlConnection cn = Create_Connection();
            cn.Close();
            cn.Dispose();
        }
    }
}