using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class Catalogue :BaseItem
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public List<CatalogueItem> Items { get; set; }
        public List<ShippingRate> ShippingRates { get; set; }
        public string QualityLevel { get; set; }
    }
}
