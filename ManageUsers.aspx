<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Trial.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/ManageUsers.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        body {
            background-color: #343a40; /* Dark background */
            color: white; /* Text color */
        }
        h2 {
            margin-top: 20px;
            animation: fadeIn 1s; /* Animation */
        }
        /* Animation */
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }
        .navbar {
            background-color: #212529; /* Dark navbar */
        }
        .navbar-brand {
            color: white !important;
        }
        .navbar-nav .nav-link {
            color: white !important;
        }
        .navbar-nav .nav-link:hover {
            color: #adb5bd !important; /* Lighten on hover */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="navbar navbar-expand-lg">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">TechFix</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" href="#">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">About Us</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <form id="form1" runat="server">
        <h2>Manage TechFix Users</h2>
        <asp:GridView ID="gvTechFixUsers" runat="server" AutoGenerateColumns="False" OnRowCommand="gvTechFixUsers_RowCommand">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="User ID" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteTechFixUser" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h2>Manage Suppliers</h2>
        <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" OnRowCommand="gvSuppliers_RowCommand">
            <Columns>
                <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                <asp:BoundField DataField="ContactEmail" HeaderText="Contact Email" />
                <asp:BoundField DataField="ContactPhone" HeaderText="Contact Phone" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteSupplier" CommandArgument='<%# Eval("SupplierID") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
