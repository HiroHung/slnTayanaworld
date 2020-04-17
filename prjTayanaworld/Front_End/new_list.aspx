<%@ Page Title="" Language="C#" MasterPageFile="~/Front_End/FrontMaster.Master" AutoEventWireup="true" CodeBehind="new_list.aspx.cs" Inherits="prjTayanaworld.Front_End.WebForm4" %>

<%@ Register Src="~/Pages.ascx" TagPrefix="uc1" TagName="Pages" %>
<%@ Register Src="~/FrontPages.ascx" TagPrefix="uc1" TagName="FrontPages" %>



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
                    <li><a href="#">News & Events</a></li>
                </ul>
            </div>
        </div>

        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>News & Events</span></div>

                <!--------------------------------內容開始---------------------------------------------------->

                <div class="box2_list">
                    <ul>
                        <asp:Repeater ID="Repeater" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="list01">
                                        <ul>
                                            <li>
                                                <div>
                                                    <p>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"/Back_End/NewsImg/"+ Eval("PrePhotoName") %>' Width="161px" Height="121px" />
                                                    </p>
                                                </div>
                                            </li>
                                            <li><span>
                                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("NewsDate") %>'></asp:Literal></span><br>
                                                <a href='<%#"new_view.aspx?id="+ Eval("id") %>'>
                                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("NewsTitle") %>'></asp:Literal></a>
                                            </li>
                                            <br>
                                            <li>
                                                <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("Introduction") %>'></asp:Literal></li>
                                        </ul>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                        <div class="pagenumber">
                            <uc1:FrontPages runat="server" id="FrontPages" />
                           <%-- | <span>1</span> | <a href="#">2</a> | <a href="#">3</a> | <a href="#">4</a> | <a href="#">5</a> |  <a href="#">Next</a>  <a href="#">LastPage</a>--%>
                        </div>
                        <%-- <div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
                    </ul>
                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
