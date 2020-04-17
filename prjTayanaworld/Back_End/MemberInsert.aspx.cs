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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckBoxListBuild();
            }
        }
        protected void btnInsertMember_Click(object sender, EventArgs e)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand insertCmd = new SqlCommand("INSERT INTO Administrator (UserName, Account, Password, LastName, FirstName, BirthDate, Gender,AdminRight) VALUES (@UserName,@Account,@Password,@LastName,@FirstName,@BirthDate,@Gender,@AdminRight)", Conn);
            insertCmd.Parameters.AddWithValue("@UserName", txbUserName.Value);
            insertCmd.Parameters.AddWithValue("@Account", txbAccount.Value);
            insertCmd.Parameters.AddWithValue("@Password",FormsAuthentication.HashPasswordForStoringInConfigFile(txbPassword.Value.Trim(), "SHA1"));
            insertCmd.Parameters.AddWithValue("@LastName", txbLastName.Value);
            insertCmd.Parameters.AddWithValue("@FirstName", txbFirstName.Value);
            insertCmd.Parameters.AddWithValue("@BirthDate", txbBirthDate.Value);
            insertCmd.Parameters.AddWithValue("@Gender", rdolstGender.SelectedValue);
            insertCmd.Parameters.AddWithValue("@AdminRight", CheckBoxValue());
            //執行命令
            insertCmd.ExecuteNonQuery();

            insertCmd.Cancel();
            Conn.Close();
            Conn.Dispose();

            Response.Redirect("MemberEdit.aspx");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberEdit.aspx");
        }
    }
}