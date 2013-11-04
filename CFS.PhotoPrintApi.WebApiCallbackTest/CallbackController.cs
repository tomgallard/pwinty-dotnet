using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CFS.PhotoPrintApi.WebApiCallbackTest
{
    public class CallbackController : ApiController
    {
        private static Dictionary<int, TaskCompletionSource<CallbackArgs>> _eventDict = new Dictionary<int, TaskCompletionSource<CallbackArgs>>();

        private static readonly AutoResetEvent _event = new AutoResetEvent(false);
        public static AutoResetEvent CallbackReceived { get { return _event; } }
        public string Post(CallbackArgs args)
        {
            if (_eventDict.ContainsKey(args.OrderId))
            {
                var are = _eventDict[args.OrderId];
                are.SetResult(args);
            }
            return "";
        }

        internal static Task<CallbackArgs> RegisterTaskCompletionSource(int orderId)
        {
            var tcs = new TaskCompletionSource<CallbackArgs>();
            _eventDict[orderId] = tcs;
            return tcs.Task;
        }
    }
    public class CallbackArgs
    {
        public int OrderId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Environment { get; set; }
        public string EventId { get; set; }
        public string EventData { get; set; }
    }
}
