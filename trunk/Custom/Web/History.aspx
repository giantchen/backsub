<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>��ʷָ��</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">����ҳ</asp:HyperLink>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            SelectCommand="SELECT [ImageId], [TimeStamp], [Message], [Pdas], [Id] FROM [Sends] ORDER BY [Id] DESC">
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="Id" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:ImageField DataImageUrlField="ImageId" DataImageUrlFormatString="GetImage.aspx?id={0}" HeaderText="ͼ��">
                    <ControlStyle Height="90px" Width="120px" />
                </asp:ImageField>
                <asp:HyperLinkField DataNavigateUrlFields="ImageId" DataNavigateUrlFormatString="GetImage.aspx?Id={0}"
                    HeaderText="�Ŵ�" Target="_blank" Text="[��ͼ]" />
                <asp:BoundField DataField="TimeStamp" HeaderText="ʱ��" SortExpression="TimeStamp" />
                <asp:BoundField DataField="Pdas" HeaderText="Pda" SortExpression="Pdas" />
                <asp:BoundField DataField="Message" HeaderText="��Ϣ" SortExpression="Message" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
