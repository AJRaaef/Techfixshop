using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class UpdateOrdersStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] != null && Session["UserType"].ToString().ToLower() == "supplier")
                {
                    int supplierID = Convert.ToInt32(Session["UserID"]); // Get SupplierID from session
                    InsertInitialProcessingStatusForConfirmedOrders(supplierID); // Insert processing status for confirmed orders
                    LoadConfirmedOrders(supplierID); // Load confirmed orders for the supplier
                }
                else
                {
                    lblMessage.Text = "Please log in as a supplier to view and update your orders.";
                    lblMessage.Visible = true;
                    Response.Redirect("default.aspx");
                }
            }
        }

        // Method to insert initial processing status for confirmed orders
        private void InsertInitialProcessingStatusForConfirmedOrders(int supplierID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to find confirmed orders for the supplier
            string query = @"
                SELECT OrderID 
                FROM Orders 
                WHERE SupplierID = @SupplierID AND order_status = 'confirmed'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int orderID = reader.GetInt32(0);
                        // Insert initial processing status for each confirmed order
                        InsertInitialProcessingStatus(orderID, supplierID);
                    }
                }
            }
        }

        // Method to load confirmed orders for the logged-in supplier
        // Method to load confirmed orders for the logged-in supplier
        private void LoadConfirmedOrders(int supplierID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Query to load confirmed orders with order updates
            string query = @"
        SELECT o.OrderID, p.product_name, o.Quantity, o.TotalAmount, o.order_status, u.order_updates
        FROM Orders o
        INNER JOIN Products p ON o.product_id = p.product_id
        LEFT JOIN order_update u ON o.OrderID = u.OrderID
        WHERE o.SupplierID = @SupplierID AND o.order_status = 'confirmed'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    try
                    {
                        con.Open();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            gvConfirmedOrders.DataSource = dt;
                            gvConfirmedOrders.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "No confirmed orders found for this supplier.";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error loading orders: " + ex.Message;
                        lblMessage.Visible = true;
                    }
                }
            }
        }


        // Method to insert the initial "Processing" status for each order
        private void InsertInitialProcessingStatus(int orderID, int supplierID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Check if there is already an entry for this order in order_update
            string checkQuery = "SELECT COUNT(*) FROM order_update WHERE OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    try
                    {
                        con.Open();
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0) // If no existing entry, insert the initial status
                        {
                            string insertProcessingQuery = @"
                                INSERT INTO order_update (OrderID, order_updates, SupplierID, update_timestamp)
                                VALUES (@OrderID, 'Processing', @SupplierID, GETDATE())";

                            using (SqlCommand insertCmd = new SqlCommand(insertProcessingQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@OrderID", orderID);
                                insertCmd.Parameters.AddWithValue("@SupplierID", supplierID);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error inserting initial processing status: " + ex.Message;
                        lblMessage.Visible = true;
                    }
                }
            }
        }

        // Method to handle the order update
        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            DropDownList ddlOrderUpdates = (DropDownList)row.FindControl("ddlOrderUpdates");
            string selectedUpdate = ddlOrderUpdates.SelectedValue;
            int orderID = Convert.ToInt32(btn.CommandArgument);
            int supplierID = Convert.ToInt32(Session["UserID"]); // Get supplier ID from session

            // Update the order status based on the selected value
            UpdateOrderStatus(orderID, selectedUpdate, supplierID);
        }

        // Method to update the order status in the Orders and order_update tables
        private void UpdateOrderStatus(int orderID, string orderUpdate, int supplierID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Update the order status
            string updateOrderUpdateQuery = @"
                UPDATE order_update 
                SET order_updates = @OrderUpdates, update_timestamp = GETDATE() 
                WHERE OrderID = @OrderID AND SupplierID = @SupplierID";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand updateCmd = new SqlCommand(updateOrderUpdateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@OrderID", orderID);
                        updateCmd.Parameters.AddWithValue("@OrderUpdates", orderUpdate);
                        updateCmd.Parameters.AddWithValue("@SupplierID", supplierID);

                        // Execute the update query
                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Order updated successfully!";
                        }
                        else
                        {
                            lblMessage.Text = "No existing order update found for this order.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating order: " + ex.Message;
            }
            finally
            {
                lblMessage.Visible = true;
            }

            // Reload orders after update
            LoadConfirmedOrders(supplierID);
        }
    }
}
