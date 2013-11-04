using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class CatalogueItem :BaseItem
    {
        public string Name { get; set; }
        public decimal HorizontalSize { get; set; }
        public decimal VerticalSize { get; set; }
        public string SizeUnits { get; set; }
        public int RecommendedHorizontalResolution { get; set; }
        public int RecommendedVerticalResolution { get; set; }
        public string Description { get; set; }
        public int PriceGBP { get; set; }
        public int PriceUSD { get; set; }
    }
}
