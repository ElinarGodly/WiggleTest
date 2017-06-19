using System.Collections.Generic;

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

        public struct SystemValues //TODO check variables names and methods for Capitals 
        {
            public struct Buttons
            {
                public static string templ = "{0}{1}";
                public static string delete = "Delete";
                public static string decrease = "Left";
                public static string increase = "Right";
                public static string qtyLbl = "qtyLbl";
            }

            public struct RadioButtonList
            {
                public struct Gift
                {
                    public const string btn = @"gift";
                    public static List<bool> setVE = new List<bool> { true, false, false, false, true, true}; 
                }

                public struct Item
                {
                    public const string btn = @"item";
                    public static List<bool> setVE = new List<bool> { false, true, false, false, true, false};
                }
                
                public struct Offer
                {
                    public const string btn = @"offer";
                    public static List<bool> setVE = new List<bool> { false, false, true, false, false, true};
                }
                
                public struct Default
                {
                    public const string btn = @"default"; //TODO think of a better name
                    public static List<bool> setVE = new List<bool> { false, false, false, true, false, false}; //TODO remove plan document
                }
            }

            public struct TableValues
            {               
                public struct Templates
                {
                    public static string itemNoSub = @"{0}";
                    public static string itemWithSub = @"{0} ({1} Category of Product)";
                    public static string giftItem = @"£{0} Gift Voucher";
                    public static string giftVoucher = @"£{0} Gift Voucher {1} applied.";
                    public static string offerNoSub = @"£{0} off baskets over £{1} Offer Voucher {2} applied.";
                    public static string offerWithSub = @"£{0} off {1} in baskets over £{2} Offer Voucher {3} applied.";
                    public static string totalSum = @"Total: £{0}";
                }

                public struct RowLabels
                {
                    public static string rowBuy = "rowBuy{0}";
                    public static string rowApply = "rowApply{0}";
                    public static string rowOffer = "rowOffer";
                    public static string rowMsg = "rowMsg";
                    public static string rowTotal = "rowTotal";
                }


                public static List<string> HeaderCells = new List<string> { "Qty Control", "Information", "Value" }; //TODO check names
            }
            
            public struct ErrorMessages
            {
                public static string SpendThresholdTemplate = "You have not reached the spend threshold for voucher {0}. Spend another £{1} to receive £{2} discount from your basket total.";
                public static string SubsetTemplate = "There are no products in your basket applicable to Voucher {0}.";
                public static string GiftFailApply = "There are no products in your basket to use your gift voucher with.";
            }
        }

    }
}
