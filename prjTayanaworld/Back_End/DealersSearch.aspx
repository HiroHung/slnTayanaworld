<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="DealersSearch.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm21" %>
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
                經銷商編輯與新增
            </div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="panel-heading" style="background: #5bc0de; color: white">經銷商列表</div>
                    <asp:GridView ID="gvRegion" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvRegion_RowDeleting" CssClass="table table-hover text-center">
                        <Columns>
                            <asp:BoundField DataField="Region" HeaderText="分部區域" SortExpression="Region" />
                            <asp:BoundField DataField="Title" HeaderText="職稱" SortExpression="Title" />
                            <asp:TemplateField HeaderText="聯絡人相片" SortExpression="ContactPhoto">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ContactPhoto") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%#"/Back_End/DealersImg/"+ Eval("ContactPhoto") %>' Width="50px" Height="50px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderText="編輯內容">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="btnRegionEdit" runat="server" NavigateUrl='<%#"DealersEdit.aspx?id="+Eval("id")%>' Text="編輯" class="btn btn-outline btn-primary"></asp:HyperLink>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除資料">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" class="btn btn-outline btn-danger" CommandName="Delete" OnClientClick="return confirm('您確定要刪除此經銷商嗎？')" Text="刪除"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="text-center">
                        <asp:Label ID="lblError" runat="server" Text="查無資料！" Font-Size="XX-Large" ForeColor="Red" Visible="False"></asp:Label>
                    </div>
                    <asp:Button ID="btnCancel" runat="server" Text="返回上一頁" class="btn btn-success" UseSubmitBehavior="False" OnClick="btnCancel_Click" Width="30%" />
                    <br />
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
