<%@ Page Title="Order History" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Trial.OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .header {
            text-align: center;
            margin: 30px 0;
            font-size: 28px;
            font-weight: bold;
            color: #333;
        }

        .message {
            color: green;
            text-align: center;
            margin: 10px 0;
            font-size: 18px;
        }

        .table {
            margin: 0 auto;
            width: 90%;
            border-radius: 8px;
            overflow: hidden;
        }

        .table th {
            background-color: #007bff;
            color: white;
            font-weight: bold;
        }

        .table td {
            background-color: #f8f9fa;
            color: #333;
        }

        .table img {
            max-width: 100px;
            height: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formOrderHistory" runat="server">
        <div class="header">Supplier Order History</div>

        <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>

        <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="product_id" HeaderText="Product ID" />
                <asp:BoundField DataField="product_name" HeaderText="Product Name" />
                <asp:ImageField DataImageUrlField="product_image" HeaderText="Product Image" ControlStyle-Width="100px" ControlStyle-Height="100px" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="order_status" HeaderText="Order Status" />

                <asp:TemplateField HeaderText="Update Status">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                            <asp:ListItem Text="Select" Value="" />
                            <asp:ListItem Text="Confirmed" Value="Confirmed" />
                            <asp:ListItem Text="Cancelled" Value="Cancelled" />
                        </asp:DropDownList>
                        <asp:HiddenField ID="hfOrderID" runat="server" Value='<%# Eval("OrderID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</asp:Content>
