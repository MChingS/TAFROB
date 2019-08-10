<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="seatAllocation.aspx.cs" Inherits="seatAllocation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    seatAllocation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">




    <style type="text/css">
        .auto-style1 {
            width: 159px;
        }

        .auto-style2 {
            border-radius: 5px 0 0 5px;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: 0;
            margin-top: 0px;
        }
        .auto-style3 {
            float: left;
            background-color: rgb(255,255,255);
            border-radius: 5px 5px 5px 5px;
            width: 75%;
            height: 500px;
        }
        .auto-style4 {
            float: right;
            background-color: rgb(255,255,255);
            border-radius: 5px 5px 5px 5px;
            width: 24%;
            height: 500px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="Server">
   <div id="details" class="auto-style3">
    <p align="center" style="font-size: x-large; font-weight: bolder;">
        <asp:Label ID="pageName" runat="server" Text=" Seat Allocation"></asp:Label>
    </p>
    <p align="center" style="font-size: x-large; font-weight: bolder;">
        &nbsp;
    </p>
    <h3 align="center">Course and Subject selection</h3>
    <table align="center">
        <tr>
            <td class="auto-style1">Course Code
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="courseList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="courseList_SelectedIndexChanged" Height="26px" Width="111px">
                    <asp:ListItem>Select course</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Subject Code
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="subjectList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="subjectList_SelectedIndexChanged" Height="25px" Width="111px">
                    <asp:ListItem>Select subject</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="numOfStud0" runat="server" Text="Number of students"></asp:Label>
            </td>
            <td class="auto-style2">
                <asp:Label ID="numOfStud" runat="server" Text="null"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Choose Date
            </td>
            <td class="auto-style2">
                <asp:Calendar ID="slotDate" runat="server" CssClass="auto-style2" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="16px" Width="136px" OnSelectionChanged="slotDate_SelectionChanged">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>

            </td>
        </tr>
        <tr>
            <td class="auto-style1">Choose a slot
            </td>
            <td class="auto-style2">
                <asp:DropDownList ID="slots" runat="server" AutoPostBack="true" Height="25px" Width="111px" OnSelectedIndexChanged="slots_SelectedIndexChanged">
                    <asp:ListItem>Slot 1</asp:ListItem>
                    <asp:ListItem>Slot 2</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Choose spacing type
            </td>
            <td>
                <asp:RadioButtonList ID="spacingType" runat="server" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="spacingType_SelectedIndexChanged">
                    <asp:ListItem>Even Number Spacing</asp:ListItem>
                    <asp:ListItem>Odd Number Spacing</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

    <br />
    <p align="center">
        <asp:Label ID="warning" Style="color: red;" runat="server" Text="" Font-Size="Medium"></asp:Label>
    </p>
    <p align="center" style="font-size: x-large; font-weight: bolder;">
        <asp:Button ID="nextToLab" runat="server" Visible="false" Text="NEXT" OnClick="Button2_Click" />
    </p>
       </div>
    <div id="det" class="auto-style4">
         
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         
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
         <td><asp:Label ID="Label7" runat="server" Text=" Surname : "></asp:Label></td>
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
    <asp:Panel ID="Panel1" Width="400" Visible="false" HorizontalAlign="Left" runat="server">
        <h3 align="center">Venue selection</h3>
        <p align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="Label2" runat="server" Text="venue Name" Width="100px"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Lab Capacity" Width="100px"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Lab Structure" Width="100px"></asp:Label>
        </p>
        <p>&nbsp;&nbsp; &nbsp;</p>
    </asp:Panel>
    <asp:Panel ID="labControler" CssClass="str" Width="400" Visible="false" HorizontalAlign="Center" runat="server">
    </asp:Panel>

    <p>&nbsp;</p>
    <p align="center" style="font-size: x-large; font-weight: bolder;">
        <asp:Button ID="nextToPc" runat="server" Visible="false" Text="NEXT" OnClick="nextToPc_Click" Style="height: 26px" />
        &nbsp;
    </p>
    <div id="seats">
        <asp:Panel ID="pcControlerBy4" CssClass="str" Width="1100" Visible="false" runat="server">
            <h3 align="center">
                <asp:Label ID="seatName" Visible="false" runat="server" Text="Label"></asp:Label>
            </h3>
        </asp:Panel>

        <asp:Panel ID="pcControlerBy5" runat="server">
        </asp:Panel>


        <asp:Panel ID="pcControlerBy3" runat="server">
        </asp:Panel>


    </div>

    <asp:Panel ID="Panel2" CssClass="str" Visible="false" runat="server">
        
        <p align="center" style="font-size: x-large; font-weight: bolder;">
            <asp:Label ID="numStudeUpdates" runat="server" Visible="false" Text=""></asp:Label>
            <br />
            <button id="SAVE" onclick="printo()">Save/Print</button>

            &nbsp;&nbsp;
        
      <asp:Button ID="saveToDB" runat="server" Text="StoreToDB" OnClientClick="return confirm('Are you sure you want to save to database?');" OnClick="saveToDB_Click" />

        </p>
    </asp:Panel>
    <p align="right" style="font-size: x-large; font-weight: bolder;">
        <asp:Button ID="exit" runat="server" Text="LOGOUT" Width="66px" OnClick="exit_Click" OnClientClick="return confirm('Are you sure you want to LOGOUT?');" />
        &nbsp;
    </p>
       <br />
    <script>

        function printo() {
            var printContents = document.getElementById("seats").innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;

            return true;
        }
    </script>

</asp:Content>


