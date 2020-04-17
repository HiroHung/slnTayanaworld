using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPostNews_Click(object sender, EventArgs e)
        {
            insertDate();
        }

        private void insertDate()
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
                    CsImg.GenerateThumbnailImageWidth(161,fileName, NewsPhotoUpload.PostedFile.InputStream, savePath, "mini");
                    string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    //連接資料庫
                    SqlConnection Conn = new SqlConnection(ConnectionString);
                    Conn.Open();
                    //Cmd命令
                    SqlCommand Cmd = new SqlCommand("INSERT INTO News (NewsDate, NewsTitle, NewsContent, Sticky,PrePhotoName,Introduction) VALUES (@NewsDate,@NewsTitle,@NewsContent,@Sticky,@PrePhotoName,@Introduction)", Conn);
                    Cmd.Parameters.AddWithValue("@NewsDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                    Cmd.Parameters.AddWithValue("@NewsTitle", txbNewsTitle.Text);
                    Cmd.Parameters.AddWithValue("@NewsContent", HttpUtility.HtmlEncode(txbNewsContent.Text));
                    Cmd.Parameters.AddWithValue("@Sticky", rdolstSticky.SelectedValue);
                    Cmd.Parameters.AddWithValue("@PrePhotoName", fileName);
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
                lblMessage.Visible = true;
                lblMessage.Text = "請選擇檔案上傳！";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsMgmt.aspx");
        }
    }
}