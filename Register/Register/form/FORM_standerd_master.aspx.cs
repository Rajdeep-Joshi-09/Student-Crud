using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_standerd_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {
            String std_name = stdName.Text;
            if (Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL.BAL_standerd_master.update_standerd_record(editId, std_name);
                if (retId > 0)
                {
                    Response.Redirect("LIST_standerd_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {


                int ret_id = BAL.BAL_standerd_master.rajdeep_insert_standerd_master(std_name);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    stdName.Text = "";
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
        }

        public void editData(int edit_id)
        {
            DataTable dt = BAL.BAL_standerd_master.edit_standerd_records(edit_id);

            stdName.Text = dt.Rows[0]["std_name"].ToString();
        }
    }
    }
