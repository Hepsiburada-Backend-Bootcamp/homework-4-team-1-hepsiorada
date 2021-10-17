using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Repository;
using Hepsiorada.Domain.UnitOfWork;
using Hepsiorada.Infrastructure.Repository;
using Hepsiorada.Infrastructure.Repository.MongoRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hepsiorada.Infrastructure.Module
{
    public static class RegisterExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
            serviceCollection.AddTransient<IRepository<User>, UserRepository>();
            serviceCollection.AddTransient<IRepository<Product>, ProductRepository>();
            serviceCollection.AddTransient<IMongoOrderRepository, MongoOrderRepository>();
    }
    }
}
