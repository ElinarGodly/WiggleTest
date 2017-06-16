using cl = WiggleClasses;
using dl = WiggleData.DataLayer;

namespace WiggleBusinessLogic
{
    public class BusLayer
    {
        public cl.Basket LoadBasket(int index)
        {
            dl dl1 = new dl();
            return dl1.GetBasket(index);
        }
    }
}
