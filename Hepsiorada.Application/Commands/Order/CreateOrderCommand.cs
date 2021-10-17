using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Hepsiorada.Application.Models;

namespace Hepsiorada.Application.Commands.Order
{
    public class CreateOrderCommand : IRequest<OrderCreateDTO>
    {
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
