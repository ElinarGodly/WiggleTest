using System;
using System.Collections.Generic;
using cl = WiggleClasses;

namespace WiggleData
{
    public class DataLayer
    {
        private static List<cl.Basket> defaultBaskets;

        public DataLayer()
        {
            defaultBaskets = new List<cl.Basket>();
            
            //Basket1 
            cl.Basket basket = new cl.Basket();
            cl.Item item1 = new cl.Item( "Hat", String.Empty, 10.50m, 1);
            cl.Item item2 = new cl.Item( "Jumper", String.Empty, 54.65m, 1);
            cl.Gift gift = new cl.Gift(String.Empty, 5.00m, 1);

            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket2
            item1 = new cl.Item("Hat", String.Empty, 25.00m, 1);
            item2 = new cl.Item("Jumper", String.Empty, 26.00m, 1);
            cl.Offer offer = new cl.Offer("YYY-YYY", "Head Gear", 50.00m, 5.00m);
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket3
            item1 = new cl.Item("Hat", String.Empty, 25.00m, 1);
            item2 = new cl.Item("Jumper", String.Empty, 26.00m, 1 );
            cl.Item item3 = new cl.Item("Head Light", "Head Gear", 3.50m, 1);
            offer = new cl.Offer("YYY-YYY", "Head Gear", 50.00m, 5.00m);
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddItemToBuy(item3);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);

            //Basket4
            item1 = new cl.Item("Hat", String.Empty, 25.00m, 1);
            item2 = new cl.Item("Jumper", String.Empty, 26.00m, 1);
            offer = new cl.Offer("YYY-YYY", String.Empty, 50.00m, 5.00m);
            gift = new cl.Gift(String.Empty, 5.00m, 1);
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket5
            item1 = new cl.Item("Hat", String.Empty, 25.00m, 1);
            gift = new cl.Gift( String.Empty, 30.00m, 1);
            offer = new cl.Offer("YYY-YYY", String.Empty, 50.00m, 5.00m);
            basket = new cl.Basket();

            basket.AddItemToBuy(item1);
            basket.AddGift(gift, true);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
        }

        public cl.Basket GetBasket(int index)
        {
            return defaultBaskets[index];
        }
    }
}
