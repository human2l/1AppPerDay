<%@ Page Title="Data Maintainer Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataMaintainer.aspx.cs" Inherits="UTS.ScheduleSystem.Web.DataMaintainer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="DataMaintainerGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" 
        OnRowCancelingEdit ="DataMaintainerGridView_RowCancelingEdit"
        OnRowDeleting="DataMaintainerGridView_RowDeleting" OnRowEditing="DataMaintainerGridView_RowEditing" >
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Topic" HeaderText="Topic" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" />
            <asp:BoundField DataField="Participants" HeaderText="Participants" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" />
            <asp:BoundField DataField="LastEditUserId" HeaderText="LastEditUserId" />
            <asp:ButtonField Text="Edit" CommandName="Edit" />
            <asp:ButtonField Text="Delete" CommandName="Delete" />
        </Columns>
    </asp:GridView>

    <asp:Panel ID="Panel1" runat="server" Height="300px" style="margin-bottom: 0px" BorderStyle="Solid" Width="200px">
        
        <asp:Label ID="Label2" runat="server" Text="Topic"></asp:Label>
        <br />
        <asp:TextBox ID="TopicTB" runat="server"></asp:TextBox>
        <br/>
        
        <asp:Label ID="Label3" runat="server" Text="UserId"></asp:Label>
        <br />
        <asp:TextBox ID="UserIdTB" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label4" runat="server" Text="Participants"></asp:Label>
        <br />
                <asp:TextBox ID="ParticipantsTB" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label5" runat="server" Text="Location"></asp:Label>
        <br />
                <asp:TextBox ID="LocationTB" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label6" runat="server" Text="StartDate"></asp:Label>
        <br />
                <asp:TextBox ID="StartDateTB" runat="server"></asp:TextBox>
        <br />
        
        <asp:Label ID="Label7" runat="server" Text="EndDate"></asp:Label>
        <br />
                <asp:TextBox ID="EndDateTB" runat="server"></asp:TextBox>
        
        <br />
        
        
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
    </asp:Panel>

</asp:Content>
