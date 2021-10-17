using System;
using System.Collections.Generic;

namespace Hepsiorada.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
    }
}
