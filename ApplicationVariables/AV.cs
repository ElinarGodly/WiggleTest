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

        public struct SystemValues
        {
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
                    public static List<bool> setVE = new List<bool> { false, false, false, true, false, false};
                }
                
            }

            public struct TableValues
            {               
                public struct Templates
                {
                    public static string itemNoSub = @"{0}";
                    public static string itemWithSub = @"{0} ({1} Category of Product)";
                    public static string giftItem = @"£{0} Gift Voucher";
                    public static string giftVoucher = @"{0} x £{1} Gift Voucher {2} applied.";
                    public static string offerNoSub = @"1 x £{0} off baskets over £{1} Offer Voucher {2} applied.";
                    public static string offerWithSub = @"1 x £{0} off {1} in baskets over £{2} Offer Voucher {3} applied.";
                    public static string totalSum = @"Total: £{0}";
                }

                public static List<string> HeaderCells = new List<string> { "Object Information", "Qty Control", "Value" };
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
