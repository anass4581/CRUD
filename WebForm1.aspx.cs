using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication7
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataTable studentData;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //studentData = new DataTable(); // Initialize studentData DataTable
                LoadRecord();
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
                studentData = new DataTable(); // Initialize the DataTable
                d.Fill(studentData);
                gridview2.DataSource = studentData;
                gridview2.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
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

                string query = "UPDATE  student_info SET studentname ='" + t2 + "',address='" + t3 + "',age='" + t4 + "' where studentid= '" + t1 + "'";
                using (SqlCommand comm = new SqlCommand(query, conn))
                {

                    comm.ExecuteNonQuery();
                }
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
                LoadRecord();
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            // Get the student ID from TextBox1
            string studentIdToDelete = TextBox1.Text;

            // Remove the student from the database
            DeleteStudentFromDatabase(studentIdToDelete);

            // Reload the data from the database and bind to GridView
            LoadRecord();

            // Display a success message
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Deleted Data from Application');", true);
        }

        private void DeleteStudentFromDatabase(string studentId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create a SQL command to delete the student
                    string query = "DELETE FROM student_info WHERE studentid = @studentId";

                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        comm.Parameters.AddWithValue("@studentId", studentId);

                        // Execute the command
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('An error occurred while deleting the student: " + ex.Message + "');", true);
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";

        }
    }

}


//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;
// namespace WebApplication7
//{
//    public interface IStudent
//    {
//        void LoadRecord();
//    }
//    public interface IStudentUpdate
//    {
//        void UpdateRecord();
//    }
//    public interface IStudentDelete
//    {
//        void DeleteRecord();
//    }
//    public interface IClearAll
//    {
//        void ClearAll();
//    }
//    public partial class WebForm1 : System.Web.UI.Page, IStudent, IStudentUpdate, IStudentDelete, IClearAll
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                LoadRecord();
//            }
//        }
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
//                gridview2.DataSource = dt;
//                gridview2.DataBind();
//            }
//        }
//        public void UpdateRecord()
//        {

//        }
//        public void DeleteRecord()
//        {

//        }
//        public void ClearAll()
//        {

//        }

//    }




//}


//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WebApplication7
//{
//    public partial class WebForm1 : System.Web.UI.Page, IStudentDataManagement, IStudentDataUpdate, IStudentDataDelete
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                LoadRecord();
//            }
//        }

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
//                gridview2.DataSource = dt;
//                gridview2.DataBind();
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
//                if (!ClientScript.IsClientScriptBlockRegistered(this.GetType(), "script"))
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

//        protected void Button2_Click(object sender, EventArgs e)
//        {

//            UpdateRecord();
//        }

//        protected void Button3_Click(object sender, EventArgs e)
//        {

//            DeleteRecord();
//        }

//        protected void ButtonClear_Click(object sender, EventArgs e)
//        {
//            TextBox1.Text = "";
//            TextBox2.Text = "";
//            TextBox3.Text = "";
//            TextBox4.Text = "";

//        }
//    }


//    public interface IStudentDataManagement
//    {
//        void LoadRecord();
//    }
//    public interface IStudentDataUpdate
//    {
//        void UpdateRecord();
//    }


//    public interface IStudentDataDelete
//    {
//        void DeleteRecord();
//    }
//}


//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;


//namespace WebApplication7
//{
//    public partial class WebForm1 : System.Web.UI.Page
//    {

//        IStudentManagement studentManagement = new StudentDataOperations();
//        IStudentUpdate studentUpdate = new StudentDataOperations();
//        IStudentDelete studentDelete = new StudentDataOperations();
//        IClearAll clearAll = new StudentDataOperations();

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                studentManagement.LoadRecord();
//            }
//        }

//        protected void Button2_Click(object sender, EventArgs e)
//        {

//            string studentId = TextBox1.Text; 
//            string studentName = TextBox2.Text;
//            string address = TextBox3.Text;
//            string age = TextBox4.Text;
//            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text))
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please fill in all fields.');", true);
//                return;
//            }

//            studentUpdate.UpdateRecord(studentId, studentName, address, age);
//            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Updated Data'); window.location='default.aspx';", true);

//        }

//        protected void Button3_Click(object sender, EventArgs e)
//        {

//            string studentId = TextBox1.Text; 

//            studentDelete.DeleteRecord(studentId);
//            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "alert('Successfully Deleted  Data');window.location='default.aspx';", true);

//        }


//        protected void ButtonClear_Click(object sender, EventArgs e)
//        {

//            StudentDataOperations clear = new StudentDataOperations();
//            clear.ClearAll(TextBox1, TextBox2, TextBox3, TextBox4);
//    }
//    }
//}

