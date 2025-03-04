using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_std_student_subject : System.Web.UI.Page
    {
        // Stores the IDs of expanded student rows.
        public List<int> ExpandedStudents
        {
            get
            {
                if (ViewState["ExpandedStudents"] == null)
                    ViewState["ExpandedStudents"] = new List<int>();
                return (List<int>)ViewState["ExpandedStudents"];
            }
            set { ViewState["ExpandedStudents"] = value; }
        }

        // Dictionary to persist entered marks across postbacks.
        private Dictionary<int, Dictionary<string, string>> StudentMarks
        {
            get
            {
                if (ViewState["StudentMarks"] == null)
                    ViewState["StudentMarks"] = new Dictionary<int, Dictionary<string, string>>();
                return (Dictionary<int, Dictionary<string, string>>)ViewState["StudentMarks"];
            }
            set { ViewState["StudentMarks"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindStandards();
            }
        }

        // Load standard details into dropdown.
        public void bindStandards()
        {
            DataTable dt = BAL.BAL_std_student_subject.get_standerd_details();
            stdList.DataSource = dt;
            stdList.DataTextField = "std_name";
            stdList.DataValueField = "std_id";
            stdList.DataBind();
            stdList.Items.Insert(0, new ListItem("-- Select Standard --", "0"));
        }

        // Handle standard selection.
        protected void stdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpandedStudents = new List<int>();
            // Clear saved marks when standard changes.
            StudentMarks = new Dictionary<int, Dictionary<string, string>>();
            int stdId = Convert.ToInt32(stdList.SelectedValue);
            if (stdId > 0)
            {
                LoadStudents(stdId);
            }
            else
            {
                rptStudents.DataSource = null;
                rptStudents.DataBind();
                lblMessage.Text = "Please select a valid standard.";
            }
        }

        // Load students based on selected standard.
        private void LoadStudents(int stdId)
        {
            DataTable dt = BAL.BAL_std_student_subject.get_students_by_standard(stdId);
            if (dt.Rows.Count > 0)
            {
                rptStudents.DataSource = dt;
                rptStudents.DataBind();
                lblMessage.Text = "";
            }
            else
            {
                rptStudents.DataSource = null;
                rptStudents.DataBind();
                lblMessage.Text = "No students found for this standard.";
            }
        }

        // Save marks from inner repeaters into StudentMarks dictionary.
        private void SaveStudentMarks()
        {
            foreach (RepeaterItem studentItem in rptStudents.Items)
            {
                HiddenField hfStudentId = (HiddenField)studentItem.FindControl("hfStudentId");
                if (hfStudentId == null) continue;
                int studentId = Convert.ToInt32(hfStudentId.Value);

                Repeater rptSubjects = (Repeater)studentItem.FindControl("rptSubjects");
                if (rptSubjects == null || rptSubjects.Items.Count == 0)
                    continue;

                Dictionary<string, string> subjectMarks = new Dictionary<string, string>();

                foreach (RepeaterItem subjectItem in rptSubjects.Items)
                {
                    HiddenField hfSubjectId = (HiddenField)subjectItem.FindControl("hfSubjectId");
                    TextBox txtMarks = (TextBox)subjectItem.FindControl("txtMarks");
                    string subjectId = hfSubjectId != null ? hfSubjectId.Value : "";
                    string mark = txtMarks != null ? txtMarks.Text.Trim() : "";
                    subjectMarks[subjectId] = mark;
                }
                StudentMarks[studentId] = subjectMarks;
            }
        }

        // Restore saved marks into inner repeaters.
        private void RestoreStudentMarks(int studentId, Repeater rptSubjects)
        {
            if (StudentMarks.ContainsKey(studentId))
            {
                Dictionary<string, string> subjectMarks = StudentMarks[studentId];
                foreach (RepeaterItem subjectItem in rptSubjects.Items)
                {
                    HiddenField hfSubjectId = (HiddenField)subjectItem.FindControl("hfSubjectId");
                    TextBox txtMarks = (TextBox)subjectItem.FindControl("txtMarks");
                    if (hfSubjectId != null && txtMarks != null)
                    {
                        string subjectId = hfSubjectId.Value;
                        if (subjectMarks.ContainsKey(subjectId))
                        {
                            txtMarks.Text = subjectMarks[subjectId];
                        }
                    }
                }
            }
        }

        // Handle commands from the students repeater.
        protected void rptStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ExpandSubjects")
            {
                // Save current marks before re-binding.
                SaveStudentMarks();

                int studentId = Convert.ToInt32(e.CommandArgument);
                if (ExpandedStudents.Contains(studentId))
                    ExpandedStudents.Remove(studentId);
                else
                    ExpandedStudents.Add(studentId);

                int stdId = Convert.ToInt32(stdList.SelectedValue);
                LoadStudents(stdId);
            }
        }

        // Bind subjects to the inner repeater if the student row is expanded.
        protected void rptStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                int studentId = Convert.ToInt32(row["id"]);
                if (ExpandedStudents.Contains(studentId))
                {
                    Repeater rptSubjects = (Repeater)e.Item.FindControl("rptSubjects");
                    int stdId = Convert.ToInt32(stdList.SelectedValue);
                    DataTable dtSubjects = BAL.BAL_std_student_subject.get_subjects_by_student(studentId, stdId);
                    rptSubjects.DataSource = dtSubjects;
                    rptSubjects.DataBind();

                    // Restore previously entered marks for this student.
                    RestoreStudentMarks(studentId, rptSubjects);
                }
            }
        }

        // Returns "table-row" if student is expanded; otherwise "none".
        public string GetDisplayStyle(object studentIdObj)
        {
            int studentId = Convert.ToInt32(studentIdObj);
            return ExpandedStudents.Contains(studentId) ? "table-row" : "none";
        }

        // Global submit: validates that at least one student's marks are fully entered.
        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            // Save current marks before submission.
            SaveStudentMarks();

            bool anyFormExpanded = false;
            List<string> missingMarksMessages = new List<string>();

            // Loop through each student row.
            foreach (RepeaterItem studentItem in rptStudents.Items)
            {
                HiddenField hfStudentId = (HiddenField)studentItem.FindControl("hfStudentId");
                if (hfStudentId == null)
                    continue;
                int studentId = Convert.ToInt32(hfStudentId.Value);

                Repeater rptSubjects = (Repeater)studentItem.FindControl("rptSubjects");
                Label studentNameLabel = (Label)studentItem.FindControl("lblStudentName");
                string studentName = studentNameLabel != null ? studentNameLabel.Text : "Unknown Student";

                // Only process if the student form is expanded.
                if (rptSubjects == null || rptSubjects.Items.Count == 0)
                    continue;

                anyFormExpanded = true;
                bool complete = true;   // Are all subjects filled?
                List<string> emptySubjects = new List<string>();

                // Loop through each subject for this student.
                foreach (RepeaterItem subjectItem in rptSubjects.Items)
                {
                    HiddenField hfSubjectName = (HiddenField)subjectItem.FindControl("hfSubjectName");
                    TextBox txtMarks = (TextBox)subjectItem.FindControl("txtMarks");
                    string marks = txtMarks != null ? txtMarks.Text.Trim() : "";

                    if (string.IsNullOrEmpty(marks))
                    {
                        emptySubjects.Add(hfSubjectName != null ? hfSubjectName.Value : "Unknown Subject");
                        complete = false;
                    }
                }

                if (!complete)
                {
                    missingMarksMessages.Add($"The marks for {string.Join(", ", emptySubjects)} for {studentName} are missing.");
                }
            }

            if (!anyFormExpanded)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('No student forms are open. Please click the expand button for at least one student before submitting.');", true);
            }
            else if (missingMarksMessages.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('" + string.Join("\\n", missingMarksMessages) + "');", true);
            }
            else
            {
                // If validation passes, insert marks for each expanded student.
                foreach (RepeaterItem studentItem in rptStudents.Items)
                {
                    HiddenField hfStudentId = (HiddenField)studentItem.FindControl("hfStudentId");
                    if (hfStudentId == null) continue;
                    int studentId = Convert.ToInt32(hfStudentId.Value);

                    Repeater rptSubjects = (Repeater)studentItem.FindControl("rptSubjects");
                    if (rptSubjects == null || rptSubjects.Items.Count == 0)
                        continue;

                    foreach (RepeaterItem subjectItem in rptSubjects.Items)
                    {
                        HiddenField hfSubjectId = (HiddenField)subjectItem.FindControl("hfSubjectId");
                        TextBox txtMarks = (TextBox)subjectItem.FindControl("txtMarks");
                        if (hfSubjectId != null && txtMarks != null && !string.IsNullOrEmpty(txtMarks.Text.Trim()))
                        {
                            int subjectId = Convert.ToInt32(hfSubjectId.Value);
                            decimal mark;
                            if (decimal.TryParse(txtMarks.Text.Trim(), out mark))
                            {
                                // Insert the mark using the BAL method.
                                BAL.BAL_std_student_subject.InsertStudentMark(studentId, subjectId, mark);
                            }
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Marks successfully inserted for all students.');", true);
            }
        }

        // Helper method: Get marks for all students in the given standard.
        // Returns a dictionary with key: "studentId_subjectId" and value: marks.
        private Dictionary<string, string> GetMarksForStudents(List<int> studentIds, int stdId)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (studentIds.Count == 0)
                return dict;

            // Create a comma-separated list of student IDs.
            string ids = string.Join(",", studentIds);

            // Set up the stored procedure call.
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rajdeep_get_marks_for_students";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Assuming you have a method in your parameter class to add VARCHAR parameters.
            var para = new Register.DAL.parameter();
            cmd.Parameters.Add(para.StringInputPara("@StudentIds", ids));
            cmd.Parameters.Add(para.IntInputPara("@stdId", stdId));

            // Execute the stored procedure.
            DataTable dtMarks = Register.DAL.command.ExecuteQuery(cmd);

            // Populate the dictionary from the DataTable.
            foreach (DataRow dr in dtMarks.Rows)
            {
                int studentId = Convert.ToInt32(dr["student_id"]);
                int subjectId = Convert.ToInt32(dr["subject_id"]);
                string mark = dr["marks"].ToString();
                string key = studentId + "_" + subjectId;
                dict[key] = mark;
            }

            return dict;
        }


        // Excel Export using EPPlus – exports ALL students, their subjects as columns, and marks beneath.
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (stdList.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Please select a standard first.');", true);
                    return;
                }
                int stdId = Convert.ToInt32(stdList.SelectedValue);

                // Fetch all students using BAL.
                DataTable dtStudents = BAL.BAL_std_student_subject.get_students_by_standard(stdId);
                if (dtStudents == null || dtStudents.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('No students found for this standard.');", true);
                    return;
                }

                // Fetch subjects using the new BAL method.
                DataTable dtSubjects = BAL.BAL_std_student_subject.get_subjects_by_standard(stdId);
                if (dtSubjects == null || dtSubjects.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('No subjects found for this standard.');", true);
                    return;
                }

                // Build list of student IDs.
                List<int> studentIds = new List<int>();
                foreach (DataRow dr in dtStudents.Rows)
                {
                    studentIds.Add(Convert.ToInt32(dr["id"]));
                }

                // Get marks for all students.
                Dictionary<string, string> marksDict = GetMarksForStudents(studentIds, stdId);

                // Create export DataTable: first column is "Student Name", then one column per subject.
                DataTable dtExport = new DataTable();
                dtExport.Columns.Add("Student Name");
                foreach (DataRow drSub in dtSubjects.Rows)
                {
                    string subjectName = drSub["subject_name"].ToString();
                    dtExport.Columns.Add(subjectName);
                }

                // Populate export table: for each student, fill in the mark for each subject.
                foreach (DataRow drStudent in dtStudents.Rows)
                {
                    DataRow newRow = dtExport.NewRow();
                    int studentId = Convert.ToInt32(drStudent["id"]);
                    newRow["Student Name"] = drStudent["student_name"].ToString();
                    foreach (DataRow drSub in dtSubjects.Rows)
                    {
                        int subjectId = Convert.ToInt32(drSub["subject_id"]);
                        string subjectName = drSub["subject_name"].ToString();
                        string key = studentId + "_" + subjectId;
                        newRow[subjectName] = marksDict.ContainsKey(key) ? marksDict[key] : "";
                    }
                    dtExport.Rows.Add(newRow);
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Student Marks");

                    // Write header row.
                    for (int col = 1; col <= dtExport.Columns.Count; col++)
                    {
                        ws.Cells[1, col].Value = dtExport.Columns[col - 1].ColumnName;
                        ws.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Write data rows.
                    for (int row = 0; row < dtExport.Rows.Count; row++)
                    {
                        for (int col = 0; col < dtExport.Columns.Count; col++)
                        {
                            object cellValue = dtExport.Rows[row][col];
                            ws.Cells[row + 2, col + 1].Value = cellValue;
                            // For subject columns (not the first column), apply gray fill if empty.
                            if (col > 0 && (cellValue == null || string.IsNullOrEmpty(cellValue.ToString())))
                            {
                                ws.Cells[row + 2, col + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells[row + 2, col + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            }
                        }
                    }

                    // Apply borders to all cells in the used range.
                    using (var range = ws.Cells[ws.Dimension.Address])
                    {
                        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }

                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    byte[] fileBytes = package.GetAsByteArray();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment;filename=StudentMarks.xlsx");
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Error exporting Excel: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (!fuExcelImport.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select an Excel file to import.');", true);
                return;
            }

            try
            {
                // Load the uploaded Excel file into EPPlus.
                using (ExcelPackage package = new ExcelPackage(fuExcelImport.PostedFile.InputStream))
                {
                    // Assume the first worksheet contains the data.
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int totalRows = ws.Dimension.End.Row;
                    int totalCols = ws.Dimension.End.Column;

                    // Build a mapping of subject column index to subject name (header row).
                    // Assumes the first column is "Student Name" so subject columns start from 2.
                    Dictionary<int, string> subjectColumnMapping = new Dictionary<int, string>();
                    for (int col = 2; col <= totalCols; col++)
                    {
                        string header = ws.Cells[1, col].Text.Trim();
                        if (!string.IsNullOrEmpty(header))
                        {
                            subjectColumnMapping[col] = header;
                        }
                    }

                    // Get the selected standard ID.
                    int stdId = Convert.ToInt32(stdList.SelectedValue);

                    // Get current students for this standard.
                    DataTable dtStudents = BAL.BAL_std_student_subject.get_students_by_standard(stdId);
                    // Map student name (trimmed) to student id.
                    Dictionary<string, int> studentMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (DataRow dr in dtStudents.Rows)
                    {
                        string studentName = dr["student_name"].ToString().Trim();
                        int studentId = Convert.ToInt32(dr["id"]);
                        if (!studentMapping.ContainsKey(studentName))
                        {
                            studentMapping.Add(studentName, studentId);
                        }
                    }

                    // Get subjects for this standard.
                    DataTable dtSubjects = BAL.BAL_std_student_subject.get_subjects_by_standard(stdId);
                    // Map subject name (trimmed) to subject id.
                    Dictionary<string, int> subjectIdMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    foreach (DataRow dr in dtSubjects.Rows)
                    {
                        string subjectName = dr["subject_name"].ToString().Trim();
                        int subjectId = Convert.ToInt32(dr["subject_id"]);
                        if (!subjectIdMapping.ContainsKey(subjectName))
                        {
                            subjectIdMapping.Add(subjectName, subjectId);
                        }
                    }

                    // Loop through each data row in the Excel (starting at row 2).
                    for (int row = 2; row <= totalRows; row++)
                    {
                        // Read the student name from the first column.
                        string studentName = ws.Cells[row, 1].Text.Trim();
                        if (string.IsNullOrEmpty(studentName))
                            continue;

                        if (!studentMapping.TryGetValue(studentName, out int studentId))
                        {
                            // Student not found in this standard. Log or skip.
                            continue;
                        }

                        // Process each subject column.
                        foreach (KeyValuePair<int, string> kvp in subjectColumnMapping)
                        {
                            int colIndex = kvp.Key;
                            string subjectName = kvp.Value;

                            if (!subjectIdMapping.TryGetValue(subjectName, out int subjectId))
                            {
                                // Subject not found in this standard.
                                continue;
                            }

                            // Read the mark value from the cell.
                            string markText = ws.Cells[row, colIndex].Text.Trim();
                            if (!string.IsNullOrEmpty(markText))
                            {
                                if (decimal.TryParse(markText, out decimal mark))
                                {
                                    // Insert (or update) the mark in the database.
                                    // This method call can be modified if you need to update instead of insert.
                                    BAL.BAL_std_student_subject.InsertStudentMark(studentId, subjectId, mark);
                                }
                                else
                                {
                                    // Optionally log an error for invalid mark format.
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Marks imported successfully!');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Error importing Excel: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }
    }
}
