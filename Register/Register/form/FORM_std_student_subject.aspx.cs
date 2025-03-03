using System;
using System.Collections.Generic;
using System.Data;
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
        // Key: studentId; Value: Dictionary where key = subjectId (as string), value = mark
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
                    // Use hfSubjectId as the key
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

        // Global submit: validates that at least one student's marks are fully entered,
        // and for each expanded student, ensures no subject is missing marks.
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
    }
}
