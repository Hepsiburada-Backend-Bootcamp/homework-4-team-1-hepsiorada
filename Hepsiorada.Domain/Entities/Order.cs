using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal TotalPrice { get; set; } = 0;
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
