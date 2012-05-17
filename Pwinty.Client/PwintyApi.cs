using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pwinty.Client
{
    public class PwintyApi
    {
        private readonly string _merchantId;
        private readonly string _publicApiKey;
        private readonly string _baseUrl;
        public PwintyApi(string merchantId,string publicApiKey,string baseUrl)
        {
            _merchantId = merchantId;
            _publicApiKey = publicApiKey;
            _baseUrl = baseUrl;
        }
        public OrderResource Order
        {
            get {
                    return new OrderResource(_merchantId, _publicApiKey, _baseUrl);
                }
        }
        public StickerResource Stickers
        {
            get
            {
                return new StickerResource(_merchantId, _publicApiKey, _baseUrl);
            }
        }
        public OrderItemResource OrderItems
        {
            get
            {
                    return new OrderItemResource(_merchantId, _publicApiKey, _baseUrl);
            }
        }
        public DocumentResource DocumentItems
        {
            get
            {
                    return new DocumentResource(_merchantId, _publicApiKey, _baseUrl);
                
            }
        }
    }
}
