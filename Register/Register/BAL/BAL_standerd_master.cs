using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_standerd_master
    {
        public static int rajdeep_insert_standerd_master(string std_name)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_standerd_master";
            cmd.Parameters.Add(para.StringInputPara("@stdName", std_name));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

        public static DataTable get_standerd_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_standerd_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_standerd_details(int std_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_standerd_master";
            cmd.Parameters.Add(para.IntInputPara("@id", std_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static DataTable edit_standerd_records(int std_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_standerd_master";
            cmd.Parameters.AddWithValue("@id", std_id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_standerd_record(int edit_id, string std_name)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_standerd_master";
            cmd.Parameters.Add(para.IntInputPara("@id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@stdName", std_name));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

    }
}