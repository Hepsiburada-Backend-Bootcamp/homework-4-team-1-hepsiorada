using Hepsiorada.Domain.Entities;
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
        public IRepository<Order> OrderRepository { get; set; }
    }
}
