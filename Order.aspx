<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="YourNamespace.Order" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Product</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #121212; /* Dark background */
            color: #ffffff; /* White text */
            margin: 0;
            padding: 20px;
            animation: fadeIn 0.5s; /* Fade in animation for body */
        }
        
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }

        .navbar {
            background-color: #1f1f1f; /* Darker navbar background */
            border-bottom: 2px solid #007bff; /* Accent line */
        }
        
        .navbar-brand, .nav-link {
            color: #ffffff !important; /* White text */
            font-weight: bold; /* Bold text */
        }

        .nav-link:hover {
            color: #00bfff !important; /* Highlight color on hover */
            text-decoration: underline; /* Underline on hover */
            transition: color 0.3s; /* Smooth color transition */
        }

        .product-details {
            border: 1px solid #444; /* Dark border */
            padding: 20px;
            border-radius: 8px;
            background-color: #1e1e1e; /* Dark background for product details */
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
            max-width: 600px;
            margin: auto;
            text-align: center; /* Center text for alignment */
            margin-top: 20px;
            transform: translateY(20px); /* Initial position for animation */
            opacity: 0; /* Initial opacity for animation */
            animation: slideIn 0.5s forwards; /* Slide in animation */
        }

        @keyframes slideIn {
            from {
                transform: translateY(20px);
                opacity: 0;
            }
            to {
                transform: translateY(0);
                opacity: 1;
            }
        }

        .product-image {
            max-width: 100%;
            height: auto;
            border-radius: 8px;
            margin-bottom: 20px;
            transition: transform 0.3s; /* Smooth zoom effect */
        }

        .product-image:hover {
            transform: scale(1.05); /* Zoom effect on hover */
        }

        .label {
            font-weight: bold;
            display: block;
            margin: 10px 0;
            color: #e0e0e0; /* Lighter text color for labels */
        }

        .quantity-input {
            width: 60px;
            margin-right: 10px;
        }

        .button {
            margin-top: 20px;
            padding: 10px 20px;
            background-color: #28a745; /* Green background */
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s, transform 0.3s; /* Smooth transition */
        }

        .button:hover {
            background-color: #218838; /* Darker green on hover */
            transform: scale(1.05); /* Slightly enlarge button on hover */
        }

        h1 {
            text-align: center;
            color: #ffffff; /* White text for the title */
            margin-bottom: 20px; /* Add some space below the title */
        }

        #lblTotalAmount {
            font-size: 18px; /* Increase font size for total amount */
            font-weight: bold; /* Make it bold */
        }

        #lblErrorMessage {
            margin-top: 10px; /* Add some margin for error messages */
        }
    </style>
    <script>
        let unitPrice = 0;
        let discountPrice = 0;

        function updateTotal() {
            const quantity = document.getElementById('<%= txtQuantity.ClientID %>').value;
            const totalLabel = document.getElementById('<%= lblTotalAmount.ClientID %>');

            // Determine the effective price to use based on discount availability
            const priceToUse = discountPrice > 0 ? discountPrice : unitPrice;

            // Calculate total amount
            if (quantity > 0) {
                const totalAmount = priceToUse * quantity;
                totalLabel.innerText = "Total Amount: $" + totalAmount.toFixed(2);
            } else {
                totalLabel.innerText = "Total Amount: $0.00";
            }
        }

        function setPrices(unit, discount) {
            unitPrice = unit;
            discountPrice = discount;
            updateTotal(); // Update total on page load with initial values
        }
    </script>
</head>
<body>
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
                    <a class="nav-link" href="Compare.aspx">Compare</a>
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

    <form id="form1" runat="server">
        <div class="product-details">
            <h1>Order Product</h1>
            <asp:Label ID="lblProductName" runat="server" CssClass="label" Font-Bold="True" Font-Size="Large"></asp:Label>
            <asp:Image ID="imgProduct" runat="server" CssClass="product-image" />
            <asp:Label ID="lblProductDescription" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblProductCategory" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblProductBrand" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblProductWeight" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblProductWarranty" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblRAM" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblROM" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblProductPrice" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblDiscountPrice" runat="server" CssClass="label" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblAvailableQuantity" runat="server" CssClass="label"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>

            <div class="d-flex justify-content-center align-items-center">
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="quantity-input" onkeyup="updateTotal()"></asp:TextBox>
                <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" CssClass="button" OnClick="btnConfirmOrder_Click" />
            </div>
            <asp:Label ID="lblTotalAmount" runat="server" CssClass="label" Font-Size="Medium" ForeColor="Green"></asp:Label>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script>
        // Call the setPrices function with example prices when the page loads
        window.onload = function () {
            setPrices(200, 150); // Example unit price and discount price
        };
    </script>
</body>
</html>

