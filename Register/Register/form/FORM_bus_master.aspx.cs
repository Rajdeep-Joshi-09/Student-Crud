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
    public partial class FORM_bus_master : System.Web.UI.Page
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
            string bus_name = name.Text;
            string bus_number = txt_bus_number.Text;
            if (Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_bus_master.update_bus_record(editId, bus_name, bus_number);
                if (retId > 0)
                {
                    Response.Redirect("LIST_bus_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {


                String stop_name = name.Text;
                int ret_id = BAL_bus_master.insert_bus_master(bus_name, bus_number);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    name.Text = "";
                    txt_bus_number.Text = "";
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
            DataTable dt = BAL_bus_master.edit_bus_records(edit_id);

            name.Text = dt.Rows[0]["bus_name"].ToString();
            txt_bus_number.Text = dt.Rows[0]["bus_number"].ToString();
        }
    }
}