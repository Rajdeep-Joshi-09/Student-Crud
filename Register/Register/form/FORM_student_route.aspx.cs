using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_student_route : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindData();
                if(Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        public void editData(int id)
        {
            DataTable dt = BAL.BAL_student_to_route.edit_student_route_details(id);
            routeList.SelectedValue = dt.Rows[0]["brd_id"].ToString();
            stopList.SelectedValue = dt.Rows[0]["stop_id"].ToString();
        } 

        public void bindData ()
        {
            DataTable bindRoute = BAL.BAL_student_to_route.getAll_route_details();
            routeList.DataSource = bindRoute;
            routeList.DataTextField = "RouteDetails";
            routeList.DataValueField = "brd_id";
            routeList.DataBind();
            routeList.Items.Insert(0, new ListItem("--select route--"));

            DataTable bindStop = BAL.BAL_student_to_route.getAll_stop_details();
            stopList.DataSource = bindStop;
            stopList.DataTextField = "stop_name";
            stopList.DataValueField = "stop_id";
            stopList.DataBind();
            stopList.Items.Insert(0, new ListItem("--select stop--"));
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            int strId = routeList.SelectedIndex;
            int stopId = stopList.SelectedIndex;
            if(strId == 0)
            {
                l1.Text = "Please select value";
                l1.Style.Add("color", "red");
            } if(stopId == 0)
            {
                l2.Text = "Please select Stop";
                l2.Style.Add("color", "red");
                return;
            }

            if(Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int ret_id = BAL.BAL_student_to_route.update_studet_route_details(editId, strId, stopId);
                if (ret_id > 0)
                {
                    Response.Redirect("LIST_student_route.aspx");
                }
            } else
            {
                int ret_id = BAL.BAL_student_to_route.insert_student_route_details(strId, stopId);
                if(ret_id > 0)
                {
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    routeList.ClearSelection();
                    stopList.ClearSelection();
                }
            }
        } 
    }
}