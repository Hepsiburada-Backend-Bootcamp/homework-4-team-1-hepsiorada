﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hepsiorada.Application.Commands.Order;
using Hepsiorada.Application.Models;
using Hepsiorada.Domain.Entities.MongoDB;
using Hepsiorada.Domain.UnitOfWork;
using Mapster;
using MediatR;

namespace Hepsiorada.Application.Handlers.Order
{
    public class OrderHandler : IRequestHandler<CreateOrderCommand, OrderCreateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            
        }

        public async Task<OrderCreateDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = request.Adapt<Domain.Entities.Order>();

            if (orderEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            //Save entities to db
            await _unitOfWork.OrderRepository.Add(orderEntity);

            foreach (var item in request.OrderDetails)
            {

            }

            await _unitOfWork.OrderSummary.Add(new OrderSummary());

            return request.Adapt<OrderCreateDTO>();//TODO 
        }
    }
}