using System;
using System.Collections.Generic;
using cl = WiggleClasses;
using dl = WiggleData.DataLayer;

namespace WiggleBusinessLogic
{
    public class BusinessLayer
    {
        public cl.Basket LoadBasket(int index)
        {
            dl dl1 = new dl();
            return dl1.GetBasket(index);
        }

        public void AddItemToBasket(ref cl.Basket basket, string name, string subset, string value, string qty)
        {
            cl.Item item = new cl.Item(name, subset, decimal.Parse(value), int.Parse(qty));
            basket.AddItemToBuy(item);
        }

        public void AddGiftToBasket(ref cl.Basket basket, bool buy,  string code, string value, string qty)
        {
            cl.Gift gift = new cl.Gift(code, decimal.Parse(value), int.Parse(qty));
            basket.AddGift(gift, buy);
        }

        public void AddOfferToBasket(ref cl.Basket basket, string code, string subset, string threshold, string value)
        {
            cl.Offer offer = new cl.Offer(code, subset, decimal.Parse(threshold), decimal.Parse(value));
            basket.ApplyOffer(offer);
        }

        public void DeleteBought(ref cl.Basket basket, int index, bool item)
        {
            basket.DeleteBuy(index, item);
        }

        public void DeleteApplied(ref cl.Basket basket, int index)
        {
            basket.DeleteApply(index);
        }

        public void ChangeItemQuantity(ref cl.Basket basket, int index, int qty)
        {
            basket.ChangeItemQuantity(index, qty);
        }

        public void ChangeGiftQuantity(ref cl.Basket basket, int index, int qty, bool buy)
        {
            basket.ChangeGiftQuantity(index, qty, buy);
        }
    }
}
