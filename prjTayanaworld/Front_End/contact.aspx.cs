using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

namespace prjTayanaworld.Front_End
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                countryDate();
                yachtsDate();
            }
        }

        private void countryDate()
        {
            //建立CheckBoxList與資料庫的繫結，並匯入資料
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT Continent FROM DealersContinent", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();

            //繫結下拉式選單countrySelect
            countrySelect.DataSource = Reader;
            //設定Text
            countrySelect.DataTextField = "Continent";
            countrySelect.DataBind();

            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        private void yachtsDate()
        {
            // 建立CheckBoxList與資料庫的繫結，並匯入資料
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT id, YachtsName+' '+Model YachtsModel, NewTypeMark, UploadDate, LayoutDeckplan, OverViewContent, OverViewDownload, Specifications FROM Yachts", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();

            //繫結下拉式選單countrySelect
            yachtsSelect.DataSource = Reader;
            //設定Text
            yachtsSelect.DataTextField += "YachtsModel";

            yachtsSelect.DataBind();

            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        public void SendEmail()
        {
            //設定smtp主機
            string smtpAddress = WebConfigurationManager.AppSettings["smtpServer"].Trim();
            //設定Port
            int portNumber = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"].Trim());
            bool enableSSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["enableSSL"].Trim());
            //填入寄送方email和密碼
            string emailFrom = WebConfigurationManager.AppSettings["sendMail"].Trim();
            string password = WebConfigurationManager.AppSettings["mailPwd"].Trim();
            //收信方email
            string emailToCompany = WebConfigurationManager.AppSettings["receiveMails"].Trim();
            string emailToCustomer = txbEmail.Value;
            //主旨
            string subject = WebConfigurationManager.AppSettings["mailTitle"].Trim() + DateTime.Now.ToString("yyyy/MM/dd/hh:mm");
            //信件內容，讀取Email樣板檔
            string body = File.ReadAllText(Server.MapPath("/Front_End/Email.html"));
            string bodyForCumstomer = File.ReadAllText(Server.MapPath("/Front_End/EmailToCustomer.html"));
            //將回覆內容部分的換行換成<br>符號
            string comment = txbComments.Value.Replace("\r\n", "<br>");
            //將樣板檔中的假字，以網頁項目中的值取代
            body = body.Replace("@Name", txbName.Value).Replace("@Phone", txbPhone.Value).Replace("@Email", txbEmail.Value).Replace("@Country", countrySelect.Value).Replace("@Yachts", yachtsSelect.Value).Replace("@Comments", comment);
            bodyForCumstomer = bodyForCumstomer.Replace("@Name", txbName.Value).Replace("@Phone", txbPhone.Value).Replace("@Email", txbEmail.Value).Replace("@Country", countrySelect.Value).Replace("@Yachts", yachtsSelect.Value).Replace("@Comments", comment);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(emailFrom);
            mail.To.Add(emailToCompany);
            mail.Subject = subject;
            mail.Body = body;
            // 若你的內容是HTML格式，則為True
            mail.IsBodyHtml = true;

            MailMessage mailForCustomer = new MailMessage();
            mailForCustomer.From = new MailAddress(emailFrom);
            mailForCustomer.To.Add(emailToCustomer);
            mailForCustomer.Subject = subject;
            mailForCustomer.Body = bodyForCumstomer;
            mailForCustomer.IsBodyHtml = true;

            //夾帶檔案
            //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
            //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

            SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
            smtp.Credentials = new NetworkCredential(emailFrom, password);
            smtp.EnableSsl = enableSSL;
            try
            {
                smtp.Send(mail);
                smtp.Send(mailForCustomer);
                mail.Dispose();
                Response.Redirect("EmailMsg.aspx?send=success",false);
            }
            catch
            {
                Response.Redirect("EmailMsg.aspx?send=false");
            }
            smtp = null;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            SendEmail();
        }
    }
}