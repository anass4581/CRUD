using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = (DataTable)Session["StudentData"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    TextBox1.Text = dt.Rows[0]["studentid"].ToString();
                    TextBox2.Text = dt.Rows[0]["studentname"].ToString();
                    TextBox3.Text = dt.Rows[0]["address"].ToString();
                    TextBox4.Text = dt.Rows[0]["age"].ToString();
                }

                TextBox1.ReadOnly = true;
                TextBox2.ReadOnly = true;
                TextBox3.ReadOnly = true;
                TextBox4.ReadOnly = true;
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
                gridview3.DataSource = dt;
                gridview3.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
            bool isSecondClick = (Session["IsSecondClick"] != null && (bool)Session["IsSecondClick"]);

            
            if (!isSecondClick)
            {
                TextBox1.ReadOnly = false;
                TextBox2.ReadOnly = false;
                TextBox3.ReadOnly = false;
                TextBox4.ReadOnly = false;

               
                Session["IsSecondClick"] = true;
                return;
            }

            
            Session["IsSecondClick"] = false;

            
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please fill in all fields.');", true);
                return;
            }

            
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string t1 = TextBox1.Text;
                string t2 = TextBox2.Text;
                string t3 = TextBox3.Text;
                string t4 = TextBox4.Text;

                string query = "UPDATE student_info SET studentname = @studentname, address = @address, age = @age WHERE studentid = @studentid";
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    comm.Parameters.AddWithValue("@studentname", t2);
                    comm.Parameters.AddWithValue("@address", t3);
                    comm.Parameters.AddWithValue("@age", t4);
                    comm.Parameters.AddWithValue("@studentid", t1);

                    comm.ExecuteNonQuery();
                }

                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated'); window.location='Default.aspx';", true);

                
                LoadRecord();
            }
        }




        protected void Button6_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM student_info where studentid ='" + TextBox1.Text + "'", conn);
                SqlDataAdapter d = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                d.Fill(dt);
                gridview3.DataSource = dt;
                gridview3.DataBind();
                //TextBox1.Text = "";
                //TextBox2.Text = "";
                //TextBox3.Text = "";
                //TextBox4.Text = "";
                // Response.Redirect("WebForm2.aspx");

            }


        }



        protected void Button3_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string t1 = TextBox1.Text;

                string query = "DELETE  student_info  where studentid= '" + t1 + "'";
                using (SqlCommand comm = new SqlCommand(query, conn))
                {

                    comm.ExecuteNonQuery();
                }

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Deleted');", true);
                LoadRecord();
                if (!ClientScript.IsClientScriptBlockRegistered(this.GetType(), "script"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Deleted  Data');", true);
                }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirectScript", "window.location='Default.aspx';", true);
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
            }
        }
    }
}