using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; // Add this namespace
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;

namespace Trial
{
    public partial class TechFixUserDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryDropdown();
                BindBrandDropdown();
                BindProductData(); // Existing method to bind the product data
            }
        }

        private void BindCategoryDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT DISTINCT product_category FROM Products";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    ddlCategory.DataSource = cmd.ExecuteReader();
                    ddlCategory.DataTextField = "product_category";
                    ddlCategory.DataValueField = "product_category";
                    ddlCategory.DataBind();
                }
            }

            ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
        }


        private void BindBrandDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT DISTINCT product_brand FROM Products";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    ddlBrand.DataSource = cmd.ExecuteReader();
                    ddlBrand.DataTextField = "product_brand";
                    ddlBrand.DataValueField = "product_brand";
                    ddlBrand.DataBind();
                }
            }

            ddlBrand.Items.Insert(0, new ListItem("Select Brand", ""));
        }

        private void BindProductData()
        {
            // Retrieve the connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = @"
    SELECT 
        p.product_id,
        p.product_name,
        p.product_description,
        p.product_category,
        p.product_brand,
        p.product_weight,
        p.product_image,
        p.product_warranty,
        pr.unit_price,
        pr.discount_price,
        i.quantity,
        s.CompanyName AS supplier_name,
        s.ContactEmail AS supplier_contact_email,
        s.ContactPhone AS supplier_contact_phone,
        p.RAM,
        p.ROM
    FROM 
        Products p
    JOIN 
        Pricing pr ON p.pricing_id = pr.pricing_id
    JOIN 
        Inventory i ON p.inventory_id = i.inventory_id
    JOIN 
        Supplier s ON p.SupplierID = s.SupplierID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            string selectedCategory = ddlCategory.SelectedValue;
            string selectedBrand = ddlBrand.SelectedValue;
            string selectedRAM = ddlRAM.SelectedValue;
            string selectedROM = ddlROM.SelectedValue;

            string query = @"
    SELECT 
        p.product_id,
        p.product_name,
        p.product_description,
        p.product_category,
        p.product_brand,
        p.product_weight,
        p.product_image,
        p.product_warranty,
        pr.unit_price,
        pr.discount_price,
        i.quantity,
        s.CompanyName AS supplier_name,
        s.ContactEmail AS supplier_contact_email,
        s.ContactPhone AS supplier_contact_phone,
        p.RAM,
        p.ROM
    FROM 
        Products p
    JOIN 
        Pricing pr ON p.pricing_id = pr.pricing_id
    JOIN 
        Inventory i ON p.inventory_id = i.inventory_id
    JOIN 
        Supplier s ON p.SupplierID = s.SupplierID
    WHERE 
        (@SearchQuery = '' OR p.product_name LIKE '%' + @SearchQuery + '%')
        AND (@Category = '' OR p.product_category = @Category)
        AND (@Brand = '' OR p.product_brand = @Brand)
        AND (@RAM = '' OR p.RAM = @RAM)
        AND (@ROM = '' OR p.ROM = @ROM)";

            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters for search, category, brand, RAM, and ROM
                    cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    cmd.Parameters.AddWithValue("@Category", selectedCategory);
                    cmd.Parameters.AddWithValue("@Brand", selectedBrand);
                    cmd.Parameters.AddWithValue("@RAM", selectedRAM);
                    cmd.Parameters.AddWithValue("@ROM", selectedROM);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
            }
        }


        private void PopulateRAMDropdown(string category)
        {
            // Your logic to fetch RAM options based on the selected category
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT DISTINCT RAM FROM Products WHERE product_category = @Category AND RAM IS NOT NULL AND RAM <> ''";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                    con.Open();
                    ddlRAM.DataSource = cmd.ExecuteReader();
                    ddlRAM.DataTextField = "RAM";
                    ddlRAM.DataValueField = "RAM";
                    ddlRAM.DataBind();
                }
            }

            ddlRAM.Items.Insert(0, new ListItem("Select RAM", ""));
        }

        private void PopulateROMDropdown(string category)
        {
            // Your logic to fetch ROM options based on the selected category
            string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string query = "SELECT DISTINCT ROM FROM Products WHERE product_category = @Category AND ROM IS NOT NULL AND ROM <> ''";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                    con.Open();
                    ddlROM.DataSource = cmd.ExecuteReader();
                    ddlROM.DataTextField = "ROM";
                    ddlROM.DataValueField = "ROM";
                    ddlROM.DataBind();
                }
            }

            ddlROM.Items.Insert(0, new ListItem("Select ROM", ""));
        }





        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = ddlCategory.SelectedValue;

            // Show RAM and ROM dropdowns if the selected category is Mobile, PC, or Laptop
            if (selectedCategory == "Mobile" || selectedCategory == "PC" || selectedCategory == "Laptop")
            {
                ddlRAM.Visible = true;
                ddlROM.Visible = true;

                // Populate RAM and ROM dropdowns based on the selected category
                PopulateRAMDropdown(selectedCategory);
                PopulateROMDropdown(selectedCategory);
            }
            else
            {
                // Hide the RAM and ROM dropdowns for other categories
                ddlRAM.Visible = false;
                ddlROM.Visible = false;

                // Clear the selections
                ddlRAM.SelectedIndex = 0;
                ddlROM.SelectedIndex = 0;
            }
        }






        protected void btnOrder_Click(object sender, EventArgs e)
        {
            // Get the product ID from the CommandArgument
            Button btnOrder = (Button)sender;
            string productId = btnOrder.CommandArgument;

            // Redirect to an order page or handle the order process
            // For example, redirecting to the Order page with the product ID
            Response.Redirect($"Order.aspx?productId={productId}");
        }




    }
}
