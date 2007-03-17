<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPda.aspx.cs" Inherits="EditPda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑 PDA </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display."
            ForeColor="#000040">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:BoundField DataField="Pda" HeaderText="PDA" ReadOnly="True" SortExpression="Pda" />
                <asp:BoundField DataField="Owner" HeaderText="使用人" SortExpression="Owner" />
                <asp:BoundField DataField="Unit" HeaderText="科室" SortExpression="Unit" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
            ConnectionString="<%$ ConnectionStrings:Custom %>" 
            DeleteCommand="DELETE FROM [PdaState] WHERE [Id] = @original_Id "
            InsertCommand="INSERT INTO [PdaState] ([Pda], [Owner], [Unit]) VALUES (@Pda, @Owner, @Unit)"
            OldValuesParameterFormatString="original_{0}" 
            ProviderName="<%$ ConnectionStrings:Custom.ProviderName %>"
            SelectCommand="SELECT [Pda], [Owner], [Unit], [Id] FROM [PdaState]" 
            UpdateCommand="UPDATE [PdaState] SET [Owner] = @Owner, [Unit] = @Unit WHERE [Pda] = @original_Pda" OnUpdating="SqlDataSource1_Updating">
            <DeleteParameters>
                <asp:Parameter Name="original_Id" Type="Int64" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Owner" Type="String" />
                <asp:Parameter Name="Unit" Type="String" />
                <asp:Parameter Name="original_Pda" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Pda" Type="String" />
                <asp:Parameter Name="Owner" Type="String" />
                <asp:Parameter Name="Unit" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
