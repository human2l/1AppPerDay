<%@ Page Title="Editor Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Editor_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Editor_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <asp:BulletedList class="editorListDisplayView" runat="server"></asp:BulletedList>
        </div>
        <div class="col-md-8">
            <div class="row">Username: </div>
            <div class="row">Approved Rule: </div>
            <div class="row">Rejected Rule: </div>
            <div class="row">Pending Rule: </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="row">Success Rate: </div>
            <div class="row">Overall Rate: </div>
        </div>
    </div>
    
</asp:Content>
