using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class SignupSupplier : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve form data
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string companyName = txtCompanyName.Text.Trim();
            string contactName = txtContactName.Text.Trim();
            string contactEmail = txtContactEmail.Text.Trim();
            string contactPhone = txtContactPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string postalCode = txtPostalCode.Text.Trim();
            string country = txtCountry.Text.Trim();

            // Get connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL Insert query
            string query = "INSERT INTO Supplier (supplier_usesrname, supplier_password, CompanyName, ContactName, ContactEmail, ContactPhone, Address, City, State, PostalCode, Country, UserType) " +
                           "VALUES (@Username, @Password, @CompanyName, @ContactName, @ContactEmail, @ContactPhone, @Address, @City, @State, @PostalCode, @Country, 'supplier')";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // Open connection
                    con.Open();

                    // Execute the query
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to prevent SQL Injection
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password
                        cmd.Parameters.AddWithValue("@CompanyName", companyName);
                        cmd.Parameters.AddWithValue("@ContactName", contactName);
                        cmd.Parameters.AddWithValue("@ContactEmail", contactEmail);
                        cmd.Parameters.AddWithValue("@ContactPhone", contactPhone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@City", city);
                        cmd.Parameters.AddWithValue("@State", state);
                        cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                        cmd.Parameters.AddWithValue("@Country", country);

                        // Execute query
                        cmd.ExecuteNonQuery();
                    }

                    // Success message (optional)
                    Response.Write("<script>alert('Supplier registered successfully!');</script>");
                }
                catch (Exception ex)
                {
                    // Handle error
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }
    }
}