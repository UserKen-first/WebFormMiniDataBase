<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager2.ascx.cs" Inherits="AccountingNote1.SystemAdmin.UserControl.ucPager2" %>

<div>
    <%-- 改成使用超連結控制項，較好維護 --%>
    <a runat="server" id="aLinkFirst" href="#">First</a>
    <a runat="server" id="aLink1" href="#">1</a>
    <a runat="server" id="aLink2" href="#">2</a>
    <%--<a runat="server" id="aLink3" href="#">3</a>--%>
    <asp:Literal ID="ItCurrentPage" runat="server"></asp:Literal>
    <a runat="server" id="aLink4" href="#">4</a>
    <a runat="server" id="aLink5" href="#">5</a>
    <a runat="server" id="aLinkLast" href="#">Last</a>
    <asp:Literal ID="ItPager" runat="server"></asp:Literal>
</div>