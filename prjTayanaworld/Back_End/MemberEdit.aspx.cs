using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            // SqlDataAdapter 執行SQL命令抓取資料
            SqlDataAdapter adminList = new SqlDataAdapter("SELECT AdminID,UserName, Account, Password, LastName, FirstName,  convert(datetime, BirthDate, 120)BirthDate, Gender FROM Administrator", Conn);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable listTable = new DataTable();
            adminList.Fill(listTable);
            GridView1.DataSource = listTable;
            GridView1.DataBind();
            //關閉SQL連接
            Conn.Close();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM Administrator WHERE AdminID = @AdminID;", Conn);
            Cmd.Parameters.AddWithValue("@AdminID", id);

            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberInsert.aspx");
        }

        protected void btnKeywordSearch_Click(object sender, EventArgs e)
        {
            string keyword = txbKeyword.Text;
            string sqlkeyword = $"SELECT Administrator.* FROM  Administrator WHERE LastName+FirstName LIKE '%{keyword}%'";
            Session["keyword"] = sqlkeyword;
            Response.Redirect("MemberSearch.aspx");
        }
    }
}