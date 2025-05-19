using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Trial
{
    public partial class UploadProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdowns on the first load
                PopulateDropdowns();
            }
        }

        private void PopulateDropdowns()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Populate Product Category dropdown
                string categoryQuery = "SELECT DISTINCT product_category FROM Products";
                using (SqlCommand cmd = new SqlCommand(categoryQuery, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlProductCategory.Items.Clear();
                    ddlProductCategory.Items.Add(new ListItem("Select a category", "")); // Default option

                    while (reader.Read())
                    {
                        ddlProductCategory.Items.Add(new ListItem(reader["product_category"].ToString()));
                    }
                    reader.Close();
                }

                // Populate Product Brand dropdown
                string brandQuery = "SELECT DISTINCT product_brand FROM Products";
                using (SqlCommand cmd = new SqlCommand(brandQuery, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlProductBrand.Items.Clear();
                    ddlProductBrand.Items.Add(new ListItem("Select a brand", "")); // Default option

                    while (reader.Read())
                    {
                        ddlProductBrand.Items.Add(new ListItem(reader["product_brand"].ToString()));
                    }
                    reader.Close();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('Session expired. Please log in again.');</script>");
                Response.Redirect("default.aspx");
                return;
            }

            int userID = Convert.ToInt32(Session["UserID"]);
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            int pricingId = 0;
            int inventoryId = 0;
            string imageUrl = null;
            string ram = null;
            string rom = null;

            // Check if new category or brand is provided
            string newCategory = txtProductCategory.Text.Trim();
            string newBrand = txtProductBrand.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Insert Pricing details
                    string pricingQuery = "INSERT INTO Pricing (unit_price, discount_price, created_at, updated_at) " +
                                          "VALUES (@UnitPrice, @DiscountPrice, @CreatedAt, @UpdatedAt); " +
                                          "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(pricingQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UnitPrice", decimal.Parse(txtUnitPrice.Text));
                        cmd.Parameters.AddWithValue("@DiscountPrice", decimal.Parse(txtDiscountPrice.Text));
      
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                        pricingId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert Inventory details
                    string inventoryQuery = "INSERT INTO Inventory (quantity, restock_date, last_updated) " +
                                            "VALUES (@Quantity, @RestockDate, @LastUpdated); " +
                                            "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(inventoryQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));
                        cmd.Parameters.AddWithValue("@RestockDate", DateTime.Parse(txtRestockDate.Text));
                        cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                        inventoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Handle file upload
                    if (fileUploadImage.HasFile)
                    {
                        string fileName = Path.GetFileName(fileUploadImage.PostedFile.FileName);
                        string filePath = Server.MapPath("~/Uploads/") + fileName;

                        try
                        {
                            fileUploadImage.SaveAs(filePath);
                            imageUrl = "Uploads/" + fileName;
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('Error uploading file: " + ex.Message + "');</script>");
                            return;
                        }
                    }

                    // Get RAM and ROM values if applicable
                    string category = ddlProductCategory.SelectedValue.Trim();
                    if (category == "Mobile" || category == "PC" || category=="LAPTOP")
                    {
                        ram = txtRAM.Text.Trim();
                        rom = txtROM.Text.Trim();
                    }

                    // Insert Product details
                    string productQuery = "INSERT INTO Products (product_name, product_description, product_category, product_brand, product_weight, product_image, product_warranty, created_at, updated_at, pricing_id, inventory_id, SupplierID, RAM, ROM) " +
                                          "VALUES (@ProductName, @ProductDescription, @ProductCategory, @ProductBrand, @ProductWeight, @ProductImage, @ProductWarranty, @CreatedAt, @UpdatedAt, @PricingId, @InventoryId, @SupplierId, @RAM, @ROM)";
                    using (SqlCommand cmd = new SqlCommand(productQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@ProductDescription", txtProductDescription.Text);

                        // Use new category if provided, otherwise dropdown selected category
                        cmd.Parameters.AddWithValue("@ProductCategory", string.IsNullOrEmpty(newCategory) ? ddlProductCategory.SelectedValue : newCategory);

                        // Use new brand if provided, otherwise dropdown selected brand
                        cmd.Parameters.AddWithValue("@ProductBrand", string.IsNullOrEmpty(newBrand) ? ddlProductBrand.SelectedValue : newBrand);

                        cmd.Parameters.AddWithValue("@ProductWeight", decimal.Parse(txtProductWeight.Text));
                        cmd.Parameters.AddWithValue("@ProductImage", imageUrl);
                        cmd.Parameters.AddWithValue("@ProductWarranty", txtProductWarranty.Text);
                        cmd.Parameters.AddWithValue("@PricingId", pricingId);
                        cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
                        cmd.Parameters.AddWithValue("@SupplierId", userID);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@RAM", string.IsNullOrEmpty(ram) ? (object)DBNull.Value : ram);
                        cmd.Parameters.AddWithValue("@ROM", string.IsNullOrEmpty(rom) ? (object)DBNull.Value : rom);

                        cmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("ManageProduct.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error saving product details: " + ex.Message + "');</script>");
            }
        }


    }
}
