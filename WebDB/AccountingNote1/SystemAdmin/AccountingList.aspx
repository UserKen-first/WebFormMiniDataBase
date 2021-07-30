<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

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
                    <asp:Button ID="btnAddAcc" runat="server" Text="Add Accounting" onclick="btnAddAcc_Click1" />
                    
                    <asp:GridView ID="gvAccountingList" runat="server" 
                        AutoGenerateColumns="false" Width="554px" OnRowDataBound="gvAccountingList_RowDataBound">
                       
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="Caption" /> 
                            <asp:BoundField HeaderText="金額" DataField="Amount" />        
                            
                            <asp:TemplateField HeaderText="支出/收入">
                                <ItemTemplate>
                                    <%--<%# ((int)Eval("ActType") == 0) ? "支出"  : "收入"%>--%>
                                    <asp:Label ID="lbltype" runat="server" ></asp:Label>
                                    <asp:Literal runat="server" ID="ItActType"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}"/>   
                            <asp:TemplateField HeaderText="資料編輯">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                                </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:PlaceHolder ID="PlcNoData" runat="server" Visible="false">
                        <p>
                            No data in your Accounting Note.
                        </p>
                    </asp:PlaceHolder>
                </td>         
            </tr>
        </table>
    </form>
</body>
</html>
