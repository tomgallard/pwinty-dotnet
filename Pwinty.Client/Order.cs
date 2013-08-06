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
    public enum QualityLevel
    {
        STANDARD,
        PRO
    }
    public enum Payment
    {
        InvoiceMe,
        InvoiceRecipient
    }
    public class Order :BaseItem
    {
        public Order()
        {
            qualityLevel = QualityLevel.PRO;
        }
        public long id { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string postalOrZipCode { get; set; }
        public string countryCode { get; set; }
        public string addressTownOrCity { get; set; }
        public string recipientName { get; set; }
        public string stateOrCounty { get; set; }
        public OrderStatus status { get; set; }
        public Payment payment { get; set; }
        public string paymentUrl { get; set; }
        public QualityLevel qualityLevel { get; set; }
        public List<OrderItem> photos { get; set; }
    }
    public class OrderRequest
    {
        public OrderRequest()
        {
            qualityLevel = QualityLevel.PRO;
        }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string postalOrZipCode { get; set; }
        public string countryCode { get; set; }
        public string addressTownOrCity { get; set; }
        public string recipientName { get; set; }
        public string stateOrCounty { get; set; }
        public Payment payment { get; set; }
        public QualityLevel qualityLevel { get; set; }
        public List<OrderItem> photos { get; set; }
    }
    public class CreateOrderRequest :OrderRequest
    {
        
    }
    public class UpdateOrderRequest : OrderRequest
    {
        public long id { get; set; }

    }


}

