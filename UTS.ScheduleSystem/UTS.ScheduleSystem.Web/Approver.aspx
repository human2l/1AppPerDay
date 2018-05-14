<%@ Page Title="Approver Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <br />
        <div class="col-md-8">Pending Rules:</div>
        <div class="col-md-4">
            <asp:Button ID="PassedRulesButton" runat="server" Text="ApprovedRules" onclick="PassedRulesButton_Click" CssClass="btn btn-default"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:GridView ID="PendingRuleDisplayView" runat="server" AutoGenerateColumns="false" OnRowCommand="PendingRuleDisplayView_RowCommand" CellPadding="7">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Input" HeaderText="Input" />
                    <asp:BoundField DataField="Output" HeaderText="Output" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="LastRelatedUserID" HeaderText="Last Editor" />
                    <asp:ButtonField Text="Approve" CommandName="Approve" />
                    <asp:ButtonField Text="Reject" CommandName="Reject" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-4">
            <asp:Button ID="EditorDashboardButton" runat="server" Text="Editor Dashboard" onclick="EditorDashboardButton_Click" CssClass="btn btn-default"/>
        </div>
    </div>
</asp:Content>
