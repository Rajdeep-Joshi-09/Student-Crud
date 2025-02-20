using Register.BAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Register.form
{
    public partial class reg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCity();
                if (Request.QueryString["id"] != null && !String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    editData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
        }

       public void BindCity ()
        {
            DataTable bindCtiy = BAL.BAL_student_master.get_all_city();
            cityList.DataSource = bindCtiy;
            cityList.DataTextField = "CityName";
            cityList.DataValueField = "CityID";
            cityList.DataBind();
            cityList.Items.Insert(0, new ListItem("--select city--"));
        }

        protected void subBtn_Click(object sender, EventArgs e)
        {
            string studentName = txtName.Text;
            string email = txtEmail.Text;
            string address = txtAddress.Text;
            string gender = genderList.SelectedValue;
            int city = cityList.SelectedIndex;
            string mobile = txtMobile.Text;



            if (Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_student_master.update_student_records(editId, studentName, email, address, gender, city, mobile);
                if (retId > 0)
                {
                    Response.Redirect("LIST_student_master.aspx");
                }
                else
                {
                    lbl1.Text = "Got an error";
                    lbl1.Style.Add("color", "red");
                }
            }
            else
            {


                int ret_id = BAL_student_master.rajdeep_insert_student_master(studentName, email, address, gender, city, mobile);
                if (ret_id > 0)
                {
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";
                    genderList.ClearSelection();
                    cityList.ClearSelection();
                    txtMobile.Text = "";
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
            DataTable dt = BAL_student_master.edit_student_records(edit_id);

            txtName.Text = dt.Rows[0]["student_name"].ToString();
            txtEmail.Text = dt.Rows[0]["student_email"].ToString();
            txtAddress.Text = dt.Rows[0]["student_address"].ToString();
            genderList.SelectedValue = dt.Rows[0]["student_gender"].ToString();
            cityList.SelectedValue = dt.Rows[0]["student_city"].ToString();
            txtMobile.Text = dt.Rows[0]["student_mobile"].ToString();
        }

    }
}
