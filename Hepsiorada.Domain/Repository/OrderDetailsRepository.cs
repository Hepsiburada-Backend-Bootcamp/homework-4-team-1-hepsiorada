using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        public async Task<OrderDetails> Add(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetails> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
