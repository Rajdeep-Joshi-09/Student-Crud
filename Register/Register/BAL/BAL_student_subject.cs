using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_student_subject
    {

        public static int insert_student_to_subject(int subId, int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_student_subject";
            cmd.Parameters.Add(para.IntInputPara("@subjectId", subId));
            cmd.Parameters.Add(para.IntInputPara("@studentId", stdId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
        public static DataTable get_subject_detail()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_subject_master";
            return command.ExecuteQuery(cmd);
        }
        public static DataTable get_student_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_student_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_all_student_subject()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_student_subject";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_student_subject(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_student_subject";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
        public static DataTable edit_student_subject(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_edit_student_subject";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            return command.ExecuteQuery(cmd);
        }

        public static int update_student_subject(int editId, int subId, int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_student_subject";
            cmd.Parameters.Add(para.IntInputPara("@id", editId));
            cmd.Parameters.Add(para.IntInputPara("@subId", subId));
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
    }
}
