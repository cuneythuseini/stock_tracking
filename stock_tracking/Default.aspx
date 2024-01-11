<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="stock_tracking._Default" %>

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



    <div class="container">
        <div class="modal fade" id="mymodal" data-backdrop="false" role="dialog">
            <div class=" modal-dialog modal-dailog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Add New Category</h4>
                        
                        <asp:Label ID="lblmsg" Text="" runat="server" />
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <label>Name</label>
                        <asp:TextBox ID="txtname" CssClass="form-control" placeholder="Name" runat="server" />
                        <asp:HiddenField ID="hdid" runat="server" />

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnsave" CssClass="btn btn-primary" OnClick="btnsave_Click" Text="Save" runat="server" />
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
                        <asp:Button Text="New Category" ID="modal" CssClass="btn btn-primary" OnClick="modal_Click" runat="server" />

                    </div>
                    <div class="card-content">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 col-12">

                                    <table>
                                        <asp:Repeater ID="rptr1" DataSourceID="ds1" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <%--<th>ID</th>--%>
                                                    <th>Category Name</th>
                                                    <th>Action</th>

                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr class="separator">
                                                    <%--<td><%# Eval("category_id") %></td>--%>
                                                    <td><%# Eval("Name") %></td>



                                                    <td>
                                                        <asp:LinkButton ID="btnupdate" CommandName="Update" OnCommand="btnupdate_Command" CommandArgument='<%#Eval("category_id") %>' CssClass="btn btn-sm btn-primary" runat="server" OnClientClick="updateCategory()">
                                                            <i class="fas fa-pencil-alt"></i> Update

                                                            
                                                        </asp:LinkButton>

                                                        <asp:LinkButton CommandName="Delete" ID="btndlt" CommandArgument='<%#Eval("category_id") %>'
                                                            OnClientClick="return confirm('Are you sure you want to delete this?');"
                                                            OnCommand="btndlt_Command" CssClass="btn btn-sm btn-danger" runat="server">
                                                             <i class="fas fa-trash"></i> Delete
                                                        </asp:LinkButton>
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
    <asp:SqlDataSource ID="ds1"
        ConnectionString="Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;" runat="server"
        SelectCommand="select * from Category where isDeleted = 0 and category_id != 8" />





</asp:Content>
