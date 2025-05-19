<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Trial._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TechFix User Login</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
    <style>
        body {
            background-color: #1e1e2f; /* Dark background for a tech feel */
            color: #ffffff; /* White text for contrast */
            font-family: 'Roboto', sans-serif; /* Modern font */
        }
        .login-container {
            margin-top: 100px; /* Space from the top */
        }
        .login-form {
            background-color: #2a2a38; /* Slightly lighter background for form */
            padding: 40px;
            border-radius: 10px; /* Rounded corners */
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.5); /* Stronger shadow */
        }
        .login-title {
            margin-bottom: 30px;
            font-weight: bold;
            font-size: 28px; /* Larger title */
            text-align: center; /* Center align title */
            color: #00bcd4; /* Teal color for techy look */
        }
        .form-group {
            margin-bottom: 20px; /* Spacing between input fields */
        }
        .form-control {
            border-radius: 5px; /* Rounded input fields */
            background-color: #3a3a4a; /* Input background */
            color: #ffffff; /* Input text color */
            border: 1px solid #00bcd4; /* Teal border */
            height: 50px; /* Consistent height */
        }
        .form-control:focus {
            background-color: #3a3a4a; /* Keep background on focus */
            border-color: #00bcd4; /* Teal border on focus */
            box-shadow: 0 0 5px rgba(0, 188, 212, 0.5); /* Focus shadow */
        }
        .btn-primary {
            width: 100%; /* Full-width button */
            padding: 12px; /* Increased padding */
            border-radius: 5px; /* Rounded button */
            background-color: #00bcd4; /* Teal background */
            border: none; /* Remove border */
            font-size: 16px; /* Button font size */
            margin-top: 10px; /* Space above button */
        }
        .btn-primary:hover {
            background-color: #0097a7; /* Darker teal on hover */
        }
        .footer-text {
            margin-top: 20px;
            text-align: center;
            color: #bbbbbb; /* Light grey text */
        }
        .footer-text a {
            color: #00bcd4; /* Teal links */
            text-decoration: underline; /* Underline links */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="container login-container">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="login-form">
                        <h2 class="login-title">User Login</h2>

                        <div class="form-group">
                            <label for="Username">Username:</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter your username" required></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="Password">Password:</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password" required></asp:TextBox>
                        </div>

                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />

                  
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</asp:Content>
