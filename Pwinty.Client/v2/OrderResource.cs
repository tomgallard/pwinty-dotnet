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
        private const string _orderPath = "/v2/Orders";
        private const string _getPath = "/v2/Orders/{orderId}";
        private const string _getPhotosPath = "/v2/Orders/{orderId}/Photos";
        private const string _getAllPath = "/v2/Orders";
        public OrderResource()
        {
        }


        public void Cancel(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath +"/Status",
                Method = Method.POST
            };
            request.AddParameter("orderId", orderId.ToString(), ParameterType.UrlSegment);
            request.AddParameter("status", "Cancelled");
            var response = Client.ExecuteWithErrorCheck<BaseItem>(request);

        }
        public Order Get(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath,
                Method = Method.GET
            };
            request.AddParameter("orderId", orderId.ToString(),ParameterType.UrlSegment);
            var response = Client.ExecuteWithErrorCheck<Order>(request);
            return response.Data;
        }
        public List<OrderItem> GetPhotos(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPhotosPath,
                Method = Method.GET
            };
            request.AddParameter("orderId", orderId.ToString(), ParameterType.UrlSegment);
            var response = Client.ExecuteArrayWithErrorCheck<List<OrderItem>>(request);
            return response.Data;
        }
        public List<Order> Get()
        {
            var request = new RestRequest
            {
                Resource = _orderPath,
                Method = Method.GET
            };
            var response = Client.ExecuteArrayWithErrorCheck<List<Order>>(request);
            return response.Data;
        }
        public Order Create(CreateOrderRequest o)
        {
            var request = new RestRequest
            {
                Resource = _orderPath,
                Method = Method.POST,
                
            };
            request.RequestFormat = DataFormat.Json;
            request.AddObject(o);

            var response = Client.ExecuteWithErrorCheck<Order>(request);
            return response.Data;
        }
        public Order Update(UpdateOrderRequest o)
        {
            var request = new RestRequest
            {
                Resource = _getPath,
                Method = Method.PUT
            };
            request.AddParameter("orderId", o.id.ToString(), ParameterType.UrlSegment);
            request.AddObject(o);

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
            request.AddParameter("orderId", orderId,ParameterType.UrlSegment);
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
            request.AddParameter("orderId", orderId.ToString(),ParameterType.UrlSegment);
            request.AddParameter("status", "Submitted",ParameterType.GetOrPost);
            var response = Client.ExecuteWithErrorCheck<BaseItem>(request);
        }


        public void SubmitForPayment(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath + "/Status",
                Method = Method.POST
            };
            request.AddParameter("orderId", orderId.ToString(), ParameterType.UrlSegment);
            request.AddParameter("status", "AwaitingPayment", ParameterType.GetOrPost);
            var response = Client.ExecuteWithErrorCheck<BaseItem>(request);
        }

        public void Delete(long orderId)
        {
            var request = new RestRequest
            {
                Resource = _getPath,
                Method = Method.DELETE
            };
            request.AddParameter("orderId", orderId.ToString(),ParameterType.UrlSegment);
            var response = Client.ExecuteWithErrorCheck<BaseItem>(request);
        }
    }
}
