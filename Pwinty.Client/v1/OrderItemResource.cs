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
          private const string _orderItemPath = "/Photos";
          private const string _deleteItemPath = "/Photos";
          public OrderItemResource(string merchantId,string publicApiKey,string baseUrl)
              : base(merchantId,publicApiKey,baseUrl)
          {
          }
          public OrderItem CreateWithData(OrderItem orderItem, Stream fileData)
          {
              var request = new RestRequest
              {
                  Resource = _orderItemPath,
                  Method = Method.POST
              };
              request.AddParameter("orderId", orderItem.OrderId.ToString());
              request.AddParameter("copies", orderItem.Copies.ToString());
              request.AddParameter("type", orderItem.Type);
              request.AddParameter("sizing", orderItem.Sizing.ToString());
              if (orderItem.Price != null)
              {
                  request.AddParameter("price", orderItem.Price.Value);
              }
              byte[] allData = new byte[fileData.Length];
              fileData.Read(allData,0,allData.Length);
              request.AddFile("image",allData,"image.jpg");
              var response = Client.ExecuteWithErrorCheck<OrderItem>(request);
              fileData.Dispose();
              return response.Data;
          }
          public OrderItem Create(OrderItem orderItem)
          {
              var request = new RestRequest
              {
                  Resource = _orderItemPath,
                  Method = Method.POST
              };
              request.AddParameter("orderId", orderItem.OrderId.ToString());
              request.AddParameter("copies", orderItem.Copies.ToString());
              request.AddParameter("type", orderItem.Type);
              request.AddParameter("url", orderItem.Url);
              request.AddParameter("sizing", orderItem.Sizing.ToString());
              if (orderItem.Price != null)
              {
                  request.AddParameter("price", orderItem.Price.Value);
              }
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
