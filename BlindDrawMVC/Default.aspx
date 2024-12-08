<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BlindDrawMVC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Blind Draw Scheduler&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </h1>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Getting started</h2>
                <p>
                    Add players</p>
                <p>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"   
                    CellPadding="4" ForeColor="Black" GridLines="Vertical">  
                    <AlternatingRowStyle BackColor="White" />  
                    <Columns>  
                        <asp:TemplateField HeaderText="ID">  
                            <EditItemTemplate>  
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>  
                            </EditItemTemplate>  
                            <ItemTemplate>  
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                            </ItemTemplate>  
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Name">  
                            <EditItemTemplate>  
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>  
                            </EditItemTemplate>  
                            <ItemTemplate>  
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("name") %>'></asp:Label>  
                            </ItemTemplate>  
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Playing?">  
                            <EditItemTemplate>  
                                <asp:CheckBox ID="CheckBox1" runat="server" />  
                            </EditItemTemplate>  
                            <ItemTemplate>  
                                <asp:CheckBox ID="CheckBox1" runat="server" />  
                            </ItemTemplate>  
                        </asp:TemplateField>  
                    </Columns>  
                    <FooterStyle BackColor="#CCCC99" />  
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />  
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />  
                    <RowStyle BackColor="#F7F7DE" />  
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />  
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />  
                    <SortedAscendingHeaderStyle BackColor="#848384" />  
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />  
                    <SortedDescendingHeaderStyle BackColor="#575357" />  
                </asp:GridView>                  
                </p>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Width="176px" Text="Create Schedule" />   
            </section>
        </div>
    </main>

</asp:Content>
