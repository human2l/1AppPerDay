<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DataMaintainer_Edit.aspx.cs" Inherits="UTS.ScheduleSystem.Web.DataMaintainer_Edit" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div id="container">
        <div>
            <h2>Edit Data:
                <asp:Label ID="ModeLabel" runat="server" /></h2>
           
                        <label for="InputTextBox">Topic:</label>
                    
                        <asp:TextBox ID="TopicTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TopicTextBox" ErrorMessage="Topic is required" Text="[Required]" />
                    <br />
                        <label for="ParticipantsTextBox">Participants:</label>
                    
                        <asp:TextBox ID="ParticipantsTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ParticipantsTextBox" ErrorMessage="Participants is required" Text="[Required]" />
                    <br />
                        <label for="LocationTextBox">Location:</label>
                   
                        <asp:TextBox ID="LocationTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="LocationTextBox" ErrorMessage="Location is required" Text="[Required]" />
                   <br />
                        <label for="StartDateTextBox">StartDate:</label>
                    
                        <asp:TextBox ID="StartDateTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDateTextBox" ErrorMessage="StartDate is required" Text="[Required]" />
                    <br />
                        <label for="EndDateTextBox">EndDate:</label>
                   
                        <asp:TextBox ID="EndDateTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDateTextBox" ErrorMessage="EndDate is required" Text="[Required]" />
                   <br />
            <asp:ValidationSummary runat="server" />
            <div>
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" CssClass="btn btn-default"/>
                <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" Text="Cancel" CssClass="btn btn-default"/>
            </div>
        </div>

</div>
    </asp:Content>