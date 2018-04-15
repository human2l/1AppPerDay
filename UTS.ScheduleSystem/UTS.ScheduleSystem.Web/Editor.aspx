﻿<%@ Page Title="Editor Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Editor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">All rules: 
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Editor_Report">Run report</asp:HyperLink>
        </div>
        <div class="col-md-8">
            <asp:GridView ID="EditorGridView" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Input" HeaderText="Input" />
                    <asp:BoundField DataField="Output" HeaderText="Output" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:ButtonField Text="Edit" CommandName="Edit" />
                    <asp:ButtonField Text="Delete" CommandName="Delete" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="Input" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:TextBox ID="Output" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="Add_rule" runat="server" Text="Add rule" OnClick="Add_rule_Click" />
            <asp:Button ID="Add_fixed_rule" runat="server" Text="Add fixed rule" OnClick="Add_fixed_rule_Click"/>
        </div>
    </div>
</asp:Content>