using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;


namespace Pwinty.Client
{

    public enum OrderStatus
    {
        [Description("Not Yet Submitted")]
        NotYetSubmitted,
        [Description("Submitted")]
        Submitted,
        [Description("Complete")]
        Complete,
        [Description("Cancelled")]
        Cancelled
    }
    public enum Payment
    {
        InvoiceMe,
        InvoiceRecipient
    }
    public class Order
    {
        public long Id { get; set; }
        public string RecipientName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string StateOrCounty { get; set; }
        public string AddressTownOrCity { get; set; }
        public string PostalOrZipCode { get; set; }
        public string TextOnReverse { get; set; }
        public OrderStatus Status { get; set; }
        public Payment Payment { get; set; }
        public string PaymentUrl { get; set; }
        public Dictionary<string, string> ToRequest()
        {
            Dictionary<string, string> requestDict = new Dictionary<string, string>();
            if (Id > 0)
            {
                requestDict.Add("id", Id.ToString());
            }
            requestDict.Add("recipientName", RecipientName);
            requestDict.Add("address1", Address1);
            requestDict.Add("address2", Address2);
            requestDict.Add("stateOrCounty", StateOrCounty);
            requestDict.Add("country", Country);
            requestDict.Add("addressTownOrCity", AddressTownOrCity);
            requestDict.Add("postalOrZipCode", PostalOrZipCode);
            requestDict.Add("textOnReverse", TextOnReverse);
            requestDict.Add("payment", Payment.ToString());
            return requestDict;
        }
        public List<OrderItem> Photos { get; set; }
    }
}

