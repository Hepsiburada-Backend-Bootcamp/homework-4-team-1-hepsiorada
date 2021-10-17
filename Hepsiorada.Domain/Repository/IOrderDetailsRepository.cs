using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public interface IOrderDetailsRepository
    {
        Task<List<OrderDetails>> GetAll();
        Task<OrderDetails> GetById(Guid ProductId, Guid OrderId);
        Task<IEnumerable<OrderDetails>> GetByOrderId(Guid OrderId);
        Task<IEnumerable<OrderDetails>> GetByProductId(Guid ProductId);
        Task<OrderDetails> Add(OrderDetails entity);
        Task Update(OrderDetails entity);
        Task Delete(OrderDetails entity);
    }
}
