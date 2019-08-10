<%@ Page Title="" Language="C#" MasterPageFile="~/theGreat.master" AutoEventWireup="true" CodeFile="complexLectu.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    clear seats Allocation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
    <div align="center">
        
        <asp:GridView ID="GridView1" runat="server" DataSourceID="getvalues"></asp:GridView>
        <asp:SqlDataSource ID="getvalues" runat="server" ConnectionString="<%$ ConnectionStrings:mchingis %>" ProviderName="<%$ ConnectionStrings:mchingis.ProviderName %>" SelectCommand="SELECT studNo, pcName, st.subjCode, seatAllocDate, seatAllocSlot 
FROM tblseatallocation st,tblemployee e,tblsubj_emp se,tblsubject s
WHERE empusername = ?
AND e.empid=se.empid
AND se.subjCode = s.subjCode
AND s.subjcode = st.subjCode">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="?" SessionField="username" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>

