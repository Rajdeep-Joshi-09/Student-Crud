using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_student_to_route
    {
        public static DataTable getAll_route_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_getRouteBusDriver_details";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable getAll_stop_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_stop_master";
            return command.ExecuteQuery(cmd);
        }

        public static int insert_student_route_details(int brdId, int stopId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_student_to_route";
            cmd.Parameters.Add(para.IntInputPara("@brdId", brdId));
            cmd.Parameters.Add(para.IntInputPara("@stopId", stopId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static DataTable select_student_route_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_getAll_studentRoute";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_student_route_details(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_student_route";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static DataTable edit_student_route_details(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_edit_studentRoute";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            return command.ExecuteQuery(cmd);
        }

        public static int update_studet_route_details(int id, int brdId, int stopId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_student_route";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            cmd.Parameters.Add(para.IntInputPara("@brd_Id", brdId));
            cmd.Parameters.Add(para.IntInputPara("@stop_Id", stopId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
    }
}