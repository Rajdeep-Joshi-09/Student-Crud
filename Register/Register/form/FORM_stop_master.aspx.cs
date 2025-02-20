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
    public partial class FORM_stop_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {

            String stop_name = name.Text;
            if(Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_stop_master.update_stop_record(editId, stop_name);
                if(retId > 0)
                {
                    Response.Redirect("LIST_stops_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {


            int ret_id = BAL_stop_master.rajdeep_insert_stop_master(stop_name);
            if(ret_id > 0)
            {
                lbl1.Text = "Data insertion Successfull...";
                lbl1.Style.Add("color", "green");
                name.Text = "";
            }
            else
            {
                lbl1.Text = "Got an error";
                lbl1.Style.Add("color", "red");
            }
            }
        }

        public void editData (int edit_id)
        {
            DataTable dt = BAL_stop_master.edit_stop_records(edit_id);

            name.Text = dt.Rows[0]["stop_name"].ToString();
        }
    }
}