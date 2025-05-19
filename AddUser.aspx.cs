using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Trial
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide both forms on initial load
                TechFixUserForm.Visible = false;
                SupplierForm.Visible = false;
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show the appropriate form based on selected user type
            if (ddlUserType.SelectedValue == "TechFixUser")
            {
                TechFixUserForm.Visible = true;
                SupplierForm.Visible = false;
            }
            else if (ddlUserType.SelectedValue == "Supplier")
            {
                TechFixUserForm.Visible = false;
                SupplierForm.Visible = true;
            }
            else
            {
                TechFixUserForm.Visible = false;
                SupplierForm.Visible = false;
            }
        }

        // Add TechFixUser
        protected void btnAddTechFixUser_Click(object sender, EventArgs e)
        {
            // Retrieve TechFix user details from the text boxes
            string username = txtTechFixUsername.Text;
            string password = txtTechFixPassword.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string userType = "techfix_user";

            // Define the SQL query to insert a new TechFix user
            string query = "INSERT INTO TechFixUser (Username, Password, FullName, Email, Phone, TechFixUserType) " +
                           "VALUES (@Username, @Password, @FullName, @Email, @Phone, @TechFixUserType)";

            // Retrieve the connection string from the Web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Use the connection string to connect to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                // Add parameters for the SQL query
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password before storing
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@TechFixUserType", userType);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "TechFix User added successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Error adding user.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    // Log the error (not shown here for brevity)
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    lblMessage.Visible = true;
                }
            }
        }

        // Add Supplier
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            // Retrieve the supplier details from the text boxes
            string companyName = txtCompanyName.Text;
            string contactName = txtContactName.Text;
            string contactEmail = txtContactEmail.Text;
            string contactPhone = txtContactPhone.Text;
            string address = txtAddress.Text;
            string city = txtCity.Text;
            string state = txtState.Text;
            string postalCode = txtPostalCode.Text;
            string country = txtCountry.Text;
            string supplierUsername = txtSupplierUsername.Text;
            string supplierPassword = txtSupplierPassword.Text;

            // Define the SQL query to insert a new supplier
            string query = "INSERT INTO Supplier (CompanyName, ContactName, ContactEmail, ContactPhone, Address, City, State, PostalCode, Country, supplier_usesrname, supplier_password) " +
                           "VALUES (@CompanyName, @ContactName, @ContactEmail, @ContactPhone, @Address, @City, @State, @PostalCode, @Country, @Username, @Password)";

            // Retrieve the connection string from the Web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Use the connection string to connect to the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                // Add parameters for the SQL query
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@ContactName", contactName);
                cmd.Parameters.AddWithValue("@ContactEmail", contactEmail);
                cmd.Parameters.AddWithValue("@ContactPhone", contactPhone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@State", state);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                cmd.Parameters.AddWithValue("@Country", country);
                cmd.Parameters.AddWithValue("@Username", supplierUsername);
                cmd.Parameters.AddWithValue("@Password", supplierPassword);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Supplier added successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Error adding supplier.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    // Log the error (not shown here for brevity)
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
