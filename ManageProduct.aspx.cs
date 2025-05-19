using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class ManageProduct : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductData();
            }
        }

        private void LoadCategoriesAndBrands()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL Queries for categories and brands
            string queryCategories = "SELECT DISTINCT product_category FROM Products";
            string queryBrands = "SELECT DISTINCT product_brand FROM Products";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Load Categories
                using (SqlCommand cmdCategories = new SqlCommand(queryCategories, con))
                {
                    SqlDataReader reader = cmdCategories.ExecuteReader();
                    ddlProductCategory.Items.Clear();
                    ddlProductCategory.Items.Add(new ListItem("Enter new category", "new")); // Default option

                    while (reader.Read())
                    {
                        ddlProductCategory.Items.Add(new ListItem(reader["product_category"].ToString()));
                    }
                    reader.Close();
                }

                // Load Brands
                using (SqlCommand cmdBrands = new SqlCommand(queryBrands, con))
                {
                    SqlDataReader reader = cmdBrands.ExecuteReader();
                    ddlProductBrand.Items.Clear();
                    ddlProductBrand.Items.Add(new ListItem("Enter new brand", "new")); // Default option

                    while (reader.Read())
                    {
                        ddlProductBrand.Items.Add(new ListItem(reader["product_brand"].ToString()));
                    }
                    reader.Close();
                }

                con.Close();
            }
        }
        private void LoadProductData()
        {
            // Get SupplierID from the session
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("default.aspx");
                return;
            }

            int supplierId = Convert.ToInt32(Session["UserID"]);

            // Connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL Query to fetch product, pricing, inventory details, including RAM and ROM
            string query = @"
        SELECT p.product_id, p.product_name, p.product_description, p.product_category, p.product_brand, p.product_weight, p.product_image, p.product_warranty,
               pr.unit_price, pr.discount_price, 
               inv.quantity, inv.restock_date, p.RAM, p.ROM
        FROM Products p
        JOIN Pricing pr ON p.pricing_id = pr.pricing_id
        JOIN Inventory inv ON p.inventory_id = inv.inventory_id
        WHERE p.SupplierID = @SupplierID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Pass the SupplierID to the query
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to Repeater
                    RepeaterProducts.DataSource = dt;
                    RepeaterProducts.DataBind();
                }
            }
        }


        protected void RepeaterProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle Edit Button Click
                int productId = Convert.ToInt32(e.CommandArgument);
                // Logic to populate the EditPanel with product details
                LoadProductDetails(productId);
                EditPanel.Visible = true;
            }
            else if (e.CommandName == "Delete")
            {
                // Handle Delete Button Click
                int productId = Convert.ToInt32(e.CommandArgument);
                DeleteProduct(productId);
                LoadProductData(); // Refresh the product list
            }
        }

        private void LoadProductDetails(int productId)
        {
            LoadCategoriesAndBrands(); // Load existing categories and brands

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            string query = @"
    SELECT p.product_name, p.product_category, p.product_brand, p.product_description, p.product_weight, p.product_warranty,
           pr.unit_price, pr.discount_price, 
           inv.quantity, inv.restock_date,
           p.RAM, p.ROM -- Add RAM and ROM columns
    FROM Products p
    JOIN Pricing pr ON p.pricing_id = pr.pricing_id
    JOIN Inventory inv ON p.inventory_id = inv.inventory_id
    WHERE p.product_id = @ProductID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate form fields with product details
                        hfProductID.Value = productId.ToString();
                        txtProductName.Text = reader["product_name"].ToString();
                        ddlProductCategory.SelectedValue = reader["product_category"].ToString();
                        ddlProductBrand.SelectedValue = reader["product_brand"].ToString();
                        txtProductDescription.Text = reader["product_description"].ToString();
                        txtProductWeight.Text = reader["product_weight"].ToString();
                        txtProductWarranty.Text = reader["product_warranty"].ToString();
                        txtUnitPrice.Text = reader["unit_price"].ToString();
                        txtDiscountPrice.Text = reader["discount_price"].ToString();
                     
                        txtQuantity.Text = reader["quantity"].ToString();
                        txtRestockDate.Text = Convert.ToDateTime(reader["restock_date"]).ToString("yyyy-MM-dd");

                        // Conditionally display RAM and ROM based on the product category
                        string category = reader["product_category"].ToString();
                        if (category == "Mobile" || category == "PC" || category == "Laptop")
                        {
                            // Display RAM and ROM fields
                            divRAM.Visible = true;
                            divROM.Visible = true;

                            // Populate RAM and ROM values
                            txtRAM.Text = reader["RAM"] != DBNull.Value ? reader["RAM"].ToString() : string.Empty;
                            txtROM.Text = reader["ROM"] != DBNull.Value ? reader["ROM"].ToString() : string.Empty;
                        }
                        else
                        {
                            // Hide RAM and ROM fields if not applicable
                            divRAM.Visible = false;
                            divROM.Visible = false;
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void DeleteProduct(int productId)
        {
            // Get connection string
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL Query to delete a product
            string query = @"
                DELETE FROM Products
                WHERE product_id = @ProductID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get product ID from hidden field
            int productId = Convert.ToInt32(hfProductID.Value);

            string category = string.IsNullOrEmpty(txtNewCategory.Text) ? ddlProductCategory.SelectedValue : txtNewCategory.Text;
            string brand = string.IsNullOrEmpty(txtNewBrand.Text) ? ddlProductBrand.SelectedValue : txtNewBrand.Text;

            // Connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // Base query to update Products, Pricing, and Inventory
            string query = @"
    UPDATE Products
    SET product_name = @ProductName,
        product_category = @ProductCategory,
        product_brand = @ProductBrand,
        product_description = @ProductDescription,
        product_weight = @ProductWeight,
        product_warranty = @ProductWarranty
        {RAMROMUpdate}  -- Placeholder for RAM and ROM update
    WHERE product_id = @ProductID;
    
    UPDATE Pricing
    SET unit_price = @UnitPrice,
        discount_price = @DiscountPrice,
      
    WHERE pricing_id = (SELECT pricing_id FROM Products WHERE product_id = @ProductID);

    UPDATE Inventory
    SET quantity = @Quantity,
        restock_date = @RestockDate
    WHERE inventory_id = (SELECT inventory_id FROM Products WHERE product_id = @ProductID);";

            string ramRomUpdate = ""; // Initialize RAM and ROM update section

            // Check if the product category requires RAM and ROM fields
            if (category == "Mobile" || category == "PC" || category == "Laptop")
            {
                // Add RAM and ROM to the query for these categories
                ramRomUpdate = ", RAM = @RAM, ROM = @ROM";
            }

            // Replace placeholder in the query with RAM/ROM update
            query = query.Replace("{RAMROMUpdate}", ramRomUpdate);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Common parameters for Products, Pricing, and Inventory
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@ProductCategory", category);
                    cmd.Parameters.AddWithValue("@ProductBrand", brand);
                    cmd.Parameters.AddWithValue("@ProductDescription", txtProductDescription.Text);
                    cmd.Parameters.AddWithValue("@ProductWeight", txtProductWeight.Text);
                    cmd.Parameters.AddWithValue("@ProductWarranty", txtProductWarranty.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                    cmd.Parameters.AddWithValue("@DiscountPrice", txtDiscountPrice.Text);
             
                    cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                    cmd.Parameters.AddWithValue("@RestockDate", txtRestockDate.Text);

                    // Only add RAM and ROM parameters if needed
                    if (!string.IsNullOrEmpty(ramRomUpdate))
                    {
                        cmd.Parameters.AddWithValue("@RAM", txtRAM.Text);
                        cmd.Parameters.AddWithValue("@ROM", txtROM.Text);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Hide the edit panel and reload the product data
            EditPanel.Visible = false;
            LoadProductData(); // Optionally reload data if needed
        }



        protected void btnLowStock_Click(object sender, EventArgs e)
        {
            // Call the LoadLowStockProducts method
            LoadLowStockProducts();
        }

        private void LoadLowStockProducts()
        {
            // Get SupplierID from the session
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("default.aspx");
                return;
            }

            int supplierId = Convert.ToInt32(Session["UserID"]);

            string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // SQL Query to fetch product details with low stock for the given supplier ID
                string sqlLowStock = @"
        SELECT p.product_id, p.product_name, p.product_description, p.product_category, p.product_brand, p.product_weight, p.product_image, 
               p.product_warranty, p.RAM, p.ROM, 
               pr.unit_price, pr.discount_price, 
               inv.quantity, inv.restock_date
        FROM Products p
        JOIN Pricing pr ON p.pricing_id = pr.pricing_id
        JOIN Inventory inv ON p.inventory_id = inv.inventory_id
        WHERE p.SupplierID = @SupplierID
        AND inv.quantity < 25"; // Added condition to filter low stock

                SqlCommand cmdLowStock = new SqlCommand(sqlLowStock, conn);
                cmdLowStock.Parameters.AddWithValue("@SupplierID", supplierId);

                SqlDataAdapter daLowStock = new SqlDataAdapter(cmdLowStock);
                DataTable dtLowStock = new DataTable();
                daLowStock.Fill(dtLowStock);

                // Bind the data to Repeater
                RepeaterProducts.DataSource = dtLowStock;
                RepeaterProducts.DataBind();
            }
        }


        private void LoadProductData(string searchQuery = "")
        {
            // Get SupplierID from the session
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("default.aspx");
                return;
            }

            int supplierId = Convert.ToInt32(Session["UserID"]);

            // Connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // SQL Query with search parameters and conditional RAM and ROM inclusion
            string query = @"
        SELECT p.product_id, p.product_name, p.product_description, p.product_category, p.product_brand, p.product_weight, p.product_image, p.product_warranty,
               pr.unit_price, pr.discount_price, pr.bulk_price, 
               inv.quantity, inv.restock_date,
               CASE WHEN p.product_category IN ('Mobile', 'PC', 'Laptop') THEN p.RAM ELSE NULL END AS RAM,
               CASE WHEN p.product_category IN ('Mobile', 'PC', 'Laptop') THEN p.ROM ELSE NULL END AS ROM
        FROM Products p
        JOIN Pricing pr ON p.pricing_id = pr.pricing_id
        JOIN Inventory inv ON p.inventory_id = inv.inventory_id
        WHERE p.SupplierID = @SupplierID
        AND (p.product_name LIKE '%' + @SearchQuery + '%' 
        OR p.product_category LIKE '%' + @SearchQuery + '%' 
        OR p.product_brand LIKE '%' + @SearchQuery + '%' 
        OR p.product_description LIKE '%' + @SearchQuery + '%')";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Pass the SupplierID and SearchQuery to the query
                    cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmd.Parameters.AddWithValue("@SearchQuery", string.IsNullOrEmpty(searchQuery) ? "" : searchQuery);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to Repeater
                    RepeaterProducts.DataSource = dt;
                    RepeaterProducts.DataBind();
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            LoadProductData(searchQuery);
        }




        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Handle Cancel Button Click in the EditPanel
            EditPanel.Visible = false;
        }
    }
}
