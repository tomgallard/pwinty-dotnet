using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class TestBase
    {
        [TestInitialize]
        public void Check_Not_On_Live()
        {
            if(!String.IsNullOrEmpty(Configuration.BaseUrl))
            {
            var url = new Uri(Configuration.BaseUrl);
            if (url.DnsSafeHost.Equals("api.pwinty.com"))
            {
                throw new Exception("Trying to run unit tests against live- aborting");
            }
            }
        }
    }
}
