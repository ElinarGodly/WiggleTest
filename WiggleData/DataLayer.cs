using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 10.50m);
            cl.Item item2 = new cl.Item(1, "Jumper", String.Empty, 54.65m);
            cl.Gift gift = new cl.Gift(5.00m, String.Empty, 1);

            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket2
            item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            cl.Offer offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, "Head Gear");
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket3
            item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            cl.Item item3 = new cl.Item(1, "Head Light", "Head Gear", 3.50m);
            offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, "Head Gear");
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddItemToBuy(item3);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);

            //Basket4
            item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, String.Empty);
            gift = new cl.Gift(5.00m, String.Empty, 1);
            basket = new cl.Basket();
            
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.ApplyOffer(offer);
            basket.CalcTotal();
            defaultBaskets.Add(basket);
            //Basket5
            item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            gift = new cl.Gift(30.00m, String.Empty, 1);
            offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, String.Empty);
            basket = new cl.Basket();

            basket.AddItemToBuy(item1);
            basket.AddGift(gift, true);
            basket.ApplyOffer(offer);
            basket.CalcTotal(); //todo remove
            defaultBaskets.Add(basket);
        }

        public cl.Basket GetBasket(int index)
        {
            return defaultBaskets[index];
        }
    }
}
