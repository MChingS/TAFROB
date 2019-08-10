<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="labtec.aspx.cs" Inherits="labtec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    labTec
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            float: right;
            background-color: rgb(255,255,255);
            border-radius: 5px 5px 5px 5px;
            width: 20%;
            height: 289px;
        }
        .auto-style3 {
            float: left;
            background-color: rgb(255,255,255);
            border-radius: 5px 5px 5px 5px;
            width: 80%;
            height: 158px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <h2 align="center">Lab Configurations</h2>

    <div id="det" class="auto-style2">
         
        <asp:Image ID="Image1" CssClass="image" runat="server" ImageUrl="~/images/admin.png" />
        <br />
        <br />
         <table align="center" class="mytable">
             <tr>
        <td><asp:Label ID="Label1" runat="server" Text=" Username :"></asp:Label></td>
         <td><asp:Label ID="studUsername" runat="server" Text="current user"></asp:Label></td>
        </tr>
             <tr>
        <td><asp:Label ID="Label5" runat="server" Text=" Stuff No :"></asp:Label></td>
       <td>  <asp:Label ID="studNo" runat="server" Text="current stud"></asp:Label></td>
        </tr>
        <tr>
           <td> <asp:Label ID="Label6" runat="server" Text=" Name : "></asp:Label></td>
        <td> <asp:Label ID="studName" runat="server" Text="current name"></asp:Label></td>
        </tr>
              <tr>
         <td><asp:Label ID="Label7" runat="server" Text=" Surname : "></asp:Label>
             <asp:SqlDataSource ID="updateSource" runat="server" ConnectionString="<%$ ConnectionStrings:mchingis %>" ProviderName="<%$ ConnectionStrings:mchingis.ProviderName %>" UpdateCommand="UPDATE tblPC
SET pcStatus = 0
WHERE pcName = ?">
                 <UpdateParameters>
                     <asp:SessionParameter Name="pcName" SessionField="pcName" />
                 </UpdateParameters>
             </asp:SqlDataSource>
                  </td>
         <td> <asp:Label ID="studSurname" runat="server" Text="current stud"></asp:Label></td>
        </tr>
        <tr>
       <td>  <asp:Label ID="Label9" runat="server" Text=" Course Code : "></asp:Label></td>
        <td>  <asp:Label ID="studCoursecod" runat="server" Text=" code"></asp:Label></td>
         </tr>
        <tr>
        <td> <asp:Label ID="Label11" runat="server" Text=" Logged at : "></asp:Label></td>
        <td>  <asp:Label ID="timeLoged" runat="server" Text=" time"></asp:Label></td>
            </tr>
            </table>
  
    </div>
    <div id="details" class="auto-style3">
    <table id="lab" align="center">
        <tr>
            <td class="auto-style1">
               Choose Lab Name
            </td>
            <td class="auto-style1">
                <asp:DropDownList ID="labList" runat="server" AutoPostBack="true" Height="17px" Width="100px" OnSelectedIndexChanged="labList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Choose type of configuration
            </td>
            <td>
                <asp:DropDownList ID="configList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="configList_SelectedIndexChanged" Height="17px" Width="100px">
                    <asp:ListItem>Lab Status</asp:ListItem>
                    <asp:ListItem>Pc's Status</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="config" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Height="17px" Width="100px" AutoPostBack="True">
                    <asp:ListItem Value="0">unavailable</asp:ListItem>
                    <asp:ListItem Value="1">available</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="pcDet" runat="server" Visible="false" Text="Select To Update"></asp:Label>

            </td>
            <td>

        <asp:DropDownList ID="pcListFromDB" align="center" Visible="false" runat="server" Height="18px" Width="100px">
        </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="update" runat="server" Text="UPDATE" OnClick="update_Click" />

            </td>
        </tr>
    </table>
   </div>
    <p align="center">
        <asp:Label ID="labName" runat="server" Visible="False" Font-Bold="True" Font-Size="Large"></asp:Label>
    </p>
    <p>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="checkSource" DataTextField="pcName" DataValueField="pcName" Visible="False"></asp:CheckBoxList>
        <asp:SqlDataSource ID="checkSource" runat="server" ConnectionString="<%$ ConnectionStrings:mchingis %>" ProviderName="<%$ ConnectionStrings:mchingis.ProviderName %>" SelectCommand="SELECT pcName,pcStatus
FROM tblPc
WHERE pcStatus &lt;&gt; ?
AND venCode = ?">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList3" Name="pcStatus" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="labList" Name="venCode" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p align="right">
        <asp:Button ID="Button2" runat="server" Text="LOGOUT" OnClick="Button2_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p><br />
    </asp:Content>