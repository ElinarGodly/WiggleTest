namespace WiggleClasses
{
    public class Item
    {
        
        public string Name { get; set; }
        public string Subset { get; set; }
        public decimal Value { get; set; }
        public int Qty { get; set; } //TODO check about making them private 

        public Item(string name, string subset, decimal value, int qty)
        {
            Name = name;
            Subset = subset;
            Value = value;
            Qty = qty;
        }
    }
}
