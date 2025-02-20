using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_subject_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
            LoadTables();
                if(Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }

        }

        public void editData(int id)
        {
            DataTable dt = BAL.BAL_subject_master.edit_subject_details(id);
            txtName.Text = dt.Rows[0]["subject_name"].ToString();
            txtCode.Text = dt.Rows[0]["subject_code"].ToString();
            txtCredit.Text = dt.Rows[0]["subject_credit"].ToString();
            stdList.SelectedValue = dt.Rows[0]["subject_standerd_id"].ToString();
        }

        public void LoadTables()
        {
            DataTable dt = BAL.BAL_subject_master.get_standerd_details();
            stdList.DataSource = dt;
            stdList.DataTextField = "std_name";
            stdList.DataValueField = "std_id";
            stdList.DataBind();
            stdList.Items.Insert(0, new ListItem("--Select Standerd--"));
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string code = txtCode.Text;
            int credit = Convert.ToInt32(txtCredit.Text);
            int stdlist = stdList.SelectedIndex;

            //update
            if(Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int ret_id = BAL.BAL_subject_master.update_subject_details(editId, name, code, credit, stdlist);
                if(ret_id > 0)
                {
                    Response.Redirect("LIST_subject_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            } else
            {
                int ret_id = BAL.BAL_subject_master.insert_subject_master(name, code, credit, stdlist);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data Insertion successfull...";
                    lbl1.Style.Add("color", "green");
                    txtName.Text = "";
                    txtCode.Text = "";
                    txtCredit.Text = "";
                    stdList.ClearSelection();
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            }
     
           
        }
    }

