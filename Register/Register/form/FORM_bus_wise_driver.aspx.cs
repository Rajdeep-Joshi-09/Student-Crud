using Register.BAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM_bus_wise_driver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDowns(); 

                if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {
            int bus_id = Convert.ToInt32(busList.SelectedValue);
            int route_id = Convert.ToInt32(routeList.SelectedValue);
            int driver_id = Convert.ToInt32(driverList.SelectedValue);

            if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_bus_wise_driver.update_bus_wise_route_wise_driver(editId, bus_id, route_id, driver_id);
                if (retId > 0)
                {
                    Response.Redirect("LIST_bus_wise_driver.aspx");
                }
                else
                {
                    lbl1.Text = "Error updating record!";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {
                int ret_id = BAL_bus_wise_driver.insert_bus_wise_route_wise_driver(bus_id, route_id, driver_id);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data inserted successfully!";
                    lbl1.Style.Add("color", "green");
                }
                else
                {
                    lbl1.Text = "Error inserting record!";
                    lbl1.Style.Add("color", "red");
                }
            }
        }

        public void editData(int edit_id)
        {
            DataTable dt = BAL_bus_wise_driver.edit_bus_wise_route_wise_driver(edit_id);



            busList.SelectedValue = dt.Rows[0]["brd_bus_id"].ToString();


            routeList.SelectedValue = dt.Rows[0]["brd_route_id"].ToString();


            driverList.SelectedValue = dt.Rows[0]["brd_driver_id"].ToString();

        }

        private void LoadDropDowns()
        {

            DataTable dtBus = BAL_bus_wise_driver.get_bus_details();
            DataTable dtRoute = BAL_bus_wise_driver.get_route_details();
            DataTable dtDriver = BAL_bus_wise_driver.get_driver_details();

            busList.DataSource = dtBus;
            busList.DataTextField = "bus_name"; 
            busList.DataValueField = "bus_id";
            busList.DataBind();

            routeList.DataSource = dtRoute;
            routeList.DataTextField = "route_name"; 
            routeList.DataValueField = "route_id";
            routeList.DataBind();

            driverList.DataSource = dtDriver;
            driverList.DataTextField = "driver_name"; 
            driverList.DataValueField = "driver_id";
            driverList.DataBind();

          
            busList.Items.Insert(0, new ListItem("-- Select Bus --", "0"));
            routeList.Items.Insert(0, new ListItem("-- Select Route --", "0"));
            driverList.Items.Insert(0, new ListItem("-- Select Driver --", "0"));
        }
    }
}
