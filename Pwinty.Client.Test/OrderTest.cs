using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class OrderTest :TestBase
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
            Assert.AreEqual(Payment.InvoiceRecipient, result.payment);
            Assert.AreEqual(QualityLevel.PRO, result.qualityLevel);
            Assert.AreEqual(OrderStatus.NotYetSubmitted, result.status);
        }
        [TestMethod]
        public void Submit_Order_Without_Items()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = CreateEmptyOrderWithValidAddress(api);
                api.Order.Submit(result.id);
                Assert.Fail("Should throw error on submit empty order");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.Forbidden, exc.StatusCode);
            }
        }

     
        
        [TestMethod]
        public void Cannot_Create_Order_Without_CountryCode()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = api.Order.Create(new CreateOrderRequest()
                {
                    payment = Payment.InvoiceRecipient,
                    qualityLevel = QualityLevel.PRO
                });
                Assert.Fail("Should throw error if country code not supplied");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest,exc.StatusCode);
            }
        }
        [TestMethod]
        public void Cannot_Create_Order_With_MadeUp_CountryCode()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = api.Order.Create(new CreateOrderRequest()
                {
                    payment = Payment.InvoiceRecipient,
                    countryCode = "YY",
                    qualityLevel = QualityLevel.PRO
                });
                Assert.Fail("Should throw error if country code not supplied");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
            }
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
