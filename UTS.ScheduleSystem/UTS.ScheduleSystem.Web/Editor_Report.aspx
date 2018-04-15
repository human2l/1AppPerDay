﻿<%@ Page Title="Editor Report Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editor_Report.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Editor_Report" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-md-8">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Editor">< Go back  </asp:HyperLink>
            <b>My Approved Rules</b>
        </div>
        <div class="col-md-4">
            ???
        </div>
        <div class="col-md-8">
            <asp:GridView ID="EditorReportGridView" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Input" HeaderText="Input" />
                    <asp:BoundField DataField="Output" HeaderText="Output" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Number of rules created"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Number of rules rejected"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Success rate"></asp:Label>
            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    
</asp:Content>