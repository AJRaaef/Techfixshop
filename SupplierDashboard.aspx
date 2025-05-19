<%@ Page Title="Supplier Dashboard" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupplierDashboard.aspx.cs" Inherits="Trial.SupplierDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Head content if necessary -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        /* Dark theme background and text */
        body {
            background-color: #121212; /* Darker background */
            color: #e0e0e0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
        }

        /* Navbar container styles */
        .navbar-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            background-color: #1f1f1f; /* Dark navbar background */
            padding: 15px 40px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.5);
        }

        /* TechFix title styles */
        .techfix-title {
            font-size: 24px;
            color: #ffffff; /* White text for title */
            font-weight: bold;
        }

        /* Navigation bar styles */
        .navbar {
            display: flex;
            gap: 15px; /* Space between links */
        }

        .navbar a {
            color: #8ab4f8; /* Soft blue for links */
            text-decoration: none;
            padding: 10px 15px;
            border-radius: 5px;
            transition: background-color 0.3s ease, color 0.3s ease;
        }

        .navbar a:hover {
            background-color: #007BFF; /* Highlight on hover */
            color: #ffffff; /* Change text color on hover */
        }

        /* Grid container */
        .grid-container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); /* Adjusted min-width */
            gap: 30px;
            padding: 40px;
            margin: 0 auto;
            max-width: 1200px;
        }

        /* Dark themed individual boxes */
        .box {
            background: linear-gradient(145deg, #2a2a2a, #1f1f1f); /* Gradient background */
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.5);
            text-align: center;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .box:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.7);
        }

        /* Icons with a subtle color */
        .box i {
            font-size: 50px;
            color: #007BFF; /* Blue for icons */
            margin-bottom: 20px;
            transition: color 0.3s ease;
        }

        .box:hover i {
            color: #0056b3; /* Darker blue on hover */
        }

        /* Dark themed titles */
        .box h2 {
            font-size: 24px;
            color: #ffffff;
            margin-bottom: 20px;
            font-weight: 600;
        }

        /* Styled buttons */
        .box button {
            background-color: #007BFF; /* Blue */
            color: #ffffff;
            border: none;
            padding: 15px 25px;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
            display: inline-flex;
            align-items: center;
            justify-content: center; /* Center the content */
            margin-top: 15px; /* Space above buttons */
        }

        .box button i {
            margin-right: 10px;
        }

        .box button:hover {
            background-color: #0056b3; /* Darker blue on hover */
            transform: translateY(-2px);
        }

        /* Floating inbox button in dark theme */
        .floating-inbox {
            position: fixed;
            bottom: 20px;
            right: 20px;
            background-color: #007BFF; /* Blue */
            color: #ffffff;
            border: none;
            padding: 15px;
            border-radius: 50%;
            font-size: 24px;
            cursor: pointer;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.5);
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .floating-inbox i {
            font-size: 24px;
        }

        .floating-inbox:hover {
            background-color: #0056b3; /* Darker blue on hover */
            transform: scale(1.1);
        }

        /* Override Master Page link colors for dark theme */
        a {
            color: #8ab4f8; /* Soft blue for links */
            text-decoration: none;
            transition: color 0.3s ease;
        }

        a:hover {
            color: #669df6; /* Lighter blue on hover */
        }
    </style>

    <!-- Navigation Bar -->
    <div class="navbar-container">
        <div class="techfix-title">TechFix</div>
        <div class="navbar">
            <a href="SupplierDashboard.aspx">Home</a>
            <a href="AboutUs.aspx">About Us</a>
        </div>
    </div>

    <div class="grid-container">
        <!-- Box 1: Upload Product -->
        <div class="box">
            <i class="fas fa-cloud-upload-alt"></i> <!-- Icon for uploading products -->
            <h2>Upload Product</h2>
            <button onclick="window.location.href='UploadProduct.aspx'">
                <i class="fas fa-cloud-upload-alt"></i> Upload Product
            </button>
        </div>

        <!-- Box 2: Update Product -->
        <div class="box">
            <i class="fas fa-edit"></i> <!-- Icon for updating product -->
            <h2>Manage Product</h2>
            <button onclick="window.location.href='ManageProduct.aspx'">
                <i class="fas fa-edit"></i> Manage Product
            </button>
        </div>

    

        <!-- Box 4: Order History and Analysis -->
        <div class="box">
            <i class="fas fa-history"></i> <!-- Icon for order history -->
            <h2>Order History & Analysis</h2>
            <button onclick="window.location.href='OrderHistory.aspx'">
                <i class="fas fa-history"></i> View History
            </button>
        </div>

    

        <!-- Box 6: Manage Orders -->
        <div class="box">
            <i class="fas fa-shopping-cart"></i> <!-- Icon for managing orders -->
            <h2>Update Orders Status</h2>
            <button onclick="window.location.href='UpdateOrdersStatus.aspx'">
                <i class="fas fa-shopping-cart"></i> Manage Orders
            </button>
        </div>
    </div>



    <!-- Include FontAwesome for icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

</asp:Content>
