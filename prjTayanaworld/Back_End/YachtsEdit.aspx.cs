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
    public partial class WebForm17 : System.Web.UI.Page
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

        private void showDate()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT * FROM Yachts WHERE id =@id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                txbYachtsName.Text = Reader["YachtsName"].ToString();
                txbModel.Text = Reader["Model"].ToString();
                rdoNewType.SelectedValue = Reader["NewTypeMark"].ToString();
                txbOverview.Text = Reader["OverViewContent"].ToString();
                lblOverViewDownload.Text = Reader["OverViewDownload"].ToString();
                txbLayoutdeckplan.Text = Reader["LayoutDeckplan"].ToString();
                txbSpecifications.Text = Reader["Specifications"].ToString();
            }
            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        private void updateDate()
        {
            //PDF上傳開始
            //取得副檔名
            string extension = fulOverviewDownload.FileName.Split('.')[fulOverviewDownload.FileName.Split('.').Length - 1];
            //新檔案名稱
            string fileName = txbYachtsName.Text + txbModel.Text + DateTime.Now.ToString("yyyyMMddhhmmsss") + "." + extension;  //也可寫作：String.Format("{0:}.{1}", DateTime.Now, extension)
            //設定檔案路徑
            string savePath = Server.MapPath("OverviewPDF/");
            //設定完整存檔路徑
            string savedName = Path.Combine(savePath, fileName);
            //判斷資料夾是否存在，若無則建立資料夾，using System.IO;
            if (!Directory.Exists("OverviewPDF/"))
            {
                Directory.CreateDirectory(Server.MapPath("OverviewPDF"));
            }
            //判斷檔案否存在
            if (fulOverviewDownload.HasFile)
            {
                //判斷是否為PDF類型檔案
                if (fulOverviewDownload.PostedFile.ContentType.IndexOf("application/pdf", System.StringComparison.Ordinal) == -1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "請使用PDF類型檔案！";
                }
                else
                {
                    //判斷檔案大小是否超過4MB
                    if (fulOverviewDownload.PostedFile.ContentLength > 4194304)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "檔案大小不可超過4MB！";
                    }
                    else
                    {
                        //存檔
                        fulOverviewDownload.SaveAs(savedName);
                        //PDF上傳結束
                        string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("UPDATE Yachts SET YachtsName =@YachtsName, Model=@Model,NewTypeMark =@NewTypeMark, UploadDate =@UploadDate, LayoutDeckplan =@LayoutDeckplan, OverViewContent =@OverViewContent, OverViewDownload =@OverViewDownload,  Specifications =@Specifications WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@YachtsName", txbYachtsName.Text);
            Cmd.Parameters.AddWithValue("@Model", txbModel.Text);
            Cmd.Parameters.AddWithValue("@NewTypeMark", rdoNewType.SelectedValue);
            Cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            Cmd.Parameters.AddWithValue("@LayoutDeckplan", txbLayoutdeckplan.Text);
            Cmd.Parameters.AddWithValue("@OverViewContent", txbOverview.Text);
            Cmd.Parameters.AddWithValue("@OverViewDownload", fileName);
            Cmd.Parameters.AddWithValue("@Specifications", txbSpecifications.Text);
            int id = Convert.ToInt32(Request.QueryString["id"]);
            Cmd.Parameters.AddWithValue("@id", id);

            //執行命令
            Cmd.ExecuteNonQuery();
            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
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
                SqlCommand Cmd = new SqlCommand("UPDATE Yachts SET YachtsName =@YachtsName, Model=@Model,NewTypeMark =@NewTypeMark, UploadDate =@UploadDate, LayoutDeckplan =@LayoutDeckplan, OverViewContent =@OverViewContent,Specifications =@Specifications WHERE id = @id", Conn);
                Cmd.Parameters.AddWithValue("@YachtsName", txbYachtsName.Text);
                Cmd.Parameters.AddWithValue("@Model", txbModel.Text);
                Cmd.Parameters.AddWithValue("@NewTypeMark", rdoNewType.SelectedValue);
                Cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                Cmd.Parameters.AddWithValue("@LayoutDeckplan", txbLayoutdeckplan.Text);
                Cmd.Parameters.AddWithValue("@OverViewContent", txbOverview.Text);
                Cmd.Parameters.AddWithValue("@Specifications", txbSpecifications.Text);
                int id = Convert.ToInt32(Request.QueryString["id"]);
                Cmd.Parameters.AddWithValue("@id", id);

                //執行命令
                Cmd.ExecuteNonQuery();
                Cmd.Cancel();
                Conn.Close();
                Conn.Dispose();
            }
            Response.Redirect($"YachtsMgmt.aspx");
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            updateDate();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("YachtsMgmt.aspx");
        }
    }
}