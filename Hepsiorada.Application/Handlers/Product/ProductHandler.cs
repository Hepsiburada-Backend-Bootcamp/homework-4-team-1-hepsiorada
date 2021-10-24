using Hepsiorada.Application.Commands.Product;
using Hepsiorada.Application.Models;
using Hepsiorada.Domain.UnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Handlers.Product
{
    public class ProductHandler : IRequestHandler<CreateProductCommand, ProductCreateDTO>,
                                  IRequestHandler<GetProductsCommand, List<ProductGetDTO>>,
                                  IRequestHandler<GetSingleProductCommand, ProductGetDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ProductCreateDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = request.Adapt<Domain.Entities.Product>();

            if (productEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            //Save entities to db
            Domain.Entities.Product product = await _unitOfWork.ProductRepository.Add(productEntity);

            return request.Adapt<ProductCreateDTO>();//TODO 
        }

        public async Task<List<ProductGetDTO>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.ProductRepository.GetAll()).Adapt<List<ProductGetDTO>>();
        }
        
        public async Task<ProductGetDTO> Handle(GetSingleProductCommand request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.ProductRepository.GetById(request.Id)).Adapt<ProductGetDTO>();
        }
    }
}
