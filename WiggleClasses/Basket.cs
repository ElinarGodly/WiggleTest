using System;
using System.Collections.Generic;
using av = ApplicationVariables.AV.SystemValues.ErrorMessages;

namespace WiggleClasses
{
    public class Basket
    {
        public List<Item> BuyItems { get; set; }
        public List<Gift> BuyGifts { get; set; }
        public List<Gift> ApplyGift { get; set; }
        public Offer Offer { get; set; }
        public decimal BasketTotal { get; set; }
        public string VoucherMessage { get; set; }

        public Basket()
        {
            this.BuyItems = new List<Item>();
            this.BuyGifts = new List<Gift>();
            this.ApplyGift = new List<Gift>();
            this.Offer = new Offer();
            this.BasketTotal = 0m;
            this.VoucherMessage = string.Empty;
        }

        public void AddItemToBuy(Item inputItem)
        {
            bool isNewItem = true;
            foreach (var item in BuyItems)
                if (item.Name == inputItem.Name)
                {
                    item.Qty += inputItem.Qty;
                    isNewItem = false;
                    break;
                }

            if (isNewItem)
                this.BuyItems.Add(inputItem);
        }

        public void AddGift(Gift inputGift, bool toBuy)
        {
            if (toBuy == true)
                AddGiftInBasket(inputGift, this.BuyGifts);
            else
                AddGiftInBasket(inputGift, this.ApplyGift);
        }

        private void AddGiftInBasket(Gift inputGift, List<Gift> whereToAdd)
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
            if (this.Offer.Code == null)
                this.Offer = voucher;
        }

        public void DeleteBuy(int index, bool item)
        {
            if (item)
                this.BuyItems.RemoveAt(index);
            else
                this.BuyGifts.RemoveAt(index);
        }

        public void DeleteApply(int? index, bool gift)
        {
            if (gift)
                this.ApplyGift.RemoveAt((int)index);
            else this.Offer = new Offer();
        }

        public void CalcTotal()
        {
            //sum item values*qty and check if any is applicable for offer subtype discount and if such offer is added
            int itemIndex = 0;
            bool offerSubset = false;
            this.BasketTotal = 0.00m;
            foreach (var item in this.BuyItems)
            {
                if (item.Subset == Offer.Subset)
                {
                    if (offerSubset == true)
                    {
                        itemIndex = (item.Value > this.BuyItems[itemIndex].Value) ? BuyItems.IndexOf(item) : itemIndex;
                    }
                    else
                    {
                        itemIndex = BuyItems.IndexOf(item);
                        offerSubset = true;
                    }
                }
                this.BasketTotal += item.Value * item.Qty;
            }

            //given offerSubset (true/false) and itemIndex int this checks, applies and messages for the offer voucher cases
            if (this.Offer.Code != null)
                CheckOffer(offerSubset, itemIndex);

            //applies the gift vouchers to the item values
            foreach (var gift in this.ApplyGift)
            {
                this.BasketTotal -= gift.Value * gift.Qty;
                if (this.BasketTotal < 0)
                {
                    this.BasketTotal = 0.00m;
                    this.VoucherMessage = av.GiftFailApply;
                }
            }

            //adds the cost for the gift vouchers to be purchased
            foreach (var gift in this.BuyGifts)
                this.BasketTotal += gift.Value * gift.Qty;

        }

        private void CheckOffer(bool offerSubset, int itemIndex)
        {
            if (this.Offer.Threshold < this.BasketTotal)
            {
                if (this.Offer.Subset == String.Empty)
                {
                    this.BasketTotal -= Offer.Value;
                }
                else if (offerSubset)
                {
                    decimal change = (this.BuyItems[itemIndex].Value < Offer.Value) ?
                                        this.BuyItems[itemIndex].Value : this.BuyItems[itemIndex].Value - Offer.Value;
                    this.BasketTotal -= change;
                }
                else
                    this.VoucherMessage = String.Format(av.SubsetTemplate, Offer.Code);
            }
            else
            {
                decimal needed = (Offer.Threshold - this.BasketTotal) + 0.01m; // This 0.01 is used to display proper error message.
                this.VoucherMessage = String.Format(av.SpendThresholdTemplate, Offer.Code, needed, Offer.Value);
            }
        }
    }
}
