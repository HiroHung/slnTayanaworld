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
    public partial class WebForm14 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGridView();
            }
        }

        protected void gvYachts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvYachts.DataKeys[e.RowIndex].Value.ToString();
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM YachtsPhoto FROM YachtsPhoto INNER JOIN Yachts ON YachtsPhoto.YachtsID = @id;DELETE FROM Yachts WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }
        private void getGridView()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            // SqlDataAdapter 執行SQL命令抓取資料
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM Yachts ORDER BY YachtsName desc, Model", Conn);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            gvYachts.DataSource = dataTable;
            gvYachts.DataBind();
            //關閉SQL連接
            Conn.Close();
            Conn.Dispose();
        }

        protected void btnNewsInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("YachtsInsert.aspx");
        }

        protected void btnKeywordSearch_Click(object sender, EventArgs e)
        {
            string keyword = txbKeyword.Text;
            string sqlkeyword = $"SELECT Yachts.* FROM Yachts WHERE YachtsName+Model LIKE '%{keyword}%'";
            Session["keyword"] = sqlkeyword;
            Response.Redirect("YachtsSearch.aspx");
        }
    }
}