using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Front_End
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                newsDate();
                getPhoto();
                getlilPhoto();
            }
        }

        private void newsDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT TOP 3 id, CONVERT(char(10), NewsDate, 111)NewsDate, NewsTitle, Sticky, PrePhotoName, Introduction FROM News ORDER BY Sticky DESC, id DESC", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            NewsRepeater.DataSource = Reader;
            NewsRepeater.DataBind();

            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getPhoto()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT *,(SELECT TOP 1 YachtsPhoto.FileName FROM YachtsPhoto WHERE Yachts.id = YachtsPhoto.YachtsID) FileName FROM Yachts WHERE (SELECT COUNT(YachtsPhoto.FileName) FROM YachtsPhoto WHERE  Yachts.id = YachtsPhoto.YachtsID) > 0", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater1.DataSource = Reader;
            Repeater1.DataBind();
            
            
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getlilPhoto()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT *,(SELECT TOP 1 YachtsPhoto.FileName FROM YachtsPhoto WHERE Yachts.id = YachtsPhoto.YachtsID) FileName FROM Yachts WHERE (SELECT COUNT(YachtsPhoto.FileName) FROM YachtsPhoto WHERE  Yachts.id = YachtsPhoto.YachtsID) > 0", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater2.DataSource = Reader;
            Repeater2.DataBind();
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}