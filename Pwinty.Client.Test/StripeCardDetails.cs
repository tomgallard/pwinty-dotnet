using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    public class StripeCardDetails
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string Expiry { get; set; }
        public string CVC { get; set; }

        public static readonly StripeCardDetails VALID_VISA = new StripeCardDetails()
        {
            CardNumber = "4242424242424242",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };
        public static readonly StripeCardDetails VALID_MASTERCARD = new StripeCardDetails()
        {
            CardNumber = "5555555555554444",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };
        public static readonly StripeCardDetails INVALID_CVC = new StripeCardDetails()
        {
            CardNumber = "4000000000000101",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };
        public static readonly StripeCardDetails CARD_DECLINED = new StripeCardDetails()
        {
            CardNumber = "4000000000000002",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };
        public static readonly StripeCardDetails CARD_DECLINED_CVC = new StripeCardDetails()
        {
            CardNumber = "4000000000000127",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };
        public static readonly StripeCardDetails CARD_DECLINED_EXPIRED = new StripeCardDetails()
        {
            CardNumber = "4000000000000069",
            CardHolderName = "Mr Test",
            Expiry = "01/20",
            CVC = "123"
        };

    }
}
