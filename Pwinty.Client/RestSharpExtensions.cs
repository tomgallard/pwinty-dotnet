using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Collections;

namespace Pwinty.Client
{
    public static class RestSharpExtensions
    {
        public static IRestResponse<T> ExecuteWithErrorCheck<T>(this RestClient client, RestRequest request) where T : IBaseItem,new()
        {
            var response = client.Execute<T>(request);
            if ((int)response.StatusCode > 399)
            {
                //try and get message
                string errorMsg = "Not available";
                if (response.Data != null)
                {
                    errorMsg = response.Data.ErrorMessage;
                }
                PwintyApiException exc = new PwintyApiException(errorMsg,response.StatusCode);
                throw exc;
            }
            else
            {
                return response;
            }
        }
        public static IRestResponse<T> ExecuteArrayWithErrorCheck<T>(this RestClient client, RestRequest request) where T : IList,new()
        {
            var response = client.Execute<T>(request);
            if ((int)response.StatusCode > 399)
            {
                //try and get message
                string errorMsg = "Not available";
                if (response.Content != null)
                {
                    errorMsg = response.Content.ToString();
                }
                PwintyApiException exc = new PwintyApiException(errorMsg, response.StatusCode);
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
                PwintyApiException exc = new PwintyApiException("Not available",response.StatusCode);
                throw exc;
            }
            else
            {
                return response;
            }
        }
    }
}
