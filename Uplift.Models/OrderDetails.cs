using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int ServiceId { get; set; }
        public Service  Service{ get;  }
        public string ServiceName { get; set; }
        public double Price { get; set; }

    }
}
