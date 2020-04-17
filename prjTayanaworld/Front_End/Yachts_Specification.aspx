<%@ Page Title="" Language="C#" MasterPageFile="~/Front_End/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Yachts_Specification.aspx.cs" Inherits="prjTayanaworld.Front_End.WebForm3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <div class="banner">
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls" style="display: none">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%#"/Back_End/YachtsImg/"+Eval("FileName") %>'>
                                        <img src='<%#"/Back_End/YachtsImg/"+"mini"+Eval("FileName") %>' class="littleImg">
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>YACHTS</span></p>
                <ul>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li><a href='<%#"Yachts_Specification.aspx?id="+Eval("id") %>'>
                                <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("NewTypeMark").ToString()=="1"?Eval("YachtsName").ToString()+" "+ Eval("Model").ToString() +" "+"(New Building)":Eval("YachtsName").ToString() + " " +Eval("Model").ToString() %>'></asp:Literal></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>



            </div>




        </div>







        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> <a href="#">Yachts</a> >> <a href="#"><span class="on1">
                <asp:Literal ID="litCrumb" runat="server"></asp:Literal></span></a>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>
                        <asp:Literal ID="litTitle" runat="server"></asp:Literal></span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->

                <!--次選單-->
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <li><a class="menu_yli01" href="#" runat="server" id="urlOverview">Overview</a></li>
                        <li><a class="menu_yli02" href="#" runat="server" id="urlLayout">Layout & deck pla</a>n</li>
                        <li><a class="menu_yli03" href="#" runat="server" id="urlSpecification">Specification</a></li>
                    </ul>
                </div>

                <!--次選單-->
                <asp:Literal ID="litContent" runat="server"></asp:Literal>
                <p class="topbuttom">
                    <img src="images/top.gif" alt="top" />
                </p>




                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
