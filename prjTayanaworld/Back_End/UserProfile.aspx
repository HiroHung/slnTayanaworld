<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <div class="col-lg-12">
            <h1 class="page-header">用戶資料管理</h1>
        </div>
        <!--end page header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <!-- Form Elements -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    修改個人資料
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group text-center" style="margin: 20px 20px">
                                <div class="panel-heading" style="background: #5bc0de; color: white">上傳個人頭像</div>
                                <%--<input type="file">--%>
                                <br />
                                <asp:FileUpload ID="PhotoUpload" runat="server" onchange="onLoadBinaryFile()"/>
                                <asp:Image ID="preImg" runat="server" Width="60%" Height="60%" BorderColor="Black" BorderStyle="Dotted" BorderWidth="1px" ImageUrl="~/Back_End/assets/img/preImgDefault.jpg" /><br />
                                <script type="text/javascript"> //預覽圖片的JS
                                    function onLoadBinaryFile() {
                                        var theFile = document.getElementById("<%=PhotoUpload.ClientID %>");
                                    // 確定選取了一個二進位檔案，而非其他格式。
                                    if (theFile.files.length != 0 && theFile.files[0].type.match(/image.*/)) {
                                        var reader = new FileReader();

                                        reader.onload = function (e) {
                                            var theImg = document.getElementById("<%=preImg.ClientID %>");
                                                theImg.src = e.target.result;
                                            };
                                            reader.onerror = function (e) {
                                                alert("例外狀況，無法讀取圖檔");
                                            };
                                            // 讀取二進位檔案。
                                            reader.readAsDataURL(theFile.files[0]);
                                        }
                                        else {
                                            alert("請選取一個圖片類型檔案");
                                        }
                                    }
                                </script>
                                <br />
                                <asp:Button type="button" ID="btnPhtoUpload" runat="server" Font-Names="微軟正黑體" OnClick="btnPhtoUpload_Click" Text="檔案送出" UseSubmitBehavior="False" class="btn btn-primary" />&nbsp&nbsp<asp:Button ID="btnImgCancel" runat="server" OnClick="btnCancel_Click" Text="重置" class="btn btn-success" UseSubmitBehavior="False" />
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <div class="form-group" style="margin: 20px 20px">
                                <div class="panel-heading" style="background: #5bc0de; color: white">個人資料修改</div>
                                <label></label>
                                <ul>
                                    <li>姓：<input runat="server" id="txbLastName" class="form-control" required/></li>
                                    <li>名字：<input runat="server" id="txbFirstName" class="form-control" required/></li>
                                    <li>暱稱：<input runat="server" id="txbUserName" class="form-control" required/></li>
                                    <li>修改密碼：<input runat="server" id="txbPassword" class="form-control" type="password"/></li>
                                    <li>修改密碼確認：<asp:Label ID="lblError" runat="server" Text="請填入確認密碼！" ForeColor="Red" Visible="False"></asp:Label><input runat="server" id="txbCkPassword" class="form-control" type="password"/></li>
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ErrorMessage="密碼確認不相符" ControlToCompare="txbPassword" ControlToValidate="txbCkPassword" ForeColor="Red"></asp:CompareValidator>
                                    <li>生日：<input runat="server" id="txbBirthDate" class="form-control" type="date" required/></li>
                                    <br />
                                    <li>性別：
                                            <label class="radio-inline">
                                                <%--<input type="radio" name="optionsRadiosInline" id="optionsRadiosInline1" value="option1" checked>1--%>
                                                <asp:RadioButtonList ID="rdbtnGender" runat="server" CellSpacing="1" RepeatDirection="Horizontal" Width="250px">
                                                    <asp:ListItem Value="男">男</asp:ListItem>
                                                    <asp:ListItem Value="女">女</asp:ListItem>
                                                    <asp:ListItem Value="其他">其他</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <%--<asp:RadioButton ID="optionsRadiosInline1" runat="server" />--%>
                                            </label>
                                        <%-- <label class="radio-inline">
                                                <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline2" value="option2">2
                                                <asp:RadioButton ID="optionsRadiosInline2" runat="server" />
                                            </label>--%>
                                    </li>
                                </ul>
                                <div class="text-right"><asp:Button ID="btnUserInfo" runat="server" Text="修改資料送出" OnClick="btnUserInfo_Click" class="btn btn-primary" />&nbsp&nbsp<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="重置" class="btn btn-success" UseSubmitBehavior="False" /></div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
