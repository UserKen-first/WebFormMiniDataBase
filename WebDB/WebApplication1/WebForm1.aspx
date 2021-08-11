<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script>
        // 要注意順序，先初始化，再執行
        <%--var val = <%= this.ForJSint %>;
        var val2 = <%= (this.FOrJSBOOL) ? "true" : "false" %>;
        var val3 = '<%= this.ForJSString %>';

        var ForJSBase = <%= this.ForJSBase %>;
        var ForJSCoefficient = <%= this.ForJSCoefficient %>;--%>


        var obj = <%= this.StringObject %>;
    </script>

    <script src="Scripts/WebForm1.js"></script>



</head>
<body>
    <form id="form1" runat="server">
        <button type="button" onclick="exec()">Click me</button>
        <%-- 此Button Click事件後只執行Exec()
            ，如刷新頁面則執行所有alert事件--%>
    </form>
</body>
</html>
