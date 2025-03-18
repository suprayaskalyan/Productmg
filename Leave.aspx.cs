using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeaveManagement
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadEmployees();
            }
        }
        private void LoadEmployees()
        {
            string connString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM employee";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    
                    ddlEmployees.DataSource = dt;
                    ddlEmployees.DataTextField = "emp_name";
                    ddlEmployees.DataValueField = "emp_id";
                    ddlEmployees.DataBind();
                }
            }
        }
        [WebMethod]
        public static List<LeaveType>GetLeaveTypes(string id)
        {
            List<LeaveType> leaveList = new List<LeaveType>();
            string connString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT lm.leave_id, lm.leave_name FROM leave_master lm INNER JOIN leave_emp_assign lea ON lm.leave_id=lea.leave_id WHERE lea.emp_id =@empid";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@empid", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        leaveList.Add(new LeaveType
                        {
                            leave_id = reader["leave_id"].ToString(),
                            leave_name = reader["leave_name"].ToString()
                        });
                    }
                }
            }
            return leaveList;
        }
        public class LeaveType
        {
            public string leave_id { get; set; }
            public string leave_name { get; set; }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}