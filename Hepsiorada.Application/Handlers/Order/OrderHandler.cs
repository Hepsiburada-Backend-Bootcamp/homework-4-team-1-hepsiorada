using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hepsiorada.Application.Commands.Order;
using Hepsiorada.Domain.UnitOfWork;
using Mapster;
using MediatR;

namespace Hepsiorada.Application.Handlers.Order
{
    public class OrderHandler : IRequestHandler<CreateOrderCommand, Domain.Entities.Order>
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            
        }

        public async Task<Domain.Entities.Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var blogPostEntity = request.Adapt<Domain.Entities.Order>();

            if (blogPostEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            //Save entities to db
            await this._unitOfWork.OrderRepository.Add(blogPostEntity);



            return blogPostEntity;
        }
    }
}
