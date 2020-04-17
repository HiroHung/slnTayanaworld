using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace prjTayanaworld.Back_End
{
    public partial class MainSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //建立側邊欄方法
                SideMenu();
                int id = Convert.ToInt32(Session["Login"].ToString());
                string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //連接資料庫
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                //Cmd命令
                SqlCommand Cmd = new SqlCommand("SELECT * FROM Administrator CROSS JOIN UploadPhoto WHERE Administrator.AdminID = @AdminID AND UploadPhoto.AdminID = @AdminID", Conn);
                Cmd.Parameters.AddWithValue("@AdminID", id);
                //Reader
                SqlDataReader Reader = Cmd.ExecuteReader();
                if (Reader.Read())
                {
                    //顯示使用者名稱
                    lblUserName.Text = Reader["UserName"].ToString();
                    //顯示頭像
                    userImg.ImageUrl = "Img/" + Reader["PhotoFileName"].ToString();

                    //側邊欄權限固定寫法
                    //if (Reader["AdminRight"].ToString().IndexOf("01", 0) == -1)
                    //{
                    //    sideUserProfile.Visible = false;
                    //}
                    //if (Reader["AdminRight"].ToString().IndexOf("02", 0) == -1)
                    //{
                    //    sideMemberMana.Visible = false;
                    //}
                    //if (Reader["AdminRight"].ToString().IndexOf("03", 0) == -1)
                    //{
                    //    sideMemberInsert.Visible = false;
                    //}
                    //if (Reader["AdminRight"].ToString().IndexOf("04", 0) == -1)
                    //{
                    //    sideMemberEdit.Visible = false;
                    //}
                    //if (Reader["AdminRight"].ToString().IndexOf("05", 0) == -1)
                    //{
                    //    sideMemberList.Visible = false;
                    //}
                }
                Cmd.Cancel();
                Reader.Close();
                Conn.Close();
                Conn.Dispose();
            }
        }

        //由資料庫中的權限來建立側邊攔
        private void SideMenu()
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //SQL命令：SELECT SideMenu中的Itemid、href、ItemText、RightNumber ，同時也SELECT Administrator中的AdminRight 且AdminID為目前使用者
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT SideMenu.Itemid,SideMenu.href,SideMenu.ItemText,SideMenu.RightNumber,Administrator.AdminRight FROM Administrator CROSS JOIN SideMenu WHERE (Administrator.AdminID = @AdminID)", Conn);
            adapter.SelectCommand.Parameters.AddWithValue("@AdminID", Session["Login"].ToString());
            //將資料匯入DataTable
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row[4].ToString().IndexOf(row[3].ToString()) != -1) //比對AdminRight是否有包含各項目的RightNumber
                {
                    // 字串組，將Itemid、href、ItemText各欄資料帶入字串組中，成為側邊欄的HTML
                    Literal1.Text += $"<li id='{row[0].ToString()}' runat='server'><a href = '{row[1].ToString()}'><i class='fa fa-star fa-fw'></i>{row[2].ToString()}</a></li>";
                }
            }
            Conn.Close();
            Conn.Dispose();
        }
    }
}