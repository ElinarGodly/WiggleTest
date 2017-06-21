using System;
using System.Collections.Generic;
using av = ApplicationVariables.AV.SystemValues.ErrorMessages;

namespace WiggleClasses
{
    public class Basket
    {
        private List<Item> buyItems;  //TODO maybe make them private it will however require
        private List<Gift> buyGifts; // a lot of new methods which will decrease performance
        private List<Gift> applyGifts;
        private Offer offer; //TODO check what these three need to remain private
        private decimal basketTotal;
        private string voucherMessage;

        public Basket()
        {
            buyItems = new List<Item>();
            buyGifts = new List<Gift>();
            applyGifts = new List<Gift>();
            offer = new Offer();
            basketTotal = 0m;
            voucherMessage = string.Empty;
        }
        #region Getters
        public List<Item> BuyItems
        {
            get { return buyItems; }
        }

        public List<Gift> BuyGifts
        {
            get { return buyGifts; }
        }

        public List<Gift> ApplyGifts
        {
            get { return applyGifts; }
        }

        public Offer Offer
        {
            get { return offer; }
        }

        public decimal BasketTotal
        {
            get { return basketTotal; }
        }

        public string VoucherMessage
        {
            get { return voucherMessage; }
        }
        #endregion
        
        public void ChangeItemQuantity(int index, int qty)
        {
            if ((buyItems[index].Qty + qty) > 0) buyItems[index].Qty = buyItems[index].Qty + qty;
            else buyItems[index].Qty = 0;
        }

        public void ChangeGiftQuantity(int index, int qty, bool buy)
        {
            if (buy)
                if ((buyGifts[index].Qty + qty) > 0) buyGifts[index].Qty = buyGifts[index].Qty + qty;
                else buyGifts[index].Qty = 0;
            else
                if ((applyGifts[index].Qty + qty) > 0) applyGifts[index].Qty = applyGifts[index].Qty + qty;
            else applyGifts[index].Qty = 0;
        }
        
        public void AddItemToBuy(Item inputItem)
        {
            bool isNewItem = true;
            foreach (var item in buyItems)
                if (item.Name == inputItem.Name)
                {
                    item.Qty += inputItem.Qty;
                    isNewItem = false;
                    break;
                }

            if (isNewItem)
                buyItems.Add(inputItem);
        }

        public void AddGift(Gift inputGift, bool toBuy)
        {
            if (toBuy == true)
                addGiftInBasket(inputGift, this.buyGifts);
            else
                addGiftInBasket(inputGift, this.applyGifts);
        }

        private void addGiftInBasket(Gift inputGift, List<Gift> whereToAdd)
        {
            bool isNewGift = true;
            foreach (var gift in whereToAdd)
                if (gift.Value == inputGift.Value)
                {
                    gift.Qty += inputGift.Qty;
                    isNewGift = false;
                    break;
                }

            if (isNewGift)
                whereToAdd.Add(inputGift);
        }

        public void ApplyOffer(Offer voucher)
        {
            if (offer.Code == null)
                offer = voucher;
        }

        public void DeleteBuy(int index, bool item)
        {
            if (item)
                buyItems.RemoveAt(index);
            else
                buyGifts.RemoveAt(index);
        }

        public void DeleteApply(int? index)
        {
            if (index != null)
                applyGifts.RemoveAt((int)index);
            else offer = new Offer();
        }

        public void CalcTotal()
        {
            //sum item values*qty and check if any is applicable for offer subtype discount and if such offer is added
            int itemIndex = 0;
            bool offerSubset = false;
            basketTotal = 0.00m;
            voucherMessage = String.Empty;
            foreach (var item in buyItems)
            {
                if (item.Subset == offer.Subset)
                {
                    if (offerSubset == true)
                    {
                        itemIndex = (item.Value > buyItems[itemIndex].Value) ? buyItems.IndexOf(item) : itemIndex;
                    }
                    else
                    {
                        itemIndex = buyItems.IndexOf(item);
                        offerSubset = true;
                    }
                }
                basketTotal += item.Value * item.Qty;
            }

            //given offerSubset (true/false) and itemIndex int this checks, applies and messages for the offer voucher cases
            if (offer.Code != null)
                checkOffer(offerSubset, itemIndex);

            //applies the gift vouchers to the item values
            foreach (var gift in applyGifts)
            {
                basketTotal -= gift.Value * gift.Qty;
                if (basketTotal < 0)
                {
                    voucherMessage = String.Format(av.GiftFailApply,basketTotal);
                    basketTotal = 0.00m;
                }
            }
            //adds the cost for the gift vouchers to be purchased
            foreach (var gift in buyGifts)
                basketTotal += gift.Value * gift.Qty;
        }

        private void checkOffer(bool offerSubset, int itemIndex)
        {
            if (offer.Threshold < basketTotal)
                if (offer.Subset == String.Empty)
                    basketTotal -= offer.Value;
                else if (offerSubset)
                {
                    decimal change = (buyItems[itemIndex].Value < offer.Value) ?
                                        buyItems[itemIndex].Value : offer.Value;
                    basketTotal -= change;
                }
                else
                    voucherMessage = String.Format(av.SubsetTemplate, offer.Code);
            else
            {
                decimal needed = (offer.Threshold - basketTotal) + 0.01m; // This 0.01 is used to display proper error message.
                voucherMessage = String.Format(av.SpendThresholdTemplate, offer.Code, needed, offer.Value);
            }
        }
    }
}
