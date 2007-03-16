<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PdaState.aspx.cs" Inherits="PdaState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PDA ״̬</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1"
            EmptyDataText="There are no data records to display." ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="DeviceId" HeaderText="DeviceId" SortExpression="DeviceId" />
                <asp:BoundField DataField="Pda" HeaderText="Pda" SortExpression="Pda" />
                <asp:BoundField DataField="LastUpdate" HeaderText="LastUpdate" SortExpression="LastUpdate" />
                <asp:BoundField DataField="IpAddr" HeaderText="IpAddr" SortExpression="IpAddr" />
                <asp:BoundField DataField="Owner" HeaderText="Owner" SortExpression="Owner" />
                <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            ProviderName="<%$ ConnectionStrings:Custom.ProviderName %>" SelectCommand="SELECT [Id], [DeviceId], [Pda], [LastUpdate], [IpAddr], [Owner], [Unit] FROM [PdaState]">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
