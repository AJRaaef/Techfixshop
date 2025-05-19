<%@ Page Title="Upload Product" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UploadProduct.aspx.cs" Inherits="Trial.UploadProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #1c1e22; /* Darker background for better contrast */
            color: #ffffff; /* Light text color */
            font-family: Arial, sans-serif; /* Improved font family */
        }
        .navbar {
            background-color: #343a40; /* Darker navbar background */
        }
        .form-control {
            background-color: #2c2f33; /* Darker input background */
            color: #ffffff; /* Light text color in inputs */
            border: 1px solid #6c757d; /* Lighter border for inputs */
        }
        .form-control:focus {
            background-color: #4b545c; /* Lighter focus background */
            color: #ffffff; /* Keep light text color */
            border-color: #80bdff; /* Bootstrap focus border color */
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, .25); /* Bootstrap shadow */
        }
        .btn-primary {
            background-color: #007bff; /* Bootstrap primary color */
            border-color: #007bff; /* Keep primary border */
            transition: background-color 0.3s; /* Smooth transition on hover */
        }
        .btn-primary:hover {
            background-color: #0056b3; /* Darker blue on hover */
            border-color: #0056b3; /* Darker blue border on hover */
        }
        h2, h3 {
            margin-bottom: 20px;
            color: #ffffff; /* Light text color for headings */
        }
        .text-center {
            color: #ffffff; /* Light color for centered text */
        }
        .form-group label {
            color: #f8f9fa; /* Lighter color for labels */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark">
        <a class="navbar-brand" href="#">TechFix</a> <!-- TechFix on the left -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse justify-content-end" id="navbarNav"> <!-- Added justify-content-end for right alignment -->
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link" href="SupplierDashboard.aspx">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">About Us</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="container">
        <h2 class="text-center">Upload New Product</h2>
        <form id="form1" runat="server">
            <!-- Pricing Details -->
            <h3>Pricing Details</h3>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label for="txtUnitPrice">Unit Price:</label>
                    <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <label for="txtDiscountPrice">Discount Price:</label>
                    <asp:TextBox ID="txtDiscountPrice" runat="server" CssClass="form-control" />
                </div>
        
            </div>

            <!-- Inventory Details -->
            <h3>Inventory Details</h3>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label for="txtQuantity">Quantity:</label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <label for="txtRestockDate">Restock Date:</label>
                    <asp:TextBox ID="txtRestockDate" runat="server" CssClass="form-control" />
                </div>
            </div>

            <!-- Product Details -->
            <h3>Product Details</h3>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="txtProductName">Product Name:</label>
                    <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <label for="txtProductDescription">Product Description:</label>
                    <asp:TextBox ID="txtProductDescription" runat="server" TextMode="MultiLine" CssClass="form-control" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="ddlProductCategory">Product Category:</label>
                    <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    <asp:TextBox ID="txtProductCategory" runat="server" CssClass="form-control" placeholder="Enter new category" />
                </div>
                <div class="form-group col-md-6">
                    <label for="ddlProductBrand">Product Brand:</label>
                    <asp:DropDownList ID="ddlProductBrand" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    <asp:TextBox ID="txtProductBrand" runat="server" CssClass="form-control" placeholder="Enter new brand" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label for="txtProductWeight">Product Weight:</label>
                    <asp:TextBox ID="txtProductWeight" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <label for="fileUploadImage">Product Image:</label>
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group col-md-4">
                    <label for="txtProductWarranty">Product Warranty:</label>
                    <asp:TextBox ID="txtProductWarranty" runat="server" CssClass="form-control" />
                </div>
            </div>

            <!-- RAM and ROM fields -->
            <div id="ramRomFields" style="display: none;">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="txtRAM">RAM:</label>
                        <asp:TextBox ID="txtRAM" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtROM">ROM:</label>
                        <asp:TextBox ID="txtROM" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save Product" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </form>
    </div>

    <script type="text/javascript">
        document.getElementById('<%= ddlProductCategory.ClientID %>').onchange = function () {
            const selectedCategory = this.value;
            const ramRomFields = document.getElementById('ramRomFields');
            const isMobileOrPC = selectedCategory === 'Mobile' || selectedCategory === 'PC';

            ramRomFields.style.display = isMobileOrPC ? 'block' : 'none';

            document.getElementById('<%= txtProductCategory.ClientID %>').disabled = this.value !== "";
        };

        document.getElementById('<%= txtProductCategory.ClientID %>').oninput = function () {
            document.getElementById('<%= ddlProductCategory.ClientID %>').disabled = this.value !== "";
        };

        document.getElementById('<%= ddlProductBrand.ClientID %>').onchange = function() {
            document.getElementById('<%= txtProductBrand.ClientID %>').disabled = this.value !== "";
        };

        document.getElementById('<%= txtProductBrand.ClientID %>').oninput = function () {
            document.getElementById('<%= ddlProductBrand.ClientID %>').disabled = this.value !== "";
        };
    </script>
</asp:Content>
