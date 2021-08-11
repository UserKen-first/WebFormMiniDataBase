<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <th>產品</th>
                <td>
                    <asp:DropDownList ID="ddlProduct" runat="server">
                        <asp:ListItem Value="001">橘子</asp:ListItem>
                        <asp:ListItem Value="002">草莓</asp:ListItem>
                        <asp:ListItem Value="003">梨子</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <th>單價</th>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" Enabled="false">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>數量</th>
                <td>
                    <asp:TextBox ID="txtQuinty" runat="server" TextMode="Number"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblShowTotPric" runat="server">0</asp:Label><br />
        <asp:Button ID="btnSave" runat="server" Text="送出" OnClick="btnSave_Click" /><br />
        <asp:Label ID="lblWrongShow" runat="server" Text=""></asp:Label>
    
        <%-- 先取得控制項 --%>
        <script>
            var ddlProduct = document.getElementById("ddlProduct");
            var txtPrice = document.getElementById("txtPrice");
            var txtQuinty = document.getElementById("txtQuinty");
            var lblShowTotPric = document.getElementById("lblShowTotPric");

            // 製作一個對照表
            var priceMapping = {
                "001": 50,
                "002": 150,
                "003": 540
            };

            ddlProduct.onchange = function () {
                var productID = ddlProduct.value;
                var price = priceMapping[productID];

                txtPrice.value = price;
            }
            // onblur: 數量的TextBox輸值後離開，即會顯示
            txtQuinty.onblur = function () {
                var quantity = parseInt(txtQuinty.value, 10);
                var price = txtPrice.value;

                var TotalPrice = quantity * price;
                lblShowTotPric.innerText = TotalPrice;
            }
        </script>   
    </form>
</body>
</html>
