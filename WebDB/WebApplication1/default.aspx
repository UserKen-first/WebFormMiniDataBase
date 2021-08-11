<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #lblName {
            background-color:darkred;
        } 
        #txt1 {
            display: none;   
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lblName">Hello</asp:Label><br />
        </div>
        
        <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hf1" />
        <asp:HiddenField ID="hf2" runat="server" />
        <%-- HiddenField天生就不會顯示，其Type為hidden --%>
        
        <%-- 瀏覽器端的警告訊息 --%>
        <asp:Button ID="Button1" runat="server" Text="Button" 
            OnClick="Button1_Click" OnClientClick="exec(); alert(123);" />
    
        <script>
            // 瀏覽器端的警告訊息
            // alert("瀏覽器端的警告訊息");

            //透過運算式，將Server端的變數，拿至Client端，置入JS，使其進行使用 
            var val = <%= this.ForJSint %>;
            alert(val * 40);
            
            var val2 = <%= (this.FOrJSBOOL) ? "true" : "false" %>;
            alert(val2);

            var val3 = '<%= this.ForJSString %>';
            alert(val3);
            

            function exec2() {
                var hf2 = document.getElementById("hf2");
                var val = hf2.value;

                alert(val);
            }
            exec2();

            // 使用JS存取html控制項並調整其值
            function exec() {
                var lbl = document.getElementById("lblName");
                lbl.innerHTML = "World";

                //var txt = document.getElementById("txt1");
                //txt.value = "World";

                var hf1 = document.getElementById("hf1");
                hf1.value = "World";

            }
            
        </script>
    </form>
</body>
</html>
