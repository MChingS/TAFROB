<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">

    <table runat="server" align="center" style="background-color: #808080">
         
        <tr>
            <td colspan="2" align="center" style="height: 17px">
                <asp:Label ID="Label1" runat="server" align="center" Text="New User Registration" style="font-weight: 700; color: #000000; font-size: xx-large; text-align: center">
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
                <asp:Label ID="Label6" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Employee ID"></asp:Label>
            </td>
            <td>
                <br />
                <asp:Label ID="empid" runat="server" Text="NON" Font-Bold="True" Height="35px" Width="396px" Font-Size="Medium"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td style="height: 17px" class="text-right">
                <asp:Label ID="Label7" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Employee Type"></asp:Label>
            </td>
            <td style="height: 17px">
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" style="border-radius:5px;" Height="40px" Width="401px">
                    <asp:ListItem>Lab Technician</asp:ListItem>
                    <asp:ListItem>Lecturer</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="text-right">
                <asp:Label ID="Label10" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Username (Email)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server" style="border-radius:5px;" placeHolder="Mchingis@gmail.com" Height="35px" Width="396px"></asp:TextBox>
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
            <td class="text-right" style="height: 17px">
                <asp:Label ID="Label24" runat="server" style="color: #000000; font-size: medium; font-weight: bold;" Text="Confirm Password"></asp:Label>
                </td>
            <td style="height: 17px">
                <asp:TextBox ID="TextBox14" runat="server" style="border-radius:5px;" Height="35px" Width="396px" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox13" ControlToValidate="TextBox14" ErrorMessage="password must match" style="font-weight: 700; color: #FF0000"></asp:CompareValidator>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 45px">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="warning" Style="color: red;" runat="server" Visible="false" Text="UserName already exist" Font-Size="Medium"></asp:Label>
                      &nbsp;</td>
        </tr>
       
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" align="center" runat="server" style="border-radius:5px;" Height="30px" Text="Register" Width="180px" OnClick="Button2_Click" />
                   
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

