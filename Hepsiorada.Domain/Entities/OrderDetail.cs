using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hepsiorada.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public Product Product { get; set; }
    }
}
