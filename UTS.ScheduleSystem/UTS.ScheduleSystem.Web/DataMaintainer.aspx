<%@ Page Title="Data Maintainer Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataMaintainer.aspx.cs" Inherits="UTS.ScheduleSystem.Web.DataMaintainer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="DataMaintainerGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="DataMaintainerGridView_RowDeleting" OnRowEditing="DataMaintainerGridView_RowEditing">
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

    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Add" />

</asp:Content>
