<%@ Page Title="Product Catalog" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeFile="TechFixUserDashboard.aspx.cs" Inherits="Trial.TechFixUserDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TechFix User Dashboard - Products</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #121212; /* Dark background */
            color: #ffffff; /* Light text */
            font-family: 'Roboto', sans-serif; /* Modern font */
        }

        .navbar {
            background-color: #1f1f1f; /* Darker navbar */
            border-bottom: 2px solid #007bff; /* Accent line */
        }

        .navbar-brand, .nav-link {
            color: #ffffff !important; /* White text for navbar */
            font-weight: bold; /* Bold text */
        }

        .navbar-nav .nav-link:hover {
            color: #00bfff !important; /* Highlight color on hover */
            text-decoration: underline; /* Underline on hover */
        }

        .product-card {
            border: 1px solid #333; /* Darker border */
            border-radius: 10px;
            padding: 20px;
            transition: transform 0.3s, box-shadow 0.3s;
            height: 100%;
            background-color: #2b2b2b; /* Dark card background */
        }

        .product-card:hover {
            transform: scale(1.05); /* Slight zoom on hover */
            box-shadow: 0 8px 30px rgba(255, 255, 255, 0.2); /* Lighter shadow on hover */
        }

        .product-image {
            height: 200px;
            object-fit: cover;
            border-radius: 10px;
            margin-bottom: 15px;
        }

        .search-bar {
            margin-bottom: 20px;
        }

        .btn-primary {
            background-color: #007bff; /* Bootstrap primary color */
            border: none;
            transition: background-color 0.3s; /* Smooth transition */
        }

        .btn-primary:hover {
            background-color: #0056b3; /* Darker shade on hover */
        }

        .btn-success {
            background-color: #28a745; /* Green color */
            border: none;
            transition: background-color 0.3s; /* Smooth transition */
        }

        .btn-success:hover {
            background-color: #218838; /* Darker shade on hover */
        }

        .floating-btn {
            position: fixed;
            bottom: 20px;
            right: 20px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 50%;
            width: 60px;
            height: 60px;
            display: flex;
            align-items: center;
            justify-content: center;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.5);
            cursor: pointer;
            font-size: 24px;
            z-index: 1000;
            transition: transform 0.3s; /* Smooth transition */
        }

        .floating-btn:hover {
            transform: scale(1.1); /* Slight zoom on hover */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark">
            <a class="navbar-brand" href="#">TechFix</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="TechFixUserDashboard.aspx">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Compare.aspx">Compare</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="OrderUpdates.aspx">Order Confirmation</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="OrderUpdatesStatus.aspx">Order Updates</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="default.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <div class="container mt-4">
            <h1 class="text-center">Product Catalog</h1>

            <!-- Search Bar -->
            <div class="row mb-3 search-bar">
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search for products..." />
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="">Select Category</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Brand" Value="" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlRAM" runat="server" CssClass="form-control" Visible="false">
                        <asp:ListItem Value="">Select RAM</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlROM" runat="server" CssClass="form-control" Visible="false">
                        <asp:ListItem Value="">Select ROM</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch_Click" />
                </div>
            </div>

            <!-- Product Grid -->
            <div class="row">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 mb-4">
                            <div class="product-card">
                                <img src='<%# Eval("product_image") %>' alt="Product Image" class="img-fluid product-image" />
                                <h5><%# Eval("product_name") %></h5>
                                <p><b>Brand:</b> <%# Eval("product_brand") %></p>
                                <p><b>Category:</b> <%# Eval("product_category") %></p>
                                <p><b>Weight:</b> <%# Eval("product_weight") %> kg</p>
                                <p><b>Warranty:</b> <%# Eval("product_warranty") %> months</p>
                                <p><b>Unit Price:</b> $<%# Eval("unit_price") %></p>
                                <p><b>Discount Price:</b> $<%# Eval("discount_price") %></p>
                                <p><b>Stock:</b> <%# Eval("quantity") %> units available</p>
                                <p><b>Supplier:</b> <%# Eval("supplier_name") %></p>
                                <p><b>Contact Email:</b> <%# Eval("supplier_contact_email") %></p>
                                <p><b>Contact Phone:</b> <%# Eval("supplier_contact_phone") %></p>

                                <%-- RAM and ROM display --%>
                                <p><b>RAM:</b> <%# Eval("RAM") != DBNull.Value ? Eval("RAM") + " GB" : "Not applicable" %></p>
                                <p><b>ROM:</b> <%# Eval("ROM") != DBNull.Value ? Eval("ROM") + " GB" : "Not applicable" %></p>

                                <!-- Order Button -->
                                <asp:Button ID="btnOrder" runat="server" Text="Order" CssClass="btn btn-success" CommandArgument='<%# Eval("product_id") %>' OnClick="btnOrder_Click" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Floating Action Button -->
        <button class="floating-btn" onclick="location.href='TechFixUserDashboard.aspx';">
            <i class="fas fa-plus"></i>
        </button>

        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</asp:Content>
