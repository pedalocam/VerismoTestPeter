<%@ Page Language="C#" MasterPageFile="Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="VerismoTest.Website.Default" %>

<asp:Content ContentPlaceHolderID="cphContent1" runat="server">
    <div class="container">
        <div class="page-header">
            <h1>
                <img src="style/logo.png" alt="" />
            </h1>
        </div>
        <h3>Lista av alla "Tasks"</h3>
        <p>Det ska gå att lägga till nya, ändra samt ta bort.</p>
        <p>Inget ändras skarpt innan man har tryckt på "Spara".</p>
        <hr />
        <div>
            &nbsp
        </div>
        <div class="row">

            <asp:GridView ID="GridView1" runat="server" BorderColor="White" BorderStyle="None" ShowHeaderWhenEmpty="true" CellPadding="10"
                AutoGenerateColumns="false" UseAccessibleHeader="true" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="AddRow_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="col-md-4 col-sm-4 col-xs-4">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-sm input-xs"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DueDate" HeaderStyle-CssClass="col-md-2 col-sm-2 col-xs-2">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm input-xs"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="col-md-5 col-sm-5 col-xs-5">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-sm input-xs"></asp:TextBox>
                            <asp:PlaceHolder ID="PlaceHolder_InputControl" runat="server"></asp:PlaceHolder>

                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-CssClass="col-md-1 col-sm-1 col-xs-1">
                        <HeaderTemplate>
                            <asp:LinkButton
                                ID="LinkButton1"
                                runat="server"
                                CommandArgument="addrow"
                                CommandName="AddRow"
                                CssClass="btn btn-primary btn-md btn-sm btn-xs"
                                Style="width: 75px; height: 30px; padding-top: 5px;">Add Row</asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteRow" runat="server" Text="x" CommandName="Delete" CommandArgument="Delete" ToolTip="Delete" CssClass="btn btn-danger btn-md btn-sm btn-xs text-center" Style="width: 30px; height: 30px; padding-top: 5px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            &nbsp
        </div>
        <div class="row" style="text-align: center" align="center">
            <div class="col-md-12 text-center">
                <asp:Button
                    ID="SaveData"
                    runat="server"
                    CommandName="SaveData"
                    Text="Save"
                    CssClass="btn btn-success text-center"
                    Style="font-size: 12px; width: 50px; height: 30px;"
                    OnClick="Save_Click" />
            </div>
        </div>
    </div>
</asp:Content>
