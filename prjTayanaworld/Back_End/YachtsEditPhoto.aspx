<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="YachtsEditPhoto.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm16" %>

<%@ Register Src="~/Pages.ascx" TagPrefix="uc1" TagName="Pages" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <h1 class="page-header">遊艇船型管理</h1>
        <!--end page header -->
    </div>
    <div class="row">
        <!-- Form Elements -->
        <div class="panel panel-default">
            <div class="panel-heading">
                船型照片新增
            </div>
            <div class="panel-body">
                <div class="row" style="margin: 0 auto">
                    <div class="col-lg-8">
                        <div class="panel-heading" style="background: #5bc0de; color: white">照片列表</div>
                        <div style="margin-bottom: 1%">
                            <asp:GridView ID="gvYachtsPhoto" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvYachtsPhoto_RowDeleting" CssClass="table table-hover text-center">
                                <Columns>
                                    <asp:TemplateField HeaderText="圖片預覽">
                                        <ItemTemplate>
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#"/Back_End/YachtsImg/"+ "mini"+Eval("FileName") %>' Width="50px" Height="50px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Introduction" HeaderText="照片簡述" SortExpression="Introduction" />
                                    <asp:BoundField DataField="UploadDate" HeaderText="上傳時間" SortExpression="UploadDate" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="刪除資料">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除此張照片嗎？')" CssClass="btn btn-outline btn-danger"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="col-lg-4" style="margin-bottom: 5%;">
                        <div class="panel-heading" style="background: #5bc0de; color: white">圖片上傳</div>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="onLoadBinaryFile()" style="margin-bottom: 1%;margin-top: 5%;"  />
                        <span style="margin: 1px"><asp:Image ID="Image1" runat="server" Width="200px" Height="200px" BorderColor="Black" BorderStyle="Dotted" BorderWidth="1px" ImageUrl="~/Back_End/assets/img/preImgDefault.jpg" /></span>
                        

                        <p>
                            照片簡述：<br />
                            <asp:TextBox ID="txbIntroduction" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </p>
                        <%--預覽圖片的JS--%>
                        <script type="text/javascript"> 
                            function onLoadBinaryFile() {
                                var theFile = document.getElementById("<%=FileUpload1.ClientID %>");
                                // 確定選取了一個二進位檔案，而非其他格式。
                                if (theFile.files.length != 0 && theFile.files[0].type.match(/image.*/)) {
                                    var reader = new FileReader();
                                    reader.onload = function (e) {
                                        var theImg = document.getElementById("<%=Image1.ClientID %>");
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
                    <p style="padding-right: 15px;text-align: end;">
                        <asp:Button ID="btnPhotoInsert" class="btn btn-primary" runat="server" Text="新增照片" OnClick="btnPhotoInsert_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="離開相簿設定" class="btn btn-success" UseSubmitBehavior="False" OnClick="btnCancel_Click" />
                    </p>
                </div>
                <uc1:Pages runat="server" ID="Pages" />
            </div>
        </div>
        <!-- End Form Elements -->

    </div>
</asp:Content>
