
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRecord();
                TextBox1.Text = GetNextStudentId().ToString();
                TextBox1.ReadOnly = false;
            }
            

        }
        private int GetNextStudentId()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT ISNULL(MAX(studentid), 0) + 1 FROM student_info", conn);
                object result = comm.ExecuteScalar();
                int nextStudentId = Convert.ToInt32(result);
                return nextStudentId;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please fill in all fields.');", true);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string t1 = TextBox1.Text;
                    string t2 = TextBox2.Text;
                    string t3 = TextBox3.Text;
                    string t4 = TextBox4.Text;

                    string query = "INSERT INTO student_info VALUES(@t1, @t2, @t3, @t4)";
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        comm.Parameters.AddWithValue("@t1", t1);
                        comm.Parameters.AddWithValue("@t2", t2);
                        comm.Parameters.AddWithValue("@t3", t3);
                        comm.Parameters.AddWithValue("@t4", t4);

                        comm.ExecuteNonQuery();
                    }

                    LoadRecord();
                    // ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "script", "alert('Successfully Inserted Data');", true );
                    if (!ClientScript.IsClientScriptBlockRegistered(this.GetType(), "script"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Inserted Data');", true);
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirectScript", "window.location='Default.aspx';", true);

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Student ID already exists.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('An error occurred while inserting data.');", true);
                    }
                }
            }
        }
        void LoadRecord()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM student_info", conn);
                SqlDataAdapter d = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                d.Fill(dt);
                gridview1.DataSource = dt;
                gridview1.DataBind();
            }
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM student_info where studentid = @studentid", conn);
                comm.Parameters.AddWithValue("@studentid", TextBox1.Text);
                SqlDataAdapter d = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                d.Fill(dt);

                
                Session["StudentData"] = dt;

                Response.Redirect("WebForm2.aspx");
            }
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");
        }
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text=  string.Empty;
            TextBox4.Text = string.Empty;
        }
    }
}
