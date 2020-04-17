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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //連接資料庫
            SqlConnection Conn = new SqlConnection(ConnectionString);
            Conn.Open();
            //Cmd命令
            SqlCommand Cmd = new SqlCommand("SELECT * FROM Administrator WHERE Account =@Account AND Password=@Password", Conn);
            Cmd.Parameters.AddWithValue("@Account", txbEmail.Text);
            Cmd.Parameters.AddWithValue("@Password", FormsAuthentication.HashPasswordForStoringInConfigFile(txbPassword.Text.Trim(), "SHA1"));
            //Reader
            SqlDataReader Reader = Cmd.ExecuteReader();
            //比對資料
            if (!Reader.Read())
            {
                lblMessage.Visible = true;
                Cmd.Cancel();
                Reader.Close();
                Conn.Close();
                Conn.Dispose();
            }
            else
            {
                lblMessage.Visible = false;
                //通過帳號、密碼的標記MemberID
                string OK = Reader["AdminID"].ToString();
                Session["Login"] = OK;
                Cmd.Cancel();
                Reader.Close();
                Conn.Close();
                Conn.Dispose();

                Response.Redirect("index.aspx");
            }
        }
    }
}