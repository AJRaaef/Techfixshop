using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class Order : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get productId from query string
                string productId = Request.QueryString["productId"];
                if (!string.IsNullOrEmpty(productId))
                {
                    LoadProductDetails(productId);
                }
            }
        }

        private void LoadProductDetails(string productId)
        {
            // Connect to your database
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Retrieve product details including pricing
                string query = @"SELECT p.product_name, p.product_description, p.product_image, 
                         p.product_category, p.product_brand, p.product_weight, 
                         p.product_warranty, p.RAM, p.ROM, 
                         pr.unit_price, pr.discount_price, 
                         i.quantity AS available_quantity
                  FROM Products p 
                  JOIN Pricing pr ON p.pricing_id = pr.pricing_id 
                  JOIN Inventory i ON p.product_id = i.inventory_id
                  WHERE p.product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Display product details
                        lblProductName.Text = "Product Name: " + reader["product_name"].ToString();
                        lblProductDescription.Text = "Description: " + reader["product_description"].ToString();
                        lblProductCategory.Text = "Category: " + reader["product_category"].ToString();
                        lblProductBrand.Text = "Brand: " + reader["product_brand"].ToString();
                        lblProductWeight.Text = "Weight: " + reader["product_weight"].ToString() + " kg";
                        lblProductWarranty.Text = "Warranty: " + reader["product_warranty"].ToString();

                        // Get RAM and ROM values
                        string ram = reader["RAM"].ToString();
                        string rom = reader["ROM"].ToString();

                        // Set visibility based on RAM and ROM values
                        lblRAM.Visible = !string.IsNullOrEmpty(ram);
                        lblROM.Visible = !string.IsNullOrEmpty(rom);

                        // Display RAM and ROM details if available
                        lblRAM.Text = lblRAM.Visible ? "RAM: " + ram : "";
                        lblROM.Text = lblROM.Visible ? "ROM: " + rom : "";

                        // Get pricing details
                        decimal unitPrice = Convert.ToDecimal(reader["unit_price"]);
                        decimal discountPrice = Convert.ToDecimal(reader["discount_price"]);
                        int availableQuantity = Convert.ToInt32(reader["available_quantity"]);

                        // Display pricing details
                        lblProductPrice.Text = "Price: $" + unitPrice.ToString("F2");
                        lblDiscountPrice.Text = discountPrice > 0 ? "Discount Price: $" + discountPrice.ToString("F2") : "No Discount Available";

                        // Set product image
                        imgProduct.ImageUrl = reader["product_image"].ToString(); // Make sure this URL is accessible

                        // Set available quantity
                        lblAvailableQuantity.Text = "Available Quantity: " + availableQuantity.ToString();

                        // Calculate total amount
                        CalculateTotal(unitPrice, discountPrice);
                    }
                    else
                    {
                        // No product found
                        lblProductName.Text = "Product not applicable.";
                        lblProductDescription.Text = "";
                        lblProductCategory.Text = "";
                        lblProductBrand.Text = "";
                        lblProductWeight.Text = "";
                        lblProductWarranty.Text = "";
                        lblRAM.Text = "";
                        lblROM.Text = "";
                        lblProductPrice.Text = "";
                        lblDiscountPrice.Text = ""; // Clear discount price
                        imgProduct.ImageUrl = ""; // Hide the image
                        txtQuantity.Visible = false; // Hide quantity input
                        btnConfirmOrder.Visible = false; // Hide confirm order button
                    }
                }
            }
        }

        private void CalculateTotal(decimal unitPrice, decimal discountPrice)
        {
            // Determine the price to use based on discount availability
            decimal priceToUse = discountPrice > 0 ? discountPrice : unitPrice;

            // Calculate the total amount based on quantity input
            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                decimal totalAmount = priceToUse * quantity;
                lblTotalAmount.Text = "Total Amount: $" + totalAmount.ToString("F2");
            }
            else
            {
                lblTotalAmount.Text = "Total Amount: $0.00"; // Default if no quantity entered
            }
        }






        private decimal GetProductPrice(int pricingId)
        {
            // Connect to your database to retrieve product price and discount price using pricing_id
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT unit_price, discount_price FROM Pricing WHERE pricing_id = @PricingID"; // Fetch both unit_price and discount_price
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PricingID", pricingId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal unitPrice = reader.GetDecimal(0);    // Fetch unit_price from the first column
                            decimal discountPrice = reader.GetDecimal(1); // Fetch discount_price from the second column

                            // If discount price is greater than 0, return discount price, else return unit price
                            return discountPrice > 0 ? discountPrice : unitPrice;
                        }
                    }
                }
            }
            return 0; // Return 0 in case of no matching pricing_id (although this scenario should be handled with validation)
        }



        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            // Get the productId from the query string
            string productId = Request.QueryString["productId"];

            // Get the quantity input by the user
            if (int.TryParse(txtQuantity.Text, out int selectedQuantity) && selectedQuantity > 0)
            {
                // Step 1: Get the inventory_id for the product
                int inventoryId = GetInventoryId(productId);

                if (inventoryId > 0)
                {
                    // Step 2: Get the available quantity from the Inventory table
                    int availableQuantity = GetAvailableQuantity(inventoryId);

                    // Step 3: Check if selected quantity is greater than available quantity
                    if (selectedQuantity > availableQuantity)
                    {
                        // Show an error message using JavaScript
                        string errorScript = $"alert('The selected quantity exceeds the available stock. Only {availableQuantity} items are available.');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showError", errorScript, true);

                        // Stop further execution
                        return;
                    }

                    // Step 4: If available quantity is sufficient, proceed with the order
                    decimal price = GetProductPriceById(productId); // Method to get product price by product_id
                    decimal totalAmount = price * selectedQuantity;
                    int supplierId = GetSupplierId(productId); // Method to get supplier ID

                    // Insert the order into the Orders table
                    InsertOrder(productId, selectedQuantity, totalAmount, supplierId);

                    // Show success message using JavaScript and redirect after 3 seconds
                    string successScript = @"
                alert('Your order has been sent to the supplier. Please wait for confirmation.');
                setTimeout(function(){ window.location.href='OrderUpdates.aspx'; }, 3000);";
                    ScriptManager.RegisterStartupScript(this, GetType(), "confirmOrder", successScript, true);
                }
                else
                {
                    // Handle case where inventory_id is not found using JavaScript alert
                    string errorScript = "alert('Could not find inventory for the selected product.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showError", errorScript, true);
                }
            }
            else
            {
                // Handle invalid quantity input using JavaScript alert
                string errorScript = "alert('Please enter a valid quantity.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showError", errorScript, true);
            }
        }


        // Method to get the inventory_id from the Products table
        private int GetInventoryId(string productId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT inventory_id FROM Products WHERE product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    object result = cmd.ExecuteScalar();

                    // If the result is not null, return the inventory_id
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0; // Return 0 if no inventory_id is found
        }

        // Method to get the available quantity from the Inventory table
        private int GetAvailableQuantity(int inventoryId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT quantity FROM Inventory WHERE inventory_id = @InventoryID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                    object result = cmd.ExecuteScalar();

                    // If the result is not null, return the available quantity
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0; // Return 0 if no quantity is found
        }



        private decimal GetProductPriceById(string productId)
        {
            // Connect to your database to retrieve product price using product_id
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT pricing_id FROM Products WHERE product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    int pricingId = (int)cmd.ExecuteScalar();
                    return GetProductPrice(pricingId);
                }
            }
        }

        private int GetSupplierId(string productId)
        {
            // Connect to your database to retrieve supplier ID
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT SupplierID FROM Products WHERE product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private void InsertOrder(string productId, int quantity, decimal totalAmount, int supplierId)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Orders (product_id, Quantity, TotalAmount, SupplierID, OrderDate, order_status) " +
                               "VALUES (@ProductID, @Quantity, @TotalAmount, @SupplierID, GETDATE(), 'pending')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
