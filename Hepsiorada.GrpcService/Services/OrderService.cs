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

namespace Hepsiorada.GrpcService
{
    public class OrderSummaryService : OrderRpc.OrderRpcBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<OrderSummaryService> _logger;
        public OrderSummaryService(IMediator mediator, ILogger<OrderSummaryService> logger)
        {
            _logger = logger;
        }
        public override async Task<OrderMessage> GetOrders(OrderFilter request, ServerCallContext context)
        {

            GetOrderSummariesCommand getOrderSummariesCommand = new GetOrderSummariesCommand();
            List<OrderSummary> order = await _mediator.Send(getOrderSummariesCommand);
            _ = TypeAdapterConfig<OrderSummary, OrderDetailMessage>.NewConfig()
                .Map(dest => dest.OrderItems, src => src.OrderLines);
            OrderDetailMessage m = order.Adapt<OrderDetailMessage>();
            //var order = new OrderMessage()
            //{
            //    Order = new OrderDetailMessage()
            //    {
            //        Address = "adres deneme",
            //        Email = "email"
            //    }
            //};

            //order.Order.OrderItems.Clear();
            //order.Order.OrderItems.Add(new OrderItems() { Brand = "brand" });
            return await Task.FromResult(new OrderMessage() { Order = m });
        }
    }
}
