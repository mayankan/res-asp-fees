<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RainbowFeeSystem.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RAINBOW ONLINE FEE SYSTEM - Home</title>
    <style type="text/css">
        #btnAdmissionNo {
            height: 31px;
            width: 96px;
        }

        .hidden {
            display: none;
        }

        .auto-style1 {
            width: 100%;
            height: 140px;
            background-color: #ff0000;
        }

        .wrap {
            position: relative;
            margin: 0 auto;
            /*replace 900px with your width*/
            background-color: #ff0000;
            width: 900px;
        }

        #footer {
            width: 100%;
            height: 5%;
            float: left;
            background-color: #ff0000;
            position: absolute;
            right: 0;
            bottom: 0;
            padding-bottom: 1rem;
            left: 0;
            color:white;
        }

        .main_menu {
            padding-left: 25%;
            width: 75%;
            background-color: #ff0000;
            color: #fff;
            text-align: center;
            height: 30px;
            line-height: 30px;
            margin-right: 150px;
        }

        .level_menu {
            width: 110px;
            background-color: #000;
            color: #fff;
            text-align: center;
            height: 30px;
            line-height: 30px;
            margin-top: 5px;
        }

        .selected {
            background-color: #852B91;
            color: #fff;
        }
    </style>
</head>
<body bgcolor="#C5FFFF" style="margin: 0px; width: 100%; height: 100%;">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="IsOffline">
            <br />
            <br />
            <br />
            <br />
            <br />
            <h1 align="center">Rainbow Online Fees is down for Maintainence
            </h1>
            <h2 align="center">Please check back again soon.
            </h2>
        </asp:Panel>
        <asp:Panel runat="server" ID="NotOffline" Style="height: 100%;">
            <div>
                <div style="background-color: red">
                    <img class="auto-style1" src="http://rainbowonlinefees.com/logo4.3-1%20(1).png" />
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
                    <asp:Menu ID="Menu" runat="server" DataSourceID="SiteMapDataSource1" Orientation="Horizontal" OnMenuItemDataBound="OnMenuItemDataBound" CssClass="main_menu">
                        <LevelMenuItemStyles>
                            <asp:MenuItemStyle CssClass="main_menu" />
                            <asp:MenuItemStyle CssClass="level_menu" />
                        </LevelMenuItemStyles>
                    </asp:Menu>
                </div>
                <h1 style="color: darkblue; font-family:Georgia">&nbsp;</h1>
                <h1 style="color: darkblue; font-family:Georgia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    WELCOME TO ONLINE FEES PAYMENT SYSTEM</h1>
                <br />
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please enter Student&#39;s Admission Number :&nbsp;&nbsp;
        <asp:TextBox ID="txtAdmissionNo" runat="server" MaxLength="4" TextMode="Number"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdmissionNo" runat="server" OnClick="btnAdmissionNo_Click" Text="NEXT" BackColor="Red" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
                &nbsp;&nbsp;
        <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: red; text-decoration: underline">NOTE :</span> Only Number Input is allowed.
            <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="BackendText" />
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
                <div id="footer">
                    <div class="wrap">
                        <p>© COPYRIGHT - <a href="http://www.rainbowschooljp.com">RAINBOW ENGLISH SCHOOL</a><br />
                        </p>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
