<%@ Page Title="Add Users" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Trial.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            width: 50%;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            background-color: #f9f9f9;
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-container label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
        }

        .form-container input, 
        .form-container select {
            width: 100%;
            padding: 8px;
            margin-bottom: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-container button {
            width: 100%;
            padding: 10px;
            background-color: #007BFF;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        .form-container button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Add User</h2>

            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>

            <asp:Label ID="lblUserType" runat="server" Text="Select User Type:"></asp:Label>
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Value="" Text="-- Select User Type --" />
                <asp:ListItem Value="TechFixUser" Text="TechFix User" />
                <asp:ListItem Value="Supplier" Text="Supplier" />
            </asp:DropDownList>

            <!-- TechFixUser Form -->
            <div id="TechFixUserForm" runat="server" visible="false">
                <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                <asp:TextBox ID="txtTechFixUsername" runat="server"></asp:TextBox>

                <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="txtTechFixPassword" runat="server" TextMode="Password"></asp:TextBox>

                <asp:Label ID="lblFullName" runat="server" Text="Full Name:"></asp:Label>
                <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>

                <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

                <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>

 

                <asp:Button ID="btnAddTechFixUser" runat="server" Text="Add TechFix User" OnClick="btnAddTechFixUser_Click" />
            </div>

            <!-- Supplier Form -->
            <div id="SupplierForm" runat="server" visible="false">
                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:"></asp:Label>
                <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>

                <asp:Label ID="lblContactName" runat="server" Text="Contact Name:"></asp:Label>
                <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>

                <asp:Label ID="lblContactEmail" runat="server" Text="Contact Email:"></asp:Label>
                <asp:TextBox ID="txtContactEmail" runat="server"></asp:TextBox>

                <asp:Label ID="lblContactPhone" runat="server" Text="Contact Phone:"></asp:Label>
                <asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox>

                <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>

                <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>

                <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label>
                <asp:TextBox ID="txtState" runat="server"></asp:TextBox>

                <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
                <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>

                <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label>
                <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>

                <asp:Label ID="lblSupplierUsername" runat="server" Text="Supplier Username:"></asp:Label>
                <asp:TextBox ID="txtSupplierUsername" runat="server"></asp:TextBox>

                <asp:Label ID="lblSupplierPassword" runat="server" Text="Supplier Password:"></asp:Label>
                <asp:TextBox ID="txtSupplierPassword" runat="server" TextMode="Password"></asp:TextBox>

                <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" />
            </div>
        </div>
    </form>
</asp:Content>
