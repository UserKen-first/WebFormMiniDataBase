<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TryCookie.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        帳號<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
        密碼<asp:TextBox ID="txtPWD" runat="server"></asp:TextBox><br />
        
        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" /><br />
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </form>
</body>
</html>
