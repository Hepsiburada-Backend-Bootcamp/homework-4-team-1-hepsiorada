using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Models
{
    public class OrderDetailsDTO
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
