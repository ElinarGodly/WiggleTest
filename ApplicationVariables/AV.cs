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
                public const string Gift = @"gift";
                public const string Item = @"item";
                public const string Offer = @"offer";
                public const string Default = @"default";
            }

            public struct TableValues
            {
                public struct RowIDs
                {
                    public static string rowBuyTmpl = @"rowItem{0}";
                    public static string rowApplyTmpl = @"rowApply{0}";
                    public static string rowError = @"rowError";
                    public static string rowTotal = @"rowTotal";
                }

                public struct ItemBuy
                {
                    public static string templEmpty = @"{0}";
                    public static string templFull = @"{0} ({1} Category of Product)";
                }

                public struct GiftBuy // TODO check if they can be just string not struct
                {
                    public static string tepml = @"£{0} Gift Voucher";
                }

                public struct ApplyOffer
                {
                    public static string tepmlOffer = @"1 x £{0} off baskets over £{1} Offer Voucher {2} applied";
                    public static string templOfferSubset = @"1 x £{0} off {1} in baskets over £{2} Offer Voucher {3} applied";
                    public static object[] args = { @"offer.qty", @"offer.Value", @"offer.Threshold", @"offer.Code" };//TODO can this work if not remove
                }

                public static string sum = @"Total: £{0}";


                public static List<string> HeaderCells = new List<string> { "Object Information", "Qty Control", "Value" };
            }

            public struct DataLayerData // TODO remove or keep
            {
                public const int Gift = 0;
                public const int Items = 1;
                public const int Offers = 2;
            }

            public struct ErrorMessages
            {
                public static string SpendThresholdTemplate = "You have not reached the spend threshold for voucher {0}. Spend another £{1} to receive £{2} discount from your basket total.";
                public static string SubsetTemplate = "There are no products in your basket applicable to Voucher {0}.";


            }
        }

    }
}
