using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class ShippingRate
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal PriceGBP { get; set; }
        public decimal PriceUSD { get; set; }
    }
}
