using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Trial
{
    public partial class OrderUpdates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderDetails();
            }
        }

        private void LoadOrderDetails()
        {
            // Replace the connection string as per your configuration
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT o.OrderID, o.product_id, o.Quantity, o.TotalAmount, o.OrderDate, o.order_status, 
                       p.product_name, p.product_image, o.SupplierID  -- Added SupplierID
                FROM Orders o
                INNER JOIN Products p ON o.product_id = p.product_id
                ORDER BY o.OrderDate DESC"; // Order by OrderDate, or any other criteria

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dtOrders = new DataTable();
                da.Fill(dtOrders);

                // Bind the data to the GridView
                gvOrders.DataSource = dtOrders;
                gvOrders.DataBind();
            }
        }
    }
}
