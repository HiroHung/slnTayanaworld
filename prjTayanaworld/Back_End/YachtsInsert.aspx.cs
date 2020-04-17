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
    public partial class WebForm15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void insertDate()
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
                        SqlCommand Cmd = new SqlCommand("INSERT INTO Yachts (YachtsName,Model, NewTypeMark, UploadDate, LayoutDeckplan, OverViewContent, OverViewDownload, Specifications) VALUES (@YachtsName,@Model,@NewTypeMark,@UploadDate,@LayoutDeckplan,@OverViewContent,@OverViewDownload,@Specifications);select  @@identity", Conn);
                        Cmd.Parameters.AddWithValue("@YachtsName", txbYachtsName.Text);
                        Cmd.Parameters.AddWithValue("@Model", txbModel.Text);
                        Cmd.Parameters.AddWithValue("@NewTypeMark", rdoNewType.SelectedValue);
                        Cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                        Cmd.Parameters.AddWithValue("@LayoutDeckplan", txbLayoutdeckplan.Text);
                        Cmd.Parameters.AddWithValue("@OverViewContent", txbOverview.Text);
                        Cmd.Parameters.AddWithValue("@OverViewDownload", fileName);
                        Cmd.Parameters.AddWithValue("@Specifications", txbSpecifications.Text);

                        //執行命令
                        //Cmd.ExecuteScalar();
                        int id = Convert.ToInt32(Cmd.ExecuteScalar());
                        Cmd.Cancel();
                        Conn.Close();
                        Conn.Dispose();

                        Response.Redirect($"YachtsEditPhoto.aspx?id={id}");
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
                SqlCommand Cmd = new SqlCommand("INSERT INTO Yachts (YachtsName,Model, NewTypeMark, UploadDate, LayoutDeckplan, OverViewContent, Specifications) VALUES (@YachtsName,@Model,@NewTypeMark,@UploadDate,@LayoutDeckplan,@OverViewContent,@Specifications);select  @@identity", Conn);
                Cmd.Parameters.AddWithValue("@YachtsName", txbYachtsName.Text);
                Cmd.Parameters.AddWithValue("@Model", txbModel.Text);
                Cmd.Parameters.AddWithValue("@NewTypeMark", rdoNewType.SelectedValue);
                Cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                Cmd.Parameters.AddWithValue("@LayoutDeckplan", txbLayoutdeckplan.Text);
                Cmd.Parameters.AddWithValue("@OverViewContent", txbOverview.Text);
                Cmd.Parameters.AddWithValue("@Specifications", txbSpecifications.Text);

                //執行命令
                //Cmd.ExecuteScalar();
                int id = Convert.ToInt32(Cmd.ExecuteScalar());
                Cmd.Cancel();
                Conn.Close();
                Conn.Dispose();

                Response.Redirect($"YachtsEditPhoto.aspx?id={id}");
            }


        }
        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            insertDate();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("YachtsMgmt.aspx");
        }
    }
}