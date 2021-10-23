using Hepsiorada.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(params string[] columns);
        Task<List<T>> GetAll(string filter);
        Task<List<T>> GetAll(string filter, params string[] columns);
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
