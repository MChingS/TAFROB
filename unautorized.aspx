<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="unautorized.aspx.cs" Inherits="unautorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    unautorizes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    you are not allowed to browse this page log in with correct data
    <br />
    <p align="center">
    <asp:Button ID="Button1" runat="server" Text="BackToLogIn" OnClick="Button1_Click" />
        </p>
    <br />
</asp:Content>

