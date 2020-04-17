<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="NewsSearch.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm4" %>
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
                    新聞搜尋結果
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div>
                            <div style="margin: 20px 20px" class="form-group">
                                <div class="panel-heading" style="background: #5bc0de; color: white">新聞列表</div>
                                    <asp:GridView ID="NewsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="NewsGridView_RowDeleting" CssClass="table table-hover text-center">
                                        <Columns>
                                            <asp:BoundField DataField="NewsDate" HeaderText="刊登日期" SortExpression="NewsDate" />
                                            <asp:BoundField DataField="NewsTitle" HeaderText="新聞標題" SortExpression="NewsTitle" HeaderStyle-Width="500px" >
<HeaderStyle Width="500px"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="預覽縮圖">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#"/Back_End/NewsImg/"+ Eval("PrePhotoName") %>' Width="50px" Height="50px"/>
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
                                    </asp:GridView>
                                <div class="text-center">
                                    <asp:Label ID="lblError" runat="server" Text="查無資料！" Font-Size="XX-Large" ForeColor="Red" Visible="False"></asp:Label></div>
                                <div style="display: flex;justify-content: space-between"><asp:Button ID="btnNewsInsert"  class="btn btn-success" runat="server" Text="回到新聞管理主頁" OnClick="btnNewsInsert_Click" Width="30%" /></div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
