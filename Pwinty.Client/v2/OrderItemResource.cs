using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RestSharp;

namespace Pwinty.Client
{
    public class OrderItemResource :BaseResource
    {
        private const string _orderItemPath = "v2.1/Orders/{orderId}/Photos";
        private const string _deleteItemPath = "v2.1/Orders/{orderId}/Photos/{photoId}";
          public OrderItemResource()
          {
          }
          public OrderItem CreateWithData(long orderId,OrderItemRequest orderItem, Stream fileData)
          {
              var request = new RestRequest
              {
                  Resource = _orderItemPath,
                  Method = Method.POST
              };
              request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
              request.AddObject(orderItem, "Type", "Url", "Copies", "Sizing", "PriceToUser", "Md5Hash");
              //Add attributes ourselves
              if (orderItem.Attributes != null)
              {
                  foreach (var attrValue in orderItem.Attributes)
                  {
                      request.AddParameter(String.Format("attributes.{0}", attrValue.Key), attrValue.Value);
                  }
              }
              byte[] allData = new byte[fileData.Length];
              fileData.Read(allData,0,allData.Length);
              request.AddFile("image",allData,"image.jpg");
              var response = Client.ExecuteWithErrorCheck<OrderItem>(request);
              fileData.Dispose();
              return response.Data;
          }
          public OrderItem Create(long orderId,OrderItemRequest orderItem)
          {
              var request = new RestRequest
              {
                  Resource = _orderItemPath,
                  Method = Method.POST
              };
              request.RequestFormat = DataFormat.Json;
              request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
              request.AddBody(orderItem);
              var response = Client.ExecuteWithErrorCheck<OrderItem>(request);
              return response.Data;
          }


          public void Delete(long id,long orderId)
          {
              var request = new RestRequest
              {
                  Resource = _deleteItemPath,
                  Method = Method.DELETE
              };
              request.AddParameter("orderId", orderId,ParameterType.UrlSegment);
              request.AddParameter("photoId", id,ParameterType.UrlSegment);
              var response = Client.ExecuteWithErrorCheck<BaseItem>(request);
          }

          public OrderItem Get(long orderId, long photoId)
          {
              var request = new RestRequest
              {
                  Resource = _deleteItemPath,
                  Method = Method.GET
              };
              request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
              request.AddParameter("photoId", photoId, ParameterType.UrlSegment);
              var response = Client.ExecuteWithErrorCheck<OrderItem>(request);
              return response.Data;
          }
    }
}
