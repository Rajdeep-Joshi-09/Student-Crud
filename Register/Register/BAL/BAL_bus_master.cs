using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_bus_master
    {
        public static int insert_bus_master (string bus_name, string bus_number)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_bus_master";
            cmd.Parameters.Add(para.StringInputPara("@bus_name", bus_name));
            cmd.Parameters.Add(para.StringInputPara("@bus_number", bus_number));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

        public static DataTable get_bus_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_bus_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_bus_details(int bus_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_bus_master";
            cmd.Parameters.Add(para.IntInputPara("@bus_id", bus_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static DataTable edit_bus_records (int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_bus_master";
            cmd.Parameters.AddWithValue("@bus_id", id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_bus_record(int edit_id, string bus_name, string bus_number)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_bus_master";
            cmd.Parameters.Add(para.IntInputPara("@bus_id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@bus_name", bus_name));
            cmd.Parameters.Add(para.StringInputPara("@bus_number", bus_number));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

    }
}