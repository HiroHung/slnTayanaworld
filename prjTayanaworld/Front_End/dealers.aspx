<%@ Page Title="" Language="C#" MasterPageFile="~/Front_End/FrontMaster.Master" AutoEventWireup="true" CodeBehind="dealers.aspx.cs" Inherits="prjTayanaworld.Front_End.WebForm8" %>

<%@ Register Src="~/FrontPages.ascx" TagPrefix="uc1" TagName="FrontPages" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="371" />
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
                <p><span>DEALERS</span></p>
                <ul>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li><a href='<%#"dealers.aspx?id="+Eval("id") %>'>
                                <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("Continent") %>'></asp:Literal></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> <a href="dealers.aspx">Dealers </a>>> <a href="#"><span class="on1">
                <asp:Literal ID="litCrumb" runat="server"></asp:Literal></span></a>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>
                        <asp:Literal ID="litContinent" runat="server"></asp:Literal></span>
                </div>
                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">
                    <ul>
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="list02">
                                        <ul>
                                            <li class="list02li">
                                                <div>
                                                    <p>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"/Back_End/DealersImg/"+"mini"+ Eval("ContactPhoto") %>' />
                                                    </p>
                                                </div>
                                            </li>
                                            <li class="list02li02"><span>
                                                <asp:Literal ID="litRegion" runat="server" Text='<%# Eval("Region") %>'></asp:Literal></span><br />
                                                <asp:Literal ID="litTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Literal><br />
                                                <asp:Literal ID="litDealersInfo" runat="server" Text='<%# Eval("DealersInfo") %>'></asp:Literal>
                                            </li>
                                        </ul>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="pagenumber">
                        <uc1:FrontPages runat="server" ID="FrontPages" />
                       <%-- | <span>1</span> | <a href="#">2</a> | <a href="#">3</a> | <a href="#">4</a> | <a href="#">5</a> |  <a href="#">Next</a>  <a href="#">LastPage</a>--%>
                    </div>
                    <%--<div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
