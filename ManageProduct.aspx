<%@ Page Title="Manage Product" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="Trial.ManageProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Basic Reset and Styling */
        body {
            margin: 0;
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
        }
        
        .product-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
            padding: 20px;
            gap: 15px;
        }

        .product-box {
            width: calc(25% - 15px); /* 4 boxes per row, reduced width */
            margin: 10px 0;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 6px rgba(0,0,0,0.08); /* Softer shadow */
            background-color: #fff;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .product-box:hover {
            transform: scale(1.03);
            box-shadow: 0 4px 10px rgba(0,0,0,0.12);
        }

        .product-box img {
            width: 100%;
            height: 180px; /* Limit image height for uniformity */
            object-fit: cover;
            border-bottom: 1px solid #ddd;
        }

        .product-box h3 {
            margin: 10px 15px;
            font-size: 1.1em; /* Slightly smaller font */
            color: #333;
        }

        .product-box p {
            margin: 5px 15px;
            color: #666;
            font-size: 0.85em; /* Slightly smaller font for details */
        }

        .product-box .price {
            font-size: 1em;
            color: #333;
            font-weight: bold;
            margin: 10px 15px;
        }

        @media (max-width: 768px) {
            .product-box {
                width: calc(50% - 15px); /* 2 boxes per row on medium screens */
            }
        }

        @media (max-width: 480px) {
            .product-box {
                width: 100%; /* 1 box per row on small screens */
            }
        }

.edit-panel {
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
    margin: 20px auto;
    max-width: 600px; /* Set a max-width for better compactness */
}

.edit-field {
    margin-bottom: 15px; /* More space between fields */
}

.edit-field label {
    display: block; /* Labels on their own lines */
    font-weight: bold; /* Make labels bold */
    margin-bottom: 5px; /* Space between label and input */
}

.edit-buttons {
    display: flex;
    justify-content: flex-end; /* Align buttons to the right */
    gap: 10px; /* Space between buttons */
}

.edit-buttons .btn {
    padding: 8px 12px; /* Compact button padding */
    border-radius: 4px;
    font-size: 0.9em; /* Smaller font size for buttons */
}

.btn-success {
    background-color: #28a745; /* Success button color */
    color: white;
}

.btn-secondary {
    background-color: #6c757d; /* Secondary button color */
    color: white;
}

.btn-success:hover, .btn-secondary:hover {
    opacity: 0.8; /* Slightly transparent on hover */
}


        .product-actions {
    display: flex;
    justify-content: space-between;
    padding: 10px 15px;
}

.product-actions .btn-edit, .product-actions .btn-delete {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 8px 12px;
    border-radius: 4px;
    cursor: pointer;
    margin-right: 5px;
}

.product-actions .btn-edit {
    background-color: #28a745;
}

.product-actions .btn-delete {
    background-color: #dc3545;
}

