using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_std_student_subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindStandards();
            }
        }

        // Load Standard List into Dropdown
        public void bindStandards()
        {
            DataTable dt = BAL.BAL_std_student_subject.get_standerd_details();
            stdList.DataSource = dt;
            stdList.DataTextField = "std_name";
            stdList.DataValueField = "std_id";
            stdList.DataBind();
            stdList.Items.Insert(0, new ListItem("-- Select Standard --", "0"));
        }

        // Handle Standard Selection Change
        protected void stdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Close all opened subjects when standard changes
            ClearSubjects();

            int stdId = Convert.ToInt32(stdList.SelectedValue);
            if (stdId > 0)
            {
                LoadStudents(stdId);
            }
            else
            {
                grid_students.DataSource = null;
                grid_students.DataBind();
                lblMessage.Text = "Please select a valid standard.";
            }
        }

        // Load Students into Grid Based on Selected Standard
        private void LoadStudents(int stdId)
        {
            DataTable dt = BAL.BAL_std_student_subject.get_students_by_standard(stdId);
            if (dt.Rows.Count > 0)
            {
                grid_students.DataSource = dt;
                grid_students.DataBind();
            }
            else
            {
                grid_students.DataSource = null;
                grid_students.DataBind();
                lblMessage.Text = "No students found for this standard.";
                return;
            }
            lblMessage.Text = "";
        }
        
        // Handle GridView Commands (Expand/Collapse Subjects)
        protected void grid_students_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ExpandSubjects")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grid_students.Rows[rowIndex];
                int studentId = Convert.ToInt32(grid_students.DataKeys[rowIndex].Value);
                int stdId = Convert.ToInt32(stdList.SelectedValue);

                Button btnExpand = (Button)row.FindControl("btnExpand");
                GridView nestedGrid = (GridView)row.FindControl("grid_subjects");

                if (btnExpand != null && nestedGrid != null)
                {
                    if (nestedGrid.Visible)
                    {
                        // Hide subjects and change `x` back to `+`
                        nestedGrid.Visible = false;
                        btnExpand.Text = "+";
                    }
                    else
                    {
                        // Load subjects and change `+` to `x`
                        DataTable dtSubjects = BAL.BAL_std_student_subject.get_subjects_by_student(studentId, stdId);
                        nestedGrid.DataSource = dtSubjects;
                        nestedGrid.DataBind();
                        nestedGrid.Visible = true;
                        btnExpand.Text = "x";
                    }
                }
            }
        }

        // Hide all opened subjects when standard changes
        private void ClearSubjects()
        {
            foreach (GridViewRow row in grid_students.Rows)
            {
                GridView nestedGrid = (GridView)row.FindControl("grid_subjects");
                Button btnExpand = (Button)row.FindControl("btnExpand");

                if (nestedGrid != null)
                {
                    nestedGrid.Visible = false;
                }
                if (btnExpand != null)
                {
                    btnExpand.Text = "+";
                }
            }
        }
    }
}
