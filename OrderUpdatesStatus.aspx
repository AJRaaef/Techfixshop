<%@ Page Title="Order Updates Status" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderUpdatesStatus.aspx.cs" Inherits="Trial.OrderUpdatesStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        /* Dark theme for the entire page */
        body {
            background-color: #121212; /* Dark background for the page */
            color: #ffffff; /* Light text for readability */
        }

        /* Navbar styling */
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

        /* Title styling */
        h1, h2, h3, h4, h5, h6 {
            color: #007bff; /* Bootstrap blue color */
            text-align: center;
            margin-top: 30px;
            font-weight: bold;
        }

        /* Table styling */
        .table {
            margin: 0 auto;
            width: 90%;
            border-radius: 8px;
            overflow: hidden;
            background-color: #343a40; /* Dark table background */
        }

        .table th {
            background-color: #007bff; /* Blue table header background */
            color: white;
            font-weight: bold;
        }

        .table td {
            background-color: #495057; /* Darker grey for table rows */
            color: #ffffff; /* White text */
        }

        /* Message label styling */
        .message {
            color: red;
            text-align: center;
        }

        /* Button styling for the dark theme */
        .btn {
            background-color: #007bff; /* Bootstrap blue color for buttons */
            color: #ffffff; /* White text on buttons */
        }

        .btn:hover {
            background-color: #0056b3; /* Darker blue on hover */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark">
        <a class="navbar-brand" href="#">TechFix</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a class="nav-link" href="AdminDashboard.aspx">Home</a>
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

    <!-- Title -->
    <h2>Order Updates Status</h2>

    <!-- Message Label -->
    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>

        <!-- Table to display order updates -->
        <asp:GridView ID="gvOrderUpdates" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="order_updates" HeaderText="Order Updates" />
                <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="order_status" HeaderText="Order Status" />
                <asp:BoundField DataField="product_name" HeaderText="Product Name" />
                <asp:BoundField DataField="product_category" HeaderText="Product Category" />
                <asp:BoundField DataField="product_brand" HeaderText="Product Brand" />
            </Columns>
        </asp:GridView>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</asp:Content>

