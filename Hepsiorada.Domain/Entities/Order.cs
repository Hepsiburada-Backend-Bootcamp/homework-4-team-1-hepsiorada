using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int ProductQuantity { get; set; } = 0; //TODO
        public decimal TotalPrice { get; set; } = 0; //TODO
        public Guid UserId { get; set; }
    }
}
