using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int ProductQuantity { get; set; } //TODO
        public decimal TotalPrice { get; set; } //TODO
        public Guid UserId { get; set; }
    }
}
