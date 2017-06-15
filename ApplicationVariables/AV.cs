using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVariables
{
    public class AV
    {
        public struct DefaultValues
        {
            public struct VoucherCodes
            {
                public static string Gift = "XXX-XXX";
                public static string Offer = "YYY-YYY";
            }
        }

        public struct SystemValues
        {
            public struct DropDownLists // TODO remove or keep
            {
                public struct Items
                {
                    public static string ControlID = @"ddlItems";
                    
                }

                public struct Vouchers
                {
                    public static string ControlID = @"ddlVouchers";
                }

            }
            public struct RadioButtonList
            {
                public const string Gift = "gift";
                public const string Item = "item";
                public const string Offer = "offer";
            }

            public struct TableValues
            {
                public static List<string> HeaderCells = new List<string> { "Object Information", "Qty Control", "Value" };
            }

            public struct DataLayerData // TODO remove or keep
            {
                public const int Gift = 0;
                public const int Items = 1;
                public const int Offers = 2;
            }
        }


        public struct ErrorMessages
        {
            public static string SpendThresholdTemplate = "You have not reached the spend threshold for voucher {0}. Spend another £{1} to receive £{2} discount from your basket total.";
            public static string SubsetTemplate = "There are no products in your basket applicable to Voucher {0}.";


        }
    }
}
