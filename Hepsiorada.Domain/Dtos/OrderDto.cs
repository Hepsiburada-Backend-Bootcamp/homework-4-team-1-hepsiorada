using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Dtos
{
    public class OrderDto
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
