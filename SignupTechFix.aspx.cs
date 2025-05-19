using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Trial
{
    public partial class SignupTechFix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            // Connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Get user details from the form
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim(); // Consider hashing the password
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string userType = hfUserType.Value; // This will always be 'techfix_user'

            // Insert user data into the TechFixUser table
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TechFixUser (Username, Password, FullName, Email, Phone, TechFixUserType) " +
                               "VALUES (@Username, @Password, @FullName, @Email, @Phone, @TechFixUserType)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);  // Hash the password for security!
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@TechFixUserType", userType);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        // Successful signup, redirect to login page
                        Response.Write("<script>alert('Signup successful. Please log in.');</script>");
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        // Error message
                        Response.Write("<script>alert('Signup failed. Please try again.');</script>");
                    }
                }
            }
        }
    }
}
