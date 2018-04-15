<%@ Page Title="Approver Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-start">
        <div class="col-md-4">
            <asp:Button ID="BackButton" runat="server" Text="Back" />
        </div>
        <div class="col-md-4">
            <asp:Button ID="LogoutButton" runat="server" Text="Logout" />
        </div>
    </div>

</asp:Content>