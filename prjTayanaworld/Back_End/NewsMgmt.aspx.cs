using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGridView();
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
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT  id, CONVERT(char(10), NewsDate, 20)NewsDate, NewsTitle, Sticky, PrePhotoName, Introduction FROM News ORDER BY Sticky DESC, id DESC", Conn);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            NewsGridView.DataSource = dataTable;
            NewsGridView.DataBind();
            //關閉SQL連接
            Conn.Close();
        }

        protected void NewsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = NewsGridView.DataKeys[e.RowIndex].Value.ToString();
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM News WHERE id = @id;", Conn);
            Cmd.Parameters.AddWithValue("@id", id);

            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }

        protected void btnNewsInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsInsert.aspx");
        }

        protected void btnKeywordSearch_Click(object sender, EventArgs e)
        {
            string keyword = txbKeyword.Text;
            string sqlkeyword = $"SELECT  id, CONVERT(char(10), NewsDate, 20)NewsDate, NewsTitle, Sticky, PrePhotoName, Introduction FROM News WHERE NewsTitle LIKE '%{keyword}%' ORDER BY Sticky DESC, id DESC";
            Session["keyword"] = sqlkeyword;
            Response.Redirect("NewsSearch.aspx");
        }

        protected void btnDateSearch_Click(object sender, EventArgs e)
        {
            string start = txbStart.Text;
            string end = txbEnd.Text;
            // txbStart.Text +" 00:00:00.000"   txbEnd.Text +" 23:59:59.000" 或是使用DATEADD語法
            string sqldate = $"SELECT  id, CONVERT(char(10), NewsDate, 20)NewsDate, NewsTitle, Sticky, PrePhotoName, Introduction FROM News WHERE NewsDate BETWEEN '{start}' AND DATEADD(day,1,'{end}') ORDER BY Sticky DESC, id DESC";
            Session["keyword"] = sqldate;
            Response.Redirect("NewsSearch.aspx");
        }

        private void showData()
        {
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"WITH CTE AS (SELECT ROW_NUMBER() OVER ( ORDER BY Sticky DESC, id DESC) RowNumber, id, CONVERT(char(10), NewsDate, 20)NewsDate, NewsTitle, NewsContent, Sticky, PrePhotoName, Introduction FROM News ) SELECT * FROM CTE where ROWNUMBER >=((@page - 1) * 5 + 1) and ROWNUMBER <=(@page * 5)";
            SqlCommand command = new SqlCommand(code, connection);
            command.Parameters.Add("@page", SqlDbType.Int);
            command.Parameters["@page"].Value = Convert.ToInt32(Request.QueryString["page"] ?? "1");
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            NewsGridView.DataSource = dataReader;
            //把這個資料表跟資料(reader)作雙向繫結
            NewsGridView.DataBind();
            connection.Close();
        }
        protected void dataListCountCS()
        {
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"SELECT COUNT(*) AS total  FROM News  WHERE 1=1";
            SqlCommand command = new SqlCommand(code, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int itemsCount = table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0][0].ToString()) : 0;
            //分頁控制項丟入參數做測試
            Pages.totalitems = itemsCount;//每頁數量
            Pages.limit = 5;//資料總量
            Pages.targetpage = "NewsMgmt.aspx";
            Pages.showPageControls();//顯示分頁控制項 
        }

        //protected void btnSticky_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in NewsGridView.Rows)
        //    {
        //        // 找出GridView中的CheckBox1
        //        CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
        //        if (cb != null && cb.Checked)//如果不為空值以及被選取
        //        {
        //            // 找出Datakey設為id
        //            int id = Convert.ToInt32(NewsGridView.DataKeys[row.RowIndex].Value);
        //            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //            //連接資料庫
        //            SqlConnection Conn = new SqlConnection(ConnectionString);
        //            Conn.Open();
        //            //更新資料
        //            SqlCommand Cmd = new SqlCommand("UPDATE News SET Sticky=1 WHERE id= @id;", Conn);
        //            Cmd.Parameters.AddWithValue("@id", id);

        //            Cmd.ExecuteNonQuery();

        //            Cmd.Cancel();
        //            Conn.Close();
        //            Conn.Dispose();
        //        }
        //        else
        //        {
        //            //找出Datakey設為id
        //            int id = Convert.ToInt32(NewsGridView.DataKeys[row.RowIndex].Value);
        //            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //            //連接資料庫
        //            SqlConnection Conn = new SqlConnection(ConnectionString);
        //            Conn.Open();
        //            //更新資料
        //            SqlCommand Cmd = new SqlCommand("UPDATE News SET Sticky=0 WHERE id= @id;", Conn);
        //            Cmd.Parameters.AddWithValue("@id", id);

        //            Cmd.ExecuteNonQuery();

        //            Cmd.Cancel();
        //            Conn.Close();
        //            Conn.Dispose();
        //        }
        //        Response.Redirect(this.Request.Url.ToString());
        //    }
        //}
    }
}