using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Register.BAL;

namespace Register.form
{
    public partial class FORM_std_student_subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if this is an export request.
            if (Request.QueryString["export"] == "1")
            {
                int stdId;
                if (int.TryParse(Request.QueryString["stdId"], out stdId))
                {
                    ExportExcel(stdId);
                }
                Response.End();
            }
            // Normal page load: no server-side processing needed for AJAX UI.
        }

        // --- WebMethods for AJAX calls ---

        // Helper: Convert DataTable to a list of dictionaries.
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }

        [WebMethod]
        public static string GetStandards()
        {
            DataTable dt = BAL_std_student_subject.get_standerd_details();
            var list = DataTableToList(dt);
            return new JavaScriptSerializer().Serialize(list);
        }

        [WebMethod]
        public static string GetStudentsByStandard(int stdId)
        {
            DataTable dt = BAL_std_student_subject.get_students_by_standard(stdId);
            var list = DataTableToList(dt);
            return new JavaScriptSerializer().Serialize(list);
        }

        [WebMethod]
        public static string GetSubjectsByStudent(int studentId, int stdId)
        {
            DataTable dt = BAL_std_student_subject.get_subjects_by_student(studentId, stdId);
            var list = DataTableToList(dt);
            return new JavaScriptSerializer().Serialize(list);
        }

        [WebMethod]
        public static string GetSubjectsByStandard(int stdId)
        {
            DataTable dt = BAL_std_student_subject.get_subjects_by_standard(stdId);
            var list = DataTableToList(dt);
            return new JavaScriptSerializer().Serialize(list);
        }

        [WebMethod]
        public static string InsertStudentMarks(List<MarkEntry> entries)
        {
            foreach (MarkEntry entry in entries)
            {
                // This method should update the mark if it already exists, or insert if not.
                BAL_std_student_subject.InsertStudentMark(entry.StudentId, entry.SubjectId, entry.Mark);
            }
            return "Success";
        }

        public class MarkEntry
        {
            public int StudentId { get; set; }
            public int SubjectId { get; set; }
            public decimal Mark { get; set; }
        }

        // --- Excel Export Implementation ---
        private void ExportExcel(int stdId)
        {
            // Get students and subjects via BAL.
            DataTable dtStudents = BAL_std_student_subject.get_students_by_standard(stdId);
            DataTable dtSubjects = BAL_std_student_subject.get_subjects_by_standard(stdId);

            if (dtStudents.Rows.Count == 0)
                return;
            if (dtSubjects.Rows.Count == 0)
                return;

            // Build a dictionary mapping each student id to the subjects assigned to them.
            Dictionary<int, HashSet<int>> assignedSubjects = new Dictionary<int, HashSet<int>>();
            foreach (DataRow drStudent in dtStudents.Rows)
            {
                int studentId = Convert.ToInt32(drStudent["id"]);
                DataTable dtAssigned = BAL_std_student_subject.get_subjects_by_student(studentId, stdId);
                HashSet<int> set = new HashSet<int>();
                foreach (DataRow dr in dtAssigned.Rows)
                {
                    set.Add(Convert.ToInt32(dr["subject_id"]));
                }
                assignedSubjects[studentId] = set;
            }

            // Get marks using stored procedure.
            List<int> studentIds = new List<int>();
            foreach (DataRow dr in dtStudents.Rows)
            {
                studentIds.Add(Convert.ToInt32(dr["id"]));
            }
            string ids = string.Join(",", studentIds);
            DataTable dtMarks = BAL_std_student_subject.get_marks_for_students(ids, stdId);
            // Build marks dictionary: key = "studentId_subjectId"
            Dictionary<string, string> marksDict = new Dictionary<string, string>();
            foreach (DataRow dr in dtMarks.Rows)
            {
                int studentId = Convert.ToInt32(dr["student_id"]);
                int subjectId = Convert.ToInt32(dr["subject_id"]);
                string mark = dr["marks"].ToString();
                marksDict[$"{studentId}_{subjectId}"] = mark;
            }

            // Generate Excel file using EPPlus.
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Student Marks");
                // Header row: "Student Name" + one column per subject.
                ws.Cells[1, 1].Value = "Student Name";
                ws.Cells[1, 1].Style.Font.Bold = true;
                int colIndex = 2;
                List<Tuple<int, string>> subjectList = new List<Tuple<int, string>>();
                foreach (DataRow drSub in dtSubjects.Rows)
                {
                    int subjectId = Convert.ToInt32(drSub["subject_id"]);
                    string subjectName = drSub["subject_name"].ToString();
                    subjectList.Add(new Tuple<int, string>(subjectId, subjectName));
                    ws.Cells[1, colIndex].Value = subjectName;
                    ws.Cells[1, colIndex].Style.Font.Bold = true;
                    colIndex++;
                }

                // Data rows for each student.
                int rowIndex = 2;
                foreach (DataRow drStudent in dtStudents.Rows)
                {
                    int studentId = Convert.ToInt32(drStudent["id"]);
                    string studentName = drStudent["student_name"].ToString();
                    ws.Cells[rowIndex, 1].Value = studentName;

                    colIndex = 2;
                    foreach (var subject in subjectList)
                    {
                        int subjectId = subject.Item1;
                        string key = studentId + "_" + subjectId;
                        if (assignedSubjects.ContainsKey(studentId) && assignedSubjects[studentId].Contains(subjectId))
                        {
                            // Subject is assigned; fill mark if exists.
                            if (marksDict.ContainsKey(key))
                            {
                                ws.Cells[rowIndex, colIndex].Value = marksDict[key];
                            }
                            else
                            {
                                ws.Cells[rowIndex, colIndex].Value = ""; // mark not entered yet.
                            }
                            // White background for assigned subjects.
                            ws.Cells[rowIndex, colIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(Color.White);
                        }
                        else
                        {
                            // Subject not assigned: gray cell.
                            ws.Cells[rowIndex, colIndex].Value = "";
                            ws.Cells[rowIndex, colIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                        colIndex++;
                    }
                    rowIndex++;
                }

                // Apply borders.
                using (var range = ws.Cells[ws.Dimension.Address])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=StudentMarks.xlsx");
                Response.BinaryWrite(package.GetAsByteArray());
                Response.Flush();
                Response.End();
            }
        }

        // --- Excel Import Implementation ---
        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (!fuExcelImport.HasFile)
            {
                lblMessage.Text = "Please select an Excel file to import.";
                return;
            }
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(fuExcelImport.FileContent))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int totalRows = ws.Dimension.End.Row;
                    int totalCols = ws.Dimension.End.Column;

                    // Assume header row exists and first column is Student Name.
                    Dictionary<int, string> subjectMapping = new Dictionary<int, string>();
                    for (int col = 2; col <= totalCols; col++)
                    {
                        string subjectName = ws.Cells[1, col].Text.Trim();
                        if (!string.IsNullOrEmpty(subjectName))
                        {
                            subjectMapping[col] = subjectName;
                        }
                    }

                    // Retrieve the selected standard from Request.Form since it's populated client side.
                    string stdValue = Request.Form[stdList.UniqueID];
                    if (!int.TryParse(stdValue, out int stdId) || stdId == 0)
                    {
                        lblMessage.Text = "Please select a valid standard.";
                        return;
                    }

                    // Retrieve subjects for the standard.
                    DataTable dtSubjects = BAL_std_student_subject.get_subjects_by_standard(stdId);
                    Dictionary<string, int> subjectIdMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (DataRow dr in dtSubjects.Rows)
                    {
                        string subjName = dr["subject_name"].ToString().Trim();
                        int subjId = Convert.ToInt32(dr["subject_id"]);
                        if (!subjectIdMapping.ContainsKey(subjName))
                            subjectIdMapping[subjName] = subjId;
                    }

                    // Retrieve students for the standard.
                    DataTable dtStudents = BAL_std_student_subject.get_students_by_standard(stdId);
                    Dictionary<string, int> studentMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (DataRow dr in dtStudents.Rows)
                    {
                        string studName = dr["student_name"].ToString().Trim();
                        int studId = Convert.ToInt32(dr["id"]);
                        if (!studentMapping.ContainsKey(studName))
                            studentMapping[studName] = studId;
                    }

                    List<string> errorMessages = new List<string>();
                    List<MarkEntry> entries = new List<MarkEntry>();

                    // Loop through rows (starting at row 2).
                    for (int row = 2; row <= totalRows; row++)
                    {
                        string studentName = ws.Cells[row, 1].Text.Trim();
                        if (string.IsNullOrEmpty(studentName) || !studentMapping.ContainsKey(studentName))
                            continue;
                        int studentId = studentMapping[studentName];

                        foreach (var kvp in subjectMapping)
                        {
                            int colIndex = kvp.Key;
                            string subjectName = kvp.Value;
                            if (!subjectIdMapping.ContainsKey(subjectName))
                                continue; // subject not applicable

                            int subjectId = subjectIdMapping[subjectName];
                            var cell = ws.Cells[row, colIndex];

                            // Check if cell has light gray background indicating not assigned.
                            if (cell.Style.Fill.BackgroundColor.Rgb != null &&
                                cell.Style.Fill.BackgroundColor.Rgb.ToUpper().Contains("D3D3D3"))
                            {
                                continue;
                            }

                            string markText = cell.Text.Trim();
                            if (!string.IsNullOrEmpty(markText))
                            {
                                if (!decimal.TryParse(markText, out decimal mark))
                                {
                                    errorMessages.Add($"The marks of {studentName} for {subjectName} is invalid.");
                                }
                                else
                                {
                                    entries.Add(new MarkEntry { StudentId = studentId, SubjectId = subjectId, Mark = mark });
                                }
                            }
                        }
                    }

                    if (errorMessages.Count > 0)
                    {
                        // Alert error messages and do not process the import.
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('" + string.Join("\\n", errorMessages) + "');", true);
                        return;
                    }
                    else
                    {
                        // Process all valid entries (update/insert marks).
                        foreach (var entry in entries)
                        {
                            BAL_std_student_subject.InsertStudentMark(entry.StudentId, entry.SubjectId, entry.Mark);
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('Marks imported successfully!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Error importing Excel: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }


    }
}
