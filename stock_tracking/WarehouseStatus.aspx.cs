using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace stock_tracking
{
    public partial class WarehouseStatus : System.Web.UI.Page
    {
        private void BindWareHouseNameDropDown()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

                string query = "SELECT DISTINCT name FROM Warehouse";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        ddlWarehouseName.DataSource = dataTable;
                        ddlWarehouseName.DataTextField = "name";
                        ddlWarehouseName.DataValueField = "name";
                        ddlWarehouseName.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        private void BindCategoryDropdown()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

                string query = "SELECT DISTINCT name FROM Category where isDeleted = 0";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        ddlCategory.DataSource = dataTable;
                        ddlCategory.DataTextField = "name";
                        ddlCategory.DataValueField = "name";
                        ddlCategory.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWareHouseNameDropDown();
                BindCategoryDropdown();

                
                ListItem warehouseItem = ddlWarehouseName.Items.FindByText("Select Warehouse Name");
                if (warehouseItem == null)
                {
                    
                    ddlWarehouseName.Items.Insert(0, new ListItem("Select Warehouse Name", ""));
                }

                ListItem categoryItem = ddlCategory.Items.FindByText("Select Category Name");
                if (categoryItem == null)
                {
                    
                    ddlCategory.Items.Insert(0, new ListItem("Select Category Name", ""));
                }

                
                ddlWarehouseName.Items.FindByText("Select Warehouse Name").Selected = true;
                ddlCategory.Items.FindByText("Select Category Name").Selected = true;
            }
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                
                string selectedWarehouse = ddlWarehouseName.SelectedValue;
                string selectedCategory = ddlCategory.SelectedValue;
                bool showOutOfStock = CheckBox1.Checked;

                
                string selectQuery = "SELECT * FROM product_warehouse_status WHERE 1=1";

                if (!string.IsNullOrEmpty(selectedWarehouse))
                {
                    selectQuery += $" AND warehouse_name = '{selectedWarehouse}'";

                    
                    if (!string.IsNullOrEmpty(selectedCategory))
                    {
                        selectQuery += $" AND categoryName = '{selectedCategory}'";
                    }
                }
                else if (!string.IsNullOrEmpty(selectedCategory))
                {
                    
                    selectQuery += $" AND categoryName = '{selectedCategory}'";
                }

                if (showOutOfStock)
                {
                    selectQuery += " AND inventory_quantity = 0";
                }

                
                ds4.SelectCommand = selectQuery;

                
                rptr4.DataBind();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                
            }
        }
        protected string GetRowStyle(object inventoryQuantity)
        {
            int quantity = Convert.ToInt32(inventoryQuantity);

            return quantity == 0 ? "background-color: #ffdddd;" : "";
        }




    }
}