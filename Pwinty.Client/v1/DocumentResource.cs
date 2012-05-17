using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RestSharp;

namespace Pwinty.Client
{
    public class DocumentResource : BaseResource
    {
        private const string _orderItemPath = "/Documents";

        public DocumentResource(string merchantId, string publicApiKey, string baseUrl)
              : base(merchantId,publicApiKey,baseUrl)
          {
          }
          public Document CreateWithData(Document orderItem,long orderId, Stream fileData)
          {
              var request = new RestRequest
              {
                  Resource = _orderItemPath,
                  Method = Method.POST
              };
              request.AddParameter("orderId", orderId);
              request.AddParameter("fileName", orderItem.FileName);
              byte[] allData = new byte[fileData.Length];
              fileData.Read(allData,0,allData.Length);
              request.AddFile("doc",allData,orderItem.FileName);
              var response = Client.ExecuteWithErrorCheck<Document>(request);
              return response.Data;
          }

    }
}