.product-actions .btn-edit:hover, .product-actions .btn-delete:hover {
    opacity: 0.8;
}
 .search-container {
            margin: 20px 0;
            display: flex;
            justify-content: center;
        }

        .search-container input[type="text"] {
            padding: 8px;
            font-size: 1em;
            width: 300px;
            margin-right: 10px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }

        .search-container input[type="button"] {
            padding: 8px 15px;
            font-size: 1em;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .search-container input[type="button"]:hover {
            background-color: #0056b3;
        }
    </style>

        <script type="text/javascript">
            function toggleCategoryInput() {
                var ddlCategory = document.getElementById('<%= ddlProductCategory.ClientID %>');
            var txtNewCategory = document.getElementById('<%= txtNewCategory.ClientID %>');
            
            if (ddlCategory.value === "new") {
                txtNewCategory.disabled = false;
            } else {
                txtNewCategory.disabled = true;
                txtNewCategory.value = ""; // Clear the text box if a category is selected
            }
        }

        function toggleBrandInput() {
            var ddlBrand = document.getElementById('<%= ddlProductBrand.ClientID %>');
            var txtNewBrand = document.getElementById('<%= txtNewBrand.ClientID %>');

                if (ddlBrand.value === "new") {
                    txtNewBrand.disabled = false;
                } else {
                    txtNewBrand.disabled = true;
                    txtNewBrand.value = ""; // Clear the text box if a brand is selected
                }
            }
        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!-- Search Bar -->
        <div class="search-container">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search products..." />
            <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary" />
            <asp:Button ID="btnLowStock" Text="Low Stock" OnClick="btnLowStock_Click" runat="server" CssClass="btn btn-warning" />
        </div>

        <div class="product-container">
            <asp:Repeater ID="RepeaterProducts" runat="server" OnItemCommand="RepeaterProducts_ItemCommand">
                <ItemTemplate>
                    <div class="product-box">
                        <img src='<%# Eval("product_image") %>' alt="Product Image" class="product-image"/>
                        <div class="product-details">
                            <h3 class="product-name"><%# Eval("product_name") %></h3>
                            <p><b>Category:</b> <%# Eval("product_category") %></p>
                            <p><b>Brand:</b> <%# Eval("product_brand") %></p>
                            <p class="description"><b>Description:</b> <%# Eval("product_description") %></p>
                            <p><b>Weight:</b> <%# Eval("product_weight") %> kg</p>
                            <p><b>Warranty:</b> <%# Eval("product_warranty") %></p>
                            
                            <%# (Eval("product_category").ToString() == "Mobile" || 
                                 Eval("product_category").ToString() == "PC" || 
                                 Eval("product_category").ToString() == "LAPTOP") && 
                                 DataBinder.Eval(Container.DataItem, "RAM") != DBNull.Value ? 
                                 $"<p><b>RAM:</b> {Eval("RAM")}</p>" : "" %>

                            <%# (Eval("product_category").ToString() == "Mobile" || 
                                 Eval("product_category").ToString() == "PC" || 
                                 Eval("product_category").ToString() == "LAPTOP") && 
                                 DataBinder.Eval(Container.DataItem, "ROM") != DBNull.Value ? 
                                 $"<p><b>ROM:</b> {Eval("ROM")}</p>" : "" %>

                            <div class="price">
                                <p><b>Unit Price:</b> $<%# Eval("unit_price") %></p>
                                <p><b>Discount Price:</b> $<%# Eval("discount_price") %></p>
                            
                            </div>
                            <p><b>Quantity:</b> <%# Eval("quantity") %></p>
                            <p><b>Restock Date:</b> <%# Eval("restock_date", "{0:yyyy-MM-dd}") %></p>
                            
                            <div class="product-actions">
                                <asp:Button ID="btnEdit" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("product_id") %>' runat="server" CssClass="btn btn-edit" />
                                <asp:Button ID="btnDelete" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("product_id") %>' runat="server" CssClass="btn btn-delete" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Edit Panel -->
        <asp:Panel ID="EditPanel" runat="server" CssClass="edit-panel" Visible="false">
            <h2>Edit Product</h2>
            <asp:HiddenField ID="hfProductID" runat="server" />

            <div class="edit-field">
                <label for="txtProductName">Name:</label>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
            </div>

            <div class="edit-field">
                <label for="ddlProductCategory">Category:</label>
                <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control" onchange="toggleCategoryInput()">
                    <asp:ListItem Text="Select Category" Value="" />
                    <asp:ListItem Text="Enter new category" Value="new" />
                </asp:DropDownList>
                <asp:TextBox ID="txtNewCategory" runat="server" CssClass="form-control" placeholder="Or add new category" disabled="disabled" />
            </div>

            <div class="edit-field">
                <label for="ddlProductBrand">Brand:</label>
                <asp:DropDownList ID="ddlProductBrand" runat="server" CssClass="form-control" onchange="toggleBrandInput()">
                    <asp:ListItem Text="Select Brand" Value="" />
                    <asp:ListItem Text="Enter new brand" Value="new" />
                </asp:DropDownList>
                <asp:TextBox ID="txtNewBrand" runat="server" CssClass="form-control" placeholder="Or add new brand" disabled="disabled" />
            </div>

            <div class="edit-field">
                <label for="txtProductDescription">Description:</label>
                <asp:TextBox ID="txtProductDescription" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" />
            </div>

            <div class="edit-field" id="divRAM" runat="server" visible="false">
                <label for="txtRAM">RAM (GB):</label>
                <asp:TextBox ID="txtRAM" runat="server" CssClass="form-control" />
            </div>

            <div class="edit-field" id="divROM" runat="server" visible="false">
                <label for="txtROM">ROM (GB):</label>
                <asp:TextBox ID="txtROM" runat="server" CssClass="form-control" />
            </div>

            <div class="edit-field">
                <label for="txtProductWeight">Weight:</label>
                <asp:TextBox ID="txtProductWeight" runat="server" CssClass="form-control" />
            </div>
            
            <div class="edit-field">
                <label for="txtProductWarranty">Warranty:</label>
                <asp:TextBox ID="txtProductWarranty" runat="server" CssClass="form-control" />
            </div>
            
            <div class="edit-field">
                <label for="txtUnitPrice">Unit Price:</label>
                <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" />
            </div>
            
            <div class="edit-field">
                <label for="txtDiscountPrice">Discount Price:</label>
                <asp:TextBox ID="txtDiscountPrice" runat="server" CssClass="form-control" />
            </div>
  
            
            <div class="edit-field">
                <label for="txtQuantity">Quantity:</label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />
            </div>
            
            <div class="edit-field">
                <label for="txtRestockDate">Restock Date:</label>
                <asp:TextBox ID="txtRestockDate" runat="server" CssClass="form-control" />
            </div>
            
            <div class="edit-buttons">
                <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success" />
                <asp:Button ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-secondary" />
            </div>
        </asp:Panel>
    </form>

    <style>
        body {
            background-color: #121212;
            color: #ffffff;
            font-family: Arial, sans-serif;
            transition: background-color 0.5s ease;
        }

        .search-container {
            padding: 20px;
            display: flex;
            gap: 10px;
        }

        .form-control {
            background-color: #1e1e1e;
            color: #ffffff;
            border: 1px solid #424242;
            transition: border-color 0.3s ease, background-color 0.3s ease;
            border-radius: 5px;
            padding: 10px;
        }

        .form-control:focus {
            border-color: #bb86fc;
            background-color: #292929;
            outline: none;
        }

        .btn {
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-primary {
            background-color: #6200ea;
            border: none;
        }

        .btn-primary:hover {
            background-color: #3700b3;
        }

        .btn-warning {
            background-color: #ff9800;
            border: none;
        }

        .btn-warning:hover {
            background-color: #f57c00;
        }

        .btn-edit {
            background-color: #03a9f4;
        }

        .btn-edit:hover {
            background-color: #0288d1;
        }

        .btn-delete {
            background-color: #f44336;
        }

        .btn-delete:hover {
            background-color: #d32f2f;
        }

        .btn-success {
            background-color: #4caf50;
        }

        .btn-success:hover {
            background-color: #388e3c;
        }

        .btn-secondary {
            background-color: #757575;
        }

        .btn-secondary:hover {
            background-color: #616161;
        }

        .product-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            padding: 20px;
            justify-content: center;
        }

        .product-box {
            background-color: #1e1e1e;
            border-radius: 8px;
            padding: 15px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            width: calc(25% - 20px);
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
            position: relative;
            overflow: hidden;
        }

        .product-box:hover {
            transform: translateY(-5px);
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.5);
        }

        .product-image {
            max-width: 100%;
            height: auto;
            border-radius: 5px;
        }

        .product-name {
            font-size: 1.25em;
            margin: 10px 0;
        }

        .description {
            color: #bbb;
        }

        .price {
            font-weight: bold;
            margin-top: 10px;
        }

        .product-actions {
            display: flex;
            gap: 10px;
            margin-top: 15px;
        }

        .edit-panel {
            background-color: #1e1e1e;
            padding: 20px;
            border-radius: 8px;
            margin-top: 20px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
            transition: transform 0.3s ease;
        }

        .edit-field {
            margin-bottom: 15px;
        }

        .edit-buttons {
            display: flex;
            gap: 10px;
            margin-top: 15px;
        }

        @media (max-width: 768px) {
            .product-box {
                width: calc(50% - 20px);
            }
        }

        @media (max-width: 480px) {
            .product-box {
                width: calc(100% - 20px);
            }
        }
    </style>

    <script>
        function toggleCategoryInput() {
            const ddlCategory = document.getElementById('<%= ddlProductCategory.ClientID %>');
            const txtNewCategory = document.getElementById('<%= txtNewCategory.ClientID %>');
            txtNewCategory.disabled = ddlCategory.value !== 'new';
        }

        function toggleBrandInput() {
            const ddlBrand = document.getElementById('<%= ddlProductBrand.ClientID %>');
            const txtNewBrand = document.getElementById('<%= txtNewBrand.ClientID %>');
            txtNewBrand.disabled = ddlBrand.value !== 'new';
        }

        // Additional JavaScript to show/hide RAM and ROM fields based on category
        document.addEventListener('DOMContentLoaded', function() {
            const ddlCategory = document.getElementById('<%= ddlProductCategory.ClientID %>');
            ddlCategory.addEventListener('change', function() {
                const divRAM = document.getElementById('<%= divRAM.ClientID %>');
                const divROM = document.getElementById('<%= divROM.ClientID %>');

                if (ddlCategory.value === "Mobile" || ddlCategory.value === "PC" || ddlCategory.value === "LAPTOP") {
                    divRAM.style.display = 'block';
                    divROM.style.display = 'block';
                } else {
                    divRAM.style.display = 'none';
                    divROM.style.display = 'none';
                }
            });
        });
    </script>
</asp:Content>
