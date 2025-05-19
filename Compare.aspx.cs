using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Trial
{
    public partial class Compare : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> selectedProductIds = new List<string>();

                if (Request.QueryString["productId1"] != null)
                {
                    string productId1 = Request.QueryString["productId1"];
                    ShowProductDetails(productId1, phResults1);
                    selectedProductIds.Add(productId1);
                }

                if (Request.QueryString["productId2"] != null)
                {
                    string productId2 = Request.QueryString["productId2"];
                    ShowProductDetails(productId2, phResults2);
                    selectedProductIds.Add(productId2);
                }

                if (Request.QueryString["productId3"] != null)
                {
                    string productId3 = Request.QueryString["productId3"];
                    ShowProductDetails(productId3, phResults3);
                    selectedProductIds.Add(productId3);
                }

                if (selectedProductIds.Count > 1)
                {
                    CompareAndSuggest(selectedProductIds);
                }
            }
        }

        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch1.Text;
            SearchProducts(searchQuery, phResults1, 1);
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch2.Text;
            SearchProducts(searchQuery, phResults2, 2);
        }

        protected void btnSearch3_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch3.Text;
            SearchProducts(searchQuery, phResults3, 3);
        }

        private void SearchProducts(string searchQuery, PlaceHolder placeHolder, int searchNumber)
        {
            string query = @"
                SELECT 
                    product_id,
                    product_name,
                    product_image
                FROM 
                    Products
                WHERE 
                    product_name LIKE '%' + @SearchQuery + '%'
                    OR product_category LIKE '%' + @SearchQuery + '%'
                    OR product_brand LIKE '%' + @SearchQuery + '%'
                    OR product_description LIKE '%' + @SearchQuery + '%'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        placeHolder.Controls.Clear(); // Clear only this search result
                        var productNames = new List<string>(); // To store product names for summary
                        while (reader.Read())
                        {
                            string productId = reader["product_id"].ToString();
                            string productName = reader["product_name"].ToString();
                            string productImage = reader["product_image"].ToString();

                            Literal literal = new Literal();
                            string currentUrl = Request.Url.AbsoluteUri;
                            string newUrl = AddOrUpdateQueryString(currentUrl, $"productId{searchNumber}", productId);

                            literal.Text = $@"
                                <div class='search-result-item'>
                                    <a href='{newUrl}'>
                                        <img src='{productImage}' alt='{productName}' style='width: 50px; height: 50px;' />
                                        {productName}
                                    </a>
                                </div>";
                            placeHolder.Controls.Add(literal);

                            productNames.Add(productName); // Add to the list for summary
                        }

                        // Update the summary for all searches after the last search
                        UpdateSummarySuggestions(productNames);
                    }
                    else
                    {
                        Literal noResults = new Literal();
                        noResults.Text = "<p>No matching products found.</p>";
                        placeHolder.Controls.Add(noResults);
                    }
                }
            }
        }

        private void UpdateSummarySuggestions(List<string> productNames)
        {
            if (productNames.Count > 0)
            {
                string summary = $"Based on your search, we found {productNames.Count} products: ";
                summary += string.Join(", ", productNames);
                summary += ". You might also like similar products.";

                Literal summaryLiteral = new Literal();
                summaryLiteral.Text = $"<div class='alert alert-info'>{summary}</div>";
                phSummary.Controls.Clear(); // Clear previous summary
                phSummary.Controls.Add(summaryLiteral);
            }
        }



        // Display selected product details
        private void ShowProductDetails(string productId, PlaceHolder detailsPlaceHolder)
        {
            string query = @"
                SELECT 
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
                    p.ram,
                    p.rom
                FROM 
                    Products p
                JOIN 
                    Pricing pr ON p.pricing_id = pr.pricing_id
                JOIN 
                    Inventory i ON p.inventory_id = i.inventory_id
                JOIN 
                    Supplier s ON p.SupplierID = s.SupplierID
                WHERE 
                    p.product_id = @ProductId";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string productName = reader["product_name"].ToString();
                        string productDescription = reader["product_description"].ToString();
                        string productCategory = reader["product_category"].ToString();
                        string productBrand = reader["product_brand"].ToString();
                        string productWeight = reader["product_weight"].ToString();
                        string productImage = reader["product_image"].ToString();
                        string productWarranty = reader["product_warranty"].ToString();
                        string unitPrice = reader["unit_price"].ToString();
                        string discountPrice = reader["discount_price"].ToString();
                      
                        string quantity = reader["quantity"].ToString();
                        string supplierName = reader["supplier_name"].ToString();
                        string supplierEmail = reader["supplier_contact_email"].ToString();
                        string supplierPhone = reader["supplier_contact_phone"].ToString();
                        string ram = reader["ram"]?.ToString();
                        string rom = reader["rom"]?.ToString();

                        detailsPlaceHolder.Controls.Clear();
                        Literal literal = new Literal();
                        literal.Text = $@"
                            <div class='card mb-4'>
                                <img src='{productImage}' class='card-img-top' alt='{productName}' style='width: 100%; height: 200px; object-fit: cover;' />
                                <div class='card-body'>
                                    <h5 class='card-title'>{productName}</h5>
                                    <p class='card-text'>{productDescription}</p>
                                    <p><b>Category:</b> {productCategory}</p>
                                    <p><b>Brand:</b> {productBrand}</p>
                                    <p><b>Weight:</b> {productWeight}</p>
                                    <p><b>Warranty:</b> {productWarranty}</p>
                                    <p><b>Price:</b> <span class='text-success'>{unitPrice}</span> <del>{discountPrice}</del></small></p>
                                    <p><b>Available Quantity:</b> {quantity}</p>";

                        // Conditional RAM and ROM display
                        if (!string.IsNullOrEmpty(ram))
                        {
                            literal.Text += $"<p><b>RAM:</b> {ram}</p>";
                        }
                        if (!string.IsNullOrEmpty(rom))
                        {
                            literal.Text += $"<p><b>ROM:</b> {rom}</p>";
                        }

                        // Supplier Information
                        literal.Text += $@"
                                    <h5 class='mt-4'>Supplier Information</h5>
                                    <p><b>Name:</b> {supplierName}</p>
                                    <p><b>Email:</b> <a href='mailto:{supplierEmail}'>{supplierEmail}</a></p>
                                    <p><b>Phone:</b> {supplierPhone}</p>
                                </div>
                            </div>";

                        detailsPlaceHolder.Controls.Add(literal);
                    }
                }
            }
        }

        // Function to compare products and provide a summary suggestion
        private void CompareAndSuggest(List<string> selectedProductIds)
        {
            string query = @"
    SELECT 
        p.product_id,
        p.product_name,
        p.product_category,
        p.product_brand,
        p.product_warranty,
        p.ram,
        p.rom,
        pr.unit_price
    FROM 
        Products p
    JOIN 
        Pricing pr ON p.pricing_id = pr.pricing_id
    WHERE 
        p.product_id IN (@ProductId1, @ProductId2, @ProductId3)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId1", selectedProductIds.ElementAtOrDefault(0) ?? "");
                    cmd.Parameters.AddWithValue("@ProductId2", selectedProductIds.ElementAtOrDefault(1) ?? "");
                    cmd.Parameters.AddWithValue("@ProductId3", selectedProductIds.ElementAtOrDefault(2) ?? "");

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<(string productId, string productName, string category, string brand, string warranty, string ram, string rom, decimal unitPrice)> products = new List<(string, string, string, string, string, string, string, decimal)>();

                    while (reader.Read())
                    {
                        string productId = reader["product_id"].ToString();
                        string productName = reader["product_name"].ToString();
                        string productCategory = reader["product_category"].ToString();
                        string productBrand = reader["product_brand"].ToString();
                        string warranty = reader["product_warranty"].ToString();
                        string ram = reader["ram"]?.ToString();
                        string rom = reader["rom"]?.ToString();
                        decimal unitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price"));

                        products.Add((productId, productName, productCategory, productBrand, warranty, ram, rom, unitPrice));
                    }

                    if (products.Count > 0)
                    {
                        var categoryGroup = products.GroupBy(p => p.category).ToList();
                        PlaceHolder phComparisonSummary = new PlaceHolder();
                        phComparisonSummary.Controls.Clear();

                        if (categoryGroup.Count == 1) // Same category for all products
                        {
                            var groupedProducts = categoryGroup.First().ToList();
                            var referenceProduct = groupedProducts.First();

                            bool allRamRomMatch = groupedProducts.All(p =>
                                GetRamValue(p.ram) == GetRamValue(referenceProduct.ram) &&
                                GetRomValue(p.rom) == GetRomValue(referenceProduct.rom));

                            if (allRamRomMatch)
                            {
                                // Same RAM/ROM, suggest based on price and warranty
                                var bestRamRomProduct = groupedProducts.OrderBy(p => p.unitPrice).First();
                                phComparisonSummary.Controls.Add(new Literal
                                {
                                    Text = $"<h4>Best Option (same RAM/ROM): {bestRamRomProduct.productName} at {bestRamRomProduct.unitPrice:C} (Warranty: {bestRamRomProduct.warranty})</h4>"
                                });
                            }
                            else
                            {
                                // RAM/ROM differ, compare and suggest better options
                                foreach (var product in groupedProducts)
                                {
                                    int productRam = GetRamValue(product.ram);
                                    int productRom = GetRomValue(product.rom);

                                    var betterOptions = groupedProducts
                                        .Where(p => p != product)
                                        .ToList();

                                    foreach (var betterProduct in betterOptions)
                                    {
                                        int betterRam = GetRamValue(betterProduct.ram);
                                        int betterRom = GetRomValue(betterProduct.rom);

                                        if (betterRam > productRam && betterRom > productRom && betterProduct.unitPrice < product.unitPrice)
                                        {
                                            phComparisonSummary.Controls.Add(new Literal
                                            {
                                                Text = $"<h4>Suggestion: Consider {betterProduct.productName} (Price: {betterProduct.unitPrice:C}, RAM: {betterProduct.ram}, ROM: {betterProduct.rom}) instead of {product.productName} (Price: {product.unitPrice:C}, RAM: {product.ram}, ROM: {product.rom}) for better specs at a lower price.</h4>"
                                            });
                                        }
                                    }
                                }
                            }

                            // Summary of all selected products
                            phComparisonSummary.Controls.Add(new Literal
                            {
                                Text = "<h4>Selected Products Summary:</h4><ul>"
                            });

                            foreach (var product in groupedProducts)
                            {
                                phComparisonSummary.Controls.Add(new Literal
                                {
                                    Text = $"<li>{product.productName} - Price: {product.unitPrice:C}, Warranty: {product.warranty}</li>"
                                });
                            }

                            phComparisonSummary.Controls.Add(new Literal
                            {
                                Text = "</ul>"
                            });
                        }

                        phResultsComparison.Controls.Add(phComparisonSummary);
                    }
                }
            }
        }

        private int GetRamValue(string ram)
        {
            if (ram.Contains("GB"))
            {
                var value = ram.Split(' ')[0];
                return int.TryParse(value, out int result) ? result : 0;
            }
            return 0; // Invalid format
        }

        private int GetRomValue(string rom)
        {
            if (rom.Contains("GB") || rom.Contains("SSD"))
            {
                var value = rom.Split(' ')[0];
                return int.TryParse(value, out int result) ? result : 0;
            }
            return 0; // Invalid format
        }







        private string AddOrUpdateQueryString(string url, string key, string value)
        {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            query[key] = value;

            var uriBuilder = new UriBuilder(uri)
            {
                Query = query.ToString()
            };
            return uriBuilder.ToString();
        }
    }
}
