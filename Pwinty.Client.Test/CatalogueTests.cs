using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestFixture]
    public class CatalogueTests :TestBase
    {
        [Test]
        public void Get_catalogue_pro()
        {
            PwintyApi api = new PwintyApi();
            var cat = api.Catalogue.Get("GB", QualityLevel.PRO);
            Assert.IsNotNull(cat);
            Assert.IsTrue(cat.ShippingRates.Count > 0, "Should containg shipping rates");
            Assert.IsTrue(cat.Items.Count > 0, "Should containg items");
        }
        [Test]
        public void Get_catalogue_standard()
        {
            PwintyApi api = new PwintyApi();
            var cat = api.Catalogue.Get("GB", QualityLevel.PRO);
            Assert.IsNotNull(cat);
            Assert.IsTrue(cat.ShippingRates.Count > 0, "Should contain shipping rates");
            Assert.IsTrue(cat.Items.Count > 0, "Should contain items");
        }
    }
}
