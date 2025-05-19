<%@ Page Title="Order Updates" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderUpdates.aspx.cs" Inherits="Trial.OrderUpdates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
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

        body {
            background-color: #2b2b2b; /* Dark theme background */
            color: #ffffff; /* White text for content */
        }

        .header {
            text-align: center;
            margin: 30px 0;
            font-size: 28px;
            font-weight: bold;
            color: #ffffff;
        }

        .table {
            margin: 0 auto;
            width: 90%;
            border-radius: 8px;
            overflow: hidden;
            background-color: #343a40; /* Dark table background */
        }

        .table th {
            background-color: #007bff;
            color: white;
            font-weight: bold;
        }

        .table td {
            background-color: #495057;
            color: #ffffff;
        }

        .table img {
            max-width: 100px; /* Set a max width for images */
            height: auto; /* Maintain aspect ratio */
        }

        .message {
            color: green; /* Success message color */
            text-align: center;
            margin: 10px 0;
            font-size: 18px;
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

    <div class="header">Order Updates</div>

    <!-- The Form wrapping the GridView -->
    <form id="form1" runat="server">
        <!-- GridView to display the order details -->
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="product_id" HeaderText="Product ID" />
                <asp:BoundField DataField="product_name" HeaderText="Product Name" />
                <asp:ImageField DataImageUrlField="product_image" HeaderText="Product Image" 
                                ControlStyle-Width="100px" ControlStyle-Height="100px" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="order_status" HeaderText="Order Status" />
                <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" /> 
            </Columns>
        </asp:GridView>

        <!-- Optionally add a message label -->
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
