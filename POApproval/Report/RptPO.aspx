<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptPO.aspx.cs" Inherits="POApproval.Report.RptPO" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ShowPrintButton="false" PageCountMode="Actual" ID="CustomerListReportViewer" runat="server" Height="1000" Width="100%"></rsweb:ReportViewer>
        </div>
        <%-- <br />
    Format:
    <asp:RadioButtonList ID="rbFormat" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="Word" Value="WORD" Selected="True" />
        <asp:ListItem Text="Excel" Value="EXCEL" />
        <asp:ListItem Text="PDF" Value="PDF" />
        <asp:ListItem Text="Image" Value="IMAGE" />
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="Export" />--%>
    </form>
</body>

</html>
