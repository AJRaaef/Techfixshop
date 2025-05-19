using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserType"] != null && Session["UserType"].ToString().ToLower() == "supplier")
                {
                    // Get the SupplierID from session
                    int supplierID = Convert.ToInt32(Session["UserID"]);

                    // Fetch and bind order history for the supplier
                    LoadOrderHistory(supplierID);
                }
                else
                {
                    // If not logged in as supplier, show message and redirect to login
                    lblMessage.Text = "Please log in to view your order history.";
                    lblMessage.Visible = true;
                    Response.Redirect("default.aspx");
                }
            }
        }

        private void LoadOrderHistory(int supplierID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            string query = @"
                SELECT o.OrderID, o.product_id, o.Quantity, o.TotalAmount, o.OrderDate, o.order_status,
                       p.product_name, p.product_image 
                FROM Orders o
                INNER JOIN Products p ON o.product_id = p.product_id
                WHERE o.SupplierID = @SupplierID";

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

                        // Check if data exists
                        if (dt.Rows.Count > 0)
                        {
                            gvOrderHistory.DataSource = dt;
                            gvOrderHistory.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "No orders found for this supplier.";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred while loading order history: " + ex.Message;
                        lblMessage.Visible = true;
                    }
                }
            }
        }

        // Event handler for dropdown list selection change
        protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlOrderStatus = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlOrderStatus.NamingContainer;

            // Get OrderID from the hidden field
            HiddenField hfOrderID = (HiddenField)row.FindControl("hfOrderID");
            int orderID = Convert.ToInt32(hfOrderID.Value);

            // Get the selected status
            string selectedStatus = ddlOrderStatus.SelectedValue;

            // Update the order status in the database
            UpdateOrderStatus(orderID, selectedStatus);
        }

        private void UpdateOrderStatus(int orderID, string newStatus)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL query to update order status
            string updateOrderQuery = "UPDATE Orders SET order_status = @OrderStatus WHERE OrderID = @OrderID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateOrderQuery, con))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@OrderStatus", newStatus);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // If the status is updated successfully and the new status is 'Confirmed'
                        if (rowsAffected > 0 && newStatus == "Confirmed")
                        {
                            // Fetch the product details and inventory_id from the order
                            string getProductDetailsQuery = @"
                        SELECT o.product_id, o.Quantity, p.inventory_id 
                        FROM Orders o
                        INNER JOIN Products p ON o.product_id = p.product_id
                        WHERE o.OrderID = @OrderID";

                            using (SqlCommand productCmd = new SqlCommand(getProductDetailsQuery, con))
                            {
                                productCmd.Parameters.AddWithValue("@OrderID", orderID);

                                SqlDataReader reader = productCmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    int productID = Convert.ToInt32(reader["product_id"]);
                                    int orderedQuantity = Convert.ToInt32(reader["Quantity"]);
                                    int inventoryID = Convert.ToInt32(reader["inventory_id"]);
                                    reader.Close();

                                    // Update the available quantity in the Inventory table
                                    string updateInventoryQuery = @"
                                UPDATE Inventory 
                                SET quantity = quantity - @OrderedQuantity, last_updated = GETDATE()
                                WHERE inventory_id = @InventoryID AND quantity >= @OrderedQuantity";

                                    using (SqlCommand updateInventoryCmd = new SqlCommand(updateInventoryQuery, con))
                                    {
                                        updateInventoryCmd.Parameters.AddWithValue("@OrderedQuantity", orderedQuantity);
                                        updateInventoryCmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                                        int inventoryUpdateRows = updateInventoryCmd.ExecuteNonQuery();

                                        if (inventoryUpdateRows > 0)
                                        {
                                            lblMessage.Text = "Order confirmed and inventory quantity updated.";
                                        }
                                        else
                                        {
                                            lblMessage.Text = "Order confirmed, but inventory quantity could not be updated (insufficient stock).";
                                        }
                                        lblMessage.Visible = true;
                                    }
                                }
                            }
                        }
                        else if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Order status updated successfully.";
                            lblMessage.Visible = true;
                        }
                        else
                        {
                            lblMessage.Text = "Failed to update order status.";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred: " + ex.Message;
                        lblMessage.Visible = true;
                    }
                }
            }

            // Refresh the order history to reflect changes
            int supplierID = Convert.ToInt32(Session["UserID"]);
            LoadOrderHistory(supplierID);
        }

    }
}
