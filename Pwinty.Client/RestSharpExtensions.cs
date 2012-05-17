using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Pwinty.Client
{
    public static class RestSharpExtensions
    {
        public static IRestResponse<T> ExecuteWithErrorCheck<T>(this RestClient client, RestRequest request) where T : new()
        {
            var response = client.Execute<T>(request);
            if ((int)response.StatusCode > 399)
            {
                //Error code results;
                PwintyApiException exc = new PwintyApiException(response);
                throw exc;
            }
            else
            {
                return response;
            }
        }
        public static IRestResponse ExecuteWithErrorCheck(this RestClient client, RestRequest request)
        {
            var response = client.Execute(request);
            if ((int)response.StatusCode > 399)
            {
                //Error code results;
                PwintyApiException exc = new PwintyApiException(response);
                throw exc;
            }
            else
            {
                return response;
            }
        }
    }
}
