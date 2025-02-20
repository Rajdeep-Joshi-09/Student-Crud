using System;
using System.Data;
using System.Data.SqlClient;
using Register.DAL;
using System.Configuration;

namespace Register.BAL
{
    public class BAL_student_master
    {
        public static int rajdeep_insert_student_master(string stud_name, string stud_email, string stud_address, string stud_gender, int stud_city, string stud_mobile)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_student_master";
            cmd.Parameters.Add(para.StringInputPara("@stud_name", stud_name));
            cmd.Parameters.Add(para.StringInputPara("@stud_email", stud_email));
            cmd.Parameters.Add(para.StringInputPara("@stud_address", stud_address));
            cmd.Parameters.Add(para.StringInputPara("@stud_gender", stud_gender));
            cmd.Parameters.Add(para.IntInputPara("@stud_city", stud_city));
            cmd.Parameters.Add(para.StringInputPara("@stud_mobile", stud_mobile));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }


        public static DataTable get_student_details()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_select_student_master";
            return command.ExecuteQuery(cmd);
        }

        public static int delete_student_details(int stud_id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_student_master";
            cmd.Parameters.Add(para.IntInputPara("@id", stud_id));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }

        public static DataTable get_all_city()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_selectAll_cities";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable edit_student_records(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_edit_student_master";
            cmd.Parameters.AddWithValue("@id", id);
            return command.ExecuteQuery(cmd);
        }

        public static int update_student_records(int edit_id, string stud_name, string stud_email, string stud_address, string stud_gender, int stud_city, string stud_mobile)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_student_master";
            cmd.Parameters.Add(para.IntInputPara("@id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@stud_name", stud_name));
            cmd.Parameters.Add(para.StringInputPara("@stud_email", stud_email));
            cmd.Parameters.Add(para.StringInputPara("@stud_address", stud_address));
            cmd.Parameters.Add(para.StringInputPara("@stud_gender", stud_gender));
            cmd.Parameters.Add(para.IntInputPara("@stud_city", stud_city));
            cmd.Parameters.Add(para.StringInputPara("@stud_mobile", stud_mobile));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }
    }
}
