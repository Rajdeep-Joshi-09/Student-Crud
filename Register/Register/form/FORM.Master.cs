using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class FORM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void ListItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = Convert.ToInt32(ListItems.SelectedValue);

           
           switch(selectedValue)
            {
                case 1:
                    Response.Redirect("LIST_student_master.aspx");
                    break;
                case 2:
                    Response.Redirect("LIST_bus_master.aspx");
                    break;
                case 3:
                    Response.Redirect("LIST_route_master.aspx");
                    break;
                case 4:
                    Response.Redirect("LIST_stops_master.aspx");
                    break;
                case 5:
                    Response.Redirect("LIST_driver_master.aspx");
                    break;
                case 6:
                    Response.Redirect("LIST_bus_wise_driver.aspx");
                    break;
                case 7:
                    Response.Redirect("LIST_standerd_master.aspx");
                    break;
                case 8:
                    Response.Redirect("LIST_std_wise_student.aspx");
                    break;
                case 9:
                    Response.Redirect("LIST_subject_master.aspx");
                    break;
                case 10:
                    Response.Redirect("LIST_student_subject.aspx");
                    break;
                case 11:
                    Response.Redirect("LIST_student_route.aspx");
                    break;
            }

        
            
        }
    }
}
