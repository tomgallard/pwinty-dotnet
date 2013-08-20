using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CFS.PhotoPrintApi.WebApiCallbackTest
{
    public class TestBase
    {
        static TestBase()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8101")
                {
                    
                };
                        // Add a route
            config.Routes.MapHttpRoute(
              name: "default",
              routeTemplate: "Callback",
              defaults: new { controller = "Callback" });
            System.Web.Http.SelfHost.HttpSelfHostServer server = new System.Web.Http.SelfHost.HttpSelfHostServer(config);
            server = new HttpSelfHostServer(config);
            server.OpenAsync();
        }


    }
}
