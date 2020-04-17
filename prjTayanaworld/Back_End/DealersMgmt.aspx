<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="DealersMgmt.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm11" %>

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
                區域編輯與新增
            </div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="form-group" style="margin: 20px 20px">
                        <div class="panel-heading" style="background: #5bc0de; color: white">區域列表</div>
                        <asp:GridView ID="gvContinent" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDeleting="gvContinent_RowDeleting" CssClass="table table-hover text-center">
                            <Columns>
                                <asp:BoundField DataField="Continent" HeaderText="國際／洲際 地區" SortExpression="Continent" />
                                <asp:TemplateField ShowHeader="False" HeaderText="編輯內容">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="" Text="更新"></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" class="btn btn-outline btn-primary" PostBackUrl='<%# "~/Back_End/DealersList.aspx?id=" + Eval("id") %>'></asp:LinkButton>
                                        &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="刪除資料">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" class="btn btn-outline btn-danger" CommandName="Delete" OnClientClick="return confirm('您確定要刪除此地區，以及其所包含的經銷商資料嗎？')" Text="刪除"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        新增國際／洲際 地區：
                    <asp:TextBox ID="txbInsertContinent" runat="server" class="form-control" Height="15%" Width="40%" required="required"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnInsertContinent" runat="server" Text="新增資料" class="btn btn-primary" OnClick="btnInsertContinent_Click" />
                    </div>
                    <!-- End Form Elements -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
