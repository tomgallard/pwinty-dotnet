using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pwinty.Client
{
    public class PwintyApi
    {

        public PwintyApi()
        {
         
        }
        public OrderResource Order
        {
            get {
                    return new OrderResource();
                }
        }
      
        public OrderItemResource OrderItems
        {
            get
            {
                    return new OrderItemResource();
            }
        }

    }
}
