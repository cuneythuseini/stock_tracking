using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace stock_tracking
{
    public partial class _Default : Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-48NGVPU\SQLEXPRESS01;Initial Catalog=stock_db;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query_insert = "insert into Category(name) values (@name)";


            SqlCommand cmd;
            if (!string.IsNullOrEmpty(hdid.Value))
            {
                string id = hdid.Value;
                string query_update = "update Category set name=@name where category_id = @id";
                cmd = new SqlCommand(query_update, conn);
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                cmd = new SqlCommand(query_insert, conn);

            }
            cmd.Parameters.AddWithValue("@name", txtname.Text);

            int rowsAffected = cmd.ExecuteNonQuery();


            conn.Close();
            conn.Dispose();
            Response.Redirect("Default.Aspx");

        }

        protected void modal_Click(object sender, EventArgs e)
        {
            string script = "$('#mymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        //protected void btndlt_Command(object sender, CommandEventArgs e)
        //{
        //    string id = e.CommandArgument.ToString();

        //    conn.Open();

        //    string query = "delete from Category where category_id=@id";


        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    cmd.Parameters.AddWithValue("@id", id);
        //    cmd.ExecuteNonQuery();


        //    rptr1.DataBind();
        //}

        //protected void btndlt_Command(object sender, CommandEventArgs e)
        //{
        //    string id = e.CommandArgument.ToString();

        //    conn.Open();


        //    string updateCategoryQuery = "UPDATE Category SET IsDeleted = 1 WHERE category_id = @id";
        //    SqlCommand updateCategoryCmd = new SqlCommand(updateCategoryQuery, conn);
        //    updateCategoryCmd.Parameters.AddWithValue("@id", id);
        //    updateCategoryCmd.ExecuteNonQuery();

        //    conn.Close();

        //    rptr1.DataBind();
        //}
        private const int DefaultCategoryId = 8;

        protected void btndlt_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            conn.Open();

            try
            {
                
                string updateCategoryQuery = "UPDATE Category SET IsDeleted = 1 WHERE category_id = @id";
                SqlCommand updateCategoryCmd = new SqlCommand(updateCategoryQuery, conn);
                updateCategoryCmd.Parameters.AddWithValue("@id", id);
                updateCategoryCmd.ExecuteNonQuery();

               
                string moveProductsToDefaultCategoryQuery = "UPDATE Product SET category_id = @defaultCategoryId WHERE category_id = @id";
                SqlCommand moveProductsCmd = new SqlCommand(moveProductsToDefaultCategoryQuery, conn);
                moveProductsCmd.Parameters.AddWithValue("@defaultCategoryId", DefaultCategoryId);
                moveProductsCmd.Parameters.AddWithValue("@id", id);
                moveProductsCmd.ExecuteNonQuery();

             
                rptr1.DataBind();
            }
            catch (Exception ex)
            {
             
                throw ex;
            }
            finally
            {
                
                conn.Close();
            }
        }




        protected void btnupdate_Command(object sender, CommandEventArgs e)
        {

            string id = e.CommandArgument.ToString();
            hdid.Value = id;

            string query = "select * from Category where category_id=@id";

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                txtname.Text = dataReader["name"].ToString();

            }
            dataReader.Close();

            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#mymodal').modal('show');", true);

        }


    }
}