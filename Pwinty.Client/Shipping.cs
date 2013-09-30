using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class Shipping
    {
        public string TrackingNumber { get; set; }
        public string TrackingUrl {get;set;}
        public bool isTracked {get;set;}
        public int Price { get; set; }
    }
}
