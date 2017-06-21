using av = ApplicationVariables.AV.DefaultValues.VoucherCodes;

namespace WiggleClasses
{
    public class Offer
    {
        public string Code { get; set; }
        public string Subset { get; set; }
        public decimal Threshold { get; set; }
        public decimal Value { get; set; }

        public Offer() { }

        public Offer(string code, string subset, decimal threshold, decimal value)
        {
            if (code == string.Empty) Code = av.Offer;
            else Code = code;
            Subset = subset;
            Threshold = threshold;
            Value = value;
        }
    }
}

