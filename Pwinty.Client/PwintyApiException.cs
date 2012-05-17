using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace Pwinty.Client
{
    public class PwintyJsonErrorMessage
    {
        public string Error { get; set; }
    }
    public class PwintyApiException : Exception
    {
        private readonly string _message;
        public HttpStatusCode StatusCode { get; set; }
        public PwintyApiException(IRestResponse response)
        {
            var error = Newtonsoft.Json.JsonConvert.DeserializeObject<PwintyJsonErrorMessage>(response.Content);
            if (error != null)
            {
                _message = error.Error;
            }
            StatusCode = response.StatusCode;
        }
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
