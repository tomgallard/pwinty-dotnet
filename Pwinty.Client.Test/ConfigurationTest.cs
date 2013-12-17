using System;
using NUnit.Framework;
using System.Configuration;

namespace Pwinty.Client.Test
{
    [TestFixture]
    public class ConfigurationTest :TestBase
    {
        [SetUp]
        public void Init()
        {
            Configuration.LoadFromAppConfig();
        }
        [Test]
        public void Config_Class_Picks_Up_Merchant_Id_From_AppConfig()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-MerchantId"];
            Assert.AreEqual(settingInAppConfig, Configuration.MerchantId);
        }
        [Test]
        public void Config_Class_Picks_Up_Base_Url_From_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-Base-Url"];
            Assert.AreEqual(settingInAppConfig, Configuration.BaseUrl);
        }
        [Test]
        public void Config_Class_Picks_Up_API_Key_From_AppConfig()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-REST-API-Key"];
            Assert.AreEqual(settingInAppConfig, Configuration.RestApiKey);

        }
        [Test]
        public void Can_Override_MerchantId_ConfigSetting_With_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-MerchantId"];
            Configuration.MerchantId = "foo";
            Assert.AreEqual("foo", Configuration.MerchantId);
        }
        [Test]
        public void Can_Override_API_Key_ConfigSetting_With_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-REST-API-Key"];
            Configuration.RestApiKey = "bar";
            Assert.AreEqual("bar", Configuration.RestApiKey);
        }
        [TearDown]
        public void Cleanup()
        {
            Configuration.LoadFromAppConfig();
        }
    }
}
