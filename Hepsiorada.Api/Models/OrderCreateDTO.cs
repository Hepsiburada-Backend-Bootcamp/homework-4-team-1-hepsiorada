using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Models
{
    public class OrderCreateDTO
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Guid UserId { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
