<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WarehouseStatus.aspx.cs" Inherits="stock_tracking.WarehouseStatus" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            text-align: left;
            padding: 8px;
        }

        th {
            background-color: #4CAF50;
            color: white;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        tr.separator {
            border-top: 1px solid #ddd;
            border-bottom: 1px solid;
        }
    </style>


    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />


    <div style="display: flex; justify-content: space-between; margin-right: 2cm;">
        <div style="width: 30%;">
            <asp:DropDownList ID="ddlWarehouseName" CssClass="form-select" runat="server">
                <asp:ListItem Value="" Text="Select Warehouse Name" />
            </asp:DropDownList>
        </div>

        <div style="width: 30%;">
            <asp:DropDownList ID="ddlCategory" CssClass="form-select" runat="server">
                <asp:ListItem Value="" Text="Select Category Name" />
            </asp:DropDownList>
        </div>

        <div style="width: 30%;">
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Out of Stock Products" />
        </div>

        <div style="width: 10%;">
            <asp:Button ID="btnFilter" CssClass="btn btn-primary" OnClick="btnFilter_Click" Text="Filter" runat="server" />
        </div>


    </div>




    <section id="section">
        <div class="row match-height">
            <div class="col-12">
                <div class="card">

                    <div class="card-content">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 col-12">

                                    <table>
                                        <asp:Repeater ID="rptr4" DataSourceID="ds4" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th>Warehouse Name</th>
                                                    <th>Product Name</th>
                                                    <th>Qunatity</th>

                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <%--<tr class="separator">--%>
                                                <tr class="separator" style='<%# GetRowStyle(Eval("inventory_quantity")) %>'>
                                                    <td><%# Eval("warehouse_name") %></td>
                                                    <td><%# Eval("ProductName") %></td>
                                                    <td><%# Eval("inventory_quantity") %></td>



                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>

        </div>

    </section>

    <asp:SqlDataSource ID="ds4"
        ConnectionString="Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;" runat="server"
        SelectCommand="select * from product_warehouse_status" />
</asp:Content>
