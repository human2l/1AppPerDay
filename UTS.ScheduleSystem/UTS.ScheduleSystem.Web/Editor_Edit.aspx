<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor_Edit.aspx.cs" Inherits="UTS.ScheduleSystem.Web.Editor_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="MainForm" runat="server">
        <div>
            <h2>Edit rule: <asp:Label ID="ModeLabel" runat="server"/></h2>
            <table>
                <tr>
                    <td>
                        <label for="InputTextBox">Input:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="InputTextBox" runat="server"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="InputTextBox" ErrorMessage="Input is required" Text="[Required]" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="OutputTextBox">Output:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="OutputTextBox" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="OutputTextBox" ErrorMessage="Output is required" Text="[Required]" />
                    </td>
                </tr>
                
            </table>
            <asp:ValidationSummary runat="server" />
            <div>
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save"/>
                <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" Text="Cancel"/>
            </div>
        </div>
    </form>
</body>
</html>
