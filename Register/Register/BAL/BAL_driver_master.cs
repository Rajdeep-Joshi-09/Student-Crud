using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_driver_master
    {
        public static int insert_driver_master (string driver_name, string driver_number)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_driver_master";
            cmd.Parameters.Add(para.StringInputPara("@driver_name", driver_name));
            cmd.Parameters.Add(para.StringInputPara("@driver_mobile_number", driver_number));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

        public static DataTable get_driver_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_driver_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_driver_details(int driver_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_driver_master";
            cmd.Parameters.Add(para.IntInputPara("@driver_id", driver_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static DataTable edit_driver_records(int driver_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_driver_master";
            cmd.Parameters.AddWithValue("@driver_id", driver_id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_driver_record(int edit_id, string driver_name, string driver_number)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_driver_master";
            cmd.Parameters.Add(para.IntInputPara("@driver_id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@driver_name", driver_name));
            cmd.Parameters.Add(para.StringInputPara("@driver_number", driver_number));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }
    }
}