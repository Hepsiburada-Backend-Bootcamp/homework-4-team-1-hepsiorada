using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
