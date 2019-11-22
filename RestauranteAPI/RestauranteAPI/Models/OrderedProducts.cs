using System;

namespace RestauranteAPI.Models
{
    public class OrderedProduct
    {
        public Guid? ID { get; set; }
        public Guid? ID_Product { get; set; }
        public Guid? ID_Order { get; set; }
    }
}
