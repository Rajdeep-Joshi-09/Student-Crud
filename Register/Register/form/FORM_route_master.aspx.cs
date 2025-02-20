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
    public partial class FORM_route_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    edit_data(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {
            string route_name = txt_route_name.Text;
            string route_time = txt_route_time.Text;

            if(Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int edit_id = Convert.ToInt32(Request.QueryString["id"]);
                int ret_id1 = BAL_route_master.update_route_detail(edit_id, route_name, route_time);
                if(ret_id1 > 0)
                {
                    Response.Redirect("LIST_route_master.aspx");
                } else
                {
                    lbl1.Text = "Got an error in update";
                    lbl1.Style.Add("color", "red");
                }
            }

            int ret_id = BAL_route_master.route_master(route_name, route_time);
            if(ret_id > 0)
            {
                lbl1.Text = "Data insertion Successfull...";
                lbl1.Style.Add("color", "green");
                txt_route_name.Text = "";
                txt_route_time.Text = "";
            }
            else
            {
                lbl1.Text = "Got an error";
                lbl1.Style.Add("color", "red");
            }
        }

        public void edit_data(int edit_id)
        {
            DataTable dt = BAL_route_master.edit_route_detials(edit_id);
            txt_route_name.Text = dt.Rows[0]["route_name"].ToString();
            txt_route_time.Text = dt.Rows[0]["route_start_time"].ToString();
        }
    }
}