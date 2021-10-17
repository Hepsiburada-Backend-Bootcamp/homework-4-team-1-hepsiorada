using Hepsiorada.Domain.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Hepsiorada.Infrastructure.Module
{
    public static class RegisterExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
