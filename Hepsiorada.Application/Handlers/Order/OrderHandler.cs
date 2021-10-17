using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hepsiorada.Application.Commands.Order;
using Hepsiorada.Application.Models;
using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Entities.MongoDB;
using Hepsiorada.Domain.UnitOfWork;
using Mapster;
using MediatR;

namespace Hepsiorada.Application.Handlers.Order
{
    public class OrderHandler :     IRequestHandler<CreateOrderCommand, OrderCreateDTO>,
                                    IRequestHandler<GetOrderSummariesCommand, List<OrderSummary>>
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
            Domain.Entities.Order order = await _unitOfWork.OrderRepository.AddOrderWithDetails(orderEntity, request.OrderDetails);

            OrderSummary orderSummary = new OrderSummary();

            User user = await _unitOfWork.UserRepository.GetById(orderEntity.UserId);

            orderSummary.OrderId = order.Id.ToString();
            orderSummary.OrderDate = order.OrderDate;
            orderSummary.TotalPrice = order.TotalPrice;

            orderSummary.UserId = user.Id.ToString();
            orderSummary.FirstName = user.FirstName;
            orderSummary.LastName = user.LastName;
            orderSummary.Email = user.Email;
            orderSummary.Address = user.Address;
            orderSummary.PhoneNumber = user.PhoneNumber;

            foreach (var orderDetails in request.OrderDetails)
            {
                Domain.Entities.Product product = await _unitOfWork.ProductRepository.GetById(orderDetails.ProductId);
                OrderLines orderLine = new OrderLines();

                if (product != null)
                {
                    orderLine.ProductName = product.ProductName;
                    orderLine.Brand = product.Brand;
                    orderLine.Description = product.Description;
                    orderLine.Stock = product.Stock;
                    orderLine.Price = product.Price;
                    orderLine.ProductQuantity = orderDetails.ProductQuantity;
                    orderLine.ProductUnitPrice = orderDetails.ProductUnitPrice;

                    orderSummary.OrderLines.Add(orderLine);
                }
            }

            await _unitOfWork.OrderSummaryRepository.Add(orderSummary);

            return request.Adapt<OrderCreateDTO>();//TODO 
        }

        public async Task<List<OrderSummary>> Handle(GetOrderSummariesCommand request, CancellationToken cancellationToken)
        {
            var x = await _unitOfWork.OrderSummaryRepository.GetAll();
            return x;
        }
    }
}
