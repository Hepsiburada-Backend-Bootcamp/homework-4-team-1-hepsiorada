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
        public IOrderRepository OrderRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Product> ProductRepository { get; set; }
        public IMongoOrderRepository OrderSummary { get; set; }

    }
}
