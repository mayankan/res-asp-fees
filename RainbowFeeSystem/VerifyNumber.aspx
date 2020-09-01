<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyNumber.aspx.cs" Inherits="RainbowFeeSystem.VerifyDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VERIFY NUMBER - RAINBOW ONLINE FEES SYSTEM</title>
    <style type="text/css">
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
            width: 900px;
        }

        #footer {
            width: 100%;
            height: 5%;
            float: left;
            background-color: #ff0000;
            position: absolute;
            color:white;
            right: 0;
            bottom: 0;
            padding-bottom: 1rem;
            left: 0;
        }
    </style>
</head>
<body bgcolor="C5FFFF" style="margin: 0px; width: 100%; height: 100%;">
    <form id="form1" runat="server">
        <div style="height: 100%;">
            <img class="auto-style1" src="http://rainbowonlinefees.com/logo4.3-1%20(1).png" /><br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please enter the last 4 digits of your Mobile Number :&nbsp;
        <asp:Label ID="lblMobNo" runat="server"></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtMobileNo" runat="server" TextMode="Password" MaxLength="4"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnMobileNo" runat="server" OnClick="btnMobileNo_Click" Text="VERIFY" BackColor="Red" ForeColor="White" />
            &nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="vldMobileNo" runat="server" ErrorMessage="Please enter valid Mobile Number digits to verify" ForeColor="Red" ControlToValidate="txtMobileNo"></asp:RequiredFieldValidator>
            <br />
            <br />
            <br />
            <br />
            <br />
        <div id="footer">
            <div class="wrap">
                <p>© COPYRIGHT - <a href="http://www.rainbowschooljp.com">RAINBOW ENGLISH SCHOOL</a></p>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
