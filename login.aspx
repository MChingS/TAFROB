<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">

       <table runat="server" align="center" style="background-color: #808080">
         
        <tr>
            <td colspan="2" align="center" style="height: 17px">
                <asp:Label ID="Label1" runat="server" align="center" Text="User LogIn" style="font-weight: 700; color: #000000; font-size: xx-large; text-align: center">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
       
       
            
        <tr>
            <td class="text-right">
                <asp:Label ID="Label10" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Username (Email)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server" style="border-radius:5px;" Height="35px" Width="396px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox7" ErrorMessage="invalid email" style="font-weight: 700; color: #FF0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="text-right">
                <asp:Label ID="Label25" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox13" runat="server" style="border-radius:5px;" Height="35px" Width="396px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox13" ErrorMessage="password is required" style="font-weight: 700; color: #FF0000"></asp:RequiredFieldValidator>
                <br />
                <br />
            </td>
        </tr>
       
        <tr>
            <td colspan="2" style="height: 45px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="warning" Style="color: red;" runat="server" Visible="false" Text="Invalid Username Or Password" Font-Size="Medium"></asp:Label>
                      <br />  
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button2" align="center" runat="server" style="border-radius:5px" Height="30px" Text="LogIn" Width="180px" OnClick="Button2_Click" />
                   
                 </td>
        </tr>
       
        <tr>
            <td>
                &nbsp;</td>
            <td>
                                   
               </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        
    </table>
  <br />
</asp:Content>

