using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace stock_tracking
{
    public partial class About : Page
    {

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;");

        private void BindCategoryDropdown()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

                string query = "SELECT DISTINCT name FROM Category";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dllCategory.DataSource = dataTable;
                        dllCategory.DataTextField = "name";
                        dllCategory.DataValueField = "name";
                        dllCategory.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



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

                        dllWarehouseName.DataSource = dataTable;
                        dllWarehouseName.DataTextField = "name";
                        dllWarehouseName.DataValueField = "name";
                        dllWarehouseName.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }


        private void BindWareHouseLocationDropDown()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

                string query = "SELECT DISTINCT location FROM Warehouse";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dllWarhouseLocation.DataSource = dataTable;
                        dllWarhouseLocation.DataTextField = "location";
                        dllWarhouseLocation.DataValueField = "location";
                        dllWarhouseLocation.DataBind();
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
                BindCategoryDropdown();
                BindWareHouseNameDropDown();
                BindWareHouseLocationDropDown();
            }
        }



        protected void modal_Click(object sender, EventArgs e)
        {

        }

        private int GetWarehouseId(SqlConnection conn, string warehouseName)
        {
            
            string query = "SELECT warehouse_id FROM Warehouse WHERE name = @warehouseName";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@warehouseName", warehouseName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            return 0; 
        }

        protected void modal2_Click(object sender, EventArgs e)
        {
            string script = "$('#modalProduct').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        private int InsertOrUpdateProduct(SqlConnection conn, string productName, string productBrand, int categoryId, decimal price)
        {
            string query_insert = "INSERT INTO Product (name, brand, category_id, price) VALUES (@name, @brand, @category_id, @price); SELECT SCOPE_IDENTITY();";
            string query_update = "UPDATE Product SET name=@name, brand=@brand, category_id=@category_id, price=@price WHERE product_id=@id";

            using (SqlCommand cmd = new SqlCommand(!string.IsNullOrEmpty(hdid2.Value) ? query_update : query_insert, conn))
            {
                if (!string.IsNullOrEmpty(hdid2.Value))
                {
                    cmd.Parameters.AddWithValue("@id", hdid2.Value);
                }

                cmd.Parameters.AddWithValue("@name", productName);
                cmd.Parameters.AddWithValue("@brand", productBrand);
                cmd.Parameters.AddWithValue("@category_id", categoryId);
                cmd.Parameters.AddWithValue("@price", price);

                
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int productId))
                {
                    return productId;
                }
            }

            return 0;
        }

        private void InsertOrUpdateWarehouse(SqlConnection conn, int warehouseId, int productId, int quantity)
        {
            string queryCheckExistence = "SELECT COUNT(*) FROM the_product_is_in_warehouse WHERE warehouse_id=@warehouse_id AND product_id=@product_id";
            string queryInsert = "INSERT INTO the_product_is_in_warehouse (warehouse_id, product_id, quantity) VALUES (@warehouse_id, @product_id, @quantity)";
            string queryUpdate = "UPDATE the_product_is_in_warehouse SET quantity=@quantity WHERE warehouse_id=@warehouse_id AND product_id=@product_id";

            using (SqlCommand cmdCheckExistence = new SqlCommand(queryCheckExistence, conn))
            {
                cmdCheckExistence.Parameters.AddWithValue("@warehouse_id", warehouseId);
                cmdCheckExistence.Parameters.AddWithValue("@product_id", productId);

                int existingRecords = (int)cmdCheckExistence.ExecuteScalar();

                using (SqlCommand cmd = new SqlCommand(existingRecords > 0 ? queryUpdate : queryInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@warehouse_id", warehouseId);
                    cmd.Parameters.AddWithValue("@product_id", productId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);

                    cmd.ExecuteNonQuery();
                }
            }
        }








        private int GetCategoryId(SqlConnection conn, string categoryName)
        {
            string query = "SELECT category_id FROM Category WHERE name = @categoryName";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@categoryName", categoryName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            return 0; 
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    
                    int categoryId = GetCategoryId(conn, dllCategory.SelectedValue);
                    int warehouseId = GetWarehouseId(conn, dllWarehouseName.SelectedValue);

                    string productName = txtProductName.Text;
                    string productBrand = txtProductBrand.Text;
                    decimal productPrice = Convert.ToDecimal(txtProductPrice.Text);
                    int quantity = Convert.ToInt32(txtQuantityWarehouse.Text);

                    int productId = InsertOrUpdateProduct(conn, productName, productBrand, categoryId, productPrice);

                    
                    if (productId > 0)
                    {
                        InsertOrUpdateWarehouse(conn, warehouseId, productId, quantity);
                    }
                }

                
                Response.Redirect("About.aspx");
            }
            catch (Exception ex)
            {
                
            }
        }





        protected void btnUpdateProduct_Command(object sender, CommandEventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";

            try
            {
                string productId = e.CommandArgument.ToString();
                hdid2.Value = productId;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    
                    string queryProduct = "SELECT name, brand, category_id, price FROM Product WHERE product_id = @productId";
                    using (SqlCommand cmdProduct = new SqlCommand(queryProduct, conn))
                    {
                        cmdProduct.Parameters.AddWithValue("@productId", productId);
                        SqlDataReader dataReader = cmdProduct.ExecuteReader();

                        if (dataReader.Read())
                        {
                            txtProductName.Text = dataReader["name"].ToString();
                            txtProductBrand.Text = dataReader["brand"].ToString();
                            txtProductPrice.Text = dataReader["price"].ToString();
                            int categoryId = Convert.ToInt32(dataReader["category_id"]);
                            dataReader.Close();

                            
                            string queryCategory = "SELECT name FROM Category WHERE category_id = @categoryId";
                            using (SqlCommand cmdCategory = new SqlCommand(queryCategory, conn))
                            {
                                cmdCategory.Parameters.AddWithValue("@categoryId", categoryId);
                                object categoryName = cmdCategory.ExecuteScalar();
                                if (categoryName != null)
                                {
                                    dllCategory.SelectedValue = categoryName.ToString();
                                }
                            }

                            
                            string queryWarehouse = "SELECT wp.warehouse_id, wp.quantity, w.name AS warehouse_name, w.location AS warehouse_location " +
                                                    "FROM the_product_is_in_warehouse wp " +
                                                    "INNER JOIN Warehouse w ON wp.warehouse_id = w.warehouse_id " +
                                                    "WHERE wp.product_id = @productId";
                            using (SqlCommand cmdWarehouse = new SqlCommand(queryWarehouse, conn))
                            {
                                cmdWarehouse.Parameters.AddWithValue("@productId", productId);
                                SqlDataReader warehouseReader = cmdWarehouse.ExecuteReader();

                                if (warehouseReader.Read())
                                {
                                   
                                    txtQuantityWarehouse.Text = warehouseReader["quantity"].ToString();
                                    dllWarehouseName.SelectedValue = warehouseReader["warehouse_name"].ToString();
                                    dllWarhouseLocation.SelectedValue = warehouseReader["warehouse_location"].ToString();
                                }
                                else
                                {
                                    
                                    txtQuantityWarehouse.Text = "";
                                    dllWarehouseName.SelectedIndex = -1;  
                                    dllWarhouseLocation.SelectedIndex = -1;
                                }

                                warehouseReader.Close();
                            }
                        }
                    }

                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#modalProduct').modal('show');", true);
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        protected void btnDeleteProduct_Command(object sender, CommandEventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";
            try
            {
                string id = e.CommandArgument.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString)) ;
                {
                    conn.Open();

                    
                    string query_delete_warehouse = "DELETE FROM the_product_is_in_warehouse WHERE product_id=@id";

                    using (SqlCommand cmd_delete_warehouse = new SqlCommand(query_delete_warehouse, conn))
                    {
                        cmd_delete_warehouse.Parameters.AddWithValue("@id", id);
                        cmd_delete_warehouse.ExecuteNonQuery();
                    }

                   
                    string query_delete_product = "DELETE FROM Product WHERE product_id=@id";

                    using (SqlCommand cmd_delete_product = new SqlCommand(query_delete_product, conn))
                    {
                        cmd_delete_product.Parameters.AddWithValue("@id", id);
                        cmd_delete_product.ExecuteNonQuery();
                    }

                    rptr2.DataBind();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        private string GetCategoryName(SqlConnection conn, int categoryId)
        {
            string query = "SELECT name FROM Category WHERE category_id = @categoryId";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@categoryId", categoryId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["name"].ToString();
                    }
                }
            }

            return string.Empty;
        }

        private string GetWarehouseName(SqlConnection conn, int warehouseId)
        {
            string query = "SELECT name FROM Warehouse WHERE warehouse_id = @warehouseId";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@warehouseId", warehouseId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["name"].ToString();
                    }
                }
            }

            return string.Empty;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            string searchTerm = txtSearch.Text.Trim();

            
            string query = "SELECT * FROM warehouse_product_view WHERE product_name LIKE '%' + @SearchTerm + '%' OR product_brand LIKE '%' + @SearchTerm + '%' OR category_name LIKE '%' + @SearchTerm + '%'";




            
            ds2.SelectCommand = query;
            ds2.SelectParameters.Clear();
            ds2.SelectParameters.Add("SearchTerm", searchTerm);

            
            rptr2.DataBind();
        }



    }
}