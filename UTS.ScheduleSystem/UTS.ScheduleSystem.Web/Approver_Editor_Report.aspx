<%@ Page Title="Editor Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Editor_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Editor_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Approver">< Go back  </asp:HyperLink>
        <b>Editor Individual Rule Report:</b>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:GridView ID="editorList" runat="server" AutoGenerateColumns="false" OnRowCommand="EditorList_RowCommand" CellPadding="7">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:ButtonField Text="Check" CommandName="Check" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-8">
            <div class="row" id="username">Username: <%=EditorUsername%></div>
            <div class="row" id="userApprovedRuleNum">Approved Rule: <%=EditorApprovedRuleNum%></div>
            <div class="row" id="userRejectedRuleNum">Rejected Rule: <%=EditorRejectedRuleNum%></div>
            <div class="row" id="userPendingRuleNum">Pending Rule: <%=EditorSuccessRate%></div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">Success Rate: <%=EditorSuccessRate%></div>
        <div class="col-md-6">Overall Rate: <%=OverallSuccessRate%></div>
    </div>
    
</asp:Content>
