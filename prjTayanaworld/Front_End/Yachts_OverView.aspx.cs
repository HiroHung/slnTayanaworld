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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDate();
                getSideDate();
                getPhoto();
                getDownloadFile();
            }
        }

        private void getDate()
        {
            int id = Convert.ToInt32(Request.QueryString["id"] ?? "1") ;
            urlOverview.HRef = $"Yachts_OverView.aspx?id={id}";
            urlLayout.HRef= $"Yachts_Layout.aspx?id={id}";
            urlSpecification.HRef= $"Yachts_Specification.aspx?id={id}";
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM Yachts WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id",  id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                litContent.Text = HttpUtility.HtmlDecode(Reader["OverViewContent"].ToString());
                litTitle.Text = Reader["YachtsName"].ToString() + " " + Reader["Model"].ToString();
                litCrumb.Text = Reader["YachtsName"].ToString() + " " + Reader["Model"].ToString();
            }
            
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getSideDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM Yachts  ORDER BY YachtsName desc,Model", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater1.DataSource = Reader;
            Repeater1.DataBind();
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getPhoto()
        {
            int id = Convert.ToInt32(Request.QueryString["id"] ?? "1") ;
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT YachtsPhoto.* FROM YachtsPhoto WHERE YachtsID = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater2.DataSource = Reader;
            Repeater2.DataBind();
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }

        private void getDownloadFile()
        {
            int id = Convert.ToInt32(Request.QueryString["id"] ?? "1") ;
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd執行SQL語法
            SqlCommand Cmd = new SqlCommand("SELECT * FROM Yachts WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            Repeater3.DataSource = Reader;
            Repeater3.DataBind();
            Reader.Close();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
        }
    }
}