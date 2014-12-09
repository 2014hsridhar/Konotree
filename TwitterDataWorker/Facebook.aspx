<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="TwitterDataWorker.Facebook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Working with Facebook Data Collection</h2>
        <div>
            This application will return a list user data from facebook
            <br />
            <asp:Button ID="btnFacebookLogin" runat="server" OnClick="login_Click" Text="Login" Width="130px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFacebookLogout" runat="server" OnClick="logout_Click" Text="Logout" Width="130px" />
            <br />
            <asp:Label ID="NameText" runat="server" Text="" Font-Size="Small" Visible="false"></asp:Label>

            &nbsp;<asp:Button ID="btnAggregate" runat="server" OnClick="aggregate_click" Text="Aggregate" Width="134px" />
            &nbsp;<p>
                &nbsp;
            </p>
        </div>
                <div style="clear: left; float: left; margin-right: 80px; margin-bottom: 40px">
            <asp:Label ID="Label2" runat="server" Text="Your Info"></asp:Label>
            <br />
            <div style="overflow-y: scroll; width: 200px; height: 340px; border: 1px solid gray">
                <asp:CheckBoxList ID="FriendList" runat="server">
                </asp:CheckBoxList>
            </div>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="116px">
                <asp:ListItem>-Select-</asp:ListItem>
                <asp:ListItem>CSV/Text Report</asp:ListItem>
                <asp:ListItem>Excel Spreadsheet</asp:ListItem>
                <asp:ListItem>PDF </asp:ListItem>
                <asp:ListItem>HTML</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label1" runat="server" />
        </div>
        <div style="clear: both;">
            <asp:Label ID="Label3" runat="server" Text="Status: " Font-Bold="true"></asp:Label>
            <asp:Label ID="Status" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
