using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace Trial
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Retrieve form data
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Get connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Check user credentials and get user type and ID
            var result = CheckUserCredentials(username, password, connectionString);

            if (result != null)
            {
                string userType = result.Item1;
                int userID = result.Item2;

                // Set session variables
                Session["UserType"] = userType;
                Session["Username"] = username;
                Session["UserID"] = userID; // Store user ID in session

                // Redirect based on user type
                switch (userType.ToLower())
                {
                    case "supplier":
                        Response.Redirect("SupplierDashboard.aspx");
                        break;

                    case "techfix_user":
                        Response.Redirect("TechFixUserDashboard.aspx");
                        break;

                    case "admin":
                        Response.Redirect("AdminDashboard.aspx");
                        break;

                    default:
                        Response.Write("<script>alert('User type not recognized.');</script>");
                        break;
                }
            }
            else
            {
                // Invalid login
                Response.Write("<script>alert('Invalid username or password.');</script>");
            }
        }

        // Function to check user credentials and return user type and ID
        private Tuple<string, int> CheckUserCredentials(string username, string password, string connectionString)
        {
            string querySupplier = "SELECT SupplierID FROM Supplier WHERE supplier_usesrname = @Username AND supplier_password = @Password";
            string queryTechFixUser = "SELECT UserID FROM TechFixUser WHERE username = @Username AND password = @Password";
            string queryAdmin = "SELECT AdminID FROM Admin WHERE Username = @Username AND Password = @Password";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check Supplier table
                using (SqlCommand cmd = new SqlCommand(querySupplier, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int supplierID = Convert.ToInt32(result);
                        return Tuple.Create("supplier", supplierID);
                    }
                }

                // Check TechFixUser table
                using (SqlCommand cmd = new SqlCommand(queryTechFixUser, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int userID = Convert.ToInt32(result);
                        return Tuple.Create("techfix_user", userID);
                    }
                }

                // Check Admin table
                using (SqlCommand cmd = new SqlCommand(queryAdmin, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int adminID = Convert.ToInt32(result);
                        return Tuple.Create("admin", adminID);
                    }
                }
            }

            // No matching user found
            return null;
        }
    }
}
