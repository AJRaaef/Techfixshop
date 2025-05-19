<%@ Page Title="Supplier Signup" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SignupSupplier.aspx.cs" Inherits="Trial.SignupSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Supplier Signup</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <h2>Supplier Signup Form</h2>

        <!-- Login Link -->
        <div>
            <a href="Login.aspx">Already have an account? Login here</a>
        </div>

        <br />

        <label for="Username">Username:</label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />

        <label for="Password">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />

        <label for="CompanyName">Company Name:</label>
        <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox><br />

        <label for="ContactName">Contact Name:</label>
        <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox><br />

        <label for="ContactEmail">Contact Email:</label>
        <asp:TextBox ID="txtContactEmail" runat="server"></asp:TextBox><br />

        <label for="ContactPhone">Contact Phone:</label>
        <asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox><br />

        <label for="Address">Address:</label>
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox><br />

        <label for="City">City:</label>
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox><br />

        <label for="State">State:</label>
        <asp:TextBox ID="txtState" runat="server"></asp:TextBox><br />

        <label for="PostalCode">Postal Code:</label>
        <asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox><br />

        <label for="Country">Country:</label>
        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox><br />

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </form>
</asp:Content>
