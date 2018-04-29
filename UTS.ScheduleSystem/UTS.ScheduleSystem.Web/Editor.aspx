<%@ Page Title="Editor Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Editor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <br />
        <div class="col-md-8">All rules: 
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Selected="True">Pending</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
            </asp:DropDownList>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Editor_Report">Run report</asp:HyperLink>
        </div>
        <div class="col-md-4">
            Add new rule
        </div>
        <div class="col-md-8">
            <asp:GridView ID="PendingGridView" runat="server" Visible="true" AutoGenerateColumns="false" DataKeyNames="Id" OnRowEditing="PendingGridView_RowEditing" OnRowDeleting="PendingGridView_RowDeleting" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Input" HeaderText="Input" />
                    <asp:BoundField DataField="Output" HeaderText="Output" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:ButtonField Text="Edit" CommandName="Edit" />
                    <asp:ButtonField Text="Delete" CommandName="Delete" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="RejectedGridView" runat="server" AutoGenerateColumns="false" Visible="false">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Input" HeaderText="Input" />
                    <asp:BoundField DataField="Output" HeaderText="Output" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="Input" runat="server" TextMode="MultiLine" placeholder="Input" CssClass="form-control"></asp:TextBox><br />
            <asp:TextBox ID="Output" runat="server" TextMode="MultiLine" placeholder="Output" CssClass="form-control"></asp:TextBox>
            <br />
            <br />
            <p>Please use parameter from option with braces:</p> 
            <p>{ topic } { participants } { location } { startdate } { enddate }</p>
            <br />
            <asp:Button ID="Add_rule" runat="server" Text="Add rule" OnClick="Add_rule_Click" CssClass="btn btn-default"/>
            <asp:Button ID="Add_fixed_rule" runat="server" Text="Add fixed rule" OnClick="Add_fixed_rule_Click" CssClass="btn btn-default"/>
        </div>
    </div>
</asp:Content>