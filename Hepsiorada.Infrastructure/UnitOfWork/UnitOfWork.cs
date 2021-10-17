using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Repository;
using Hepsiorada.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Hepsiorada.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IOrderRepository OrderRepository => _serviceProvider.GetService<IOrderRepository>();
        public IRepository<User> UserRepository => _serviceProvider.GetService<IRepository<User>>();
        public IRepository<Product> ProductRepository => _serviceProvider.GetService<IRepository<Product>>();
        public IMongoOrderRepository OrderSummaryRepository => _serviceProvider.GetService<IMongoOrderRepository>();


    }
}
