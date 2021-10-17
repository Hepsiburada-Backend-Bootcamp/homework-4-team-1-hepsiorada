using Hepsiorada.Domain.Entities.MongoDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Commands.Order
{
    public class GetOrderSummariesCommand : IRequest<List<OrderSummary>>
    {

    }
}
