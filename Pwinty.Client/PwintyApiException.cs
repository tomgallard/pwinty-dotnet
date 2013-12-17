using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace Pwinty.Client
{

    public class PwintyApiException : Exception
    {
        private readonly string _message;
        public HttpStatusCode StatusCode { get; set; }
        public PwintyApiException(string errorMessage,HttpStatusCode statusCode)
        {
            _message = errorMessage;
            StatusCode = statusCode;
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
