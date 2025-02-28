using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_student_subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBindsData();
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        public void LoadBindsData()
        {
            // Fetch student data
            DataTable studData = BAL.BAL_student_subject.get_student_details();

            studList.DataSource = studData;
            studList.DataTextField = "student_name";
            studList.DataValueField = "id";
            studList.DataBind();
            studList.Items.Insert(0, new ListItem("--Select Student--", "0"));
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            int stdId = Convert.ToInt32(studList.SelectedValue);

            if (stdId == 0)
            {
                l1.Text = "Please select a student";
                lbl1.Text = "";
                return;
            }

            List<int> selectedSubjects = new List<int>();

            foreach (ListItem item in chkSubjects.Items)
            {
                if (item.Selected)
                {
                    selectedSubjects.Add(Convert.ToInt32(item.Value));
                }
            }

            if (selectedSubjects.Count == 0)
            {
                lbl1.Text = "Please select at least one subject!";
                lbl1.Style.Add("color", "red");
                return;
            }

            int successCount = 0;

            foreach (int subId in selectedSubjects)
            {
                int result = BAL.BAL_student_subject.insert_student_to_subject(subId, stdId);
                if (result > 0)
                {
                    successCount++;
                }
            }

            if (successCount > 0)
            {
                lbl1.Text = $"{successCount} subject(s) assigned successfully!";
                lbl1.Style.Add("color", "green");
                studList.ClearSelection();
                chkSubjects.Items.Clear();
                l1.Text = "";
            }
            else
            {
                lbl1.Text = "Error occurred while inserting data.";
                lbl1.Style.Add("color", "red");
            }
        }

        public void editData(int editId)
        {
            DataTable dt = BAL.BAL_student_subject.edit_student_subject(editId);
            if (dt.Rows.Count > 0)
            {
                studList.SelectedValue = dt.Rows[0]["ss_student_id"].ToString();
            }
        }

        protected void viewSub_Click(object sender, EventArgs e)
        {
            int nameId = Convert.ToInt32(studList.SelectedValue);

            if (nameId == 0)
            {
                l1.Text = "Please select a student!";
                return;
            }

            int std_id = BAL.BAL_student_subject.get_standard_id(nameId);
            DataTable subjects = BAL.BAL_student_subject.get_subject_details_from_stdId(std_id);

            chkSubjects.Items.Clear();

            if(subjects.Rows.Count == 0) {
                lbl1.Text = "No subjects found for this student";
                lbl1.Style.Add("color", "red");
                return;
            }
            lbl1.Text = "";

            foreach (DataRow row in subjects.Rows)
            {
                ListItem listItem = new ListItem(row["subject_name"].ToString(), row["subject_id"].ToString());
                chkSubjects.Items.Add(listItem);
            }
        }

        

    }
}
