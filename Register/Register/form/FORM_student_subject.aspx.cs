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
            DataTable studData = BAL.BAL_student_subject.get_student_details();
            DataTable subData = BAL.BAL_student_subject.get_subject_detail();

            studList.DataSource = studData;
            studList.DataTextField = "student_name";
            studList.DataValueField = "id";
            studList.DataBind();
            studList.Items.Insert(0, new ListItem("--Select Standered--", "0"));

            subList.DataSource = subData;
            subList.DataTextField = "subject_name";
            subList.DataValueField = "subject_id";
            subList.DataBind();
            subList.Items.Insert(0, new ListItem("--Select Subject--", "0"));

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            int subId = Convert.ToInt32(subList.SelectedValue);
            int stdId = Convert.ToInt32(studList.SelectedValue);

            if (stdId == 0)
            {
                l1.Text = "Please select student";
                lbl1.Text = "";
            }
            if (subId == 0)
            {
                l2.Text = "Please select subject";
                lbl1.Text = "";
                return;
            }
           
                //update
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int ret_id = BAL.BAL_student_subject.update_student_subject(id, subId, stdId);
                if(ret_id > 0)
                {
                    Response.Redirect("LIST_student_subject.aspx");
                }
                }
                //insert
                else
                {
                    int ret_id = BAL.BAL_student_subject.insert_student_to_subject(subId, stdId);
                    if (ret_id > 0)
                    {
                        lbl1.Text = "Data insertion successfull...";
                        lbl1.Style.Add("color", "green");
                        studList.ClearSelection();
                        subList.ClearSelection();
                        l1.Text = "";
                        l2.Text = "";
                    }
                    else
                    {
                        lbl1.Text = "Got an error";
                        lbl1.Style.Add("color", "red");
                    }
                }


            }
        

        public void editData(int editId)
        {
            DataTable dt = BAL.BAL_student_subject.edit_student_subject(editId);
            subList.SelectedValue = dt.Rows[0]["ss_subject_id"].ToString();
            studList.SelectedValue = dt.Rows[0]["ss_student_id"].ToString();
        }

    }
}