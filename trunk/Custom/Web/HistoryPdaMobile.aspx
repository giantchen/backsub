<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryPdaMobile.aspx.cs" Inherits="HistoryPdaMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" AutoGenerateRows="False"
            DataSourceID="SqlDataSource1" GridLines="Horizontal" EmptyDataText="参数不正确！">
            <PagerSettings FirstPageText="第一页" LastPageText="最后一页" Mode="NextPreviousFirstLast"
                NextPageText="下一页" Position="TopAndBottom" PreviousPageText="上一页" />
            <Fields>
                <asp:ImageField DataImageUrlField="ImageId" DataImageUrlFormatString="GetImage.aspx?id={0}" ShowHeader="False">
                </asp:ImageField>
                <asp:BoundField DataField="TimeStamp" SortExpression="TimeStamp" ShowHeader="False" />
                <asp:BoundField DataField="Message" SortExpression="Message" ShowHeader="False" />
            </Fields>
        </asp:DetailsView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Custom %>"
            SelectCommand="SELECT [Pda], [ImageId], [TimeStamp], [Message] FROM [Shows] WHERE ([Pda] = @Pda) ORDER BY [Id] DESC">
            <SelectParameters>
                <asp:QueryStringParameter Name="Pda" QueryStringField="pda" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
