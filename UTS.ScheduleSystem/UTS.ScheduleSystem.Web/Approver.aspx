<%@ Page Title="Approver Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Approver.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Approver" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">Pending Rules:</div>
        <div class="col-md-4">
            <asp:Button ID="Button1" runat="server" Text="My Rules" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:GridView ID="PendingRuleDisplayView" runat="server"></asp:GridView>
        </div>
        <div class="col-md-4">
            <asp:GridView ID="EditorListDisplayView" runat="server"></asp:GridView>
        </div>
    </div>
</asp:Content>
