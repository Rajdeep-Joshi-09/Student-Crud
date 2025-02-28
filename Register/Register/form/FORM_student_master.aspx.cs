using Register.BAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;


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
            string fname = txtFirstname.Text;
            string mname = txtMiddleName.Text;
            string lname = txtLastName.Text;
            string email = txtEmail.Text;
            string mobile = txtMobile.Text;
            string gender = genderList.SelectedValue;
            int city = cityList.SelectedIndex;
            string address = txtAddress.Text;


            if (Request.QueryString["id"] != null & !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int editId = Convert.ToInt32(Request.QueryString["id"]);
                int retId = BAL_student_master.update_student_records(editId, fname, mname, lname, email, mobile, gender, city, address);
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
                string checkResult = BAL.BAL_student_master.check_student_details(email, mobile);

                if (checkResult == "0")
                {
                    lbl1.Text = "Both email and mobile already exist.";
                    lbl1.Style.Add("color", "red");
                    mobileLabel.Text = "";
                    emailLabel.Text = "";
                    return;
                }
                else if (checkResult == "1")
                {
                    mobileLabel.Text = "Mobile number already exists.";
                    lbl1.Text = "";
                    emailLabel.Text = "";
                    return;
                }
                else if (checkResult == "2")
                {
                    emailLabel.Text = "Email already exists.";
                    lbl1.Text = "";
                    mobileLabel.Text = "";
                    return;
                }

                lbl1.Text = "";
                mobileLabel.Text = "";
                emailLabel.Text = "";


                int ret_id = BAL_student_master.rajdeep_insert_student_master(fname, mname, lname, email, mobile, gender, city, address);
                if (ret_id > 0)
                {
                    SendEmail(email, fname + " " + lname);
                    lbl1.Text = "Data insertion Successfull...";
                    lbl1.Style.Add("color", "green");
                    txtFirstname.Text = "";
                    txtMiddleName.Text = "";
                    txtLastName.Text = "";
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
        private void SendEmail(string email, string studentName)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(""); // Replace with your email
                mail.To.Add(email);  // Sends email to the student who registered
                mail.Subject = "Welcome to Our Platform!";
                mail.Body = $"Hello {studentName},\n\nYour registration was successful! We're excited to have you on board.\n\nRegards,\nRajdeep Joshi";
                mail.IsBodyHtml = false;  // Set true if you want to send HTML emails

                string attachmentPath = Server.MapPath("~/IMAGE/1.jpg");

                // Adding attachment if the file exists
                if (System.IO.File.Exists(attachmentPath))
                {
                    mail.Attachments.Add(new Attachment(attachmentPath));
                }
                else
                {
                    lbl1.Text = "Attachment file not found!";
                    lbl1.Style.Add("color", "red");
                    return;
                }
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);  // Use your SMTP provider
                smtp.Credentials = new NetworkCredential("", ""); // Use App Password if using Gmail
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                lbl1.Text = "Error sending email: " + ex.Message;
                lbl1.Style.Add("color", "red");
            }
        }



        public void editData(int edit_id)
        {
            DataTable dt = BAL_student_master.edit_student_records(edit_id);


            txtFirstname.Text = dt.Rows[0]["first_name"].ToString();
            txtMiddleName.Text = dt.Rows[0]["middle_name"].ToString();
            txtLastName.Text = dt.Rows[0]["last_name"].ToString();
            txtEmail.Text = dt.Rows[0]["student_email"].ToString();
            txtAddress.Text = dt.Rows[0]["student_address"].ToString();
            genderList.SelectedValue = dt.Rows[0]["student_gender"].ToString();
            cityList.SelectedValue = dt.Rows[0]["student_city"].ToString();
            txtMobile.Text = dt.Rows[0]["student_mobile"].ToString();
        }

    }
}
