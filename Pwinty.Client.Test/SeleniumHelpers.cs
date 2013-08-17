using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    public static class SeleniumHelpers
    {
        public static IWebElement FindElementByText(this IWebDriver driver, string searchText)
        {
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 20));
            return driver.FindElement(By.XPath(String.Format("//*[contains(.,'{0}')]",searchText)));
        }
        public static void ClickWithJavascript(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].click();", element);
        }

    }
}
