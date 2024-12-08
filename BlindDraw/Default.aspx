<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BlindDraw._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Blind Draw Schedule Maker</h1>
            <p class="lead">Use this schedule maker to keep track of past schedules and blind draws to reduce duplication</p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Enter player names:
                    <asp:TextBox ID="txtPlayerName" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
                </h2>
                <p>
     <asp:GridView ID="gvPlayers" runat="server" AutoGenerateColumns="false" CssClass="gv" HeaderStyle-CssClass="gvHeader" FooterStyle-CssClass="gvHeader">

        <Columns>

            <asp:TemplateField HeaderText="ID" ItemStyle-Width="150px">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("playerId") %>' runat="server" />

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px" ItemStyle-CssClass="Name">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("playerName") %>' runat="server" />
                    <asp:TextBox Text='<%# Eval("playerName") %>' runat="server" Style="display: none" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" ItemStyle-Width="150px" >
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("status") %>' runat="server" />
                    <asp:TextBox Text='<%# Eval("status") %>' runat="server" Style="display: none" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton Text="Edit" ID="Edit" runat="server" CssClass="Edit" />
                    <asp:LinkButton Text="Update" ID="Update" runat="server" CssClass="Update" Style="display: none" />
                    <asp:LinkButton Text="Cancel" ID="Cancel" runat="server" CssClass="Cancel" Style="display: none" />
                    <asp:LinkButton Text="Delete" ID="Delete" runat="server" CssClass="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle CssClass="gvItem" />
    </asp:GridView>
    <br />
    <label>Add new Player</label>
    <table border="1" style="margin: 2px">
        <tr>
            <td style="width: 150px;"><b>Name</b><br />
                <asp:TextBox ID="txtName" runat="server" Width="140" />
            </td>
            <td style="width: 150px;"><b>Status</b><br />
                <asp:TextBox ID="txtStatus" runat="server" Width="140" />
            </td>
            <td style="width: 100px">
                <br />
                <asp:Button ID="Button1" runat="server" Text="Add" />
            </td>
        </tr>
    </table>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
                </p>
                <p>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Players]"></asp:SqlDataSource>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
