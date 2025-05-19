<%@ Page Title="Signup TechFix" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SignupTechFix.aspx.cs" Inherits="Trial.SignupTechFix" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Signup as TechFix User</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Signup as TechFix User</h2>
    <form id="form1" runat="server">
        <label for="Username">Username:</label>
        <asp:TextBox ID="txtUsername" runat="server" required="true"></asp:TextBox><br /><br />

        <label for="Password">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required="true"></asp:TextBox><br /><br />

        <label for="FullName">Full Name:</label>
        <asp:TextBox ID="txtFullName" runat="server" required="true"></asp:TextBox><br /><br />

        <label for="Email">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" required="true"></asp:TextBox><br /><br />

        <label for="Phone">Phone:</label>
        <asp:TextBox ID="txtPhone" runat="server" required="true"></asp:TextBox><br /><br />

        <!-- Hidden field for TechFixUserType, defaulting to 'techfix_user' -->
        <asp:HiddenField ID="hfUserType" runat="server" Value="techfix_user" />

        <asp:Button ID="btnSignup" runat="server" Text="Sign Up" OnClick="btnSignup_Click" />
    </form>
</asp:Content>
