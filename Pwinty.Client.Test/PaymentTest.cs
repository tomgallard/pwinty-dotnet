using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class PaymentTest :TestBase
    {
        [TestMethod]
        [TestCategory("Payment tests")]
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
        [TestMethod]
        [TestCategory("Payment tests")]
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

        private void SubmitTestPayment(FirefoxDriver seleniumInstance,Order originalOrder, string paymentUrl)
        {
            seleniumInstance.Url = paymentUrl;
            seleniumInstance.FindElementById("Email").SendKeys("tom@pwinty.com");
            Assert.AreEqual(originalOrder.recipientName, seleniumInstance.FindElementById("Name").GetAttribute("value"));
            Assert.AreEqual(originalOrder.address1, seleniumInstance.FindElementById("Address1").GetAttribute("value"));
            Assert.AreEqual(originalOrder.address2, seleniumInstance.FindElementById("Address2").GetAttribute("value"));
            Assert.AreEqual(originalOrder.addressTownOrCity, seleniumInstance.FindElementById("AddressTownOrCity").GetAttribute("value"));
            Assert.AreEqual(originalOrder.stateOrCounty, seleniumInstance.FindElementById("StateOrCounty").GetAttribute("value"));
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
