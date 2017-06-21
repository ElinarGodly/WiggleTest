using System;
using System.Web.UI.WebControls;
using avSV = ApplicationVariables.AV.SystemValues;
using bl = WiggleBusinessLogic.BusinessLayer;
using cl = WiggleClasses;
using System.Collections.Generic;
using System.Web.UI;

namespace WiggleBasketWebPage
{
    public partial class Default : System.Web.UI.Page
    {
        private static cl.Basket cacheBasket;
        private static bool lastDefault = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cacheBasket = new cl.Basket();
            else
                tableBasket(cacheBasket);
        }
        
        private void addItemToBasket()//TODO in BL
        {
            cl.Item item = new cl.Item(int.Parse(tbItemQty.Text), tbItemName.Text, tbItemSubset.Text, decimal.Parse(tbItemValue.Text));
            cacheBasket.AddItemToBuy(item);
            tableBasket(cacheBasket);
        }

        private void addGiftToBasket(bool buy)//TODO in BL
        {
            cl.Gift gift = new cl.Gift(decimal.Parse(tbGiftValue.Text), tbGiftCode.Text, int.Parse(tbGiftQty.Text));
            cacheBasket.AddGift(gift, buy);
            tableBasket(cacheBasket);
        }

        private void addOfferToBasket() //TODO in BL
        {
            cl.Offer offer = new cl.Offer(decimal.Parse(tbOfferValue.Text),
                                        tbOfferCode.Text, decimal.Parse(tbOfferThreshold.Text), tbOfferSubset.Text);
            cacheBasket.ApplyOffer(offer);
            tableBasket(cacheBasket);
        }

        #region Table
        private void tableBasket(cl.Basket basket) //TODO in BL partial
        {

            while(tblBasket.Rows.Count!=0)
            {
                tblBasket.Rows.RemoveAt(0);
            }

            basket.CalcTotal();
            tblBasket.Visible = true;
            tableHeaders();

            foreach (var item in basket.BuyItems)
                addRowBuyItem(item);

            foreach (var gift in basket.BuyGifts)
                addRowBuyGift(gift);

            foreach (var gift in basket.ApplyGifts)
                addRowApplyGift(gift);

            if (basket.Offer.Code != null) addRowApplyOffer(basket.Offer);

            if (basket.VoucherMessage != string.Empty)
                addErrorRow(basket.VoucherMessage);

            addTotalRow(basket.BasketTotal);

            cacheBasket = basket; 
        }
        
        private void tableHeaders()
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

