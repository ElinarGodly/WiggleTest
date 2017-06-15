using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using av = ApplicationVariables.AV.DefaultValues.VoucherCodes;

namespace WiggleClasses
{
    public class Gift
    {
        public decimal Value { get; set; }
        public string Code { get; set; }
        public int Qty { get; set; }

        public Gift(decimal value, string code, int qty)
        {
            this.Value = value;
            if (code == string.Empty) this.Code = av.Gift;
            else this.Code = code;
            this.Qty = qty;
        }
    }
}
