<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fullRaven.aspx.cs" Inherits="TwitterDataWorker.fullRaven" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Twitter Data Aggregator + Extractor</title>
</head>
<body>
    <h1>Working with Twitter Data</h1>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label">Twitter User Info (1 person)</asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> <br />
    </div>
    <div style="overflow-y: scroll; width: 200px; height: 340px; border: 1px solid gray">
        <asp:CheckBoxList ID="InfoList" runat="server">
        </asp:CheckBoxList> <br />
     </div>
    <div>
        <asp:DataList ID="DataList1" runat="server"></asp:DataList><asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
