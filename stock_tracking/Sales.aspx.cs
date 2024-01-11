using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Globalization;

namespace stock_tracking
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = "Data Source=DESKTOP-48NGVPU\\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;";
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

                        dllWarehouseNameSale.DataSource = dataTable;
                        dllWarehouseNameSale.DataTextField = "name";
                        dllWarehouseNameSale.DataValueField = "name";
                        dllWarehouseNameSale.DataBind();
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

                        dllWarhouseLocationSale.DataSource = dataTable;
                        dllWarhouseLocationSale.DataTextField = "location";
                        dllWarhouseLocationSale.DataValueField = "location";
                        dllWarhouseLocationSale.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BindProductsDropDown()
        {
            try
            {
                string query = "SELECT product_id, name FROM Product";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dllSaleProduct.DataSource = dataTable;
                        dllSaleProduct.DataTextField = "name";
                        dllSaleProduct.DataValueField = "product_id";
                        dllSaleProduct.DataBind();
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
                BindWareHouseLocationDropDown();
                BindProductsDropDown();
            }

        }



        protected void modal3_Click(object sender, EventArgs e)
        {

            string script = "$('#modalSale').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        protected void btnSearchSale_Click(object sender, EventArgs e)
        {
            try
            {

                string searchTerm = txtSearchSale.Text.Trim();

                string query = "SELECT * FROM sale_view WHERE client_name LIKE '%' + @SearchTerm + '%' OR product_name LIKE '%' + @SearchTerm + '%'";


                ds3.SelectCommand = query;
                ds3.SelectParameters.Clear();
                ds3.SelectParameters.Add("SearchTerm", searchTerm);

                rptr3.DataBind();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



        protected void btnSaveProductClientSale_Click(object sender, EventArgs e)
        {
            try
            {
                string clientName = txtClientName.Text;
                string clientLastName = txtClientLastName.Text;
                string warehouseName = dllWarehouseNameSale.SelectedValue;
                string warehouseLocation = dllWarhouseLocationSale.SelectedValue;
                DateTime saleDate = DateTime.Parse(txtSaleDate.Text);
                int productId = int.Parse(dllSaleProduct.SelectedValue);
                int quantity = int.Parse(txtQuantity.Text);

                decimal productPrice = GetProductPrice(productId);
                decimal totalPrice = quantity * productPrice;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int clientId = InsertClient(clientName, clientLastName, connection);

                    string insertSaleQuery = "INSERT INTO Sale (quantity, price, sale_date, client_id, warehouse_id, product_id) " +
                                             "VALUES (@quantity, @price, @saleDate, @clientId, @warehouseId, @productId);" +
                                             "SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(insertSaleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@price", totalPrice);
                        command.Parameters.AddWithValue("@saleDate", saleDate);
                        command.Parameters.AddWithValue("@clientId", clientId);
                        command.Parameters.AddWithValue("@warehouseId", GetWarehouseId(warehouseName, warehouseLocation, connection));
                        command.Parameters.AddWithValue("@productId", productId);

                        int saleId = Convert.ToInt32(command.ExecuteScalar());

                        string insertRealizedSaleQuery = "INSERT INTO realized_sale (product_id, sale_id, sale_quantity) " +
                                                         "VALUES (@productId, @saleId, @quantity)";

                        using (SqlCommand realizedSaleCommand = new SqlCommand(insertRealizedSaleQuery, connection))
                        {
                            realizedSaleCommand.Parameters.AddWithValue("@productId", productId);
                            realizedSaleCommand.Parameters.AddWithValue("@saleId", saleId);
                            realizedSaleCommand.Parameters.AddWithValue("@quantity", quantity);

                            realizedSaleCommand.ExecuteNonQuery();
                        }

                        
                        UpdateWarehouseQuantity(productId, GetWarehouseId(warehouseName, warehouseLocation, connection), quantity, connection);
                    }
                }

                BindSaleData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateWarehouseQuantity(int productId, int warehouseId, int soldQuantity, SqlConnection connection)
        {
            try
            {
                string checkAvailabilityQuery = "SELECT quantity FROM the_product_is_in_warehouse " +
                                                "WHERE product_id = @productId AND warehouse_id = @warehouseId";

                using (SqlCommand checkAvailabilityCommand = new SqlCommand(checkAvailabilityQuery, connection))
                {
                    checkAvailabilityCommand.Parameters.AddWithValue("@productId", productId);
                    checkAvailabilityCommand.Parameters.AddWithValue("@warehouseId", warehouseId);

                    int availableQuantity = Convert.ToInt32(checkAvailabilityCommand.ExecuteScalar());

                    if (availableQuantity > 0 && availableQuantity >= soldQuantity)
                    {
                        string updateWarehouseQuantityQuery = "UPDATE the_product_is_in_warehouse " +
                                                              "SET quantity = quantity - @soldQuantity " +
                                                              "WHERE product_id = @productId AND warehouse_id = @warehouseId";

                        using (SqlCommand updateWarehouseQuantityCommand = new SqlCommand(updateWarehouseQuantityQuery, connection))
                        {
                            updateWarehouseQuantityCommand.Parameters.AddWithValue("@soldQuantity", soldQuantity);
                            updateWarehouseQuantityCommand.Parameters.AddWithValue("@productId", productId);
                            updateWarehouseQuantityCommand.Parameters.AddWithValue("@warehouseId", warehouseId);

                            updateWarehouseQuantityCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                       
                        string alertScript = "alert('Insufficient quantity available in the warehouse.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "InsufficientQuantityAlert", alertScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private int InsertClient(string clientName, string clientLastName, SqlConnection connection)
        {
            int clientId = 0;

            string selectClientQuery = "SELECT client_id FROM Client WHERE name = @clientName AND last_name = @clientLastName";
            using (SqlCommand selectClientCommand = new SqlCommand(selectClientQuery, connection))
            {
                selectClientCommand.Parameters.AddWithValue("@clientName", clientName);
                selectClientCommand.Parameters.AddWithValue("@clientLastName", clientLastName);

                object result = selectClientCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    clientId = Convert.ToInt32(result);
                }
                else
                {
                    string insertClientQuery = "INSERT INTO Client (name, last_name) VALUES (@clientName, @clientLastName);" +
                                               "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand insertClientCommand = new SqlCommand(insertClientQuery, connection))
                    {
                        insertClientCommand.Parameters.AddWithValue("@clientName", clientName);
                        insertClientCommand.Parameters.AddWithValue("@clientLastName", clientLastName);

                        object newClientId = insertClientCommand.ExecuteScalar();
                        if (newClientId != null && newClientId != DBNull.Value)
                        {
                            clientId = Convert.ToInt32(newClientId);
                        }
                    }
                }
            }

            return clientId;
        }

        private int GetWarehouseId(string warehouseName, string warehouseLocation, SqlConnection connection)
        {
            int warehouseId = 0;

            string selectWarehouseQuery = "SELECT warehouse_id FROM Warehouse WHERE name = @warehouseName AND location = @warehouseLocation";
            using (SqlCommand selectWarehouseCommand = new SqlCommand(selectWarehouseQuery, connection))
            {
                selectWarehouseCommand.Parameters.AddWithValue("@warehouseName", warehouseName);
                selectWarehouseCommand.Parameters.AddWithValue("@warehouseLocation", warehouseLocation);

                object result = selectWarehouseCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    warehouseId = Convert.ToInt32(result);
                }
                else
                {

                }
            }

            return warehouseId;
        }

        private decimal GetProductPrice(int productId)
        {
            decimal productPrice = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT price FROM Product WHERE product_id = @productId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        productPrice = Convert.ToDecimal(result);
                    }
                }
            }

            return productPrice;
        }


        //protected void btnDeleteProductClientSale_Command(object sender, CommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "Delete")
        //        {
        //            int saleId = Convert.ToInt32(e.CommandArgument);

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();


        //                int clientId = GetClientIdBySaleId(saleId, connection);

        //                string deleteRealizedSaleQuery = "DELETE FROM realized_sale WHERE sale_id = @saleId";
        //                using (SqlCommand deleteRealizedSaleCommand = new SqlCommand(deleteRealizedSaleQuery, connection))
        //                {
        //                    deleteRealizedSaleCommand.Parameters.AddWithValue("@saleId", saleId);
        //                    deleteRealizedSaleCommand.ExecuteNonQuery();
        //                }


        //                string deleteSaleQuery = "DELETE FROM Sale WHERE sale_id = @saleId";
        //                using (SqlCommand deleteSaleCommand = new SqlCommand(deleteSaleQuery, connection))
        //                {
        //                    deleteSaleCommand.Parameters.AddWithValue("@saleId", saleId);
        //                    deleteSaleCommand.ExecuteNonQuery();
        //                }


        //                BindSaleData();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

      
        protected void btnDeleteProductClientSale_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    int saleId = Convert.ToInt32(e.CommandArgument);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateIsDeletedQuery = "UPDATE Sale SET isDeleted = 1 WHERE sale_id = @saleId";

                        using (SqlCommand command = new SqlCommand(updateIsDeletedQuery, connection))
                        {
                            command.Parameters.AddWithValue("@saleId", saleId);
                            command.ExecuteNonQuery();
                        }
                    }

                    BindSaleData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private int GetClientIdBySaleId(int saleId, SqlConnection connection)
        {
            int clientId = 0;

            string selectClientIdQuery = "SELECT client_id FROM Sale WHERE sale_id = @saleId";
            using (SqlCommand selectClientIdCommand = new SqlCommand(selectClientIdQuery, connection))
            {
                selectClientIdCommand.Parameters.AddWithValue("@saleId", saleId);

                object result = selectClientIdCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    clientId = Convert.ToInt32(result);
                }
            }

            return clientId;
        }





        private int GetClientId(string clientName, string clientLastName, SqlConnection connection)
        {
            int clientId = 0;

            string selectClientQuery = "SELECT client_id FROM Client WHERE name = @clientName AND last_name = @clientLastName";
            using (SqlCommand selectClientCommand = new SqlCommand(selectClientQuery, connection))
            {
                selectClientCommand.Parameters.AddWithValue("@clientName", clientName);
                selectClientCommand.Parameters.AddWithValue("@clientLastName", clientLastName);

                object result = selectClientCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    clientId = Convert.ToInt32(result);
                }
            }

            return clientId;
        }

        protected void btnUpdateProductClientSale_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string clientId = e.CommandArgument.ToString();
                hdid3.Value = clientId;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryProductClientSale = "SELECT C.name AS client_name, C.last_name AS client_last_name, " +
                                                    "W.name AS warehouse_name, W.location AS warehouse_location, " +
                                                    "S.sale_date, P.name AS product_name, P.brand, " +
                                                    "S.quantity AS sale_quantity, S.price " +
                                                    "FROM sale_view AS S " +
                                                    "JOIN Client AS C ON S.client_id = C.client_id " +
                                                    "JOIN Warehouse AS W ON S.warehouse_id = W.warehouse_id " +
                                                    "JOIN Product AS P ON S.product_id = P.product_id " +
                                                    "WHERE S.client_id = @clientId";
                    using (SqlCommand cmdProductClientSale = new SqlCommand(queryProductClientSale, conn))
                    {
                        cmdProductClientSale.Parameters.AddWithValue("@clientId", clientId);
                        SqlDataReader productClientSaleReader = cmdProductClientSale.ExecuteReader();

                        if (productClientSaleReader.Read())
                        {
                            txtClientName.Text = productClientSaleReader["client_name"].ToString();
                            txtClientLastName.Text = productClientSaleReader["client_last_name"].ToString();
                            dllWarehouseNameSale.SelectedValue = productClientSaleReader["warehouse_name"].ToString();
                            dllWarhouseLocationSale.SelectedValue = productClientSaleReader["warehouse_location"].ToString();
                            txtSaleDate.Text = ((DateTime)productClientSaleReader["sale_date"]).ToString("yyyy-MM-dd");
                            dllSaleProduct.SelectedValue = productClientSaleReader["product_name"].ToString();
                            txtQuantity.Text = productClientSaleReader["sale_quantity"].ToString();
                            txtSalePrice.Text = productClientSaleReader["price"].ToString();
                        }

                        productClientSaleReader.Close();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#modalSale').modal('show');", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        private void BindSaleData()
        {
            ds3.DataBind();
            rptr3.DataBind();
        }



        protected void btnMakeSale_Command(object sender, CommandEventArgs e)
        {

        }

    }
}