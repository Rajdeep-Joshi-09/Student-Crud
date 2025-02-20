﻿using Register.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class LIST_driver_master : System.Web.UI.Page
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
            DataTable dt = BAL_driver_master.get_driver_details();
            grid_driver.DataSource = dt;
            grid_driver.DataBind();
        }

        protected void addDriver_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_driver_master.aspx");
        }

        protected void grid_driver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete_driver")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int ret_id = BAL_driver_master.delete_driver_details(id);
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
            int edit_id = Convert.ToInt32(grid_driver.DataKeys[id].Value);
            string url = "FORM_driver_master.aspx?id=" + edit_id;
            string script = "window.open('" + url + "','_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}