using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hepsiorada.Application.Commands.Order
{
    public class CreateOrderCommand : IRequest<Domain.Entities.Order>
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Guid UserId { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
