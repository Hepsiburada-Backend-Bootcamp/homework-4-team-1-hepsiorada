using Dapper;
using Hepsiorada.Domain.Entities;
using Hepsiorada.Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository,IRepository<Order>
    {
        private readonly IRepository<Product> _productRepository;
        private string ConnectionString;

        public OrderRepository(IConfiguration configuration, IRepository<Product> productRepository)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
            this._productRepository = productRepository;
        }

        public async Task<Order> Add(Order order)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                    = $"INSERT INTO Orders (OrderDate, UserId) VALUES (@OrderDate, @UserId)";

                var parameters = new DynamicParameters();
                parameters.Add("OrderDate", order.OrderDate, DbType.DateTimeOffset);
                parameters.Add("UserId", order.UserId, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }

            return order;
        }

        public async Task<Order> AddOrderWithDetails(Order order, List<OrderDetail> orderDetailsList)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    string query
                        = $"INSERT INTO Orders (Id, OrderDate, UserId, TotalPrice) VALUES (@Id, @OrderDate, @UserId, 0)";

                    Guid orderId = Guid.NewGuid();

                    var parameters = new DynamicParameters();
                    parameters.Add("Id", orderId, DbType.Guid);
                    parameters.Add("OrderDate", order.OrderDate, DbType.DateTimeOffset);
                    parameters.Add("UserId", order.UserId, DbType.Guid);

                    var output = await cnn.ExecuteAsync(query, parameters, transaction: transaction);

                    decimal TotalPrice = 0;

                    foreach (var orderDetails in orderDetailsList)
                    {
                        string orderDetailsQuery
                        = $"INSERT INTO OrderDetails (Id, ProductId, OrderId, ProductQuantity, ProductUnitPrice) VALUES (@Id, @ProductId, @OrderId, @ProductQuantity, @ProductUnitPrice)";

                        Product product = await _productRepository.GetById(orderDetails.ProductId);

                        var orderDetailsParameters = new DynamicParameters();
                        orderDetailsParameters.Add("Id", Guid.NewGuid(), DbType.Guid);
                        orderDetailsParameters.Add("ProductId", orderDetails.ProductId, DbType.Guid);
                        orderDetailsParameters.Add("OrderId", orderId, DbType.Guid);
                        orderDetailsParameters.Add("ProductQuantity", orderDetails.ProductQuantity, DbType.String);
                        orderDetailsParameters.Add("ProductUnitPrice", product.Price, DbType.Decimal);

                        await cnn.ExecuteAsync(orderDetailsQuery, orderDetailsParameters, transaction: transaction);

                        TotalPrice = TotalPrice + (orderDetails.ProductQuantity * product.Price);
                    }

                    string updateQuery
                            = $"UPDATE Orders SET TotalPrice = @TotalPrice WHERE Id = @OrderId";

                    var updateParameters = new DynamicParameters();
                    updateParameters.Add("TotalPrice", TotalPrice, DbType.Decimal);
                    updateParameters.Add("OrderId", order.Id, DbType.Guid);

                    order.TotalPrice = TotalPrice;

                    await cnn.ExecuteAsync(updateQuery, updateParameters, transaction: transaction);

                    transaction.Commit();

                    return order;
                }
            }
        }

        //TODO If exists?
        public async Task Delete(Order Order)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "DELETE FROM Orders WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", Order.Id, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<Order>> GetAll()
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Orders";

                return (await cnn.QueryAsync<Order>(query)).ToList();
            }
        }

        public async Task<Order> GetById(Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Orders WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Guid);

                return (await cnn.QueryAsync<Order>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task Update(Order Order)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                = "UPDATE Orders SET OrderDate = @OrderDate, UserId = @UserId";

                var parameters = new DynamicParameters();
                parameters.Add("OrderDate", Order.OrderDate, DbType.String);
                parameters.Add("UserId", Order.UserId, DbType.String);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }
    }
}
