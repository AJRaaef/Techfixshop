<%@ Page Title="Compare Products" Language="C#" MasterPageFile="~/Site1.master" AutoEventWireup="true" CodeBehind="Compare.aspx.cs" Inherits="Trial.Compare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Compare Products</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa; /* Light background color */
        }

        .navbar {
            background-color: #343a40; /* Dark background for navbar */
        }

        .search-results {
            border: 1px solid #ddd;
            max-height: 200px;
            overflow-y: auto;
            margin-top: 5px;
            background-color: #fff;
        }

        .search-result-item {
            padding: 10px;
            border-bottom: 1px solid #ddd;
            cursor: pointer;
        }

        .search-result-item:hover {
            background-color: #f1f1f1;
        }

        .product-details {
            border: 1px solid #ddd;
            padding: 15px;
            border-radius: 8px;
            background-color: #fff;
            margin-top: 20px;
        }

        h1 {
            margin-bottom: 30px;
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
                        <a class="nav-link active" href="Compare.aspx">Compare</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="OrderUpdates.aspx">Order Confirmation</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="OrderUpdatesStatus.aspx">Order Updates</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Logout.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <div class="container mt-4">
            <h1 class="text-center">Compare Products</h1>

            <div class="mb-4">
                <asp:PlaceHolder ID="phSummary" runat="server"></asp:PlaceHolder>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control" placeholder="Search for product 1..."></asp:TextBox>
                    <asp:Button ID="btnSearch1" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch1_Click" />
                    <asp:PlaceHolder ID="phResults1" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearch2" runat="server" CssClass="form-control" placeholder="Search for product 2..."></asp:TextBox>
                    <asp:Button ID="btnSearch2" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch2_Click" />
                    <asp:PlaceHolder ID="phResults2" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearch3" runat="server" CssClass="form-control" placeholder="Search for product 3..."></asp:TextBox>
                    <asp:Button ID="btnSearch3" runat="server" Text="Search" CssClass="btn btn-primary mt-2" OnClick="btnSearch3_Click" />
                    <asp:PlaceHolder ID="phResults3" runat="server"></asp:PlaceHolder>
                </div>
            </div>

            <asp:PlaceHolder ID="phResultsComparison" runat="server"></asp:PlaceHolder>

        </div>
    </form>
</asp:Content>
