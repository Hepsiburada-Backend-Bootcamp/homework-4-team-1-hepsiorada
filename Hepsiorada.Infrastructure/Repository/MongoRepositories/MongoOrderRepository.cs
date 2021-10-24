using Hepsiorada.Domain.Entities.MongoDB;
using Hepsiorada.Domain.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repository.MongoRepositories
{
    public class MongoOrderRepository : IMongoOrderRepository
    {
        private readonly IConfiguration _configuration;
        private IMongoCollection<OrderSummary> _orders;

        public MongoOrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetConnectionString("HepsiOradaMongoDb"));
            var database = client.GetDatabase("Hepsiorada");

            _orders = database.GetCollection<OrderSummary>("Orders");
        }

        public async Task Add(OrderSummary order)
        {
            await _orders.InsertOneAsync(order);
        }

        public async Task<List<OrderSummary>> GetAll() =>
            (await _orders.FindAsync(order => true)).ToList();

        public async Task<OrderSummary> GetById(string orderId) =>
            (await _orders.FindAsync(order => order.OrderId == orderId)).FirstOrDefault();

        public async Task Update(string orderId, OrderSummary orderIn) =>
            await _orders.ReplaceOneAsync(order => order.OrderId == orderId, orderIn);

        public async Task Remove(OrderSummary orderIn) =>
            await _orders.DeleteOneAsync(order => order.OrderId == orderIn.OrderId);

        public async Task Remove(string orderId) =>
            await _orders.DeleteOneAsync(order => order.OrderId == orderId);

    }
}
