using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;


namespace Pwinty.Client
{
    
 
    public class OrderResource :BaseResource
    {
        private const string _orderPath = "/Orders";
        private const string _getPath = "/Orders";
        public OrderResource(string merchantId,string publicApiKey,string baseUrl)
            :base(merchantId,publicApiKey,baseUrl)
        {
        }


        public void Cancel(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath +"/Status",
                Method = Method.POST
            };
            request.AddParameter("id", orderId.ToString());
            request.AddParameter("status", "Cancelled");
            var response = Client.ExecuteWithErrorCheck(request);

        }
        public Order Get(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath,
                Method = Method.GET
            };
            request.AddParameter("id", orderId.ToString());
            var response = Client.ExecuteWithErrorCheck<Order>(request);
            return response.Data;
        }
        public List<Order> Get()
        {
            var request = new RestRequest
            {
                Resource = _orderPath,
                Method = Method.GET
            };
            var response = Client.ExecuteWithErrorCheck<List<Order>>(request);
            return response.Data;
        }
        public Order Create(Order o)
        {
            var request = new RestRequest
            {
                Resource = _orderPath,
                Method = Method.POST
            };
            foreach (var kvp in o.ToRequest())
            {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            var response = Client.ExecuteWithErrorCheck<Order>(request);
            return response.Data;
        }
        public Order Update(Order o)
        {
            var request = new RestRequest
            {
                Resource = _orderPath,
                Method = Method.PUT
            };
            foreach (var kvp in o.ToRequest())
            {
                request.AddParameter(kvp.Key, kvp.Value);
            }

            var response = Client.ExecuteWithErrorCheck<Order>(request);
            return response.Data;
        }
        public SubmissionReadinessReport CheckReadyForSubmit(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath +"/SubmissionStatus",
                Method = Method.GET
            };
            request.AddParameter("id", orderId);
            var response = Client.ExecuteWithErrorCheck<SubmissionReadinessReport>(request);
            return response.Data;
        }

        public void Submit(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath + "/Status",
                Method = Method.POST
            };
            request.AddParameter("id", orderId.ToString());
            request.AddParameter("status", "Submitted");
            var response = Client.ExecuteWithErrorCheck(request);
        }

    }
}
