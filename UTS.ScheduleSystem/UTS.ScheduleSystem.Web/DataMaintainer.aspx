<%@ Page Title="Data Maintainer Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataMaintainer.aspx.cs" Inherits="UTS.ScheduleSystem.Web.DataMaintainer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <br />
        <div class="col-md-8">
            <asp:GridView ID="DataMaintainerGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                OnRowDeleting="DataMaintainerGridView_RowDeleting" OnRowEditing="DataMaintainerGridView_RowEditing" CellPadding="7">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Topic" HeaderText="Topic" />
                    <asp:BoundField DataField="Participants" HeaderText="Participants" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" />
                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" />
                    <asp:BoundField DataField="LastEditUserId" HeaderText="LastEditUserId" />
                    <asp:ButtonField Text="Edit" CommandName="Edit" />
                    <asp:ButtonField Text="Delete" CommandName="Delete" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-4">

            <asp:Label ID="Label2" runat="server" Text="Topic"></asp:Label>
            <br />
            <asp:TextBox ID="TopicTB" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="TopicTB" ErrorMessage="Topic is required" Text="[Required]" />
            <br />

            <asp:Label ID="Label4" runat="server" Text="Participants"></asp:Label>
            <br />
            <asp:TextBox ID="ParticipantsTB" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ParticipantsTB" ErrorMessage="Participants is required" Text="[Required]" />
            <br />

            <asp:Label ID="Label5" runat="server" Text="Location"></asp:Label>
            <br />
            <asp:TextBox ID="LocationTB" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="LocationTB" ErrorMessage="Location is required" Text="[Required]" />
            <br />

            <asp:Label ID="Label6" runat="server" Text="StartDate"></asp:Label>
            <br />
            <asp:TextBox ID="StartDateTB" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDateTB" ErrorMessage="StartDate is required" Text="[Required]" />
            <br />

            <asp:Label ID="Label7" runat="server" Text="EndDate"></asp:Label>
            <br />
            <asp:TextBox ID="EndDateTB" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDateTB" ErrorMessage="EndDate is required" Text="[Required]" />
            <br />


            <asp:Button ID="Add" runat="server" Text="Add" OnClick="Add_Click" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Content>
