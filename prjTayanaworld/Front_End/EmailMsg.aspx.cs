using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjTayanaworld.Front_End
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["send"] == "success")
                {
                    lblMsg.Text = "Email has been sent!";
                }
                if (Request.QueryString["send"] == "false")
                {
                    lblMsg.Text = "Erro!Email could not be sent!";
                }
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("contact.aspx");
        }
    }
}