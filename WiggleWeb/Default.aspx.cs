using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avSV = ApplicationVariables.AV.SystemValues;
using bl = WiggleBusinessLogic.BusLayer;
using dl = WiggleData.DataLayer;
using cl = WiggleClasses;

namespace WiggleBasketWebPage
{
    public partial class Default : System.Web.UI.Page
    {
        private static cl.Basket basket;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) //TODO
            {

            }
            else
            {
                basket = new cl.Basket();
            }
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
            ClearTable();
            TableHeaders();

            foreach (var item in basket.BuyItems)
                AddItemRow(item);

            foreach (var gift in basket.BuyGifts)
                AddGiftRow(gift, true);

            foreach (var gift in basket.ApplyGift)
                AddGiftRow(gift, false);

            if(basket.Offer.Code!=null) ApplyOfferRow(basket.Offer);

            if (basket.VoucherMessage != string.Empty)
            {
                AddErrorRow(basket.VoucherMessage);
                AddTotalRow(basket.BasketTotal);
            }
            else
                AddTotalRow(basket.BasketTotal);

        }

        private void ClearTable()
        {
            for (int i = 0; i < tblBasket.Rows.Count; i++)
                tblBasket.Rows.RemoveAt(i);
        }

        private void TableHeaders()
        {
            tblBasket.Visible = true;
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

        private void AddItemRow(cl.Item item)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            row.ID = String.Format(avSV.TableValues.RowIDs.rowBuyTmpl, tblBasket.Rows.Count);

            cell.Text = (item.Subset == String.Empty) ? String.Format(avSV.TableValues.ItemBuy.templEmpty, item.Name) :
                                    String.Format(avSV.TableValues.ItemBuy.templFull, item.Name, item.Subset);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = item.Qty.ToString();
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = (item.Value * item.Qty).ToString();
            row.Cells.Add(cell);

            tblBasket.Rows.Add(row);
        }

        private void AddGiftRow(cl.Gift gift, bool buy) //maybe another method for apply gift
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            row.ID = (buy) ? String.Format(avSV.TableValues.RowIDs.rowBuyTmpl, tblBasket.Rows.Count) :
                           String.Format(avSV.TableValues.RowIDs.rowApplyTmpl, tblBasket.Rows.Count);

            cell.Text = String.Format(avSV.TableValues.GiftBuy.tepml, gift.Value);
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = gift.Qty.ToString();
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = (gift.Value * gift.Qty).ToString();
            row.Cells.Add(cell);

            tblBasket.Rows.Add(row);
        }

        private void ApplyOfferRow(cl.Offer offer)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            row.ID = String.Format(avSV.TableValues.RowIDs.rowApplyTmpl, tblBasket.Rows.Count);

            cell.Text = (offer.Subset == String.Empty) ?
                                    String.Format(avSV.TableValues.ApplyOffer.tepmlOffer, offer.Value, offer.Threshold, offer.Code) :
                                    String.Format(avSV.TableValues.ApplyOffer.templOfferSubset, offer.Value, offer.Subset, offer.Threshold, offer.Code);
            row.Cells.Add(cell);

            tblBasket.Rows.Add(row);
            tblBasket.Rows[tblBasket.Rows.Count - 1].Cells[0].ColumnSpan = 3;
        }

        private void AddErrorRow(string voucherMessage)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            row.ID = avSV.TableValues.RowIDs.rowError;

            cell.Text = voucherMessage;
            row.Cells.Add(cell);

            tblBasket.Rows.Add(row);
            tblBasket.Rows[tblBasket.Rows.Count - 1].Cells[0].ColumnSpan = 3;
        }

        private void AddTotalRow(decimal total)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            row.ID = avSV.TableValues.RowIDs.rowTotal;

            cell.Text = String.Format(avSV.TableValues.sum, total.ToString());
            row.Cells.Add(cell);

            tblBasket.Rows.Add(row);
            tblBasket.Rows[tblBasket.Rows.Count - 1].Cells[0].ColumnSpan = 3;
        }

        private void AddRow()
        {
            // add a row with cells with the approprioate values
        }





        #endregion

        //-------------------------------------------------------------------------EVENTS

        protected void rblCreateChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rblCreateChoice.SelectedValue;

            switch (selectedValue)
            {
                case avSV.RadioButtonList.Gift:
                panelGift.Visible = true;
                panelItem.Visible = false;
                panelOffer.Visible = false;
                panelDefault.Visible = false;
                btnApply.Enabled = true;
                btnBuy.Enabled = true;
                break;
                case avSV.RadioButtonList.Item:
                panelGift.Visible = false;
                panelItem.Visible = true;
                panelOffer.Visible = false;
                panelDefault.Visible = false;
                btnApply.Enabled = false;
                btnBuy.Enabled = true;
                break;
                case avSV.RadioButtonList.Offer:
                panelGift.Visible = false;
                panelItem.Visible = false;
                panelOffer.Visible = true;
                panelDefault.Visible = false;
                btnApply.Enabled = true;
                btnBuy.Enabled = false;
                break;
                case avSV.RadioButtonList.Default:
                panelGift.Visible = false;
                panelItem.Visible = false;
                panelOffer.Visible = false;
                panelDefault.Visible = true;
                btnApply.Enabled = false;
                btnBuy.Enabled = false;
                break;
            }
            //btnApply.Visible = true; // TODO check if its not possible 
            //btnBuy.Visible = true;
        }

        //protected void btnAddNew_Click(object sender, EventArgs e) //TODO remove or keep
        //{

        //}


        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Item) AddItemToBasket();
            else AddGiftToBasket(true);
        }
        
        protected void btnApply_Click(object sender, EventArgs e) //TODO check all ifs for proper syntax no bool == true
        {
            if (!tblBasket.Visible) TableHeaders();
            if (rblCreateChoice.SelectedValue == avSV.RadioButtonList.Offer) AddOfferToBasket();
            else AddGiftToBasket(false);
            //TODO check if values are not filled and place random from the datalayer lists if ()
        }

        protected void rblDefaultChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            dl dl1 = new dl();
            TableBasket(dl1.GetBasket(rblDefaultChoice.SelectedIndex));

        }

    }
}