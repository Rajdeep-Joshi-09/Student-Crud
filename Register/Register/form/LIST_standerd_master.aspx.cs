using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_standerd_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridView();
            }
        }

        public void gridView()
        {
            DataTable dt = BAL.BAL_standerd_master.get_standerd_details();
            grid_standerds.DataSource = dt;
            grid_standerds.DataBind();
        }

      

        protected void grid_standerds_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_standerd")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int ret_id = BAL.BAL_standerd_master.delete_standerd_details(id);
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
            string url = "FORM_standerd_master.aspx?id=" + id;
            string script = "window.open('" + url + "','_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        protected void btnStd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_standerd_master.aspx");
        }
    }
}