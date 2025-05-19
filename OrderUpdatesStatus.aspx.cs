using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Trial
{
    public partial class OrderUpdatesStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderUpdateDetails();
            }
        }

        // Method to load order update details along with order and product details
        private void LoadOrderUpdateDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL query to fetch order updates with order and product details
            string query = @"
                SELECT ou.order_update_id, 
                       ou.OrderID, 
                       ou.order_updates, 
                       ou.SupplierID, 
                       ou.update_timestamp, 
                       o.Quantity, 
                       o.TotalAmount, 
                       o.OrderDate, 
                       o.order_status, 
                       p.product_name, 
                       p.product_description, 
                       p.product_category, 
                       p.product_brand, 
                       p.product_weight
                FROM order_update ou
                INNER JOIN Orders o ON ou.OrderID = o.OrderID
                INNER JOIN Products p ON o.product_id = p.product_id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    try
                    {
                        con.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            gvOrderUpdates.DataSource = dt;
                            gvOrderUpdates.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "No order updates found.";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error loading order updates: " + ex.Message;
                        lblMessage.Visible = true;
                    }
                }
            }
        }
    }
}
