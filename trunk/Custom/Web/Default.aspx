<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="refresh" content="10" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>系统状态</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/EditPda.aspx">编辑 PDA 用户信息</asp:HyperLink>&nbsp;<asp:HyperLink
            ID="HyperLink2" runat="server" NavigateUrl="~/History.aspx">查看历史指令</asp:HyperLink>
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/HistoryPda.aspx">查看 PDA 历史</asp:HyperLink><br />
        <br />
        <asp:GridView
            ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CellPadding="4" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display."
            ForeColor="#333333">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Pda" HeaderText="Pda" SortExpression="Pda" />
                <asp:BoundField DataField="Owner" HeaderText="使用者" SortExpression="Owner" />
                <asp:BoundField DataField="Unit" HeaderText="科室" SortExpression="Unit" />
                <asp:BoundField DataField="LastUpdate" HeaderText="最后上线时间" SortExpression="LastUpdate" />
                <asp:BoundField DataField="Online" HeaderText="是否出勤" SortExpression="Online" />
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            ProviderName="<%$ ConnectionStrings:Custom.ProviderName %>"
            SelectCommand="SELECT Pda, LastUpdate, Owner, Unit, CASE WHEN DATEDIFF(second , [LastUpdate] , GETDATE()) < 120 THEN '出勤' ELSE '未出勤' END AS Online FROM PdaState"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
