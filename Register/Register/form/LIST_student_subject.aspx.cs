using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Register.BAL;

namespace Register.form
{
    public partial class LIST_student_subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            DataTable dt = BAL_student_subject.get_all_student_subject();
            grid_ss.DataSource = dt;
            grid_ss.DataBind();
        }

        protected void btn_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("FORM_student_subject.aspx?id=" + id);
        }

        protected void del_edit_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            int result = BAL_student_subject.delete_student_subject(id);

            if (result > 0)
            {
                lbl1.Text = "Record Deleted Successfully!";
                lbl1.ForeColor = System.Drawing.Color.Red;
                LoadGridView();
            }
        }

        protected void btnSS_Click(object sender, EventArgs e)
        {
            Response.Redirect("FORM_student_subject.aspx");
        }
    }
}
