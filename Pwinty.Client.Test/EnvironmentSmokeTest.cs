using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestFixture]
    public class EnvironmentSmokeTest
    {
        [Test]
        public void Ensure_Dashboard_Loads_And_Can_Logon()
        {
            var dashboardUrl = ConfigurationManager.AppSettings["Pwinty-Dashboard-Url"];
            using(var firefoxDriver = new FirefoxDriver())
            {
                firefoxDriver.Url = dashboardUrl;
                var emailField = firefoxDriver.FindElementByName("Email");
                var passwordField = firefoxDriver.FindElementByName("Password");
            }
        }
    }
}
