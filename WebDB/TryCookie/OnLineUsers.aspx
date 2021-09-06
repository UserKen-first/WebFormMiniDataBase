<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnLineUsers.aspx.cs" Inherits="TryCookie.OnLineUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           目前線上人數： <asp:Literal ID="ItOnlineUser" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
