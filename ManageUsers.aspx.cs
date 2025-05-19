using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTechFixUsers();
                LoadSuppliers();
            }
        }

        private void LoadTechFixUsers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT UserID, Username, FullName, Email, Phone FROM TechFixUser";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTechFixUsers.DataSource = dt;
                gvTechFixUsers.DataBind();
            }
        }

        private void LoadSuppliers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT SupplierID, CompanyName, ContactName, ContactEmail, ContactPhone FROM Supplier";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvSuppliers.DataSource = dt;
                gvSuppliers.DataBind();
            }
        }

        protected void gvTechFixUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTechFixUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                // Redirect to the edit user page (implement accordingly)
                Response.Redirect("EditTechFixUser.aspx?userId=" + userId);
            }
            else if (e.CommandName == "DeleteTechFixUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                DeleteTechFixUser(userId);
            }
        }

        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditSupplier")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);
                // Redirect to the edit supplier page (implement accordingly)
                Response.Redirect("EditSupplier.aspx?supplierId=" + supplierId);
            }
            else if (e.CommandName == "DeleteSupplier")
            {
                int supplierId = Convert.ToInt32(e.CommandArgument);
                DeleteSupplier(supplierId);
            }
        }

        private void DeleteTechFixUser(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "DELETE FROM TechFixUser WHERE UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                lblMessage.Text = rowsAffected > 0 ? "TechFix User deleted successfully." : "Error deleting TechFix User.";
                lblMessage.ForeColor = rowsAffected > 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                lblMessage.Visible = true;

                LoadTechFixUsers(); // Refresh the TechFix users list
            }
        }

        private void DeleteSupplier(int supplierId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "DELETE FROM Supplier WHERE SupplierID = @SupplierID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                lblMessage.Text = rowsAffected > 0 ? "Supplier deleted successfully." : "Error deleting Supplier.";
                lblMessage.ForeColor = rowsAffected > 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                lblMessage.Visible = true;

                LoadSuppliers(); // Refresh the suppliers list
            }
        }
    }
}
