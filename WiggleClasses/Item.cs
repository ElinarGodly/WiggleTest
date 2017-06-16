namespace WiggleClasses
{
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
}
