using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void Create_Order()
        {
            PwintyApi api = new PwintyApi();
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                payment = Payment.InvoiceRecipient,
                qualityLevel = QualityLevel.PRO
            });
            Assert.IsTrue(result.id > 0);
        }
        [TestMethod]
        public void Create_Order_Without_Country_Throws_Exception()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = api.Order.Create(new CreateOrderRequest()
                {

                    payment = Payment.InvoiceRecipient,
                    qualityLevel = QualityLevel.PRO
                });
                Assert.Fail("Exception should be thrown when order created without country code");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
            }

        }
    }
}
