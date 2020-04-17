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
    public partial class WebForm13 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                showDate();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BackToPreviousPage();
        }

        private void showDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT * FROM DealersDetails WHERE id = @id", Conn);
            //Request.QueryString["id"].ToString()是傳DealersDetails的id值
            Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            SqlDataReader Reader = Cmd.ExecuteReader();

            if (Reader.Read())
            {
                txbRegion.Text = Reader["Region"].ToString();
                txbTitle.Text = Reader["Title"].ToString();
                ckeDealersInfo.Text = Reader["DealersInfo"].ToString();
                preImg.ImageUrl = "/Back_End/DealersImg/" + Reader["ContactPhoto"].ToString();
            }

            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            updateDate();
        }

        private void updateDate()
        {
            //圖片上傳
            //取得副檔名
            string extension = ContactPhotoUpload.FileName.Split('.')[ContactPhotoUpload.FileName.Split('.').Length - 1];
            //新檔案名稱
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmsss") + "." + extension;  //也可寫作：String.Format("{0:}.{1}", DateTime.Now, extension)
            //設定檔案路徑
            string savePath = Server.MapPath("DealersImg/");
            //設定完整存檔路徑
            string savedName = Path.Combine(savePath, fileName);
            //判斷資料夾是否存在，若無則建立資料夾，using System.IO;
            if (!Directory.Exists("DealersImg/"))
            {
                Directory.CreateDirectory(Server.MapPath("DealersImg"));
            }
            //判斷檔案否存在
            if (ContactPhotoUpload.HasFile)
            {
                //判斷是否為圖片類型檔案
                if (ContactPhotoUpload.PostedFile.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "請使用圖片類型檔案！";
                }
                else
                {
                    //判斷檔案大小是否超過4MB
                    if (ContactPhotoUpload.PostedFile.ContentLength > 4194304)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "檔案大小不可超過4MB！";
                    }
                    else
                    {
                        //存檔
                        ContactPhotoUpload.SaveAs(savedName);
                        CsImg.GenerateThumbnailImageWidth(209,fileName, ContactPhotoUpload.PostedFile.InputStream, savePath, "mini");
                        string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        //連接資料庫
                        SqlConnection Conn = new SqlConnection(ConnectionString);
                        Conn.Open();
                        //Cmd命令
                        SqlCommand Cmd = new SqlCommand("UPDATE DealersDetails SET DealersInfo =@DealersInfo, Region =@Region, Title =@Title, ContactPhoto =@ContactPhoto WHERE id = @id", Conn);
                        Cmd.Parameters.AddWithValue("@DealersInfo", ckeDealersInfo.Text);
                        Cmd.Parameters.AddWithValue("@Region", txbRegion.Text);
                        Cmd.Parameters.AddWithValue("@Title", txbTitle.Text);
                        Cmd.Parameters.AddWithValue("@ContactPhoto", fileName);
                        //Request.QueryString["id"].ToString()是傳DealersDetails的id值
                        Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                        //執行命令
                        Cmd.ExecuteNonQuery();

                        Cmd.Cancel();
                        Conn.Close();
                        Conn.Dispose();
                        BackToPreviousPage();
                    }
                }
            }
            else
            {
                string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //連接資料庫
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                //Cmd命令
                SqlCommand Cmd = new SqlCommand("UPDATE DealersDetails SET DealersInfo =@DealersInfo, Region =@Region, Title =@Title  WHERE id = @id", Conn);
                Cmd.Parameters.AddWithValue("@DealersInfo", ckeDealersInfo.Text);
                Cmd.Parameters.AddWithValue("@Region", txbRegion.Text);
                Cmd.Parameters.AddWithValue("@Title", txbTitle.Text);
                //Request.QueryString["id"].ToString()是傳DealersDetails的id值
                Cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                //執行命令
                Cmd.ExecuteNonQuery();

                Cmd.Cancel();
                Conn.Close();
                Conn.Dispose();

                BackToPreviousPage();
            }
        }

        private void BackToPreviousPage()
        {
            string id = Server.HtmlDecode(Request.Cookies["id"].Value);
            Response.Redirect($"DealersList.aspx?id={id}");//回到上頁，且回傳ContinentID的值
        }
    }
}