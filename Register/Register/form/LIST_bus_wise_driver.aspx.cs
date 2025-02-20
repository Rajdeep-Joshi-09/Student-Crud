using Register.BAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_bus_wise_driver : System.Web.UI.Page
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
            DataTable dt = BAL_bus_wise_driver.get_bus_wise_route_wise_driver();
            grid_bus_route_driver.DataSource = dt;
            grid_bus_route_driver.DataBind();
        }

        protected void addBusRouteDriver_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_bus_wise_driver.aspx");
        }

        protected void grid_bus_route_driver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_bus_route_driver")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                int ret_id = BAL_bus_wise_driver.delete_bus_wise_route_wise_driver(id);
                if (ret_id > 0)
                {
                    lbl1.Text = "Record deleted successfully!";
                    lbl1.Style.Add("color", "red");
                    gridView();
                }
            }
        }

        protected void btn_edit_Click(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string url = "FORM_bus_wise_driver.aspx?id=" + id;
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

       
    }
}
