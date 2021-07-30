<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActType" runat="server">
                        <asp:ListItem Value="0">支出</asp:ListItem>
                        <asp:ListItem Value="1">收入</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    金額:<asp:TextBox ID="TxtAmount" runat="server" TextMode="Number"></asp:TextBox><br />
                    主題:<asp:TextBox ID="TxtCap" runat="server" ></asp:TextBox><br />
                    描述:<asp:TextBox ID="TxtDesc" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click"/>
                    <asp:Button ID="btnDel" runat="server" Text="Delete" OnClick="btnDel_Click"/>
                    
                    <br />
                    <asp:Literal ID="Itmsg" runat="server"  ></asp:Literal>
                
                </td>         
            </tr>
        </table>
    </form>
</body>
</html>
