<%@ Page Title="" Language="C#" MasterPageFile="~/Front_End/FrontMaster.Master" AutoEventWireup="true" CodeBehind="new_view.aspx.cs" Inherits="prjTayanaworld.Front_End.WebForm5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner02_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->
    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="new_list.aspx">News & Events</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="new_list.aspx">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>News & Events</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <asp:Literal ID="litNewsContent" runat="server"></asp:Literal>

                <%--<!--下載開始-->
                <div class="downloads">
                    <p>
                        <img src="images/downloads.gif" alt="&quot;&quot;" /></p>
                    <ul>
                        <li><a href="#">Downloads 001</a></li>
                        <li><a href="#">Downloads 001</a></li>
                        <li><a href="#">Downloads 001</a></li>
                        <li><a href="#">Downloads 001</a></li>
                        <li><a href="#">Downloads 001</a></li>
                    </ul>
                </div>
                <!--下載結束-->--%>

                <div class="buttom001">
                    <a href="new_list.aspx">
                        <img src="images/back.gif" alt="&quot;&quot;" width="55" height="28" /></a>
                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

    <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
