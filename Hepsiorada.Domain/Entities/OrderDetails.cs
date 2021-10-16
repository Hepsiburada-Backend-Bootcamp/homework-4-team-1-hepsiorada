using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class OrderDetails
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
