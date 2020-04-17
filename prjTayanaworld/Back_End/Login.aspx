<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登入後台系統</title>
    <link href="assets/plugins/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="assets/plugins/pace/pace-theme-big-counter.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
    <link href="assets/css/main-style.css" rel="stylesheet" />
    <script src="assets/plugins/jquery-1.10.2.js"></script>
    <script src="assets/plugins/bootstrap/bootstrap.min.js"></script>
    <script src="assets/plugins/metisMenu/jquery.metisMenu.js"></script>
</head>
<body class="body-Login-back">
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div class="col-md-4 col-md-offset-4 text-center logo-margin ">
                    <img src="assets/img/logo2.png" alt="" />
                </div>
                <div class="col-md-4 col-md-offset-4">
                    <div class="login-panel panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title" style="font-family: 微軟正黑體">請登入系統</h3>
                        </div>
                        <div class="panel-body">
                            <fieldset>
                                    <div class="form-group">                                       
                                        <asp:TextBox ID="txbEmail" runat="server" type="email" class="form-control" placeholder="E-mail" required="required"></asp:TextBox>
                                    </div>
                                    <div class="form-group">     
                                        <asp:TextBox ID="txbPassword" runat="server" type="password" class="form-control" placeholder="Password" required="required"></asp:TextBox>
                                    </div>                                  
                                        <asp:Label ID="lblMessage" runat="server" Text="帳號密碼有誤" ForeColor="Red" Font-Size="Medium" Visible="False"></asp:Label>                   
  
                                    <asp:Button ID="btnLogin" runat="server" Text="登入" class="btn btn-lg btn-success btn-block" Font-Names="微軟正黑體" OnClick="btnLogin_Click" />
                                </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
