using Hepsiorada.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Commands.Product
{
    public class GetProductsCommand : IRequest<List<ProductGetDTO>>
    {
    }
}
