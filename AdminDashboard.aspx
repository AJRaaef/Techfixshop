<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Trial.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dashboard {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
            margin: 20px;
        }

        .dashboard .card {
            width: 300px;
            margin: 20px;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            text-align: center;
        }

        .dashboard .card h3 {
            margin-bottom: 15px;
            color: #333;
        }

        .dashboard .card a {
            text-decoration: none;
            padding: 10px 20px;
            background-color: #007BFF;
            color: white;
            border-radius: 5px;
            display: inline-block;
        }

        .dashboard .card a:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="text-align:center;">Admin Dashboard</h2>

    <div class="dashboard">
        <!-- Add Users Section -->
        <div class="card">
            <h3>Add Users</h3>
            <p>Create new user accounts for staff or customers.</p>
            <a href="AddUser.aspx">Go to Add Users</a>
        </div>

        <!-- Manage Users Section -->
        <div class="card">
            <h3>Manage Users</h3>
            <p>Edit or remove user accounts, reset passwords, and manage roles.</p>
            <a href="ManageUsers.aspx">Go to Manage Users</a>
        </div>

        <!-- Order History Section -->
        <div class="card">
            <h3>Order History</h3>
            <p>View the complete history of all customer orders.</p>
            <a href="OrderUpdates.aspx">Go to Order History</a>
        </div>

        <!-- Order Status Section -->
        <div class="card">
            <h3>Order Status</h3>
            <p>Check the status of current orders and update them as needed.</p>
            <a href="OrderUpdatesStatus.aspx">Go to Order Status</a>
        </div>
    </div>
</asp:Content>

