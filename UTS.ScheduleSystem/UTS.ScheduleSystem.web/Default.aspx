﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UTS.ScheduleSystem.Web._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container">
        
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.bmp" Height="200px" Width="200px"/><br />
        <br />
        <br />
                <asp:TextBox ID="question" runat="server" Height="40px" Width="400px" Font-Size="Medium" ></asp:TextBox>

    <%--<div class="container">
        <div class="row">
            <div class="col-md-2">
                <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="300px"></asp:TextBox>
            </div>
        </div>
    </div>--%>
    <%--<div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>

                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>--%>
                <asp:Button ID="submit" runat="server" OnClick="Submit_Click" Text="Check"  Height="40px" CssClass="btn btn-default" />
                <br />
        <br />
        <br />
                <asp:TextBox ID="answer" runat="server" Height="300px" ReadOnly="True" TextMode="MultiLine" Width="300px" BorderWidth="0px" Font-Size="Larger"></asp:TextBox>
</div>
</asp:Content>
    
