using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class OrderItemTest :TestBase
    {
        [TestMethod]
        public void Add_item_to_order()
        {
            PwintyApi api = new PwintyApi();
            var order = base.CreateEmptyOrderWithValidAddress(api);
            using (var dummyImage = File.OpenRead("itemtest.jpg"))
            {
                OrderItemRequest itemToAdd = new OrderItemRequest()
                {
                    Copies = 1,
                    OrderId = order.id,
                    Sizing = SizingOption.ShrinkToExactFit,
                    Type = "4x6"
                };
                var result = api.OrderItems.CreateWithData(order.id, itemToAdd, dummyImage);
                Assert.AreEqual(OrderItemStatus.Ok, result.Status);
            }

        }
        [TestMethod]
        public void Add_item_with_neither_url_nor_data()
        {
            PwintyApi api = new PwintyApi();
            var order = base.CreateEmptyOrderWithValidAddress(api);

            OrderItemRequest itemToAdd = new OrderItemRequest()
            {
                Copies = 1,
                OrderId = order.id,
                Sizing = SizingOption.ShrinkToExactFit,
                Type = "4x6"
            };
            var result = api.OrderItems.Create(order.id, itemToAdd);
            Assert.AreEqual(OrderItemStatus.AwaitingUrlOrData, result.Status);

        }
        [TestMethod]
        public void Add_item_with_invalid_url()
        {
            try
            {
            PwintyApi api = new PwintyApi();
            var order = base.CreateEmptyOrderWithValidAddress(api);

                OrderItemRequest itemToAdd = new OrderItemRequest()
                {
                    Copies = 1,
                    OrderId = order.id,
                    Sizing = SizingOption.ShrinkToExactFit,
                    Url = "ftp://farm8.staticflickr.com/7046/6904409825_fd4b1482fe_b.jpg",
                    Type = "4x6"
                };
                var result = api.OrderItems.Create(order.id, itemToAdd);
                Assert.Fail("Should throw error when invalid url");
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
            }
        }
        [TestMethod]
        public void Add_item_to_order_with_url()
        {
            PwintyApi api = new PwintyApi();
            var order = base.CreateEmptyOrderWithValidAddress(api);

            OrderItemRequest itemToAdd = new OrderItemRequest()
            {
                Copies = 1,
                OrderId = order.id,
                Sizing = SizingOption.ShrinkToExactFit,
                Url = "http://farm8.staticflickr.com/7046/6904409825_fd4b1482fe_b.jpg",
                Type = "4x6"
            };
            var result = api.OrderItems.Create(order.id, itemToAdd);
            Assert.AreEqual(OrderItemStatus.NotYetDownloaded, result.Status);

        }
        [TestMethod]
        public void Add_item_to_order_with_md5Hash()
        {

                PwintyApi api = new PwintyApi();
                var order = base.CreateEmptyOrderWithValidAddress(api);
                using (var dummyImage = File.OpenRead("itemtest.jpg"))
                {
                    var fileHash = Md5HashCalculator.MD5HashFile(dummyImage);
                    OrderItemRequest itemToAdd = new OrderItemRequest()
                    {
                        Copies = 1,
                        OrderId = order.id,
                        Sizing = SizingOption.ShrinkToExactFit,
                        Md5Hash = fileHash,
                        Type = "4x6"
                    };
                    var result = api.OrderItems.CreateWithData(order.id, itemToAdd, dummyImage);
                    Assert.AreEqual(OrderItemStatus.Ok, result.Status);
                }

        }
        [TestMethod]
        public void Add_item_to_order_with_invalid_md5Hash_causes_error()
        {
            try
            {
                PwintyApi api = new PwintyApi();
                var order = base.CreateEmptyOrderWithValidAddress(api);
                using (var dummyImage = File.OpenRead("itemtest.jpg"))
                {
                    using (var dummyImageTwo = File.OpenRead("itemtest2.jpg"))
                    {
                        var incorrectFileHash = Md5HashCalculator.MD5HashFile(dummyImageTwo);

                        OrderItemRequest itemToAdd = new OrderItemRequest()
                        {
                            Copies = 1,
                            OrderId = order.id,
                            Sizing = SizingOption.ShrinkToExactFit,
                            Md5Hash = incorrectFileHash,
                            Type = "4x6"
                        };

                        var result = api.OrderItems.CreateWithData(order.id, itemToAdd, dummyImage);
                    }
                }
            }
            catch (PwintyApiException exc)
            {
                Assert.IsNotNull(exc.Message);
                Assert.AreEqual(HttpStatusCode.BadRequest, exc.StatusCode);
            }
        }
    }
}
