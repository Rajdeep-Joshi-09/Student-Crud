using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_std_wise_student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gridView();
        }

        public void gridView()
        {
            DataTable dt = BAL.BAL_std_wise_student.select_std_wise_student();
            grid_sws.DataSource = dt;
            grid_sws.DataBind();
        }
        protected void addSWS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_std_wise_student.aspx");
        }

    

        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string url = "FORM_std_wise_student.aspx?id=" + id;
            string script = "window.open('" + url + "','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        protected void del_btn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "delete_sws")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int ret_id = BAL.BAL_std_wise_student.delete_sws_details(id);
                if (ret_id > 0)
                {
                    lbl1.Text = "Record Deleted Successfull...";
                    lbl1.Style.Add("color", "red");
                    gridView();
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