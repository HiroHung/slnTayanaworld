using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm3 : System.Web.UI.Page
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
                lblError.Visible = false;
            }


        }

        protected void btnPhtoUpload_Click(object sender, EventArgs e)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令，選取所有AdminID
            SqlCommand checkCmd = new SqlCommand("SELECT AdminID FROM UploadPhoto", Conn);
            SqlDataReader Reader = checkCmd.ExecuteReader();

            //設定檔案名稱
            string fileName = PhotoUpload.FileName;
            //判斷資料夾是否存在，若無則建立資料夾
            if (!Directory.Exists("Img/"))
            {
                Directory.CreateDirectory(Server.MapPath("Img"));
            }
            // 設定檔案路徑
            string savePath = Server.MapPath("Img/");
            //設定完整存檔路徑
            string saveResults = System.IO.Path.Combine(savePath, fileName);


            //確認使用者是否已經有上傳過檔案了，若有的話使用Update
            if (Reader.Read())
            {
                if (Reader["AdminID"].ToString() == Session["Login"].ToString())
                {
                    Reader.Close();
                    //判斷檔案否存在
                    if (PhotoUpload.HasFile)
                    {
                        //判斷是否為指定類型檔案，using System.IO;
                        string saveExt = Path.GetExtension(PhotoUpload.FileName);
                        if (saveExt == ".jpg" || saveExt == ".png")
                        {
                            PhotoUpload.SaveAs(saveResults);
                            //Cmd命令，更新檔名與日期
                            SqlCommand updateCmd = new SqlCommand("UPDATE UploadPhoto SET PhotoFileName =@PhotoFileName, UploadTime =@UploadTime WHERE (AdminID = @AdminID)", Conn);
                            updateCmd.Parameters.AddWithValue("@PhotoFileName", fileName);
                            updateCmd.Parameters.AddWithValue("@AdminID", Session["Login"].ToString());
                            updateCmd.Parameters.AddWithValue("@UploadTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                            //執行命令
                            updateCmd.ExecuteNonQuery();
                            updateCmd.Cancel();
                            Conn.Close();
                            Conn.Dispose();
                            Response.Redirect(this.Request.Url.ToString()); //頁面跳轉更新
                        }
                        else
                        {
                            lblMessage.Text = "請使用指定檔案類型：jpg、png";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "請選擇檔案上傳！";
                    }
                }
            }
            else  //若沒有的話使用INSERT INTO
            {
                Reader.Close();
                //判斷檔案否存在
                if (PhotoUpload.HasFile)
                {
                    //判斷是否為指定類型檔案，using System.IO;
                    string saveExt = Path.GetExtension(PhotoUpload.FileName);
                    if (saveExt == ".jpg" || saveExt == ".png")
                    {
                        PhotoUpload.SaveAs(saveResults);
                        //Cmd命令，新增Photo資料
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO UploadPhoto (AdminID, PhotoFileName, UploadTime) VALUES  (@AdminID,@PhotoFileName,@UploadTime)", Conn);
                        insertCmd.Parameters.AddWithValue("@AdminID", Session["Login"].ToString());
                        insertCmd.Parameters.AddWithValue("@PhotoFileName", fileName);
                        insertCmd.Parameters.AddWithValue("@UploadTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                        //執行命令
                        insertCmd.ExecuteNonQuery();

                        insertCmd.Cancel();
                        Conn.Close();
                        Conn.Dispose();
                        Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
                    }
                    else
                    {
                        lblMessage.Text = "請使用指定檔案類型：jpg、png";
                    }
                }
                else
                {
                    lblMessage.Text = "請選擇檔案上傳！";
                }
            }
        }
        protected void btnUserInfo_Click(object sender, EventArgs e)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            string hashpwd;
            if (txbPassword.Value != "")
            {

                if (txbCkPassword.Value == "")
                {
                    lblError.Visible = true;
                    return;
                }
                else
                {
                    hashpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txbPassword.Value.Trim(), "SHA1");
                }
            }
            else
            {
                hashpwd = Session["pwd"].ToString();
            }
            //Cmd命令，更新用戶個人資料
            SqlCommand Cmd = new SqlCommand("UPDATE Administrator SET UserName =@UserName, Password =@Password, LastName =@LastName, FirstName =@FirstName, BirthDate =@BirthDate, Gender =@Gender  WHERE (AdminID = @AdminID)", Conn);
            Cmd.Parameters.AddWithValue("@AdminID", Session["Login"].ToString());
            Cmd.Parameters.AddWithValue("@UserName", txbUserName.Value); ;
            Cmd.Parameters.AddWithValue("@Password", hashpwd);
            Cmd.Parameters.AddWithValue("@LastName", txbLastName.Value);
            Cmd.Parameters.AddWithValue("@FirstName", txbFirstName.Value);
            Cmd.Parameters.AddWithValue("@BirthDate", txbBirthDate.Value);
            Cmd.Parameters.AddWithValue("@Gender", rdbtnGender.SelectedValue);
            Cmd.ExecuteNonQuery();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect("index.aspx");//頁面跳轉首頁
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.Request.Url.ToString());
        }

        private void showDate()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT Administrator.*, UploadPhoto.PhotoFileName FROM Administrator INNER JOIN UploadPhoto ON Administrator.AdminID = UploadPhoto.AdminID WHERE (Administrator.AdminID = @AdminID) ", Conn);
            Cmd.Parameters.AddWithValue("@AdminID", Session["Login"].ToString());
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                txbLastName.Value = Reader["LastName"].ToString();
                txbFirstName.Value = Reader["FirstName"].ToString();
                txbUserName.Value = Reader["UserName"].ToString();
                Session["pwd"] = Reader["Password"].ToString();
                //txbPassword.Value = Reader["Password"].ToString();
                //txbCkPassword.Value = Reader["Password"].ToString();

                //將Reader["BirthDate"].ToString()字串轉換成DateTime形式
                DateTime dateTime = DateTime.Parse(Reader["BirthDate"].ToString());
                //設定字串的時間形式
                string BirthDate = dateTime.ToString("yyyy-MM-dd");
                txbBirthDate.Value = BirthDate;
                rdbtnGender.SelectedValue = Reader["Gender"].ToString();
                preImg.ImageUrl = "/Back_End/Img/" + Reader["PhotoFileName"].ToString();
            }
            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }
    }
}