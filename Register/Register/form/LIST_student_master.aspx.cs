using Register.BAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_studet_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridView();
            }
        }

        private void gridView()
        {
            DataTable dt = BAL_student_master.get_student_details();
            grid_stud.DataSource = dt;
            grid_stud.DataBind();
        }

        protected void addStd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_student_master.aspx");
        }

        protected void grid_stud_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_data")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int ret_id = BAL_student_master.delete_student_details(id);
                if (ret_id > 0)
                {
                    gridView();
                    lbl1.Text = "Record deleted successfully";
                    lbl1.Style.Add("color", "red");
                }
            }
        }

        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string url = "FORM_student_master.aspx?id=" + id;
            string script = "window.open('" + url + "','_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}
