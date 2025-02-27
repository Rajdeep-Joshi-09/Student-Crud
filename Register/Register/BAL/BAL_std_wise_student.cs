using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_std_wise_student
    {
        public static int insert_std_wise_student(int sws_std_id, int sws_stud_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_standerd_wise_student";
            cmd.Parameters.Add(para.IntInputPara("@swsStdId", sws_std_id));
            cmd.Parameters.Add(para.IntInputPara("@swsStudId", sws_stud_id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static DataTable select_std_wise_student()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_standard_wise_student";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_standerd_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_standerd_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_student_detials()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_get_student_full_name";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_sws_details(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_standerd_wise_student";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        } 

        public static DataTable edit_sws_details(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_edit_standerd_wise_student";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            return command.ExecuteQuery(cmd);
        }

        public static int update_sws_details(int editId, int sws_std_id, int sws_stud_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_standerd_wise_student";
            cmd.Parameters.Add(para.IntInputPara("@id", editId));
            cmd.Parameters.Add(para.IntInputPara("@swsStdId", sws_std_id));
            cmd.Parameters.Add(para.IntInputPara("@swsStudId", sws_stud_id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
    }
}