using Register.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Register.BAL
{
    public class BAL_std_student_subject
    {
        public static DataTable get_standerd_details()
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_select_standerd_master";
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_students_by_standard(int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_get_all_student_by_standard";
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_subjects_by_student(int studentId, int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_get_subjects_by_student";
            cmd.Parameters.Add(para.IntInputPara("@studentId", studentId));
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));
            return command.ExecuteQuery(cmd);
        }

        // New method: Fetch subjects for a given standard.
        public static DataTable get_subjects_by_standard(int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_get_subject_by_std_id";
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));
            return command.ExecuteQuery(cmd);
        }

        public static DataTable get_all_students_marks()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_get_all_students_marks";  // Stored procedure to get all marks.
            cmd.CommandType = CommandType.StoredProcedure;
            return command.ExecuteQuery(cmd);
        }

        public static int InsertStudentMark(int studentId, int subjectId, decimal marks)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_insertMarks";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(para.IntInputPara("@StudentId", studentId));
            cmd.Parameters.Add(para.IntInputPara("@SubjectId", subjectId));
            cmd.Parameters.Add(para.DecimalInputPara("@Marks", marks));
            return command.ExecuteNonQuery1(cmd);
        }

        // New method: Get marks for students using the stored procedure 'rajdeep_get_marks_for_students'
        public static DataTable get_marks_for_students(string studentIds, int stdId)
        {
            SqlCommand cmd = new SqlCommand();
            parameter para = new parameter();
            cmd.CommandText = "rajdeep_get_marks_for_students";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(para.StringInputPara("@StudentIds", studentIds));
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));
            return command.ExecuteQuery(cmd);
        }
    }
}
