using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_student_subject : System.Web.UI.Page
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
            DataTable dt = BAL.BAL_student_subject.get_all_student_subject();
            grid_ss.DataSource = dt;
            grid_ss.DataBind();
        }

        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string url = "FORM_student_subject.aspx?id=" + id;
            string script = "window.open('" + url +"','_blank')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        protected void del_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            int ret_id = BAL.BAL_student_subject.delete_student_subject(id);
            if(ret_id > 0)
            {
                lbl1.Text = "Record Deleted Successfull..";
                lbl1.Style.Add("color", "red");
                gridView();
            }
        }

        protected void btnSS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_student_subject.aspx");
        }
    }
}