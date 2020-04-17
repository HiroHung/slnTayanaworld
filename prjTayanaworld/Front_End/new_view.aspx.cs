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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getNewsDate();
            }
        }

        private void getNewsDate()
        {
            string id = Request.QueryString["id"];
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM News WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                litNewsContent.Text =HttpUtility.HtmlDecode(Reader["NewsContent"].ToString());
            }
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }
    }
}