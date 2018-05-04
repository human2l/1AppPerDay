<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataMaintainer_Edit.aspx.cs" Inherits="UTS.ScheduleSystem.Web.DataMaintainer_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="MainForm" runat="server">
        <div>
            <h2>Edit Data:
                <asp:Label ID="ModeLabel" runat="server" /></h2>
            <table>
                <tr>
                    <td>
                        <label for="InputTextBox">Topic:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="TopicTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TopicTextBox" ErrorMessage="Topic is required" Text="[Required]" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="ParticipantsTextBox">Participants:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="ParticipantsTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ParticipantsTextBox" ErrorMessage="Participants is required" Text="[Required]" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="LocationTextBox">Location:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="LocationTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="LocationTextBox" ErrorMessage="Location is required" Text="[Required]" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="StartDateTextBox">StartDate:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="StartDateTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDateTextBox" ErrorMessage="StartDate is required" Text="[Required]" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="EndDateTextBox">EndDate:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="EndDateTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDateTextBox" ErrorMessage="EndDate is required" Text="[Required]" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary runat="server" />
            <div>
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save" />
                <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" Text="Cancel" />
            </div>
        </div>
    </form>
</body>
</html>
