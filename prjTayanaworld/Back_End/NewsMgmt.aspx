<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="NewsMgmt.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm8" %>

<%@ Register Src="~/Pages.ascx" TagPrefix="uc1" TagName="Pages" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <div class="col-lg-12">
            <h1 class="page-header">新聞管理</h1>
        </div>
        <!--end page header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <!-- Form Elements -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    新聞資料編輯與新增
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group" style="margin: 20px 20px">
                                <div class="panel-heading" style="background: #5bc0de; color: white">關鍵字搜尋</div>
                                <asp:TextBox ID="txbKeyword" runat="server" CssClass="form-control"></asp:TextBox><br />
                                <asp:Button ID="btnKeywordSearch" runat="server" Text="搜尋" OnClick="btnKeywordSearch_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group" style="margin: 20px 20px">
                                <div class="panel-heading" style="background: #5bc0de; color: white">時間搜尋</div>
                                <asp:TextBox ID="txbStart" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox ID="txbEnd" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />
                                <asp:Button ID="btnDateSearch" runat="server" Text="搜尋" OnClick="btnDateSearch_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div style="margin: 20px 20px" class="form-group">
                                <div class="panel-heading" style="background: #5bc0de; color: white">新聞列表</div>
                                <asp:GridView ID="NewsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="NewsGridView_RowDeleting" CssClass="table table-hover text-center">
                                    <Columns>
                                        <asp:BoundField DataField="NewsDate" HeaderText="刊登日期" SortExpression="NewsDate" />
                                        <asp:BoundField DataField="NewsTitle" HeaderText="新聞標題" SortExpression="NewsTitle" HeaderStyle-Width="500px">
                                            <HeaderStyle Width="500px"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="預覽縮圖">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#"/Back_End/NewsImg/"+"mini"+ Eval("PrePhotoName") %>' Width="100px" Height="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="編輯內容">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="btnNewsEdit" class="btn btn-outline btn-primary" runat="server" NavigateUrl='<%# "NewsEdit.aspx?ID=" + Eval("id") %>' Text="編輯"></asp:HyperLink>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="刪除資料">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnNewsDel" runat="server" CausesValidation="False" class="btn btn-outline btn-danger" CommandName="Delete" OnClientClick="return confirm('您確定要刪除此則新聞嗎？')" Text="刪除"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否置頂">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Sticky").ToString() == "1" ? "是" : "否" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView><br/>
                                <div style="display: flex; justify-content: space-between">
                                    <asp:Button ID="btnNewsInsert" class="btn btn-primary" runat="server" Text="新增新聞" OnClick="btnNewsInsert_Click" Width="30%" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <uc1:Pages runat="server" ID="Pages" />
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
