<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ScorecardApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Scoreboard App<br />
            <br />
            Number of players:
            <asp:TextBox ID="txtPlayerCount" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="Button1_Click" Text="Submit" />
            &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
            <br />
            <br />
        </div>
        <asp:Panel ID="pnl1" runat="server" Height="230px">
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </asp:Panel>
    </form>
</body>
</html>
