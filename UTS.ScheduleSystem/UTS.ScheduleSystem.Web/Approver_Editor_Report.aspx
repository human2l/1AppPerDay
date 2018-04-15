<%@ Page Title="Editor Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Editor_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Editor_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <asp:BulletedList class="editorListDisplayView" runat="server"></asp:BulletedList>
        </div>
        <div class="col-md-8">
            <div class="row" id="username">Username: </div>
            <div class="row" id="userApprovedRuleNum">Approved Rule: </div>
            <div class="row" id="userRejectedRuleNum">Rejected Rule: </div>
            <div class="row" id="userPendingRuleNum">Pending Rule: </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">Success Rate: </div>
        <div class="col-md-6">Overall Rate: </div>
    </div>
    
</asp:Content>
