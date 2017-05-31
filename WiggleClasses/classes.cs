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

        }

        public void CalcTotal()
        {

        }

        public void CheckOffer()
        {

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
        public decimal Treshold { get; set; }
        public string Subset { get; set; }

        public Offer(decimal value, string code, decimal treshold, string subset)
        {
            this.Value = value;
            if (code == string.Empty) this.Code = "YYY-YYY";
            else this.Code = code;
            this.Treshold = treshold;
            this.Subset = subset;
        }
    }
}