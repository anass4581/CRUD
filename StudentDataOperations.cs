//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WebApplication7
//{
//    public class StudentDataOperations : IStudentManagement, IStudentUpdate, IStudentDelete, IClearAll
//    {
//        public void LoadRecord()
//        {
//            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                SqlCommand comm = new SqlCommand("SELECT * FROM student_info", conn);
//                SqlDataAdapter d = new SqlDataAdapter(comm);
//                DataTable dt = new DataTable();
//                d.Fill(dt);
//                //gridview2.DataSource = dt;
//                //gridview2.DataBind();
//            }

//        }

//        public void UpdateRecord()
//        {
//            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text))
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please fill in all fields.');", true);
//                return;
//            }

//            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                string t1 = TextBox1.Text;
//                string t2 = TextBox2.Text;
//                string t3 = TextBox3.Text;
//                string t4 = TextBox4.Text;

//                string query = "UPDATE  student_info SET studentname ='" + t2 + "',address='" + t3 + "',age='" + t4 + "' where studentid= '" + t1 + "'";
//                using (SqlCommand comm = new SqlCommand(query, conn))
//                {

//                    comm.ExecuteNonQuery();
//                }
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
//                LoadRecord();
//                TextBox1.Text = "";
//                TextBox2.Text = "";
//                TextBox3.Text = "";
//                TextBox4.Text = "";
//            }

//        }

//        public void DeleteRecord()
//        {
//            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                string t1 = TextBox1.Text;

//                string query = "DELETE  student_info  where studentid= '" + t1 + "'";
//                using (SqlCommand comm = new SqlCommand(query, conn))
//                {

//                    comm.ExecuteNonQuery();
//                }

//                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Deleted');", true);
//                LoadRecord();
//                if (!ClientScriptManager.IsClientScriptBlockRegistered(this.GetType(), "script"))
//                {
//                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Updated Data');", true);
//                }
//                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirectScript", "window.location='WebForm1.aspx';", true);
//                TextBox1.Text = "";
//                TextBox2.Text = "";
//                TextBox3.Text = "";
//                TextBox4.Text = "";
//            }

//        }
//        public void ClearAll()
//        {
//            TextBox1.Text = "";
//            TextBox2.Text = "";
//            TextBox3.Text = "";
//            TextBox4.Text = "";

//        }
//    }
//}



using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public class StudentDataOperations : IStudentManagement, IStudentUpdate, IStudentDelete, IClearAll
    {
        public void LoadRecord()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM student_info", conn);
                SqlDataAdapter d = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                d.Fill(dt);

               
            }
        }

        public void UpdateRecord(string studentId, string studentName, string address, string age)
        {
           
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE student_info SET studentname = @studentName, address = @address, age = @age WHERE studentid = @studentId";
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    comm.Parameters.AddWithValue("@studentId", studentId);
                    comm.Parameters.AddWithValue("@studentName", studentName);
                    comm.Parameters.AddWithValue("@address", address);
                    comm.Parameters.AddWithValue("@age", age);
                    comm.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRecord(string studentId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM student_info WHERE studentid = @studentId";
                using (SqlCommand comm = new SqlCommand(query, conn))
                {
                    comm.Parameters.AddWithValue("@studentId", studentId);
                   
                    comm.ExecuteNonQuery();
                }

            }
        }

        //public void ClearAll(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4)
        //{
        //    textBox1.Text = string.Empty;
        //    textBox2.Text = string.Empty;
        //    textBox3.Text = string.Empty;
        //    textBox4.Text = string.Empty;

        //}
        public void ClearAll(System.Web.UI.WebControls.TextBox textBox1, System.Web.UI.WebControls.TextBox textBox2, System.Web.UI.WebControls.TextBox textBox3, System.Web.UI.WebControls.TextBox textBox4)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }



    }
}