using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Repository;
using Hepsiorada.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hepsiorada.Tests.RepositoryTest.OrderTest
{
    public class OrderRepositoryTest
    {
        private readonly Mock<OrderRepository> _mockOrderRepository;
        private readonly Mock<IRepository<Product>> _mockProductRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<Order> _mockOrder;
        
        public OrderRepositoryTest()
        {
            _mockOrderRepository = new Mock<OrderRepository>();
            _mockProductRepo = new Mock<IRepository<Product>>();
            _mockConfig = new Mock<IConfiguration>();
            _mockOrder = new Mock<Order>();

            _mockOrder.Object.Id = Guid.NewGuid();
            _mockOrder.Object.UserId = Guid.NewGuid();
            _mockOrder.Object.OrderDate = DateTime.Now;
            _mockOrder.Object.TotalPrice = 250.00m;
     
        }
        [Fact]
        public async Task ShouldInvokeOrderRepositoryAddOnce()
        {

            _mockOrderRepository.Setup(x => x.Add(It.IsAny<Order>()))
               .ReturnsAsync(new Order());
            var orderRepo = new OrderRepository(_mockConfig.Object, _mockProductRepo.Object);
            await orderRepo.Add(_mockOrder.Object);

            _mockOrderRepository.Verify(x => x.Add(It.IsAny<Order>()), Times.Once);


        }
    }
}
