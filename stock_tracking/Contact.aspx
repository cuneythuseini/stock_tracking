<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="stock_tracking.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
   
   <%-- <style>
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


    <div class="row">
        <div class="col-md-6 col-12">
            <div class="input-group mb-3">
                <asp:TextBox ID="txtSearchSale" CssClass="form-control" placeholder="Search Sale" runat="server"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearchSale" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchSale_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>



    <div class="container">
        <div class="modal fade" id="modalSale" data-backdrop="false" role="dialog">
            <div class=" modal-dialog modal-dailog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Make a Sale</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Product ID</label>
                        <asp:TextBox ID="txtProductID" CssClass="form-control" placeholder="ID" runat="server" />

                        <label>Product Brand</label>
                        <asp:TextBox ID="txtProductBrand" CssClass="form-control" placeholder="Brand" runat="server" />


                        <label>Category</label>
                        <asp:DropDownList ID="dllCategory" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Category</asp:ListItem>
                        </asp:DropDownList>

                        <label>Warehouse Name</label>

                        <asp:DropDownList ID="dllWarehouseName" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Warehouse Name</asp:ListItem>
                        </asp:DropDownList>


                        <label>Warehouse Location</label>
                        <asp:DropDownList ID="dllWarhouseLocation" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Warehouse Location</asp:ListItem>
                        </asp:DropDownList>


                        <label>Quantity</label>
                        <asp:TextBox ID="txtQuantityWarehouse" CssClass="form-control" placeholder="Warehouse Quantity" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regexValidatorQuantity" runat="server" ErrorMessage="Please enter a numerical value." ControlToValidate="txtQuantityWarehouse"
                            ValidationExpression="^\d+$" Display="Dynamic" CssClass="text-danger" />

                        <label>Price</label>
                        <asp:TextBox ID="txtProductPrice" CssClass="form-control" placeholder="Product Price" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a numerical value." ControlToValidate="txtQuantityWarehouse"
                            ValidationExpression="^\d+$" Display="Dynamic" CssClass="text-danger" />





                        <asp:HiddenField ID="hdid2" runat="server" />


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSaveProduct" CssClass="btn btn-primary" OnClick="btnSaveProduct_Click" Text="Save" runat="server" />
                    </div>
                </div>


            </div>

        </div>
    </div>






    <section id="section">
        <div class="row match-height">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <asp:Button Text="New Product" ID="modal2" CssClass="btn btn-primary" OnClick="modal2_Click" runat="server" />

                    </div>
                    <div class="card-content">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 col-12">

                                    <table>
                                        <asp:Repeater ID="rptr2" DataSourceID="ds2" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <%--<th>ID</th>--%>
                                                    <th>Product Name</th>
                                                    <th>Product Brand</th>
                                                    <th>Category Name</th>
                                                    <th>Warehouse Name</th>
                                                    <th>Warehouse Location</th>
                                                    <th>Quantity</th>
                                                    <th>Product Price</th>
                                                    <th>Action</th>

                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr class="separator">
                                                    <%--<td><%# Eval("category_id") %></td>--%>
                                                    <td><%# Eval("product_name") %></td>
                                                    <td><%# Eval("product_brand") %></td>
                                                    <td><%# Eval("category_name") %></td>
                                                    <td><%# Eval("warehouse_name") %></td>
                                                    <td><%# Eval("warehouse_location") %></td>
                                                    <td><%# Eval("warehouse_quantity") %></td>
                                                    <td><%# Eval("product_price") %>€</td>


                                                    <td>
                                                        <div class="btn-group" role="group">
                                                            <asp:LinkButton ID="btnUpdateProduct" CommandName="Update" OnCommand="btnUpdateProduct_Command" CommandArgument='<%#Eval("product_id") %>' CssClass="btn btn-sm btn-primary" runat="server">
                                                            <i class="fas fa-pencil-alt"></i> Update
                                                            </asp:LinkButton>

                                                            <!-- Add some space between the buttons -->
                                                            &nbsp;

                                                        <asp:LinkButton CommandName="Delete" ID="btnDeleteProduct" CommandArgument='<%#Eval("product_id") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this?');"
                                                            OnCommand="btnDeleteProduct_Command" CssClass="btn btn-sm btn-danger" runat="server">
                                                            <i class="fas fa-trash"></i> Delete
                                                        </asp:LinkButton>
                                                        </div>
                                                    </td>


                                                    <%--<td>
                                                        <asp:LinkButton ID="btnUpdateProduct" CommandName="Update" OnCommand="btnUpdateProduct_Command" CommandArgument='<%#Eval("product_id") %>' CssClass="btn btn-sm btn-primary" runat="server">
                                                            <i class="fas fa-pencil-alt"></i> Update

                                                            
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CommandName="Delete" ID="btnDeleteProoduct" CommandArgument='<%#Eval("product_id") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this?');"
                                                            OnCommand="btnDeleteProduct_Command" CssClass="btn btn-sm btn-danger" runat="server">
                                                             <i class="fas fa-trash"></i> Delete
                                                        </asp:LinkButton>
                                                    </td>--%>
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

    
    <asp:SqlDataSource ID="ds2"
        ConnectionString="Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;" runat="server"
        SelectCommand="select * from warehouse_product_view" />--%>

</asp:Content>
