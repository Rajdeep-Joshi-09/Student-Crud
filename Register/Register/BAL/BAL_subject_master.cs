using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_subject_master
    {
        public static int insert_subject_master(string subName, string subCode, int subCredit, int subStdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insert_subject_master";
            cmd.Parameters.Add(para.StringInputPara("@subName", subName));
            cmd.Parameters.Add(para.StringInputPara("@subCode", subCode));
            cmd.Parameters.Add(para.IntInputPara("@subCredit", subCredit));
            cmd.Parameters.Add(para.IntInputPara("@subStanderdId", subStdId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static DataTable get_subject_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_subject_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_standerd_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_standerd_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable edit_subject_details(int editID)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_edit_subject_master";
            cmd.Parameters.Add(para.IntInputPara("@id", editID));
            return command.ExecuteQuery(cmd);
        }

        public static int delete_subject_details(int id)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_delete_subject_master";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }

        public static int update_subject_details(int id, string subName, string subCode, int subCredit, int subStdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_update_subject_master";
            cmd.Parameters.Add(para.IntInputPara("@id", id));
            cmd.Parameters.Add(para.StringInputPara("@subName", subName));
            cmd.Parameters.Add(para.StringInputPara("@subCode", subCode));
            cmd.Parameters.Add(para.IntInputPara("@subCredit", subCredit));
            cmd.Parameters.Add(para.IntInputPara("@subStdId", subStdId));
            int result = command.ExecuteNonQuery1(cmd);
            return result;
        }
    }
}