        private void addCell (string text, int colSpan, TableRow row)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            cell.ColumnSpan = colSpan;
            row.Cells.Add(cell);
        }

        private void addCellQty(int qty, TableRow row) //TODO can it be refactored?
        {
            TableCell cell = new TableCell();
            ImageButton btn = new ImageButton();
            Label lbl = new Label();
            btn.ID = String.Format(avSV.Buttons.Template, row.ID, avSV.Buttons.Delete);
            btn.ImageUrl = avSV.Paths.DeleteButtonImage;
            btn.Click += imgDelete_Click;
            cell.Controls.Add(btn);

            btn = new ImageButton();
            btn.ID = String.Format(avSV.Buttons.Template, row.ID, avSV.Buttons.Decrease);
            btn.ImageUrl = avSV.Paths.DecreaseButtonImage;
            btn.Click += btnDecrease_Click;
            cell.Controls.Add(btn);

            lbl.Text = qty.ToString();
            lbl.ID = String.Format(avSV.Buttons.Template, row.ID, avSV.Buttons.QuantityLabel);
            cell.Controls.Add(lbl);

            btn = new ImageButton();
            btn.ID = String.Format(avSV.Buttons.Template, row.ID, avSV.Buttons.Increase);
            btn.ImageUrl = avSV.Paths.IncreaseButtonImage;
            btn.Click += btnIncrease_Click;
            cell.Controls.Add(btn);

            row.Cells.Add(cell);
        }


        private void addCellQty(TableRow row)
        {
            TableCell cell = new TableCell();
            ImageButton btn = new ImageButton();
            Label lbl = new Label();
            btn.ID = String.Format(avSV.Buttons.Template, row.ID, avSV.Buttons.Delete);
            btn.ImageUrl = avSV.Paths.DeleteButtonImage;
            btn.Click += imgDelete_Click;
            cell.Controls.Add(btn);

            row.Cells.Add(cell);
        }

        private void addRowBuyItem(cl.Item item)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.RowBuy, tblBasket.Rows.Count);
            addCellQty(item.Qty, row);
            addCell((item.Subset == String.Empty) ? String.Format(avSV.TableValues.Templates.ItemNoSub, item.Name) :
                                    String.Format(avSV.TableValues.Templates.ItemWithSub, item.Name, item.Subset), 0,row);
            
            addCell((item.Value * item.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }


        private void addRowBuyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.RowBuy, tblBasket.Rows.Count);
            addCellQty (gift.Qty, row);
            addCell(String.Format(avSV.TableValues.Templates.GiftItem, gift.Value), 0, row);
            addCell((gift.Value * gift.Qty).ToString(), 0, row);
            tblBasket.Rows.Add(row);
        }
        
        private void addRowApplyGift(cl.Gift gift)
        {
            TableRow row = new TableRow();
            row.ID = String.Format(avSV.TableValues.RowLabels.RowApply, tblBasket.Rows.Count);
            addCellQty(gift.Qty, row);
            addCell(String.Format(avSV.TableValues.Templates.GiftVoucher, gift.Value, gift.Code), 2, row);
            tblBasket.Rows.Add(row);
        }

        private void addRowApplyOffer(cl.Offer offer)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.RowOffer;
            addCellQty(row);
            addCell((offer.Subset == String.Empty) ?
                    String.Format(avSV.TableValues.Templates.OfferNoSub, offer.Value, offer.Threshold, offer.Code) :
                    String.Format(avSV.TableValues.Templates.OfferWithSub, offer.Value, offer.Subset, offer.Threshold, offer.Code), 2, row);
            tblBasket.Rows.Add(row);
        }
        
        private void addErrorRow(string voucherMessage)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.RowMsg;
            addCell(voucherMessage, 3, row);
            tblBasket.Rows.Add(row);
        }

        private void addTotalRow(decimal total)
        {
            TableRow row = new TableRow();
            row.ID = avSV.TableValues.RowLabels.RowTotal;
            addCell(String.Format(avSV.TableValues.Templates.TotalSum, total.ToString()),3,row);
            tblBasket.Rows.Add(row);
        }
        #endregion

        #region Events
        protected void rblCreateChoice_SelectedIndexChanged(object sender, EventArgs e)//TODO make extensible testing on adding new items
        {
            string selectedValue = rblCreateChoice.SelectedValue;
            if (lastDefault) cacheBasket = new cl.Basket();

            switch (selectedValue)
            {
                case avSV.RadioButtonList.Gift.Button:
                    lastDefault = false;
                    setVisibleAndEnabled(avSV.RadioButtonList.Gift.setVE);
                    tableBasket(cacheBasket);
                    break;
                case avSV.RadioButtonList.Item.Button:
                    lastDefault = false;
                    setVisibleAndEnabled(avSV.RadioButtonList.Item.setVE);
                    tableBasket(cacheBasket);
                    break;
                case avSV.RadioButtonList.Offer.Button:
                    lastDefault = false;
                    setVisibleAndEnabled(avSV.RadioButtonList.Offer.setVE);
                    tableBasket(cacheBasket);
                    break;
                case avSV.RadioButtonList.Provided.Button:
                    lastDefault = true;
                    setVisibleAndEnabled(avSV.RadioButtonList.Provided.setVE);
                    rblProvidedChoice.ClearSelection();
                    break;
            }
        }

        private void setVisibleAndEnabled(List<bool> args)
        {
            panelGift.Visible = args[0];
            panelItem.Visible = args[1];
            panelOffer.Visible = args[2];
            panelProvided.Visible = args[3];

            btnBuy.Enabled = args[4];
            btnApply.Enabled = args[5];
        }

        protected void rblProvidedChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            bl bl1 = new bl();
            tableBasket(bl1.LoadBasket(rblProvidedChoice.SelectedIndex));
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Item.Button) addItemToBasket();
            else addGiftToBasket(true);
        }

        protected void btnApply_Click(object sender, EventArgs e) 
        {
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Offer.Button) addOfferToBasket();
            else addGiftToBasket(false);
        }

        protected void imgDelete_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string containerID = btn.ID.Remove(btn.ID.Length - 6);
            int index;
            indexOfItem(ref containerID, out index);

            if (containerID.Length.Equals(3))
            {
                if (index < cacheBasket.BuyItems.Count) cacheBasket.DeleteBuy(index, true);
                else cacheBasket.DeleteBuy(index - cacheBasket.BuyItems.Count, false);
            }
            else if (containerID.Equals(5))
            {
                cacheBasket.DeleteApply(index, true);
            }
            else
                cacheBasket.DeleteApply(null, false);

            tableBasket(cacheBasket);
        }

        private void indexOfItem(ref string containerID, out int index)
        {
            containerID = containerID.Remove(0, 3);
            index = 0;
            if (containerID.StartsWith(avSV.Buy))
            {
                index = int.Parse(containerID.Remove(0,3)) - 1;
                containerID = containerID.Remove(3);
            }
            else if (containerID.StartsWith(avSV.Apply))
            {
                containerID = containerID.Remove(0, 5);
                index = int.Parse(containerID) - cacheBasket.BuyItems.Count - 1;
            }
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string containerID = btn.ID.Remove(btn.ID.Length - 8);
            int index;
            indexOfItem(ref containerID, out index);

            if (containerID.Length.Equals(3))
            {
                if (index < cacheBasket.BuyItems.Count) cacheBasket.BuyItems[index].Qty--;
                else cacheBasket.BuyGifts[index - cacheBasket.BuyItems.Count].Qty--;
            }
            else if (containerID.Equals(5))
                cacheBasket.ApplyGifts[index].Qty--;

            tableBasket(cacheBasket);
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string containerID = btn.ID.Remove(btn.ID.Length - 8);
            int index;
            indexOfItem(ref containerID, out index);

            if (containerID.Length.Equals(3))
            {
                if (index < cacheBasket.BuyItems.Count) cacheBasket.BuyItems[index].Qty++;
                else cacheBasket.BuyGifts[index - cacheBasket.BuyItems.Count].Qty++;
            }
            else if (containerID.Equals(5))
                cacheBasket.ApplyGifts[index].Qty++;

            tableBasket(cacheBasket);
        }

        #endregion

    }
}