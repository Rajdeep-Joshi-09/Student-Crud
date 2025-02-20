using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_stop_master
    {
        public static int rajdeep_insert_stop_master(string stop_name)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_stop_master";
            cmd.Parameters.Add(para.StringInputPara("@stop_name", stop_name));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

        public static DataTable get_stops_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_stop_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_stop_details(int stop_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_stop_master";
            cmd.Parameters.Add(para.IntInputPara("@stop_id", stop_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static DataTable edit_stop_records(int stop_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_stop_master";
            cmd.Parameters.AddWithValue("@stop_id", stop_id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_stop_record (int edit_id, string stop_name)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_stop_master";
            cmd.Parameters.Add(para.IntInputPara("@stop_id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@stop_name", stop_name));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

      
    }
}