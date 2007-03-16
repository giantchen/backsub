<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PdaState.aspx.cs" Inherits="PdaState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PDA ״̬</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="DeviceId" HeaderText="DeviceId" SortExpression="DeviceId" />
                <asp:BoundField DataField="Pda" HeaderText="Pda" SortExpression="Pda" />
                <asp:BoundField DataField="LastUpdate" HeaderText="LastUpdate" SortExpression="LastUpdate" />
                <asp:BoundField DataField="IpAddr" HeaderText="IpAddr" SortExpression="IpAddr" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            DeleteCommand="DELETE FROM [PdaState] WHERE [Id] = @Id" InsertCommand="INSERT INTO [PdaState] ([DeviceId], [Pda], [LastUpdate], [IpAddr]) VALUES (@DeviceId, @Pda, @LastUpdate, @IpAddr)"
            ProviderName="<%$ ConnectionStrings:Custom.ProviderName %>"
            SelectCommand="SELECT [Id], [DeviceId], [Pda], [LastUpdate], [IpAddr] FROM [PdaState]"
            UpdateCommand="UPDATE [PdaState] SET [DeviceId] = @DeviceId, [Pda] = @Pda, [LastUpdate] = @LastUpdate, [IpAddr] = @IpAddr WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int64" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="DeviceId" Type="String" />
                <asp:Parameter Name="Pda" Type="String" />
                <asp:Parameter Name="LastUpdate" Type="DateTime" />
                <asp:Parameter Name="IpAddr" Type="String" />
                <asp:Parameter Name="Id" Type="Int64" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="DeviceId" Type="String" />
                <asp:Parameter Name="Pda" Type="String" />
                <asp:Parameter Name="LastUpdate" Type="DateTime" />
                <asp:Parameter Name="IpAddr" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
