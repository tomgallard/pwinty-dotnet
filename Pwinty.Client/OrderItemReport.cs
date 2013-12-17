using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class OrderItemReport
    {
        public OrderItemReport()
        {
            errors = new List<string>();
            warnings = new List<string>();
        }
        public long id { get; set; }
        public List<string> errors { get; set; }
        public List<string> warnings { get; set; }
    }
}
