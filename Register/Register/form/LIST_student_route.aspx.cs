using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_student_route : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gridView();
            }
        }

        public void gridView()
        {
            DataTable dt = BAL.BAL_student_to_route.select_student_route_details();
            grid_str.DataSource = dt;
            grid_str.DataBind();
        }
        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string url = "FORM_student_route.aspx?id=" + id;
            string script = "window.open('" + url + "','_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

   

        protected void del_btn_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            int ret_id = BAL.BAL_student_to_route.delete_student_route_details(id);
            if(ret_id > 0)
            {
                lbl1.Text = "Record deleted SuccessFull...";
                lbl1.Style.Add("color", "red");
                gridView();
            }
        }

        protected void btnStr_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_student_route.aspx");
        }
    }
}