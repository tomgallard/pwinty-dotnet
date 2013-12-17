using System;
using NUnit.Framework;
using Pwinty.Client;

namespace CFS.PhotoPrintApi.WebApiCallbackTest
{
    [TestFixture]
    public class CallbackTest :TestBase
    {
        [Test]
        public void Receives_Callback_On_Create_Order()
        {
            PwintyApi api = new PwintyApi();
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                payment = Payment.InvoiceMe,
                qualityLevel = QualityLevel.PRO
            });
           
            Assert.IsTrue(result.id > 0);
            var tcs = CallbackController.RegisterTaskCompletionSource((int)result.id);
            var callbackResult = tcs.GetAwaiter().GetResult();
            Assert.AreEqual(result.id, callbackResult.OrderId);
        }

    }
}
