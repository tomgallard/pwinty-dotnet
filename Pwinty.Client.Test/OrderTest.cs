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
    }
}
