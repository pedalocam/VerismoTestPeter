<%@ Page Language="C#" MasterPageFile="Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="VerismoTest.Website.Default" %>

<asp:Content ContentPlaceHolderID="cphContent1" runat="server">




    <div class="container">
        <div class="page-header">
            <h1>
                <img src="style/logo.png" alt="" /></h1>
        </div>
        <h3>Lista av alla "Tasks"</h3>
        <p>Det ska gå att lägga till nya, ändra samt ta bort.</p>
        <p>Inget ändras skarpt innan man har tryckt på "Spara".</p>
        <div class="col-sx-12">
            &nbsp;
        </div>
        <asp:GridView ID="gvData"
            runat="server"
            AutoGenerateColumns="False"
            ShowFooter="True"
            DataKeyNames="Id"
            ShowHeaderWhenEmpty="True"
            CellPadding="7"
            GridLines="None"
            OnRowEditing="gvData_RowEditing1"
            OnRowUpdating="gvData_RowUpdating1"
            OnRowCancelingEdit="gvData_RowCancelingEdit1"
            OnRowDeleting="gvData_RowDeleting">

            <EditRowStyle BackColor="#fffffff" />
            <FooterStyle BackColor="#fffffff" Font-Bold="True" ForeColor="Black" BorderStyle="None" />
            <HeaderStyle BackColor="#fffffff" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#fffffff" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#fffffff" />
            <SelectedRowStyle BackColor="#fffffff" Font-Bold="True" ForeColor="#fffffff" />
            <SortedAscendingCellStyle BackColor="#fffffff" />
            <SortedAscendingHeaderStyle BackColor="#fffffff" />
            <SortedDescendingCellStyle BackColor="#fffffff" />
            <SortedDescendingHeaderStyle BackColor="#fffffff" />

            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' runat="server" CssClass="form-control input-sm" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNameFooter" Text='<%# Eval("Name") %>' runat="server" CssClass="form-control input-sm" />
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DueDate">
                    <ItemTemplate>
                        <asp:Label Text='<%#Eval("DueDate") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDueDate" Text='<%#Eval("DueDate") %>' runat="server" CssClass="form-control input-sm" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDueDateFooter" Text='<%#Eval("DueDate") %>' runat="server" CssClass="form-control input-sm"/>
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label Text='<%#Eval("Description") %>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" Text='<%#Eval("Description") %>' runat="server" CssClass="form-control input-sm" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDescriptionFooter" Text='<%#Eval("Description") %>' runat="server" CssClass="form-control input-sm" />
                        <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" CommandName="Edit" ToolTip="Edit" Text="Edit" class="btn btn-success btn-sm" />
                        <asp:Button runat="server" CommandName="Delete" ToolTip="Delete" Text="X" class="btn btn-danger btn-sm" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button runat="server" CommandName="Update" ToolTip="Update" Text="Save" class="btn btn-success btn-sm" />
                    </EditItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="col-md-6 text-center">
            <asp:Button runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="Save_Click" />
        </div>
        <br />
        <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
        <br />
        <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />
    </div>
</asp:Content>
