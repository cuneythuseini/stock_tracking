<%@ Page Title="Reports and statistics" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportsAndStatistics.aspx.cs" Inherits="stock_tracking.ReportsAndStatistics" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Reports and Statistics</h2>

        <div class="row mt-4">
            <div class="col-md-3">
                <asp:Button ID="reportButton1" runat="server" Text="Total Sales by Product" CssClass="btn btn-warning btn-block mb-3" OnClick="reportButton1_Click" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="reportButton2" runat="server" Text="Total Sales by Category" CssClass="btn btn-warning btn-block mb-3" OnClick="reportButton2_Click" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="reportButton3" runat="server" Text="Monthly Sales Report" CssClass="btn btn-warning btn-block mb-3" OnClick="reportButton3_Click" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="reportButton4" runat="server" Text="Annual Sales Summary" CssClass="btn btn-warning btn-block mb-3" OnClick="reportButton4_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1214px"></rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
