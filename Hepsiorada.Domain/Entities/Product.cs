using System;
using System.Collections.Generic;

namespace Hepsiorada.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
