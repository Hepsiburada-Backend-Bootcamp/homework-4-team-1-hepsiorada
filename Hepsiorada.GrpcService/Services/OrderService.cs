using Grpc.Core;
using Hepsiorada.Application.Commands.Order;
using Hepsiorada.Domain.Entities.MongoDB;
using Hepsiorada.GrpcService.Protos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Google.Protobuf.WellKnownTypes;

namespace Hepsiorada.GrpcService
{
    public class OrderSummaryService : OrderRpc.OrderRpcBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<OrderSummaryService> _logger;
        public OrderSummaryService(IMediator mediator, ILogger<OrderSummaryService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public override async Task<OrderMessage> GetOrders(OrderFilter request, ServerCallContext context)
        {

            GetOrderSummariesCommand getOrderSummariesCommand = new GetOrderSummariesCommand();
            List<OrderSummary> order = await _mediator.Send(getOrderSummariesCommand);
            //_ = TypeAdapterConfig<OrderSummary, OrderDetailMessage>.NewConfig()
            //    .Map(dest => dest.OrderItems, src => src.OrderLines);

            //OrderDetailMessage m = order.Adapt<OrderDetailMessage>();
            OrderMessage o = new OrderMessage();
            foreach (var item in order)
            {
                var or = item.Adapt<OrderDetailMessage>();
                or.OrderDate = Timestamp.FromDateTimeOffset(item.OrderDate);//date mapping is wrong
                foreach (var item2 in item.OrderLines)
                {
                    or.OrderItems.Add(item2.Adapt<OrderItemsMessage>());
                }
                o.Order.Add(or);
            }

            return await Task.FromResult(o);
        }
    }
}
