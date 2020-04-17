using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                getGridView();
                showData();
                dataListCountCS();
            }
        }

        protected void btnPhotoInsert_Click(object sender, EventArgs e)
        {
            uploadImg(FileUpload1);
        }

        private void uploadImg(FileUpload myFileUpload)
        {
            //判斷資料夾是否存在，若無則建立資料夾，using System.IO;
            if (!Directory.Exists("YachtsImg/"))
            {
                Directory.CreateDirectory(Server.MapPath("YachtsImg"));
            }
            //判斷檔案否存在
            if (myFileUpload.HasFile)
            {

                //判斷是否為圖片類型檔案
                if (myFileUpload.PostedFile.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "請使用圖片類型檔案！";
                }
                else
                {
                    //判斷檔案大小是否超過4MB
                    if (myFileUpload.PostedFile.ContentLength > 4194304)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "檔案大小不可超過4MB！";
                    }
                    else
                    {
                        string extension = myFileUpload.FileName.Split('.')[myFileUpload.FileName.Split('.').Length - 1];
                        //新檔案名稱
                        string fileName = DateTime.Now.ToString("yyyyMMddhhmmsss") + "." + extension;  //也可寫作：String.Format("{0:}.{1}", DateTime.Now, extension)
                        //設定檔案路徑
                        string savePath = Server.MapPath("YachtsImg/");
                        //設定完整存檔路徑
                        string savedName = Path.Combine(savePath, fileName);
                        //存檔
                        myFileUpload.SaveAs(savedName);
                        CsImg.GenerateThumbnailImage(fileName, myFileUpload.PostedFile.InputStream, savePath, "mini", 58);
                        string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        //連接資料庫
                        SqlConnection Conn = new SqlConnection(ConnectionString);
                        Conn.Open();
                        //Cmd命令
                        SqlCommand Cmd = new SqlCommand("INSERT INTO YachtsPhoto (YachtsID,FileName, UploadDate,Introduction) VALUES (@YachtsID,@FileName,@UploadDate,@Introduction)", Conn);
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        Cmd.Parameters.AddWithValue("@YachtsID", id);//遊艇船型的id值
                        Cmd.Parameters.AddWithValue("@FileName", fileName);
                        Cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                        Cmd.Parameters.AddWithValue("@Introduction", txbIntroduction.Text);

                        //執行命令
                        Cmd.ExecuteNonQuery();
                        Cmd.Cancel();
                        Conn.Close();
                        Conn.Dispose();

                        Response.Redirect(this.Request.Url.ToString());
                    }
                }
            }
            else
            {
                lblMessage.Text = "請選擇檔案上傳！";
            }
        }

        private void getGridView()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            // SqlDataAdapter 執行SQL命令抓取資料
            SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM YachtsPhoto WHERE YachtsID=@id", Conn);
            Adapter.SelectCommand.Parameters.AddWithValue("@id", id);
            //using System.Data;
            //宣告DataTable 將SqlDataAdapter匯入，DataBind將資料來源繫結至 GridView
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            gvYachtsPhoto.DataSource = dataTable;
            gvYachtsPhoto.DataBind();
            //關閉SQL連接
            Conn.Close();
            Conn.Dispose();
        }

        protected void gvYachtsPhoto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvYachtsPhoto.DataKeys[e.RowIndex].Value);
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            SqlCommand Cmd = new SqlCommand("DELETE FROM YachtsPhoto WHERE id = @id", Conn);
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect(this.Request.Url.ToString());//頁面跳轉更新
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("YachtsMgmt.aspx");
        }

        private void showData()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"WITH CTE AS (SELECT ROW_NUMBER() OVER (ORDER BY YachtsPhoto.id) RowNumber, id,YachtsID, FileName, UploadDate,Introduction FROM YachtsPhoto WHERE YachtsID=@id) SELECT * FROM CTE where ROWNUMBER >=((@page - 1) * 5 + 1)  and ROWNUMBER <=(@page * 5)";
            SqlCommand command = new SqlCommand(code, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.Add("@page", SqlDbType.Int);
            command.Parameters["@page"].Value = Convert.ToInt32(Request.QueryString["page"] ?? "1");
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            gvYachtsPhoto.DataSource = dataReader;
            //把這個資料表跟資料(reader)作雙向繫結
            gvYachtsPhoto.DataBind();
            connection.Close();
        }
        protected void dataListCountCS()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string strConn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strConn);
            string code = $"SELECT COUNT(*) AS total  FROM YachtsPhoto  WHERE 1=1 AND YachtsID={id}";
            SqlCommand command = new SqlCommand(code, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int itemsCount = table.Rows.Count > 0 ? Convert.ToInt32(table.Rows[0][0].ToString()) : 0;
            //分頁控制項丟入參數做測試
            Pages.totalitems = itemsCount;//每頁數量
            Pages.limit = 5;//資料總量
            Pages.targetpage = $"YachtsEditPhoto.aspx?id={id}";
            Pages.showPageControls();//顯示分頁控制項 
        }
    }
}