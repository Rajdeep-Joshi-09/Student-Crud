using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_route_master
    {
        public static int route_master (string route_name, string route_time)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_route_master";
            cmd.Parameters.Add(para.StringInputPara("@route_name", route_name));
            cmd.Parameters.Add(para.StringInputPara("@route_start_time", route_time));
            int resultval = command.ExecuteNonQuery1(cmd);
            return resultval;
        }

        public static DataTable get_route_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_route_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_route_details(int route_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_route_master";
            cmd.Parameters.Add(para.IntInputPara("@route_id", route_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static int update_route_detail(int route_id, string route_name, string route_time)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_route_master";
            cmd.Parameters.Add(para.IntInputPara("@route_id", route_id));
            cmd.Parameters.Add(para.StringInputPara("@route_name", route_name));
            cmd.Parameters.Add(para.StringInputPara("@route_time", route_time));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static DataTable edit_route_detials(int edit_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_route_master";
            cmd.Parameters.AddWithValue("@route_id", edit_id);
            return command.ExecuteQuery(cmd);
        }
    }
}