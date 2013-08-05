using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public enum OrderItemStatus
    {
        AwaitingUrlOrData,
        NotYetDownloaded,
        Ok,
        FileNotFoundAtUrl,
        Invalid
    }
    public enum SizingOption
    {
        //Crop so whole print is taken up by photo, but some of photo may be missing
        Crop,
        //Resize image to exactly fit photo dimensions- distortion may occur
        ShrinkToExactFit,
        //
        ShrinkToFit
    }

    public class OrderItemRequest
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int Copies { get; set; }
        public SizingOption Sizing { get; set; }
        public long OrderId { get; set; }
        public decimal? Price { get; set; }
        public string Md5Hash { get; set; }
    }
    public class OrderItem
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public OrderItemStatus Status { get; set; }
        public int Copies { get; set; }
        public SizingOption Sizing { get; set; }
        public long OrderId { get; set; }
        public decimal? Price { get; set; }
        public string Md5Hash { get; set; }
    }
}
