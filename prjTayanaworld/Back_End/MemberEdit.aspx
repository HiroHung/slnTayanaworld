<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="MemberEdit.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <div class="col-lg-12">
            <h1 class="page-header">成員管理</h1>
        </div>
        <!--end page header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <!-- Form Elements -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    成員資料編輯與新增
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group" style="margin: 20px 20px">
                                <div class="panel-heading" style="background: #5bc0de; color: white">關鍵字搜尋</div>
                                <asp:TextBox ID="txbKeyword" runat="server" CssClass="form-control"></asp:TextBox><br />
                                <asp:Button ID="btnKeywordSearch" runat="server" Text="搜尋" CssClass="btn btn-primary" OnClick="btnKeywordSearch_Click" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div style="margin: 20px 20px" class="form-group">
                                <div class="panel-heading" style="background: #5bc0de; color: white">成員列表</div>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="AdminID" OnRowDeleting="GridView1_RowDeleting" CssClass="table table-hover text-center">
                                    <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="暱稱" SortExpression="UserName" />
                                        <asp:BoundField DataField="LastName" HeaderText="姓" SortExpression="LastName" />
                                        <asp:BoundField DataField="FirstName" HeaderText="名" SortExpression="FirstName" />
                                        <asp:BoundField DataField="BirthDate" HeaderText="生日" SortExpression="BirthDate" />
                                        <asp:BoundField DataField="Gender" HeaderText="性別" SortExpression="Gender" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="編輯內容">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" class="btn btn-outline btn-primary" runat="server" NavigateUrl='<%# "MemberEditDetail.aspx?ID=" + Eval("AdminID") %>'>編輯</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="刪除資料">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClientClick="return confirm('您確定要刪除此成員嗎？')" CommandName="Delete" Text="刪除" class="btn btn-outline btn-danger"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle Wrap="True" />
                                </asp:GridView>
                                <p>
                                    <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="新增成員" class="btn btn-primary" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
