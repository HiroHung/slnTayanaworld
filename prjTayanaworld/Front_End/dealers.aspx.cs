using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Front_End
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getContinentDate();
                getDealersCrumbTitle();
                getDealersContent();
                showData();
                dataListCountCS();
            }
        }

        private void getContinentDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM DealersContinent", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater1.DataSource = Reader;
            Repeater1.DataBind();

            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getDealersCrumbTitle()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            //SELECT * FROM DealersDetails WHERE ContinentID = @id;
            SqlCommand Cmd = new SqlCommand("SELECT DealersContinent.Continent FROM DealersContinent INNER JOIN DealersDetails ON DealersContinent.id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"] ?? "1");
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                litContinent.Text = Reader["Continent"].ToString();
                litCrumb.Text = Reader["Continent"].ToString();
            }
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getDealersContent()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM DealersDetails WHERE ContinentID = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"] ?? "1");
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater2.DataSource = Reader;
            Repeater2.DataBind();

            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void showData()
        {
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"WITH CTE AS (SELECT ROW_NUMBER() OVER (ORDER BY DealersDetails.id) RowNumber,* FROM DealersDetails) SELECT * FROM CTE where ROWNUMBER >=((@page - 1) * 5 + 1) and ROWNUMBER <=(@page * 5) and ContinentID =@id";
            SqlCommand command = new SqlCommand(code, connection);
            command.Parameters.AddWithValue("@id", Request.QueryString["id"] ?? "1");
            command.Parameters.Add("@page", SqlDbType.Int);
            command.Parameters["@page"].Value = Convert.ToInt32(Request.QueryString["page"] ?? "1");
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            Repeater2.DataSource = dataReader;
            //把這個資料表跟資料(reader)作雙向繫結
            Repeater2.DataBind();
            connection.Close();
        }
        protected void dataListCountCS()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]??"1");
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"SELECT COUNT(*) AS total  FROM DealersDetails WHERE 1=1 and ContinentID ={id}";
            SqlCommand command = new SqlCommand(code, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int itemsCount = table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0][0].ToString()) : 0;
            //分頁控制項丟入參數做測試
            FrontPages.totalitems = itemsCount;//每頁數量
            FrontPages.limit = 5;//資料總量
            FrontPages.targetpage = $"dealers.aspx?id={id}"; 
            FrontPages.showPageControls();//顯示分頁控制項 
        }
    }
}