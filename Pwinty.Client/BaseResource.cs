using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;


namespace Pwinty.Client
{
    public abstract class BaseResource
    {
        
        protected RestClient Client
        {
            get
            {
                var client = new RestClient
                ();
                client.BaseUrl = Configuration.BaseUrl;
                client.DefaultParameters.Add(
                    new Parameter()
                    {
                        Name = "X-Pwinty-MerchantId",
                        Value = Configuration.MerchantId,
                        Type = ParameterType.HttpHeader
                    });
                client.DefaultParameters.Add(
                    new Parameter()
                    {
                        Name = "X-Pwinty-REST-API-Key",
                        Value = Configuration.RestApiKey,
                        Type = ParameterType.HttpHeader
                    });
                return client;
            }
        }
    
        

       
    }
}
