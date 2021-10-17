using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Entities.MongoDB;
using Hepsiorada.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Product> ProductRepository { get; }
        public IMongoOrderRepository OrderSummaryRepository { get; }

    }
}
