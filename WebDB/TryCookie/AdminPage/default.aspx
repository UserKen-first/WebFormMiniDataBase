<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TryCookie.AdminPage._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>

        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click"/>
    </form>
</body>
</html>
