using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_std_student_subject : System.Web.UI.Page
    {
        // Store the list of student IDs whose subjects are expanded.
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindStandards();
            }
        }

        // Load Standard List into Dropdown.
        public void bindStandards()
        {
            DataTable dt = BAL.BAL_std_student_subject.get_standerd_details();
            stdList.DataSource = dt;
            stdList.DataTextField = "std_name";
            stdList.DataValueField = "std_id";
            stdList.DataBind();
            stdList.Items.Insert(0, new ListItem("-- Select Standard --", "0"));
        }

        // Handle Standard Selection Change.
        protected void stdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset expanded state when standard changes.
            ExpandedStudents = new List<int>();
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

        // Load Students into Repeater Based on Selected Standard.
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

        // Toggle the subject detail row when expand/collapse is clicked.
        protected void rptStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ExpandSubjects")
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                // Toggle: if already expanded, remove it; otherwise, add it.
                if (ExpandedStudents.Contains(studentId))
                    ExpandedStudents.Remove(studentId);
                else
                    ExpandedStudents.Add(studentId);

                int stdId = Convert.ToInt32(stdList.SelectedValue);
                LoadStudents(stdId);
            }
        }

        // Bind subjects to the inner repeater if the student is expanded.
        protected void rptStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
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
                }
            }
        }

        // Helper method for inline binding: returns "table-row" if the student is expanded, otherwise "none".
        public string GetDisplayStyle(object studentIdObj)
        {
            int studentId = Convert.ToInt32(studentIdObj);
            return ExpandedStudents.Contains(studentId) ? "table-row" : "none";
        }
    }
}
