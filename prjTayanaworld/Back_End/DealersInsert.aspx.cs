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
    public partial class WebForm18 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void insertDate()
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
                        CsImg.GenerateThumbnailImageWidth(209, fileName, ContactPhotoUpload.PostedFile.InputStream, savePath, "mini");
                        string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        //連接資料庫
                        SqlConnection Conn = new SqlConnection(ConnectionString);
                        Conn.Open();
                        //Cmd命令
                        SqlCommand Cmd = new SqlCommand("INSERT INTO DealersDetails (ContinentID,ContactPhoto,DealersInfo,Region,Title) VALUES (@ContinentID,@ContactPhoto,@DealersInfo,@Region,@Title)", Conn);
                        Cmd.Parameters.AddWithValue("@ContinentID", Request.QueryString["Cid"]);//DealersContinent的id值
                        Cmd.Parameters.AddWithValue("@Region", txbRegion.Text);
                        Cmd.Parameters.AddWithValue("@Title", txbTitle.Text);
                        Cmd.Parameters.AddWithValue("@ContactPhoto", fileName);
                        Cmd.Parameters.AddWithValue("@DealersInfo", ckeDealersInfo.Text);

                        //執行命令
                        Cmd.ExecuteNonQuery();
                        Cmd.Cancel();
                        Conn.Close();
                        Conn.Dispose();

                        string id = Request.QueryString["Cid"].ToString();
                        Response.Redirect($"DealersList.aspx?id={id}");//回到上頁，且回傳ContinentID的值
                    }
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "請選擇檔案上傳！";
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            insertDate();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string Cid = Request.QueryString["Cid"];
            Response.Redirect($"DealersList.aspx?id={Cid}");
        }
    }
}