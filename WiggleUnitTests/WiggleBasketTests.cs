using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cl = WiggleClasses;

namespace WiggleTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Basket1()
        {
            //Arrange
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 10.50m);
            cl.Item item2 = new cl.Item(1, "Jumper", String.Empty, 54.65m);
            cl.Gift gift = new cl.Gift(5.00m, String.Empty, 1);
            cl.Basket basket = new cl.Basket();
            string expectMsg = String.Empty;
            decimal expectSum = 60.15m;

            //Act
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.CalcTotal();

            //Assert
            Assert.AreEqual(String.Empty, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket2()
        {
            //Arrange
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            cl.Item item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            cl.Offer offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, "Head Gear");
            cl.Basket basket = new cl.Basket();
            string expectMsg = "There are no products in your basket applicable to Voucher YYY-YYY.";
            decimal expectSum = 51.00m;

            //Act
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.ApplyOffer(offer);
            basket.CalcTotal();

            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket3()
        {
            //Arrange
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            cl.Item item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            cl.Item item3 = new cl.Item(1, "Head Light", "Head Gear", 3.50m);
            cl.Offer offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, "Head Gear");
            cl.Basket basket = new cl.Basket();
            string expectMsg = String.Empty;
            decimal expectSum = 51.00m;

            //Act
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddItemToBuy(item3);
            basket.ApplyOffer(offer);
            basket.CalcTotal();

            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket4()
        {
            //Arrange
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            cl.Item item2 = new cl.Item(1, "Jumper", String.Empty, 26.00m);
            cl.Offer offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, String.Empty);
            cl.Gift gift = new cl.Gift(5.00m, String.Empty, 1);
            cl.Basket basket = new cl.Basket();
            string expectMsg = String.Empty;
            decimal expectSum = 41.00m;

            //Act
            basket.AddItemToBuy(item1);
            basket.AddItemToBuy(item2);
            basket.AddGift(gift, false);
            basket.ApplyOffer(offer);
            basket.CalcTotal();

            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket5()
        {
            //Arrange
            cl.Item item1 = new cl.Item(1, "Hat", String.Empty, 25.00m);
            cl.Gift gift = new cl.Gift(30.00m, String.Empty, 1);
            cl.Offer offer = new cl.Offer(5.00m, "YYY-YYY", 50.00m, String.Empty);
            cl.Basket basket = new cl.Basket();
            string expectMsg = "You have not reached the spend threshold for voucher YYY-YYY. Spend another £25.01 to receive £5.00 discount from your basket total.";
            decimal expectSum = 55.00m;

            //Act
            basket.AddItemToBuy(item1);
            basket.AddGift(gift, true);
            basket.ApplyOffer(offer);
            basket.CalcTotal();

            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

    }
}