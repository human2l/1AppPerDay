<%@ Page Title="Approver Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-start">
            <div class="col-md-8">
                All Approved Rules:
            </div>
        </div>
        <div class="row justify-content-start">
            <div class="col-md-8">
                <asp:GridView ID="ApprovedRulesDisplayView" runat="server"></asp:GridView>
            </div>
            <div class="col-md-4">
                <div class="row">Approved Rules: </div>
                <div class="row">Rejected Rules: </div>
                <div class="row">Success Rate: </div>
            </div>
        </div>
    </div>
</asp:Content>
