using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public static class Configuration
    {
        private static string MERCHANT_ID_CONFIG_KEY = "Pwinty-MerchantId";
        private static string REST_API_KEY_CONFIG_KEY = "Pwinty-REST-API-Key";
        private static string BASE_URL_KEY = "Pwinty-Base-Url";
        public static string MerchantId {get;set;}
        public static string RestApiKey { get; set; }
        public static string BaseUrl { get; set; }
        static Configuration()
        {
            LoadFromAppConfig();
        }

        public static void LoadFromAppConfig()
        {
            MerchantId = ConfigurationManager.AppSettings[MERCHANT_ID_CONFIG_KEY];
            RestApiKey = ConfigurationManager.AppSettings[REST_API_KEY_CONFIG_KEY];
            BaseUrl = ConfigurationManager.AppSettings[BASE_URL_KEY];
        }
    }
}
