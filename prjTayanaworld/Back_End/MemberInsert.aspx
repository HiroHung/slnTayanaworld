<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="MemberInsert.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <!-- page header -->
        <div class="col-lg-12">
            <h1 class="page-header">成員資料管理</h1>
        </div>
        <!--end page header -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <!-- Form Elements -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    成員新增
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div>
                            <div style="margin: 20px 20px" class="form-group">
                                <ul style="padding-inline-start: 20px;">
                                    <li>帳號（E-mail）：<input runat="server" id="txbAccount" class="form-control" required type="email"/></li>
                                    <li>姓：<input runat="server" id="txbLastName" class="form-control" required/></li>
                                    <li>名字：<input runat="server" id="txbFirstName" class="form-control" required/></li>
                                    <li>暱稱：<input runat="server" id="txbUserName" class="form-control" required/></li>
                                    <li>密碼：<input runat="server" id="txbPassword" class="form-control" required type="password"/></li>
                                    <li>密碼確認：<input runat="server" id="txbCkPassword" class="form-control" required type="password"/></li>
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ErrorMessage="密碼確認不相符" ControlToCompare="txbPassword" ControlToValidate="txbCkPassword" ForeColor="Red"></asp:CompareValidator>
                                    <li>生日：<input runat="server" id="txbBirthDate" class="form-control" type="date" required/></li>
                                    <li>性別：
                                            <label class="radio-inline">
                                                <%--<input type="radio" name="optionsRadiosInline" id="optionsRadiosInline1" value="option1" checked>1--%>
                                                <asp:RadioButtonList ID="rdolstGender" runat="server" CellSpacing="1" RepeatDirection="Horizontal" Width="250px">
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
                                    <li>成員權限：
                                        <asp:CheckBoxList ID="rightCheckBox" runat="server">
                                        </asp:CheckBoxList>
                                    </li>
                                    <li style="list-style: none; display: flex; justify-content: flex-end">
                                        <asp:Button ID="btnInsertMember" runat="server" Text="新增成員" OnClick="btnInsertMember_Click" class="btn btn-primary"/>&nbsp&nbsp<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" class="btn btn-success" UseSubmitBehavior="False" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
