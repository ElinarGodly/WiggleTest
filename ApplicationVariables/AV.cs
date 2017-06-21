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
            public struct Paths
            {
                public static string DeleteButtonImage = @"GraphicResources\red-delete.png";
                public static string DecreaseButtonImage = @"GraphicResources\left-blue-jelly-arrowhead.png";
                public static string IncreaseButtonImage = @"GraphicResources\right-blue-jelly-arrowhead.png";
            }

            public struct Buttons
            {
                public static string Template = "{0}{1}";
                public static string Delete = "Delete";
                public static string Decrease = "Decrease";
                public static string Increase = "Increase";
                public static string QuantityLabel = "QtyLbl";
            }

            public struct RadioButtonList
            {
                public struct Gift
                {
                    public const string Button = "gift";
                    public static List<bool> setVE = new List<bool> { true, false, false, false, true, true}; 
                }

                public struct Item
                {
                    public const string Button = "item";
                    public static List<bool> setVE = new List<bool> { false, true, false, false, true, false};
                }
                
                public struct Offer
                {
                    public const string Button = "offer";
                    public static List<bool> setVE = new List<bool> { false, false, true, false, false, true};
                }
                
                public struct Provided
                {
                    public const string Button = "provided"; 
                    public static List<bool> setVE = new List<bool> { false, false, false, true, false, false}; //TODO maybe remove plan document, move Please Read outside of the project
                }
            }

            public struct TableValues
            {               
                public struct Templates
                {
                    public static string ItemNoSub = "{0}";
                    public static string ItemWithSub = "{0} ({1} Category of Product)";
                    public static string GiftItem = "£{0} Gift Voucher";
                    public static string GiftVoucher = "£{0} Gift Voucher {1} applied.";
                    public static string OfferNoSub = "£{0} off baskets over £{1} Offer Voucher {2} applied.";
                    public static string OfferWithSub = "£{0} off {1} in baskets over £{2} Offer Voucher {3} applied.";
                    public static string TotalSum = "Total: £{0}";
                }

                public struct RowLabels
                {
                    public static string RowBuy = "rowBuy{0}";
                    public static string RowApply = "rowApply{0}";
                    public static string RowOffer = "rowOffer";
                    public static string RowMsg = "rowMsg";
                    public static string RowTotal = "rowTotal";

                    public static string Buy = "Buy";
                    public static string Apply = "Apply";
                }


                public static List<string> HeaderCells = new List<string> { "Qty Control", "Information", "Value" }; //TODO check names
            }
            
            public struct ErrorMessages
            {
                public static string SpendThresholdTemplate = "You have not reached the spend threshold for voucher {0}. Spend another {1} to receive £{2} discount from your basket total.";
                public static string SubsetTemplate = "There are no products in your basket applicable to Voucher {0}.";
                public static string GiftFailApply = "You have £{0} left to spend with your gift vouchers.";
            }
        }

    }
}
