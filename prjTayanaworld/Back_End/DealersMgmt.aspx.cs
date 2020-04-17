using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGridView();
            }
        }

        protected void btnInsertContinent_Click(object sender, EventArgs e)
        {
            insertContinent();
        }
        private void getGridView()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            // SqlDataAdapter 執行SQL命令抓取資料
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM DealersContinent", Conn);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            gvContinent.DataSource = dataTable;
            gvContinent.DataBind();
            //關閉SQL連接
            Conn.Close();
        }

        private void insertContinent()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("INSERT INTO DealersContinent (Continent) VALUES (@Continent)", Conn);
            Cmd.Parameters.AddWithValue("@Continent",txbInsertContinent.Text);
            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();

            Response.Redirect(this.Request.Url.ToString());
        }

        protected void gvContinent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvContinent.DataKeys[e.RowIndex].Value.ToString();
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM DealersDetails FROM DealersContinent INNER JOIN DealersDetails ON @id = DealersDetails.ContinentID", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.ExecuteNonQuery();
            Cmd.Cancel();

            SqlCommand Cmd2 = new SqlCommand("DELETE FROM DealersContinent WHERE id = @id", Conn);
            Cmd2.Parameters.AddWithValue("@id", id);
            Cmd2.ExecuteNonQuery();
            Cmd2.Cancel();

            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }
    }
}