<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Editor_Edit.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Editor_Edit" %>



    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="container">

        <div>
            <h2>Edit rule: <asp:Label ID="ModeLabel" runat="server"/></h2>
            
                        <label for="InputTextBox">Input:</label>
                    
                        <asp:TextBox ID="InputTextBox" runat="server"/><br />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="InputTextBox" ErrorMessage="Input is required" Text="[Required]" />
                    <br />
                        <label for="OutputTextBox">Output:</label>
                   
                        <asp:TextBox ID="OutputTextBox" runat="server" /><br />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="OutputTextBox" ErrorMessage="Output is required" Text="[Required]" />
                   
            <asp:ValidationSummary runat="server" />
            <div>
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" CssClass="btn btn-default"/>
                <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" Text="Cancel" CssClass="btn btn-default"/>
            </div>
        </div>

        </div>


        </asp:Content>
