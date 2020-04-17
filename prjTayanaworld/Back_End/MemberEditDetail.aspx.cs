using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace prjTayanaworld.Back_End
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                CheckBoxListBuild();
                string id = Request.QueryString["ID"];
                string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //連接資料庫
                SqlConnection Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                //Cmd命令
                SqlCommand Cmd = new SqlCommand("SELECT * FROM Administrator WHERE AdminID =@AdminID ", Conn);
                Cmd.Parameters.AddWithValue("@AdminID", id);
                SqlDataReader Reader = Cmd.ExecuteReader();
                if (Reader.Read())
                {
                    txbLastName.Value = Reader["LastName"].ToString();
                    txbFirstName.Value = Reader["FirstName"].ToString();
                    txbUserName.Value = Reader["UserName"].ToString();

                    //將Reader["BirthDate"].ToString()字串轉換成DateTime形式
                    DateTime dateTime = DateTime.Parse(Reader["BirthDate"].ToString());
                    //設定字串的時間形式
                    string BirthDate = dateTime.ToString("yyyy-MM-dd");
                    txbBirthDate.Value = BirthDate;
                    rdbtnGender.SelectedValue = Reader["Gender"].ToString();

                    CheckBoxSelected();
                }
                Cmd.Cancel();
                Reader.Close();
                Conn.Close();
                Conn.Dispose();
            }
        }

        protected void btnUserInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            
            //Cmd命令，更新用戶個人資料
            SqlCommand Cmd = new SqlCommand("UPDATE Administrator SET UserName =@UserName, LastName =@LastName, FirstName =@FirstName, BirthDate =@BirthDate, Gender =@Gender,AdminRight =@AdminRight  WHERE (AdminID = @AdminID)", Conn);
            Cmd.Parameters.AddWithValue("@AdminID", id);
            Cmd.Parameters.AddWithValue("@UserName", txbUserName.Value);
            Cmd.Parameters.AddWithValue("@LastName", txbLastName.Value);
            Cmd.Parameters.AddWithValue("@FirstName", txbFirstName.Value);
            Cmd.Parameters.AddWithValue("@BirthDate", txbBirthDate.Value);
            Cmd.Parameters.AddWithValue("@Gender", rdbtnGender.SelectedValue);
            Cmd.Parameters.AddWithValue("@AdminRight", CheckBoxValue());
            Cmd.ExecuteNonQuery();

            Cmd.Cancel();
            Conn.Close();
            Conn.Dispose();
            Response.Redirect("MemberEdit.aspx");//頁面跳轉更新
        }

        //由資料庫中來建立CheckBoxList
        private void CheckBoxListBuild()
        {
            //建立CheckBoxList與資料庫的繫結，並匯入資料
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT ItemText, RightNumber FROM SideMenu", Conn);
            SqlDataReader Reader = Cmd.ExecuteReader();

            //繫結CheckBoxList
            rightCheckBox.DataSource = Reader;
            //設定Value
            rightCheckBox.DataValueField = "RightNumber";
            //設定Text
            rightCheckBox.DataTextField = "ItemText";
            rightCheckBox.DataBind();
            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        //CheckBoxList取值方法
        private string CheckBoxValue()
        {
            string x = "";
            foreach (ListItem item in rightCheckBox.Items)
            {
                if (item.Selected == true)
                {
                    if (x.Length > 0)
                    {
                        x += ",";
                    }
                    x += item.Value;
                }
            }
            return x;
        }

        //從資料庫中判斷CheckBoxList項目是否已被勾選
        private void CheckBoxSelected()
        {
            string id = Request.QueryString["ID"];
            //建立CheckBoxList與資料庫的繫結，並匯入資料
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT AdminRight FROM Administrator WHERE AdminID=@AdminID", Conn);
            Cmd.Parameters.AddWithValue("@AdminID", id);
            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.Read())
            {
                foreach (ListItem item in rightCheckBox.Items)
                {
                    //判斷資料庫中的AdminRight，是否有包含rightCheckBox中各項目的Value，若有包含（不為-1）則打勾
                    if ((Reader["AdminRight"].ToString().IndexOf(item.Value) != -1))
                    {
                        item.Selected = true;
                    }
                }
            }
            Cmd.Cancel();
            Reader.Close();
            Conn.Close();
            Conn.Dispose();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberEdit.aspx");
        }
    }
}