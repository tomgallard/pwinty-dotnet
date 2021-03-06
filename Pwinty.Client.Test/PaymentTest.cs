using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestFixture]
    public class PaymentTest :TestBase
    {
        [Test]
        [Category("Payment tests")]
        public void Submit_Order_And_Retrieve_Payment_Url()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api, Payment.InvoiceRecipient);
            Add_item_to_order(api, result.id, 200);
            api.Order.SubmitForPayment(result.id);
            var paymentUrl = result.paymentUrl;
            Console.WriteLine("Payment url is " + paymentUrl);
            using (var seleniumInstance = new FirefoxDriver())
            {
                SubmitTestPayment(seleniumInstance, result, paymentUrl);
                CheckOrderSummary(seleniumInstance);
                EnterDummyPaymentOptions(seleniumInstance, StripeCardDetails.VALID_VISA);
                AssertPaymentSuccess(seleniumInstance);
            }
        }
        [Test]
        [Category("Payment tests")]
        public void Submit_Order_With_No_AddressAnd_Retrieve_Payment_Url()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrder(api, Payment.InvoiceRecipient);
            Add_item_to_order(api, result.id, 200);
            api.Order.SubmitForPayment(result.id);
            var paymentUrl = result.paymentUrl;
            Console.WriteLine("Payment url is " + paymentUrl);
            using (var seleniumInstance = new FirefoxDriver())
            {
                AddAddressAndSubmitTestPayment(seleniumInstance, result, paymentUrl, "Mr Test", "test st", "test town", "Testeshire");
                CheckOrderSummary(seleniumInstance);
                EnterDummyPaymentOptions(seleniumInstance, StripeCardDetails.VALID_VISA);
                AssertPaymentSuccess(seleniumInstance);
            }
        }
        [Test]
        [Category("Payment tests")]
        public void Submit_Order_With_Card_Declined_Card()
        {
            PwintyApi api = new PwintyApi();
            var result = CreateEmptyOrderWithValidAddress(api, Payment.InvoiceRecipient);
            Add_item_to_order(api, result.id, 200);
            api.Order.SubmitForPayment(result.id);
            var paymentUrl = result.paymentUrl;
            Console.WriteLine("Payment url is " + paymentUrl);
            using (var seleniumInstance = new FirefoxDriver())
            {
                SubmitTestPayment(seleniumInstance,result, paymentUrl);
                CheckOrderSummary(seleniumInstance);
                EnterDummyPaymentOptions(seleniumInstance,StripeCardDetails.CARD_DECLINED);
                AssertPaymentFailed(seleniumInstance);
            }

        }
        public void AssertEqualOrBothNullOrEmpty(string expected,string actual)
        {
            if(String.IsNullOrEmpty(expected) && String.IsNullOrEmpty(actual))
            {
                return;
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }
        private void SubmitTestPayment(FirefoxDriver seleniumInstance,Order originalOrder, string paymentUrl)
        {
            seleniumInstance.Url = paymentUrl;
            seleniumInstance.FindElementById("Email").SendKeys("tom@pwinty.com");
            AssertOrderAddress(seleniumInstance, originalOrder);
            var continueButton = seleniumInstance.FindElementById("btnAddressEntered") as IWebElement;
            continueButton.ClickWithJavascript(seleniumInstance);
        }

        private void AssertOrderAddress(FirefoxDriver seleniumInstance, Order originalOrder)
        {
            AssertEqualOrBothNullOrEmpty(originalOrder.recipientName, seleniumInstance.FindElementById("Name").GetAttribute("value"));
            AssertEqualOrBothNullOrEmpty(originalOrder.address1, seleniumInstance.FindElementById("Address1").GetAttribute("value"));
            AssertEqualOrBothNullOrEmpty(originalOrder.address2, seleniumInstance.FindElementById("Address2").GetAttribute("value"));
            AssertEqualOrBothNullOrEmpty(originalOrder.addressTownOrCity, seleniumInstance.FindElementById("AddressTownOrCity").GetAttribute("value"));
            AssertEqualOrBothNullOrEmpty(originalOrder.stateOrCounty, seleniumInstance.FindElementById("StateOrCounty").GetAttribute("value"));
        }
        private void AddAddressAndSubmitTestPayment(FirefoxDriver seleniumInstance,Order originalOrder, string paymentUrl,string name,string address1,string townOrCity,string county)
        {
            seleniumInstance.Url = paymentUrl;
            seleniumInstance.FindElementById("Email").SendKeys("tom@pwinty.com");
            AssertOrderAddress(seleniumInstance, originalOrder);
            seleniumInstance.FindElementById("Name").SendKeys(name);
            seleniumInstance.FindElementById("Address1").SendKeys(address1);
            seleniumInstance.FindElementById("AddressTownOrCity").SendKeys(townOrCity);
            seleniumInstance.FindElementById("StateOrCounty").SendKeys(county);
            var continueButton = seleniumInstance.FindElementById("btnAddressEntered") as IWebElement;
            continueButton.ClickWithJavascript(seleniumInstance);
        }
        private void CheckOrderSummary(FirefoxDriver seleniumInstance)
        {
            //should charge vat
            var vatEl = seleniumInstance.FindElementByText("VAT");
            Assert.IsNotNull(vatEl);
            var totalEl = seleniumInstance.FindElementByText("£5.04");
            Assert.IsNotNull(totalEl,"Should have total cost of £5.04 on page");
            var continueButton = seleniumInstance.FindElementById("btnConfirmAndPay") as IWebElement;
            continueButton.ClickWithJavascript(seleniumInstance);
        }

        private void EnterDummyPaymentOptions(FirefoxDriver seleniumInstance,StripeCardDetails cardDetails)
        {
            seleniumInstance.SwitchTo().Frame(seleniumInstance.FindElementByCssSelector(".stripe_checkout_app"));
            var emailEl = seleniumInstance.FindElementByName("email");
            emailEl.SendKeys("tom@pwinty.com");
            var cardEl = seleniumInstance.FindElementByName("card_number");
            cardEl.SendKeys(cardDetails.CardNumber);
            var expiresEl = seleniumInstance.FindElementByName("cc-exp");
            expiresEl.SendKeys(cardDetails.Expiry);
            //var nameOnCard = seleniumInstance.FindElementById("paymentName");
            //nameOnCard.SendKeys(cardDetails.CardHolderName);
            var cvc = seleniumInstance.FindElementByName("cc-csc");
            cvc.SendKeys(cardDetails.CVC);

            var submitButton = seleniumInstance.FindElementByCssSelector(".button button");
            submitButton.ClickWithJavascript(seleniumInstance);
            

        }

        private static void AssertPaymentSuccess(FirefoxDriver seleniumInstance)
        {
            //should move to a thanks page
            var thanksElement = seleniumInstance.FindElementByText("Thanks");
            Assert.IsNotNull(thanksElement);
        }
        private static void AssertPaymentFailed(FirefoxDriver seleniumInstance)
        {
            //should move to a thanks page
            var thanksElement = seleniumInstance.FindElementByText("This card was declined");
            Assert.IsNotNull(thanksElement);
        }
    }
}

