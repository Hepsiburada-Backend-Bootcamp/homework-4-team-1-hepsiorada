using Hepsiorada.Domain.Entities.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public class MongoOrderRepository : IMongoOrderRepository
    {
        //TODO
        public async Task Add(OrderSummary orderSummary)
        {
            return;
        }
        //TODO
        public async Task<List<OrderSummary>> GetAll()
        {
            return new List<OrderSummary>();
        }

    }
}
