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
        public int PriceGBP { get; set; }
        public int PriceUSD { get; set; }
    }
}
