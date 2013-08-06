using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwinty.Client.Test
{
    [TestClass]
    public class OrderItemTest :TestBase
    {
        [TestMethod]
        public void AddItemToOrder()
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
    }
}
