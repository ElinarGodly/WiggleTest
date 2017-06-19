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

        //public void AddItemToBasket(ref cl.Basket basket, List<string> args)
        //{
        //    cl.Item item = new cl.Item(int.Parse(tbItemQty.Text), tbItemName.Text, tbItemSubset.Text, decimal.Parse(tbItemValue.Text));
        //    cacheBasket.AddItemToBuy(item);
        //    TableBasket(cacheBasket);
        //}

        //public void AddGiftToBasket(ref cl.Basket basket, List<string> args, bool buy)
        //{
        //    cl.Gift gift = new cl.Gift(decimal.Parse(tbGiftValue.Text), tbGiftCode.Text, int.Parse(tbGiftQty.Text));
        //    cacheBasket.AddGift(gift, buy);
        //    TableBasket(cacheBasket);
        //}

        //public void AddOfferToBasket(ref cl.Basket basket, List<string> args)
        //{
        //    cl.Offer offer = new cl.Offer(decimal.Parse(tbOfferValue.Text),
        //                                tbOfferCode.Text, decimal.Parse(tbOfferThreshold.Text), tbOfferSubset.Text);
        //    cacheBasket.ApplyOffer(offer);
        //    TableBasket(cacheBasket);
        //}
    }
}
