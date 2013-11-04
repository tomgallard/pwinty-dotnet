using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class ConfigurationTest :TestBase
    {
        [TestInitialize]
        public void Init()
        {
            Configuration.LoadFromAppConfig();
        }
        [TestMethod]
        public void Config_Class_Picks_Up_Merchant_Id_From_AppConfig()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-MerchantId"];
            Assert.AreEqual(settingInAppConfig, Configuration.MerchantId);
        }
        [TestMethod]
        public void Config_Class_Picks_Up_Base_Url_From_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-Base-Url"];
            Assert.AreEqual(settingInAppConfig, Configuration.BaseUrl);
        }
        [TestMethod]
        public void Config_Class_Picks_Up_API_Key_From_AppConfig()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-REST-API-Key"];
            Assert.AreEqual(settingInAppConfig, Configuration.RestApiKey);

        }
        [TestMethod]
        public void Can_Override_MerchantId_ConfigSetting_With_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-MerchantId"];
            Configuration.MerchantId = "foo";
            Assert.AreEqual("foo", Configuration.MerchantId);
        }
        [TestMethod]
        public void Can_Override_API_Key_ConfigSetting_With_Code()
        {
            var settingInAppConfig = ConfigurationManager.AppSettings["Pwinty-REST-API-Key"];
            Configuration.RestApiKey = "bar";
            Assert.AreEqual("bar", Configuration.RestApiKey);
        }
        [TestCleanup]
        public void Cleanup()
        {
            Configuration.LoadFromAppConfig();
        }
    }
}
