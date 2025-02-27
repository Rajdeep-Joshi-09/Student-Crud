using System;
using System.Data;
using System.Data.SqlClient;
using Register.DAL;
using System.Configuration;

namespace Register.BAL
{
    public class BAL_student_master
    {


        public static string check_student_details(string semail, string snumber)
        {
            SqlCommand cmdCheck = new SqlCommand();
            parameter para = new parameter();
            cmdCheck.CommandText = "rajdeep_get_student_emailMobile";
            cmdCheck.Parameters.Add(para.StringInputPara("@student_email", semail));
            cmdCheck.Parameters.Add(para.StringInputPara("@student_mobile", snumber));

            
            DataTable dt = command.ExecuteQuery(cmdCheck);  

            if (dt.Rows.Count > 0)
            {
                string checkResult = dt.Rows[0][0].ToString();  

                if (checkResult == "BOTH_EXIST")
                    return "0";
                else if (checkResult == "MOBILE_EXISTS")
                    return "1";
                else if (checkResult == "EMAIL_EXISTS")
                    return "2";
            }

           
            return "UNIQUE";  
        }



        public static int rajdeep_insert_student_master(string fname, string mname, string lname, string semail, string snumber, string sgender, int scity, string saddress)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_student_master";
            cmd.Parameters.Add(para.StringInputPara("@first_name", fname));
            cmd.Parameters.Add(para.StringInputPara("@middle_name", mname));
            cmd.Parameters.Add(para.StringInputPara("@last_name", lname));
            cmd.Parameters.Add(para.StringInputPara("@stud_email", semail));
            cmd.Parameters.Add(para.StringInputPara("@stud_mobile", snumber));
            cmd.Parameters.Add(para.StringInputPara("@stud_gender", sgender));
            cmd.Parameters.Add(para.IntInputPara("@stud_city", scity));
            cmd.Parameters.Add(para.StringInputPara("@stud_address", saddress));
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

        public static int update_student_records(int edit_id, string fname, string mname, string lname, string semail, string snumber, string sgender, int scity, string saddress)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_student_master";
            cmd.Parameters.Add(para.IntInputPara("@id", edit_id));
            cmd.Parameters.Add(para.StringInputPara("@first_name", fname));
            cmd.Parameters.Add(para.StringInputPara("@middle_name", mname));
            cmd.Parameters.Add(para.StringInputPara("@last_name", lname));
            cmd.Parameters.Add(para.StringInputPara("@stud_email", semail));
            cmd.Parameters.Add(para.StringInputPara("@stud_mobile", snumber));
            cmd.Parameters.Add(para.StringInputPara("@stud_gender", sgender));
            cmd.Parameters.Add(para.IntInputPara("@stud_city", scity));
            cmd.Parameters.Add(para.StringInputPara("@stud_address", saddress));
            int resultVal = command.ExecuteNonQuery1(cmd);
            return resultVal;
        }
    }
}
