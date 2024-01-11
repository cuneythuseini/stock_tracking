using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace stock_tracking
{
    public partial class Client : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSaveClient_Click(object sender, EventArgs e)
        {
            conn.Open();

            string queryInsert = "INSERT INTO Client(name, last_name) VALUES (@name, @lastName)";
            string queryUpdate = "UPDATE Client SET name = @name, last_name = @lastName WHERE client_id = @id";

            SqlCommand cmd;
            if (!string.IsNullOrEmpty(hdid3.Value))
            {
                string id = hdid3.Value;
                cmd = new SqlCommand(queryUpdate, conn);
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                cmd = new SqlCommand(queryInsert, conn);
            }

            cmd.Parameters.AddWithValue("@name", txtClientName.Text);
            cmd.Parameters.AddWithValue("@lastName", txtClientLastName.Text);

            int rowsAffected = cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();

            rptr3.DataBind();
        }

        protected void modal3_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#modalClient').modal('show');", true);
        }

        protected void btnDeleteClient_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            conn.Open();

            string query = "DELETE FROM Client WHERE client_id = @id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            rptr3.DataBind();

            conn.Close();
        }

        protected void btnUpdateClient_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            hdid3.Value = id;

            string query = "SELECT * FROM Client WHERE client_id = @id";

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                txtClientName.Text = dataReader["name"].ToString();
                txtClientLastName.Text = dataReader["last_name"].ToString();
            }

            dataReader.Close();
            conn.Close();

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#modalClient').modal('show');", true);
        }

        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            
            string searchTerm = txtSearchClient.Text.Trim();

            
            string query = "SELECT * FROM Client WHERE name LIKE '%' + @SearchTerm + '%' OR last_name LIKE '%' + @SearchTerm + '%'";

            
            ds3.SelectCommand = query;
            ds3.SelectParameters.Clear();
            ds3.SelectParameters.Add("SearchTerm", searchTerm);

            
            rptr3.DataBind();
        }

    }
}
