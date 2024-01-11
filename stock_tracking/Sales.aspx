<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="stock_tracking.WebForm1" %>

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





    <div class="row">
        <div class="col-md-6 col-12">
            <div class="input-group mb-3">
                <asp:TextBox ID="txtSearchSale" CssClass="form-control" placeholder="Client name or product name." runat="server"></asp:TextBox>
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
                        <h4 class="modal-title">Sales Form</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Client Name</label>
                        <asp:TextBox ID="txtClientName" CssClass="form-control" placeholder="Name" runat="server" />

                        <label>Client Last Name</label>
                        <asp:TextBox ID="txtClientLastName" CssClass="form-control" placeholder="Last Name" runat="server" />


                        <label>Warehouse Name</label>

                        <asp:DropDownList ID="dllWarehouseNameSale" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Warehouse Name</asp:ListItem>
                        </asp:DropDownList>


                        <label>Warehouse Location</label>
                        <asp:DropDownList ID="dllWarhouseLocationSale" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Warehouse Location</asp:ListItem>
                        </asp:DropDownList>

                        <label>Select Date</label>
                        <asp:TextBox ID="txtSaleDate" runat="server" CssClass="form-control" placeholder="Select Date" TextMode="Date"></asp:TextBox>


                        <label>Select Products</label>
                        <asp:DropDownList ID="dllSaleProduct" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">Select Product</asp:ListItem>
                        </asp:DropDownList>
                        <%--<div class="checkbox-list">
                            <asp:Repeater ID="rptrProducts" runat="server">
                                <ItemTemplate>
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox ID="chkProduct" runat="server" Text='<%# Container.DataItem %>' AutoPostBack="false" OnClientClick="handleCheckboxClick(this)" />
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control quantity-input" placeholder="Quantity" Text="" Style="display: none;" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                        <asp:Label ID="lblProductPrice" runat="server" CssClass="product-price" Text=""></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>


                            <label>Total Price</label>
                            <asp:TextBox ID="txtSaleProductsPrice" CssClass="form-control" placeholder="Total Price" runat="server" ReadOnly="true" />

                        </div>--%>

                        <label>Qunatity</label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Qunatity" Enabled="true"></asp:TextBox>

                        <label>Price</label>
                        <asp:TextBox ID="txtSalePrice" runat="server" CssClass="form-control" placeholder="Product Price" Enabled="false"></asp:TextBox>

                        <asp:HiddenField ID="hdid3" runat="server" />
                        <asp:HiddenField ID="hdSelectedClientId" runat="server" />



                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnSaveProductClientSale" CssClass="btn btn-primary" OnClick="btnSaveProductClientSale_Click" Text="Save" runat="server" />
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
                        <asp:Button Text="Make a Sale" ID="modal3" CssClass="btn btn-primary" OnClick="modal3_Click" runat="server" />

                    </div>
                    <div class="card-content">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 col-12">

                                    <table>
                                        <asp:Repeater ID="rptr3" DataSourceID="ds3" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th>Client ID</th>
                                                    <th>Client Name</th>
                                                    <th>Client Last Name</th>
                                                    <th>Product Name</th>
                                                    <th>Product Brand</th>
                                                    <th>Product Price</th>
                                                    <th>Qunatity</th>
                                                    <th>Sale Date</th>
                                                    <th>Warehouse Name</th>


                                                    <th>Action</th>

                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>



                                                <tr class="separator">
                                                    <td><%# Eval("client_id") %></td>
                                                    <td><%# Eval("client_name") %></td>
                                                    <td><%# Eval("client_last_name") %></td>
                                                    <td><%# Eval("product_name") %></td>
                                                    <td><%# Eval("brand") %></td>
                                                    <td><%# Eval("price") %>€</td>
                                                    <td><%# Eval("sale_quantity") %></td>

                                                    <td><%# ((DateTime)Eval("sale_date")).ToString("yyyy-MM-dd") %></td>

                                                    <td><%# Eval("warehouse_name") %></td>





                                                    <td>
                                                        <div class="btn-group" role="group">
                                                            <asp:LinkButton ID="btnUpdateProductClientSale" CommandName="Update" OnCommand="btnUpdateProductClientSale_Command" CommandArgument='<%#Eval("product_id") %>' CssClass="btn btn-sm btn-primary" runat="server">
                                                            <i class="fas fa-pencil-alt"></i> Update
                                                            </asp:LinkButton>


                                                            &nbsp;



                                                              
                                                                  <asp:HiddenField ID="hdSaleId" runat="server" Value='<%# Eval("sale_id") %>' />
                                                                  <!-- Other controls for the actions column... -->
                                                                  <asp:LinkButton ID="btnDeleteProductClientSale" CommandName="Delete" CommandArgument='<%#Eval("sale_id") %>'
                                                                      OnClientClick="return confirm('Are you sure you want to delete this?');"
                                                                      OnCommand="btnDeleteProductClientSale_Command" CssClass="btn btn-sm btn-danger" runat="server">
                                                                <i class="fas fa-trash"></i> Delete
                                                                  </asp:LinkButton>
                                                              

                                                            &nbsp;

                                                        <div class="btn-group" role="group">
                                                            <asp:LinkButton ID="btnMakeSale" CommandName="Update" OnCommand="btnMakeSale_Command" CommandArgument='<%#Eval("client_id") %>' CssClass="btn btn-sm btn-warning" runat="server">
                                                            <i class="fas fa-tag"></i> Sale
                                                            </asp:LinkButton>
                                                        </div>
                                                    </td>


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


    <asp:SqlDataSource ID="ds3"
        ConnectionString="Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;" runat="server"
        SelectCommand="select * from sale_view" />



</asp:Content>

