<%@ Page Title="Client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="stock_tracking.Client" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div>


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
                        <asp:TextBox ID="txtSearchClient" CssClass="form-control" placeholder="Search Client" runat="server"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnSearchClient" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchClient_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>



            <div class="container">
                <div class="modal fade" id="modalClient" data-backdrop="false" role="dialog">
                    <div class=" modal-dialog modal-dailog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Add New Client</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <label>Client Name</label>
                                <asp:TextBox ID="txtClientName" CssClass="form-control" placeholder="Name" runat="server" />

                                <label>Client Last Name</label>
                                <asp:TextBox ID="txtClientLastName" CssClass="form-control" placeholder="Last Name" runat="server" />


                                <asp:HiddenField ID="hdid3" runat="server" />


                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                <asp:Button ID="btnSaveClient" CssClass="btn btn-primary" OnClick="btnSaveClient_Click" Text="Save" runat="server" />
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
                                <asp:Button Text="New Client" ID="modal3" CssClass="btn btn-primary" OnClick="modal3_Click" runat="server" />

                            </div>
                            <div class="card-content">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12 col-12">

                                            <table>
                                                <asp:Repeater ID="rptr3" DataSourceID="ds3" runat="server">
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th>ID</th>
                                                            <th>Client Name</th>
                                                            <th>Client Last Name</th>
                                                            <th>Action</th>

                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <tr class="separator">

                                                            <td><%# Eval("client_id") %></td>
                                                            <td><%# Eval("name") %></td>
                                                            <td><%# Eval("last_name") %></td>


                                                            <td>
                                                                <div class="btn-group" role="group">
                                                                    <asp:LinkButton ID="btnUpdateClient" CommandName="Update" OnCommand="btnUpdateClient_Command" CommandArgument='<%#Eval("client_id") %>' CssClass="btn btn-sm btn-primary" runat="server">
                                                            <i class="fas fa-pencil-alt"></i> Update
                                                                    </asp:LinkButton>

                                                                    <!-- Add some space between the buttons -->
                                                                    &nbsp;

                                                        <asp:LinkButton CommandName="Delete" ID="btnDeleteClient" CommandArgument='<%#Eval("client_id") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this?');"
                                                            OnCommand="btnDeleteClient_Command" CssClass="btn btn-sm btn-danger" runat="server">
                                                            <i class="fas fa-trash"></i> Delete
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
                SelectCommand="select * from Client" />



        </div>

</asp:Content>
