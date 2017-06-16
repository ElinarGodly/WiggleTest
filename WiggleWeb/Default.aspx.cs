using System;
using System.Web.UI.WebControls;
using avT = ApplicationVariables.AV.SystemValues.TableValues;
using avR = ApplicationVariables.AV.SystemValues.RadioButtonList;
using bl = WiggleBusinessLogic.BusLayer;
using cl = WiggleClasses;
using System.Collections.Generic;

namespace WiggleBasketWebPage
{
    public partial class Default : System.Web.UI.Page
    {
        private static cl.Basket basket;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                basket = new cl.Basket();
        }

        private void AddItemToBasket()
        {
            cl.Item item = new cl.Item(int.Parse(tbItemQty.Text), tbItemName.Text, tbItemSubset.Text, decimal.Parse(tbItemValue.Text));
            basket.AddItemToBuy(item);
            TableBasket(basket);
        }

        private void AddGiftToBasket(bool buy)
        {
            cl.Gift gift = new cl.Gift(decimal.Parse(tbGiftValue.Text), tbGiftCode.Text, int.Parse(tbGiftQty.Text));
            basket.AddGift(gift, buy);
            TableBasket(basket);
        }

        private void AddOfferToBasket()
        {
            cl.Offer offer = new cl.Offer(decimal.Parse(tbOfferValue.Text),
                                        tbOfferCode.Text, decimal.Parse(tbOfferThreshold.Text), tbOfferSubset.Text);
            basket.ApplyOffer(offer);
            TableBasket(basket);
        }

        #region Table
        private void TableBasket(cl.Basket basket)
        {
            basket.CalcTotal();
            tblBasket.Visible = true;
            TableHeaders();

            foreach (var item in basket.BuyItems)
                AddRowBuyItem(item);

            foreach (var gift in basket.BuyGifts)
                AddRowBuyGift(gift);

            foreach (var gift in basket.ApplyGift)
                AddRowApplyGift(gift);

            if (basket.Offer.Code != null) AddRowApplyOffer(basket.Offer);

            if (basket.VoucherMessage != string.Empty)
            {
                AddErrorRow(basket.VoucherMessage);
                AddTotalRow(basket.BasketTotal);
            }
            else
                AddTotalRow(basket.BasketTotal);
        }
        
        private void TableHeaders()
        {
            TableHeaderRow header = new TableHeaderRow();
            header.Font.Bold = true;

            for (int i = 0; i < avT.HeaderCells.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = avT.HeaderCells[i];
                header.Cells.Add(cell);
            }
            tblBasket.Rows.Add(header);
        }

        private void AddCell(string text, int colSpan, TableRow row)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            cell.ColumnSpan = colSpan;
            row.Cells.Add(cell);
        }

        private void AddRowBuyItem(cl.Item item)
        {
            TableRow row = new TableRow();
            AddCell((item.Subset == String.Empty) ? String.Format(avT.Templates.itemNoSub, item.Name) :
                                    String.Format(avT.Templates.itemWithSub, item.Name, item.Subset), 0,row);
            AddCell(item.Qty.ToString(), 0, row);
            AddCell((item.Value * item.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }

        private void AddRowBuyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            AddCell(String.Format(avT.Templates.giftItem, gift.Value), 0, row);
            AddCell(gift.Qty.ToString(), 0, row);
            AddCell((gift.Value * gift.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }
        
        private void AddRowApplyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            AddCell(String.Format(avT.Templates.giftVoucher, gift.Qty, gift.Value, gift.Code), 3, row);
            tblBasket.Rows.Add(row);
        }

        private void AddRowApplyOffer(cl.Offer offer)
        {
            TableRow row = new TableRow();
            AddCell((offer.Subset == String.Empty) ?
                    String.Format(avT.Templates.offerNoSub, offer.Value, offer.Threshold, offer.Code) :
                    String.Format(avT.Templates.offerWithSub, offer.Value, offer.Subset, offer.Threshold, offer.Code), 3, row);
            tblBasket.Rows.Add(row);
        }

        private void AddErrorRow(string voucherMessage)
        {
            TableRow row = new TableRow();
            AddCell(voucherMessage, 3, row);
            tblBasket.Rows.Add(row);
        }

        private void AddTotalRow(decimal total)
        {
            TableRow row = new TableRow();
            AddCell(String.Format(avT.Templates.totalSum, total.ToString()),3,row);
            tblBasket.Rows.Add(row);
        }
        #endregion

        #region Events
        protected void rblCreateChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rblCreateChoice.SelectedValue;

            switch (selectedValue)
            {
                case avR.Gift.btn:
                    SetVisibleAndEnabled(avR.Gift.setVE);
                    TableBasket(basket);
                    break;
                case avR.Item.btn:
                    SetVisibleAndEnabled(avR.Item.setVE);
                    TableBasket(basket);
                    break;
                case avR.Offer.btn:
                    SetVisibleAndEnabled(avR.Offer.setVE);
                    TableBasket(basket);
                    break;
                case avR.Default.btn:
                    SetVisibleAndEnabled(avR.Default.setVE);
                    break;
            }
        }

        private void SetVisibleAndEnabled(List<bool> args)
        {
            panelGift.Visible = args[0];
            panelItem.Visible = args[1];
            panelOffer.Visible = args[2];
            panelDefault.Visible = args[3];

            btnBuy.Enabled = args[4];
            btnApply.Enabled = args[5];
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (rblCreateChoice.SelectedValue == avR.Item.btn) AddItemToBasket();
            else AddGiftToBasket(true);
        }

        protected void btnApply_Click(object sender, EventArgs e) 
        {
            if (rblCreateChoice.SelectedValue == avR.Offer.btn) AddOfferToBasket();
            else AddGiftToBasket(false);
        }

        protected void rblDefaultChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            bl bl1 = new bl();
            TableBasket(bl1.LoadBasket(rblDefaultChoice.SelectedIndex));
        }

        #endregion

    }
}