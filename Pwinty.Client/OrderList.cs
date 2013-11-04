using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pwinty.Client
{
    public class OrderList :List<Order>,IBaseItem
    {
        public string ErrorMessage { get; set; }
    }
}
