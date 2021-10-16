using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class ProductOrder
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
