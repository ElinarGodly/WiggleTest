using av = ApplicationVariables.AV.DefaultValues.VoucherCodes;

namespace WiggleClasses
{
    public class Gift
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
        public int Qty { get; set; }

        public Gift(string code, decimal value, int qty)
        {
            
            if (code == string.Empty) Code = av.Gift;
            else Code = code;
            Value = value;
            Qty = qty;
        }
    }
}
