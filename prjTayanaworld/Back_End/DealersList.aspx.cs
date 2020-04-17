using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                getGridView();
                getContinentName();
                Response.Cookies["id"].Value = Server.HtmlEncode(Request.QueryString["id"].ToString());
                showData();
                dataListCountCS();
            }
        }
        private void getGridView()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            // SqlDataAdapter 執行SQL命令抓取資料
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM DealersDetails WHERE ContinentID = @id", Conn);
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            gvRegion.DataSource = dataTable;
            gvRegion.DataBind();
            //關閉SQL連接
            Conn.Close();
        }
        protected void gvRegion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvRegion.DataKeys[e.RowIndex].Value.ToString();
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM DealersDetails WHERE id = @id;", Conn);
            Cmd.Parameters.AddWithValue("@id", id);

            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }

        protected void btnDealersInsert_Click(object sender, EventArgs e)
        {
            //傳資料表DealersContinent的id值
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Response.Redirect($"DealersInsert.aspx?Cid={id}");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DealersMgmt.aspx");
        }

        protected void btnUpdateContinentName_Click(object sender, EventArgs e)
        {
            updateDate();
        }

        private void updateDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("UPDATE DealersContinent SET  Continent =@Continent WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@Continent", txbContinentName.Text);
            //Request.QueryString["id"].ToString()是傳DealersDetails的id值
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
            //執行命令
            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getContinentName()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT * FROM DealersContinent WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
            //執行命令
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                txbContinentName.Text = Reader["Continent"].ToString();
            }
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void showData()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"WITH CTE AS (SELECT ROW_NUMBER() OVER (ORDER BY DealersDetails.id) RowNumber, id, ContinentID, DealersInfo, Region, Title, ContactPhoto FROM DealersDetails WHERE ContinentID = @id) SELECT * FROM CTE where ROWNUMBER >=((@page - 1) * 5 + 1)  and ROWNUMBER <=(@page * 5)";
            SqlCommand command = new SqlCommand(code, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.Add("@page", SqlDbType.Int);
            command.Parameters["@page"].Value = Convert.ToInt32(Request.QueryString["page"] ?? "1");
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            gvRegion.DataSource = dataReader;
            //把這個資料表跟資料(reader)作雙向繫結
            gvRegion.DataBind();
            connection.Close();
        }
        protected void dataListCountCS()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"SELECT COUNT(*) AS total  FROM DealersDetails WHERE 1=1 AND ContinentID={id}";
            SqlCommand command = new SqlCommand(code, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int itemsCount = table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0][0].ToString()) : 0;
            //分頁控制項丟入參數做測試
            Pages.totalitems = itemsCount;//每頁數量
            Pages.limit = 5;//資料總量
            Pages.targetpage = $"DealersList.aspx?id={id}";
            Pages.showPageControls();//顯示分頁控制項 
        }

        protected void btnKeywordSearch_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string keyword = txbKeyword.Text;
            string sqlkeyword = $"SELECT * FROM DealersDetails WHERE Region LIKE '%{keyword}%' AND ContinentID={id}";
            Session["keyword"] = sqlkeyword;
            Response.Redirect("DealersSearch.aspx");
        }
    }
}