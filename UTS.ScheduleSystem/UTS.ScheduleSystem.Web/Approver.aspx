<%@ Page Title="Approver Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">Pending Rules:</div>
        <div class="col-md-4">
            <asp:Button ID="MyRules" runat="server" Text="My Rules" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:GridView ID="PendingRuleDisplayView" runat="server">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
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
            <asp:Button ID="EditorDashboard" runat="server" Text="Editor Dashboard" />
        </div>
    </div>
</asp:Content>
