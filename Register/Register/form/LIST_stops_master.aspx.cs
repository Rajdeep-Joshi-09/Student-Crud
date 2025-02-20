using Register.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_stops_master : System.Web.UI.Page
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
            DataTable dt = BAL_stop_master.get_stops_details();
            grid_stops.DataSource = dt;
            grid_stops.DataBind();
        }

        protected void btnStops_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_stop_master.aspx");
        }

        protected void grid_stops_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "delete_stop")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int ret_id = BAL_stop_master.delete_stop_details(id);
                if(ret_id>0)
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
            int edit_id = Convert.ToInt32(grid_stops.DataKeys[id].Value);
            string url = "FORM_stop_master.aspx?id=" + edit_id;
            string script = "window.open('" + url + "','_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}