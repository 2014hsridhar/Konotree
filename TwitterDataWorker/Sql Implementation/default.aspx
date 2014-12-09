%<%@ Page Title="ASP.NET Tweets-Demo" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TwitterApplication1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Twitter API: HTTP:/TWEETSHARP.CODEPLEX.COM/<br />
  Submit &quot;Login&quot; button to retrieve a list of user informaiton.
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="btnAuthenticate_Click" />
    
        <br />
        <asp:Label ID="Label1" runat="server">URL: </asp:Label>
    
    </div>
    </form>
</body>
</html>
