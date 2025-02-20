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
    public partial class FORM_driver_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {

            string driverName = txt_driver_name.Text;
            string driverNumber = txt_driver_number.Text;

            if (Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_driver_master.update_driver_record(editId, driverName, driverNumber);
                if (retId > 0)
                {
                    Response.Redirect("LIST_driver_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {


               
                int ret_id = BAL_driver_master.insert_driver_master(driverName, driverNumber);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    txt_driver_name.Text = "";
                    txt_driver_number.Text = "";
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
        }


        public void editData(int edit_id)
        {
            DataTable dt = BAL_driver_master.edit_driver_records(edit_id);

            txt_driver_name.Text = dt.Rows[0]["driver_name"].ToString();
            txt_driver_number.Text = dt.Rows[0]["driver_mobile_number"].ToString();
        }
    }
}