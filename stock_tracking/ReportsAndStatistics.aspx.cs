using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace stock_tracking
{
    public partial class ReportsAndStatistics : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void reportButton1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from total_sale_product", conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("TotalSaleProduct.rdlc");
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void reportButton2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from total_sales_by_category", conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet2", dataTable);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("TotalSalesByCategory.rdlc");
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void reportButton3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from monthly_sales_report", conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet3", dataTable);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("MonthlySalesReport.rdlc");
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void reportButton4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from annual_sales_summary", conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet4", dataTable);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AnnualSalesSummary.rdlc");
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }











    }
}