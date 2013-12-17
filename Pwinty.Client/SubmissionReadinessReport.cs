using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class SubmissionReadinessReport :BaseItem
    {
        public SubmissionReadinessReport()
        {
            photos = new List<OrderItemReport>();
            generalErrors = new List<string>();
        }
        public long id { get; set; }
        public bool isValid { get; set; }
        public List<OrderItemReport> photos { get; set; }
        public List<string> generalErrors { get; set; }
    }
}
