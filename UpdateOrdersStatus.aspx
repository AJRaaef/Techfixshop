<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateOrdersStatus.aspx.cs" Inherits="Trial.UpdateOrdersStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #2c2f33;
            color: white;
        }

        .table {
            margin: 20px auto;
            width: 90%;
            background-color: #23272a;
            border-collapse: collapse;
        }

        .table th, .table td {
            padding: 10px;
            text-align: left;
            border: 1px solid #99aab5;
        }

        .table th {
            background-color: #7289da;
            color: white;
        }

        .table td {
            color: white;
        }

        /* Update button styling */
        .btn-update {
            background-color: #7289da;
            border: none;
            color: white;
            padding: 5px 10px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 4px 2px;
            cursor: pointer;
        }

        .btn-update:hover {
            background-color: #5a6eae;
        }

        /* Dropdown list styling */
        .ddl-updates {
            background-color: #23272a;
            color: white;
            border: 1px solid #7289da;
            padding: 5px;
        }

        .ddl-updates option {
            background-color: #2c2f33;
            color: white;
        }

        /* Message label styling */
        #lblMessage {
            color: red;
        }

        /* Heading styling */
        h2 {
            text-align: center;
            color: #7289da;
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Heading for the page -->
    <h2>Update Order Status</h2>

    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        <asp:GridView ID="gvConfirmedOrders" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="product_name" HeaderText="Product Name" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="order_status" HeaderText="Order Status" />
                <asp:BoundField DataField="order_updates" HeaderText="Order Updates" /> 
                <asp:TemplateField HeaderText="Update Status">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlOrderUpdates" runat="server" CssClass="ddl-updates">
                            <asp:ListItem Text="Processing" Value="processing"></asp:ListItem>
                            <asp:ListItem Text="Shipping" Value="shipping"></asp:ListItem>
                            <asp:ListItem Text="Delivered" Value="delivered"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnUpdateOrder" runat="server" Text="Update" CommandArgument='<%# Eval("OrderID") %>' OnClick="btnUpdateOrder_Click" CssClass="btn-update" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
