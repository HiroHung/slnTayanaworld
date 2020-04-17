<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="DealersInsert.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div class="row">
        <!-- page header -->
        <h1 class="page-header">經銷商管理</h1>
        <!--end page header -->
    </div>
    <div class="row">
        <!-- Form Elements -->
        <div class="panel panel-default">
            <div class="panel-heading">
                新增經銷商
            </div>
            <div class="panel-body" style="display: flex">
                <div>
                    <ul >
                        <li>分部區域：<asp:TextBox ID="txbRegion" runat="server" class="form-control" Width="30%"></asp:TextBox></li>
                        <li>職稱：<asp:TextBox ID="txbTitle" runat="server" class="form-control" Width="30%"></asp:TextBox></li>
                        <li>經銷商聯絡人相片：<asp:Label ID="lblMessage" runat="server" Font-Names="微軟正黑體" ForeColor="Red" Visible="False"></asp:Label>
                            <asp:FileUpload ID="ContactPhotoUpload" runat="server" onchange="onLoadBinaryFile()" />
                            <asp:Image ID="preImg" runat="server" Width="200px" Height="200px" BorderColor="Black" BorderStyle="Dotted" BorderWidth="1px" ImageUrl="~/Back_End/assets/img/preImgDefault.jpg" />
                            <script type="text/javascript"> //預覽圖片的JS
                                function onLoadBinaryFile() {
                                    var theFile = document.getElementById("<%=ContactPhotoUpload.ClientID %>");
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
                        </li>
                        <li>經銷商資訊：<asp:TextBox ID="ckeDealersInfo" runat="server" TextMode="MultiLine" class="ckeditor" ValidateRequestMode="Disabled"></asp:TextBox></li>
                        <br>
                        <li style="list-style: none; display: flex; justify-content: flex-end">
                            <asp:Button ID="btnInsert" runat="server" Text="新增資料" class="btn btn-primary" OnClick="btnInsert_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消" class="btn btn-success" UseSubmitBehavior="False" OnClick="btnCancel_Click" />
                        </li>
                    </ul>
                </div>
                <br />
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
