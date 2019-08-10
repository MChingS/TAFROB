<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="homepage.aspx.cs" Inherits="homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    theGreatHomePage
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <div class="homeP">
        <h1>Home page</h1>
    <h3>The Great seat allocation</h3>

    <asp:Button ID="Button1" runat="server" CssClass="button" Text="GoToLogIn" OnClick="Button1_Click"  />
    <br />
        <asp:Button ID="Button2" runat="server" CssClass="button" Text="Register Account" OnClick="Button2_Click" />
        <br />
        <br />
    </div>
    <br />
</asp:Content>

