using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Models
{
    public class OrderDetailsDTO
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
