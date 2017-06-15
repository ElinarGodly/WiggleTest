<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiggleBasketWebPage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wiggle Basket Test</title>
    <link rel="stylesheet" type="text/css" href="default.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="panelChoice" runat="server">
                <asp:Label ID="lblCreateChoice" runat="server">Pick to Create</asp:Label>
                <asp:RadioButtonList ID="rblCreateChoice" runat="server" TextAlign="Right" OnSelectedIndexChanged="rblCreateChoice_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Gift" Value="gift" Selected="True" />
                    <asp:ListItem Text="Item" Value="item" />
                    <asp:ListItem Text="Offer" Value="offer" />
                </asp:RadioButtonList>
                <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" Visible="false" />--%>
            </asp:Panel>
            <asp:Panel ID="panelGift" runat="server" Visible="true"  CssClass="panel">
                <div>
                    <asp:Label ID="lblGiftCode" runat="server" CssClass="label">Gift Code</asp:Label>
                    <asp:TextBox ID="tbGiftCode" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblGiftValue" runat="server" CssClass="label">Gift Value</asp:Label>
                    <asp:TextBox ID="tbGiftValue" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblGiftQty" runat="server" CssClass="label">Gift Qty</asp:Label>
                    <asp:TextBox ID="tbGiftQty" runat="server"></asp:TextBox>
                </div>

            </asp:Panel>
            <asp:Panel ID="panelItem" runat="server" Visible="false" CssClass="panel">
                <div>
                    <asp:Label ID="lblItemName" runat="server" CssClass="label">Item Name</asp:Label>
                    <asp:TextBox ID="tbItemName" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblItemSubset" runat="server" CssClass="label">Item Subset</asp:Label>
                    <asp:TextBox ID="tbItemSubset" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblItemValue" runat="server" CssClass="label">Item Value</asp:Label>
                    <asp:TextBox ID="tbItemValue" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblItemQty" runat="server" CssClass="label">Item Qty</asp:Label>
                    <asp:TextBox ID="tbItemQty" runat="server"></asp:TextBox>
                </div>
            </asp:Panel>
            <asp:Panel ID="panelOffer" runat="server" Visible="false"  CssClass="panel">
                <div>
                    <asp:Label ID="lblOfferCode" runat="server" CssClass="label">Offer Code</asp:Label>
                    <asp:TextBox ID="tbOfferCode" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblOfferValue" runat="server" CssClass="label">Offer Value</asp:Label>
                    <asp:TextBox ID="tbOfferValue" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblOfferThreshold" runat="server" CssClass="label">Offer Threshold</asp:Label>
                    <asp:TextBox ID="tbOfferThreshold" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblOfferSubset" runat="server" CssClass="label">Offer Subset</asp:Label>
                    <asp:TextBox ID="tbOfferSubset" runat="server"></asp:TextBox>
                </div>
            </asp:Panel>
        </div>
        <div>
            <asp:Button ID="btnBuy" runat ="server" Text="Buy" Enabled ="false" OnClick ="btnBuy_Click" />
            <asp:Button ID="btnApply" runat ="server" Text ="Apply" Enabled="false" OnClick ="btnApply_Click" />
        </div>
        <%--<div>
            <asp:Label ID="lblDdlItem" runat="server">Pick an Item</asp:Label>
            <asp:DropDownList ID="ddlItems" runat="server" AutoPostBack="true"></asp:DropDownList>
            <asp:Label ID="lblDdlVoucher" runat="server">Pick a Voucher</asp:Label>
            <asp:DropDownList ID="ddlVouchers" runat="server" AutoPostBack="true"></asp:DropDownList>
        </div>--%>
        <div>
            <asp:Table ID="tblBasket" runat="server" Visible="false"></asp:Table>
        </div>
    </form>
</body>
</html>
