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
          private const string _orderItemPath = "/Orders/{orderId}/Photos";
          private const string _deleteItemPath = "/Orders/{orderId}/Photos/{photoId}";
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
              request.AddObject(orderItem);
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
              request.AddParameter("orderId", orderId, ParameterType.UrlSegment);
              request.AddObject(orderItem);
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
              request.AddParameter("orderId", orderId);
              request.AddParameter("id", id);
              var response = Client.ExecuteWithErrorCheck(request);
          }
    }
}
