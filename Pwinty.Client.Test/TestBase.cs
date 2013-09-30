using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class TestBase
    {
        [TestInitialize]
        public void Check_Not_On_Live()
        {
            if(!String.IsNullOrEmpty(Configuration.BaseUrl))
            {
            var url = new Uri(Configuration.BaseUrl);
            if (url.DnsSafeHost.Equals("api.pwinty.com"))
            {
                throw new Exception("Trying to run unit tests against live- aborting");
            }
            }
        }
        protected  Order CreateEmptyOrderWithValidAddress(PwintyApi api,Payment paymentOption = Payment.InvoiceMe,string countryCode = "GB")
        {
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = countryCode,
                address1 = "Linton Travel Tavern",
                address2 = "Nr Longstanton Spice Museum",
                addressTownOrCity = "NORWICH",
                postalOrZipCode = "AGP1",
                recipientName = "Alan Gordan Partridge",
                stateOrCounty = "East Anglia",
                payment = paymentOption,
                qualityLevel = QualityLevel.PRO
            });
            Assert.IsTrue(result.id > 0);
            Assert.AreEqual(result.countryCode, countryCode);
            return result;
        }
        protected void Add_item_to_order(PwintyApi api,long orderId,int? price = null)
        {
            using (var dummyImage = File.OpenRead("itemtest.jpg"))
            {
                OrderItemRequest itemToAdd = new OrderItemRequest()
                {
                    Copies = 1,
                    OrderId = orderId,
                    Sizing = SizingOption.ShrinkToExactFit,
                    Type = "4x6",
                    PriceToUser = price
                };
                var result = api.OrderItems.CreateWithData(orderId, itemToAdd, dummyImage);
                Assert.AreEqual(OrderItemStatus.Ok, result.Status);
            }
        }
    }
}
