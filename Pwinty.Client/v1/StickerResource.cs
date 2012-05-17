using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Pwinty.Client
{
    public class StickerResource : BaseResource
    {
        private const string _stickerPath = "/Stickers";

        public StickerResource(string merchantId, string publicApiKey, string baseUrl)
            : base(merchantId, publicApiKey, baseUrl)
        { }


        public Sticker CreateWithData(Sticker sticker, long orderId, System.IO.Stream fileData)
        {
            var request = new RestRequest
            {
                Resource = _stickerPath,
                Method = Method.POST
            };
            request.AddParameter("orderId", orderId);
            request.AddParameter("fileName", sticker.FileName);
            byte[] allData = new byte[fileData.Length];
            fileData.Read(allData, 0, allData.Length);
            request.AddFile("doc", allData, sticker.FileName);
            var response = Client.ExecuteWithErrorCheck<Sticker>(request);
            fileData.Dispose();
            return response.Data;
        }

        public void Delete(long stickerId, long orderId)
        {
            var request = new RestRequest
            {
                Resource = _stickerPath,
                Method = Method.DELETE
            };
            request.AddParameter("id", stickerId);
            request.AddParameter("orderId", orderId);
            var response = Client.ExecuteWithErrorCheck(request);
        }

        public Sticker Get(long stickerId, long orderId)
        {
            var request = new RestRequest
            {
                Resource = _stickerPath,
                Method = Method.GET
            };
            request.AddParameter("orderId", orderId);
            request.AddParameter("id", stickerId);
            var response = Client.ExecuteWithErrorCheck<Sticker>(request);
            return response.Data;
        }


    }
}
