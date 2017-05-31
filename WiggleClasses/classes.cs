using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {
                if (item.Name == inputItem.Name)
                {
                    item.Qty += inputItem.Qty;
                    isNewItem = false;
                    break;
                }
            }
            if (isNewItem == true)
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
            {
                if (gift.Value == inputGift.Value)
                {
                    gift.Qty += inputGift.Qty;
                    isNewGift = false;
                    break;
                }
            }
            if (isNewGift == true)
                whereToAdd.Add(inputGift);
        }

        public void ApplyOffer(Offer voucher)
        {
            if (this.Offer.Code == null)
                this.Offer = voucher;
        }

        public void CalcTotal()
        {
            //sum item values*qty and check if any is applicable for offer subtype discount and if such offer is added
            int itemIndex = 0;
            bool offerSubset = false;
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
                this.BasketTotal -= gift.Value * gift.Qty;


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
                else if (offerSubset == true)
                {
                    decimal change = (this.BuyItems[itemIndex].Value < Offer.Value) ?
                                        this.BuyItems[itemIndex].Value : this.BuyItems[itemIndex].Value - Offer.Value;
                    this.BasketTotal -= change;
                }
                else
                    this.VoucherMessage = String.Format("There are no products in your basket applicable to Voucher {0}.", Offer.Code);
            }
            else
            {
                decimal needed = (Offer.Threshold - this.BasketTotal) + 0.01m;
                this.VoucherMessage = String.Format(
                    "You have not reached the spend threshold for voucher {0}. Spend another £{1} to receive £{2} discount from your basket total."
                    , Offer.Code, needed, Offer.Value);
            }
        }
    }

    public class Item
    {
        public int Qty { get; set; }
        public string Name { get; set; }
        public string Subset { get; set; }
        public decimal Value { get; set; }

        public Item(int qty, string name, string subset, decimal value)
        {
            this.Qty = qty;
            this.Name = name;
            this.Subset = subset;
            this.Value = value;
        }
    }

    public class Gift
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public int Qty { get; set; }

        public Gift(decimal value, string code, int qty)
        {
            this.Value = value;
            if (code == string.Empty) this.Code = "XXX-XXX";
            else this.Code = code;
            this.Qty = qty;
        }
    }

    public class Offer
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public decimal Threshold { get; set; }
        public string Subset { get; set; }

        public Offer() { }

        public Offer(decimal value, string code, decimal threshold, string subset)
        {
            this.Value = value;
            if (code == string.Empty) this.Code = "YYY-YYY";
            else this.Code = code;
            this.Threshold = threshold;
            this.Subset = subset;
        }
    }
}

