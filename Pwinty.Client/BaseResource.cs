using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;


namespace Pwinty.Client
{
    public abstract class BaseResource
    {
        protected string PublicApiKey
        {
            get;
            private set;
        }
        protected string MerchantId
        {
            get;
            private set;
        }
        protected string BaseUrl
        {
            get;
            private set;
        }
        protected RestClient Client
        {
            get
            {
                var client = new RestClient
                ();
                client.BaseUrl = BaseUrl;
                client.DefaultParameters.Add(
                    new Parameter()
                    {
                        Name = "X-Pwinty-MerchantId",
                        Value = MerchantId,
                        Type = ParameterType.HttpHeader
                    });
                client.DefaultParameters.Add(
                    new Parameter()
                    {
                        Name = "X-Pwinty-REST-API-Key",
                        Value = PublicApiKey,
                        Type = ParameterType.HttpHeader
                    });
                return client;
            }
        }
    
        public BaseResource(string merchantId,string publicApiKey,string baseUrl)
        {
            PublicApiKey = publicApiKey;
            MerchantId = merchantId;
            BaseUrl = baseUrl;
        }

       
    }
}
