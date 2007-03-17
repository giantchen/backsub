<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryPda.aspx.cs" Inherits="HistoryPda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PDA 历史</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">回主页</asp:HyperLink>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2"
            DataTextField="Pda" DataValueField="Pda">
        </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            SelectCommand="SELECT [Pda], [Owner], [Unit] FROM [PdaState] ORDER BY [Pda]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" EmptyDataText="There are no data records to display.">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Pda" HeaderText="Pda" SortExpression="Pda" />
                <asp:ImageField DataImageUrlField="ImageId" DataImageUrlFormatString="GetImage.aspx?id={0}"
                    HeaderText="图像">
                    <ControlStyle Height="90px" Width="120px" />
                </asp:ImageField>
                <asp:HyperLinkField DataNavigateUrlFields="ImageId" DataNavigateUrlFormatString="GetImage.aspx?Id={0}"
                    HeaderText="放大" Target="_blank" Text="[大图]" />
                <asp:BoundField DataField="TimeStamp" HeaderText="时间" SortExpression="TimeStamp" />
                <asp:BoundField DataField="Message" HeaderText="消息" SortExpression="Message" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            SelectCommand="SELECT [Pda], [ImageId], [TimeStamp], [Message], [Id] FROM [Shows] WHERE ([Pda] = @Pda) ORDER BY [Id] DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Pda" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
