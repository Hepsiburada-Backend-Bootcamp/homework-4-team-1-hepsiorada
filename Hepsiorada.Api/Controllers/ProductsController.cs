using Hepsiorada.Application.Commands.Product;
using Hepsiorada.Application.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            GetProductsCommand getProductsCommand = new GetProductsCommand();
            List<ProductGetDTO> products = await _mediator.Send(getProductsCommand);

            return Ok(products);//TODO
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] ProductCreateDTO productDTO
            )
        {
            CreateProductCommand createCommand = productDTO.Adapt<CreateProductCommand>();
            ProductCreateDTO product = await _mediator.Send(createCommand);

            return Ok(product);//TODO
        }
    }
}
