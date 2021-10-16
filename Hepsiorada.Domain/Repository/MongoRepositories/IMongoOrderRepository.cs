using Hepsiorada.Domain.Entities.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public interface IMongoOrderRepository
    {
        Task Add(OrderSummary orderSummary);

        Task<List<OrderSummary>> GetAll();
    }
}
