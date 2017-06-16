using av = ApplicationVariables.AV.DefaultValues.VoucherCodes;

namespace WiggleClasses
{
    public class Offer
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public decimal Threshold { get; set; }
        public string Subset { get; set; }

        public Offer() { }

        public Offer(decimal value, string code, decimal threshold, string subset)
        {
            this.Value = value;
            if (code == string.Empty) this.Code = av.Offer;
            else this.Code = code;
            this.Threshold = threshold;
            this.Subset = subset;
        }
    }
}

