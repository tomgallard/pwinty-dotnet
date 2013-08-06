using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        protected  Order CreateEmptyOrderWithValidAddress(PwintyApi api)
        {
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                address1 = "Linton Travel Tavern",
                address2 = "Nr Longstanton Spice Museum",
                addressTownOrCity = "NORWICH",
                postalOrZipCode = "AGP1",
                stateOrCounty = "NORWICH",
                payment = Payment.InvoiceMe,
                qualityLevel = QualityLevel.PRO
            });
            Assert.IsTrue(result.id > 0);
            return result;
        }
    }
}
