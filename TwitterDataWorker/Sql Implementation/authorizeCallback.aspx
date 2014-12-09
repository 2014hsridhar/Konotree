<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizeCallback.aspx.cs" Inherits="TwitterApplication1.AuthorizeCallback" %>

<%--<%@ MasterType VirtualPath="~/Site.Master" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>WHAT'S UP</h2>
        </div>
        <asp:TextBox ID="txtUpdateStatus" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSend" runat="server" OnClick="Button1_Click" Text="Update" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="[lblText]"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <br />
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </form>
</body>
</html>
