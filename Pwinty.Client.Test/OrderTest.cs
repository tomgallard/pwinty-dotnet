using System;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestFixture]
    public class OrderTest :TestBase
    {
        [Test]
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
            Assert.IsTrue(result.price > 0);
            Assert.AreEqual(Payment.InvoiceRecipient, result.payment);
            Assert.AreEqual(QualityLevel.PRO, result.qualityLevel);
            Assert.AreEqual(OrderStatus.NotYetSubmitted, result.status);
            Assert.IsNotNull(result.shippingInfo);
            Assert.IsTrue(result.shippingInfo.Price > 0);
            Assert.IsFalse(result.shippingInfo.isTracked);
        }
        [Test]
        public void List_Orders()
        {
            PwintyApi api = new PwintyApi();
            var orders = api.Order.Get();
            Assert.IsNotNull(orders);
        }
        [Test]
        public void Get_Order_Photos()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            Add_item_to_order(api, result.id);
            var photos = api.Order.GetPhotos(result.id);
            Assert.AreEqual(1, photos.Count);
        }
        [Test]
        public void Create_International_Order()
        {
            PwintyApi api = new PwintyApi();
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                destinationCountryCode = "FR",
                payment = Payment.InvoiceRecipient,
                qualityLevel = QualityLevel.PRO
            });
            Assert.IsTrue(result.id > 0);
            Assert.AreEqual(Payment.InvoiceRecipient, result.payment);
            Assert.AreEqual(QualityLevel.PRO, result.qualityLevel);
            Assert.AreEqual(OrderStatus.NotYetSubmitted, result.status);
        }
        [Test]
        public void Cancel_order()
        {
            PwintyApi api = new PwintyApi();
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                payment = Payment.InvoiceMe,
                qualityLevel = QualityLevel.PRO
            });
            api.Order.Cancel(result.id);
            var order = api.Order.Get(result.id);
            Assert.AreEqual(OrderStatus.Cancelled, order.status);
        }
        [Test]
        public void Check_order_submission_status()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            Add_item_to_order(api, result.id);
            var status = api.Order.CheckReadyForSubmit(result.id);
            Assert.IsTrue(status.isValid, "Order should be valid for submit");
        }
        [Test]
        public void Check_order_submission_status_when_errors()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            var status = api.Order.CheckReadyForSubmit(result.id);
            Assert.IsFalse(status.isValid, "Order should not be valid for submit with no items");
            Assert.IsTrue(status.generalErrors.Count > 0, "Order should have general errors");
        }
        [Test]
        public void Update_order_address()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            var updateRequest = new UpdateOrderRequest()
            {
                address1 = "new address 1",
                id = result.id,
                address2 = "new address 2",
                addressTownOrCity = "newtown",
                postalOrZipCode = "NN1 1NN",
                recipientName = "mr new",
                stateOrCounty = "NEWARK"
            };
            var updatedOrder = api.Order.Update(updateRequest);
            Assert.AreEqual(updateRequest.address1,updatedOrder.address1);
            Assert.AreEqual(updateRequest.address2,updatedOrder.address2);
            Assert.AreEqual(updateRequest.addressTownOrCity,updatedOrder.addressTownOrCity);
            Assert.AreEqual(updateRequest.postalOrZipCode,updatedOrder.postalOrZipCode);
            Assert.AreEqual(updateRequest.recipientName,updatedOrder.recipientName);
            Assert.AreEqual(updateRequest.stateOrCounty,updatedOrder.stateOrCounty);
        }
        
        [Test]
        public void Cancel_Submitted_Order_Causes_Error()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            Add_item_to_order(api, result.id);
            api.Order.Submit(result.id);
            try
            {
                api.Order.Cancel(result.id);
                Assert.Fail("Should throw error when cancel submitted order");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.Forbidden, exc.StatusCode, "Should return status code forbidden");
            }
        }
        [Test]
        public void Cancel_Order_Using_Delete_Endpoint()
        {
            PwintyApi api = new PwintyApi();
            var result = api.Order.Create(new CreateOrderRequest()
            {
                countryCode = "GB",
                payment = Payment.InvoiceMe,
                qualityLevel = QualityLevel.PRO
            });
            api.Order.Delete(result.id);
            var order = api.Order.Get(result.id);
            Assert.AreEqual(OrderStatus.Cancelled, order.status);
        }
        [Test]
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
        [Test]
        public void Submit_Order_And_Retrieve_Payment_Url()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api,Payment.InvoiceRecipient);
            Add_item_to_order(api, result.id,200);
            api.Order.SubmitForPayment(result.id);
            var paymentUrl = result.paymentUrl;
            Console.WriteLine("Payment url is " + paymentUrl);
            using (WebClient client = new WebClient())
            {
                var webResult = client.DownloadString(paymentUrl);
                Console.WriteLine(webResult);
            }
        }
     
        
        [Test]
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
        [Test]
        public void Cant_submit_order_with_invoice_recipient()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = CreateEmptyOrderWithValidAddress(api, Payment.InvoiceRecipient);
                Add_item_to_order(api, result.id);
                api.Order.Submit(result.id);
                Assert.Fail("Should throw error on submit empty order");
            }
            catch (PwintyApiException exc)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode, "Should return bad request");
            }
        }
        [Test]
        public void Invoice_recipient_order_returns_payment_url()
        {

                PwintyApi api = new PwintyApi();
                var result = CreateEmptyOrderWithValidAddress(api, Payment.InvoiceRecipient);
                Assert.IsNotNull(result.paymentUrl, "Payment url should be available");
                Add_item_to_order(api, result.id,200);
                api.Order.SubmitForPayment(result.id);
        }
        [Test]
        public void Can_submit_order_with_invoice_me()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api);
            Add_item_to_order(api, result.id);
            api.Order.Submit(result.id);
        }
        [Test]
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
        [Test]
        public void Cannot_Create_Order_With_MadeUp_QualityLevel()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var result = api.Order.Create(new CreateOrderRequest()
                {
                    payment = Payment.InvoiceRecipient,
                    countryCode = "GB",
                    qualityLevel = QualityLevel.MadeUp
                });
                Assert.Fail("Should throw error if country code not supplied");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
            }
        }
        [Test]
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
