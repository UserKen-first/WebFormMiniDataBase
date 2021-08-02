<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote1.SystemAdmin.Userlist" %>

<%@ Register Src="~/SystemAdmin/UserControl/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Add" />
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <uc1:ucPager runat="server" id="ucPager" />
</asp:Content>
