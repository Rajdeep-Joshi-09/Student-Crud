using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.BAL
{
    public partial class FORM_std_wise_student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDown();
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        public void editData(int id)
        {

            DataTable dt = BAL.BAL_std_wise_student.edit_sws_details(id);
            stdDetails.SelectedValue = dt.Rows[0]["sws_standerd_id"].ToString();
            studDetails.SelectedValue = dt.Rows[0]["sws_student_id"].ToString();
        }

        private void LoadDropDown()
        {
            DataTable dtStanderd = BAL_std_wise_student.get_standerd_details();
            DataTable dtStudent = BAL_std_wise_student.get_student_detials();

            stdDetails.DataSource = dtStanderd;
            stdDetails.DataTextField = "std_name";
            stdDetails.DataValueField = "std_id";
            stdDetails.DataBind();

            studDetails.DataSource = dtStudent;
            studDetails.DataTextField = "student_name";
            studDetails.DataValueField = "id";
            studDetails.DataBind();

            stdDetails.Items.Insert(0, new ListItem("--Select Standerd--"));
            studDetails.Items.Insert(0, new ListItem("--Select Student--"));

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            int stdId = stdDetails.SelectedIndex;
            int studId = studDetails.SelectedIndex;

            if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int ret_id = BAL_std_wise_student.update_sws_details(editId, stdId, studId);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion successful...";
                    lbl1.Style.Add("color", "green");
                    stdDetails.ClearSelection();
                    studDetails.ClearSelection();
                }
                else
                {
                    lbl1.Text = "Got an error...";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {
                //INSERT
                int ret_id = BAL_std_wise_student.insert_std_wise_student(stdId, studId);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion successful...";
                    lbl1.Style.Add("color", "green");
                    stdDetails.ClearSelection();
                    studDetails.ClearSelection();
                }
                else
                {
                    lbl1.Text = "Got an error...";
                    lbl1.Style.Add("color", "red");
                }
            }



        }
    }
}