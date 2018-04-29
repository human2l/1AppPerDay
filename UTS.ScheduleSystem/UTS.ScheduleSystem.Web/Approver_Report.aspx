<%@ Page Title="Approver Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Approver">< Go back  </asp:HyperLink>
            <b>Approver Rule Report:</b>
        </div>
        <div class="row justify-content-start">
            <div class="col-md-10">
                All Approved Rules:
            </div>
        </div>
        <div class="row justify-content-start">
            <div class="col-md-10">
                <asp:GridView ID="ApprovedRulesDisplayView" runat="server" AutoGenerateColumns="false" CellPadding="7">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Input" HeaderText="Input" />
                        <asp:BoundField DataField="Output" HeaderText="Output" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="LastRelatedUserId" HeaderText="Last Editor" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-2">
                <div class="row">Approved Rules: <%=ApprovedRuleNum%></div>
                <div class="row">Rejected Rules: <%=RejectedRuleNum%></div>
                <div class="row">Success Rate: <%=SuccessRate%></div>
            </div>
        </div>
    </div>
</asp:Content>
