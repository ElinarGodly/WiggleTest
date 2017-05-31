<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiggleBasketWebPage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="DropDownAddOptions" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div>
            <asp:Panel ID="pnlGetInfo" runat ="server"></asp:Panel>
        </div>
        <div>
            <asp:Button ID="btn_Add" Text="Add" runat="server" Enabled="false" Visible ="false" />
        </div>
    </form>
</body>
</html>
