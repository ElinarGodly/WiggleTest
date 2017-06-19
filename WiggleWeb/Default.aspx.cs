using System;
using System.Web.UI.WebControls;
using avSV = ApplicationVariables.AV.SystemValues;
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

        private void AddItemToBasket()//TODO in BL
        {
            cl.Item item = new cl.Item(int.Parse(tbItemQty.Text), tbItemName.Text, tbItemSubset.Text, decimal.Parse(tbItemValue.Text));
            basket.AddItemToBuy(item);
            TableBasket(basket);
        }

        private void AddGiftToBasket(bool buy)//TODO in BL
        {
            cl.Gift gift = new cl.Gift(decimal.Parse(tbGiftValue.Text), tbGiftCode.Text, int.Parse(tbGiftQty.Text));
            basket.AddGift(gift, buy);
            TableBasket(basket);
        }

        private void AddOfferToBasket() //TODO in BL
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
                AddErrorRow(basket.VoucherMessage);

            AddTotalRow(basket.BasketTotal);
        }
        
        private void TableHeaders()
        {
            TableHeaderRow header = new TableHeaderRow();
            header.Font.Bold = true;

            for (int i = 0; i < avSV.TableValues.HeaderCells.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = avSV.TableValues.HeaderCells[i];
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

        private void AddCellQty(int qty, TableRow row)
        {
            //AddCell(qty.ToString(), 0, row);
            
            TableCell cell = new TableCell();
            Button btn = new Button();
            Label lbl = new Label();
            btn.ID = String.Format(avSV.Buttons.templ, row.ID, avSV.Buttons.delete);
            cell.Controls.Add(btn);

            btn = new Button();
            btn.ID = String.Format(avSV.Buttons.templ, row.ID, avSV.Buttons.decrease);
            cell.Controls.Add(btn);

            lbl.Text = qty.ToString();
            lbl.ID = String.Format(avSV.Buttons.templ, row.ID, avSV.Buttons.qtyLbl);
            cell.Controls.Add(lbl);

            btn = new Button();
            btn.ID = String.Format(avSV.Buttons.templ, row.ID, avSV.Buttons.increase);
            cell.Controls.Add(btn);

            row.Cells.Add(cell);
        }

        private void AddRowBuyItem(cl.Item item)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.rowBuy, tblBasket.Rows.Count);
            AddCellQty(item.Qty, row);
            AddCell((item.Subset == String.Empty) ? String.Format(avSV.TableValues.Templates.itemNoSub, item.Name) :
                                    String.Format(avSV.TableValues.Templates.itemWithSub, item.Name, item.Subset), 0,row);
            
            AddCell((item.Value * item.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }


        private void AddRowBuyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.rowBuy, tblBasket.Rows.Count);
            AddCellQty (gift.Qty, row);
            AddCell(String.Format(avSV.TableValues.Templates.giftItem, gift.Value), 0, row);
            AddCell((gift.Value * gift.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }
        
        private void AddRowApplyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.rowApply, tblBasket.Rows.Count);
            AddCellQty(gift.Qty, row);
            AddCell(String.Format(avSV.TableValues.Templates.giftVoucher, gift.Value, gift.Code), 2, row);
            tblBasket.Rows.Add(row);
        }

        private void AddRowApplyOffer(cl.Offer offer)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.rowOffer;
            AddCell((offer.Subset == String.Empty) ?
                    String.Format(avSV.TableValues.Templates.offerNoSub, offer.Value, offer.Threshold, offer.Code) :
                    String.Format(avSV.TableValues.Templates.offerWithSub, offer.Value, offer.Subset, offer.Threshold, offer.Code), 3, row);
            tblBasket.Rows.Add(row);
        }

        private void AddErrorRow(string voucherMessage)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.rowMsg;
            AddCell(voucherMessage, 3, row);
            tblBasket.Rows.Add(row);
        }

        private void AddTotalRow(decimal total)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.rowTotal;
            AddCell(String.Format(avSV.TableValues.Templates.totalSum, total.ToString()),3,row);
            tblBasket.Rows.Add(row);
        }
        #endregion

        #region Events
        protected void rblCreateChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rblCreateChoice.SelectedValue;

            switch (selectedValue)
            {
                case avSV.RadioButtonList.Gift.btn:
                    SetVisibleAndEnabled(avSV.RadioButtonList.Gift.setVE);
                    TableBasket(basket);
                    break;
                case avSV.RadioButtonList.Item.btn:
                    SetVisibleAndEnabled(avSV.RadioButtonList.Item.setVE);
                    TableBasket(basket);
                    break;
                case avSV.RadioButtonList.Offer.btn:
                    SetVisibleAndEnabled(avSV.RadioButtonList.Offer.setVE);
                    TableBasket(basket);
                    break;
                case avSV.RadioButtonList.Default.btn:
                    SetVisibleAndEnabled(avSV.RadioButtonList.Default.setVE);
                    rblDefaultChoice.ClearSelection();
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
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Item.btn) AddItemToBasket();
            else AddGiftToBasket(true);
        }

        protected void btnApply_Click(object sender, EventArgs e) 
        {
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Offer.btn) AddOfferToBasket();
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