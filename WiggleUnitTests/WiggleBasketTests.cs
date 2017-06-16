using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cl = WiggleClasses;
using dl = WiggleData.DataLayer;


namespace WiggleTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Basket1()
        {
            //Arrange
            dl dl1 = new dl();
            cl.Basket basket = dl1.GetBasket(0);
            string expectMsg = String.Empty;
            decimal expectSum = 60.15m;
            
            //Assert
            Assert.AreEqual(String.Empty, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket2()
        {
            //Arrange
            dl dl1 = new dl();
            cl.Basket basket = dl1.GetBasket(1);
            string expectMsg = "There are no products in your basket applicable to Voucher YYY-YYY.";
            decimal expectSum = 51.00m;
            
            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket3()
        {
            //Arrange
            dl dl1 = new dl();
            cl.Basket basket = dl1.GetBasket(2);
            string expectMsg = String.Empty;
            decimal expectSum = 51.00m;
            
            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket4()
        {
            //Arrange
            dl dl1 = new dl();
            cl.Basket basket = dl1.GetBasket(3);
            string expectMsg = String.Empty;
            decimal expectSum = 41.00m;
            
            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

        [TestMethod]
        public void Basket5()
        {
            //Arrange
            dl dl1 = new dl();
            cl.Basket basket = dl1.GetBasket(4);
            string expectMsg = "You have not reached the spend threshold for voucher YYY-YYY. Spend another £25.01 to receive £5.00 discount from your basket total.";
            decimal expectSum = 55.00m;
            
            //Assert
            Assert.AreEqual(expectMsg, basket.VoucherMessage);
            Assert.AreEqual(expectSum, basket.BasketTotal);
        }

    }
}