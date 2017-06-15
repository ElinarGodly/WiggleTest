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
        public List<cl.Item> AllItems { get; set; }
        public List<cl.Offer> AllOffers { get; set; }
        public List<cl.Gift> AllGift { get; set; }

        public DataLayer()
        {
            this.AllItems = new List<cl.Item>
            {
                new cl.Item(1, "Hat", String.Empty, 10.50m),
                new cl.Item(1, "Jumper", String.Empty, 54.65m),
                new cl.Item(1, "Hat", String.Empty, 25.00m),
                new cl.Item(1, "Jumper", String.Empty, 26.00m),
                new cl.Item(1, "Head Light", "Head Gear", 3.50m)
        };
            this.AllOffers = new List<cl.Offer>
            {
                new cl.Offer(5.00m, "YYY-YYY", 50.00m, "Head Gear"),
                new cl.Offer(5.00m, "YYY-YYY", 50.00m, String.Empty)

        };
            this.AllGift = new List<cl.Gift>
            {
                new cl.Gift(5.00m, String.Empty, 1),
                new cl.Gift(30.00m, String.Empty, 1)
            };
        }
    }
}
