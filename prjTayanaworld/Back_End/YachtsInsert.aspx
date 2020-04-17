<%@ Page Title="" Language="C#" MasterPageFile="~/Back_End/MainSite.Master" AutoEventWireup="true" CodeBehind="YachtsInsert.aspx.cs" Inherits="prjTayanaworld.Back_End.WebForm15" %>

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
                新增遊艇資料
            </div>
            <div class="panel-body">
                <div>
                    遊艇系列：<asp:TextBox ID="txbYachtsName" runat="server" required="required"></asp:TextBox>
                    <br />
                    <br />
                    遊艇型號：<asp:TextBox ID="txbModel" runat="server" required="required"></asp:TextBox><br />
                    <br />
                    新船型標記：<asp:RadioButtonList ID="rdoNewType" runat="server" CellSpacing="0" RepeatDirection="Horizontal" Width="100px" required="required">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:RadioButtonList><br />
                    <div class="panel-heading" style="background: #5bc0de; color: white">Overview</div>
                    <div style="margin-bottom: 1%">
                        <asp:TextBox ID="txbOverview" runat="server" TextMode="MultiLine" class="ckeditor" ValidateRequestMode="Disabled"></asp:TextBox><br />
                        上傳船型規格書：<asp:FileUpload ID="fulOverviewDownload" runat="server" ClientIDMode="Static" />
                        <asp:Label ID="lblMessage" runat="server" Font-Names="微軟正黑體" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="panel-heading" style="background: #5bc0de; color: white">Layout & deck plan</div>
                    <div style="margin-bottom: 1%">
                        <asp:TextBox ID="txbLayoutdeckplan" runat="server" TextMode="MultiLine" class="ckeditor" ValidateRequestMode="Disabled"></asp:TextBox>
                    </div>
                    <div class="panel-heading" style="background: #5bc0de; color: white">Specifications</div>
                    <div style="margin-bottom: 1%">
                        <asp:TextBox ID="txbSpecifications" runat="server" TextMode="MultiLine" class="ckeditor" ValidateRequestMode="Disabled"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnNextStep" runat="server" Text="下一步，設定遊艇照片" class="btn btn-info" Width="50%" OnClick="btnNextStep_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" class="btn btn-success" UseSubmitBehavior="False" OnClick="btnCancel_Click" />
                </div>
            </div>
            <!-- End Form Elements -->
        </div>
    </div>
</asp:Content>
