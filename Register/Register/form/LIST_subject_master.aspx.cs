using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_subject_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gridView();
            }
        }

        public void gridView ()
        {
            DataTable dt = BAL.BAL_subject_master.get_subject_details();
            grid_sub.DataSource = dt;
            grid_sub.DataBind();
        }

        protected void addSub_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_subject_master.aspx");
        }


        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string url = "FORM_subject_master.aspx?id=" + id;
            string script = "window.open('" + url + "','_blank')";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        protected void btn_del_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            int ret_id = BAL.BAL_subject_master.delete_subject_details(id);
            if(ret_id > 0)
            {
                lbl1.Text = "Record Deleted Successfull...";
                lbl1.Style.Add("color", "red");
                gridView();
            }
        }
    }
}