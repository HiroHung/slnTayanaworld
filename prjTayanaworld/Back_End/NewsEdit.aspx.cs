using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                showDate();
            }
        }

        protected void btnUpdateNews_Click(object sender, EventArgs e)
        {
            updateDate();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsMgmt.aspx");
        }

        private void updateDate()
        {
            //圖片上傳
            //取得副檔名
            string extension = NewsPhotoUpload.FileName.Split('.')[NewsPhotoUpload.FileName.Split('.').Length - 1];
            //新檔案名稱
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmsss") + "." + extension;  //也可寫作：String.Format("{0:}.{1}", DateTime.Now, extension)
            //設定檔案路徑
            string savePath = Server.MapPath("NewsImg/");
            //設定完整存檔路徑
            string savedName = Path.Combine(savePath, fileName);
            //判斷資料夾是否存在，若無則建立資料夾，using System.IO;

            if (!Directory.Exists("NewsImg/"))
            {
                Directory.CreateDirectory(Server.MapPath("NewsImg"));
            }
            //判斷檔案否存在
            if (NewsPhotoUpload.HasFile)
            {
                //判斷是否為圖片類型檔案
                if (NewsPhotoUpload.PostedFile.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "請使用圖片類型檔案！";
                }
                else
                {
                    //存檔
                    NewsPhotoUpload.SaveAs(savedName);
                    CsImg.GenerateThumbnailImageWidth(161, fileName, NewsPhotoUpload.PostedFile.InputStream, savePath, "mini");
                    string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    //連接資料庫
                    SqlConnection Conn = new SqlConnection(ConnectionString);
                    Conn.Open();
                    //Cmd命令
                    SqlCommand Cmd = new SqlCommand("UPDATE News SET NewsDate =@NewsDate, NewsTitle =@NewsTitle, NewsContent =@NewsContent, Sticky =@Sticky, PrePhotoName =@PrePhotoName,Introduction=@Introduction WHERE id = @id", Conn);
                    Cmd.Parameters.AddWithValue("@NewsDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                    Cmd.Parameters.AddWithValue("@NewsTitle", txbNewsTitle.Text);
                    Cmd.Parameters.AddWithValue("@NewsContent", txbNewsContent.Text);
                    Cmd.Parameters.AddWithValue("@Sticky", rdolstSticky.SelectedValue);
                    Cmd.Parameters.AddWithValue("@PrePhotoName", fileName);
                    Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                    Cmd.Parameters.AddWithValue("@Introduction", txbIntroduction.Text);
                    //執行命令
                    Cmd.ExecuteNonQuery();

                    Cmd.Cancel();
                    Conn.Close();
                    Conn.Dispose();

                    Response.Redirect("NewsMgmt.aspx");//頁面跳轉更新
                }
            }
            else
            {
                string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //連接資料庫
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                //Cmd命令
                SqlCommand Cmd = new SqlCommand("UPDATE News SET NewsDate =@NewsDate, NewsTitle =@NewsTitle, NewsContent =@NewsContent, Sticky =@Sticky,Introduction=@Introduction WHERE id = @id", Conn);
                Cmd.Parameters.AddWithValue("@NewsDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                Cmd.Parameters.AddWithValue("@NewsTitle", txbNewsTitle.Text);
                Cmd.Parameters.AddWithValue("@NewsContent", txbNewsContent.Text);
                Cmd.Parameters.AddWithValue("@Sticky", rdolstSticky.SelectedValue);
                Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                Cmd.Parameters.AddWithValue("@Introduction", txbIntroduction.Text);
                //執行命令
                Cmd.ExecuteNonQuery();
                Cmd.Cancel();
                Conn.Close();
                Conn.Dispose();

                Response.Redirect("NewsMgmt.aspx");//頁面跳轉更新
            }
        }

        private void showDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT id, NewsDate, NewsTitle, NewsContent, Sticky, PrePhotoName,Introduction FROM News WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
            SqlDataReader Reader = Cmd.ExecuteReader();
            
            if (Reader.Read())
            {
                txbNewsTitle.Text = Reader["NewsTitle"].ToString();
                txbNewsContent.Text = HttpUtility.HtmlDecode(Reader["NewsContent"].ToString());
                txbIntroduction.Text = Reader["Introduction"].ToString();
                rdolstSticky.SelectedValue = Reader["Sticky"].ToString();
                preImg.ImageUrl = "/Back_End/NewsImg/" + Reader["PrePhotoName"].ToString();
            }
            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }
    }
}