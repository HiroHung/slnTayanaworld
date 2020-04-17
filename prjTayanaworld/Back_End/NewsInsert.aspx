<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="NewsInsert.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <div class="col-lg-12">
            <h1 class="page-header">發布新聞內容</h1>
        </div>
        <!--end page header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <!-- Form Elements -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    內容編輯
                </div>
                <div class="panel-body">
                    <span>新聞標題：</span>
                    <asp:TextBox ID="txbNewsTitle" runat="server" Width="80%" class="form-control"></asp:TextBox><br>
                    <span>新聞簡介：</span>
                    <asp:TextBox ID="txbIntroduction" runat="server" Width="80%" class="form-control"></asp:TextBox><br>
                    <div style="display: flex" class="form-group">
                        <div>
                            <asp:TextBox ID="txbNewsContent" runat="server" TextMode="MultiLine" class="ckeditor" ValidateRequestMode="Disabled"></asp:TextBox><br />
                            <span>是否置頂：</span>
                            <label class="radio-inline">
                                <asp:RadioButtonList ID="rdolstSticky" runat="server" CellSpacing="0" RepeatDirection="Horizontal" Width="100px">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </label>
                            <br>
                            <br>
                            <p>
                                <asp:Button ID="btnPostNews" runat="server" OnClick="btnPostNews_Click" Text="發布新聞" class="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" class="btn btn-success" />
                            </p>
                        </div>
                        <div style="margin: 0 5%;">
                            <span>設定預覽圖片：<asp:Label ID="lblMessage" runat="server" Font-Names="微軟正黑體" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                            </span>
                            <asp:FileUpload ID="NewsPhotoUpload" runat="server" onchange="onLoadBinaryFile()" />
                            <asp:Image ID="preImg" runat="server" Width="450px" Height="450px" BorderColor="Black" BorderStyle="Dotted" BorderWidth="1px" ImageUrl="~/Back_End/assets/img/preImgDefault.jpg" />
                            <script type="text/javascript"> //預覽圖片的JS
                                function onLoadBinaryFile() {
                                    var theFile = document.getElementById("<%=NewsPhotoUpload.ClientID %>");
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
                        </div>
                    </div>
                </div>
                <!-- End Form Elements -->
            </div>
        </div>
    </div>
</asp:Content>
