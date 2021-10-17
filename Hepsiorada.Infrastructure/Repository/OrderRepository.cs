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
    public class OrderRepository : IOrderRepository
    {
        private string ConnectionString;
        public OrderRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
        }

        public async Task<Order> Add(Order order)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                    = $"INSERT INTO Order (OrderDate, UserId) VALUES (@OrderDate, @UserId)";

                var parameters = new DynamicParameters();
                parameters.Add("OrderDate", order.OrderDate, DbType.DateTimeOffset);
                parameters.Add("UserId", order.UserId, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }

            return order;
        }

        public async Task<Order> AddOrderWithDetails(Order order, List<OrderDetails> orderDetailsList)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    string query
                        = $"INSERT INTO Order (OrderDate, UserId) VALUES (@OrderDate, @UserId)";

                    var parameters = new DynamicParameters();
                    parameters.Add("OrderDate", order.OrderDate, DbType.DateTimeOffset);
                    parameters.Add("UserId", order.UserId, DbType.Guid);

                    var output = await cnn.ExecuteAsync(query, parameters, transaction: transaction);

                    int ProductQuantity = 0;
                    decimal TotalPrice = 0;

                    foreach (var orderDetails in orderDetailsList)
                    {
                        string orderDetailsQuery
                        = $"INSERT INTO OrderDetails (ProductId, OrderId, ProductQuantity, ProductUnitPrice) VALUES (@ProductId, @OrderId, @ProductQuantity, @ProductUnitPrice)";

                        var orderDetailsParameters = new DynamicParameters();
                        orderDetailsParameters.Add("ProductId", orderDetails.ProductId, DbType.Guid);
                        orderDetailsParameters.Add("OrderId", orderDetails.OrderId, DbType.Guid);
                        orderDetailsParameters.Add("ProductQuantity", orderDetails.ProductQuantity, DbType.String);
                        orderDetailsParameters.Add("ProductUnitPrice", orderDetails.ProductQuantity, DbType.Decimal);

                        await cnn.ExecuteAsync(orderDetailsQuery, orderDetailsParameters);

                        ProductQuantity = ProductQuantity + orderDetails.ProductQuantity;
                        TotalPrice = TotalPrice + (orderDetails.ProductQuantity * orderDetails.ProductUnitPrice);
                    }

                    string updateQuery
                            = $"UPDATE Order SET ProductQuantity = @ProductQuantity, TotalPrice = @TotalPrice WHERE Id = @OrderId";

                    var updateParameters = new DynamicParameters();
                    updateParameters.Add("ProductQuantity", ProductQuantity, DbType.Int32);
                    updateParameters.Add("TotalPrice", TotalPrice, DbType.Decimal);
                    updateParameters.Add("OrderId", order.Id, DbType.Guid);

                    order.TotalPrice = TotalPrice;
                    order.ProductQuantity = ProductQuantity;

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

                string query = "DELETE FROM Order WHERE Id = @Id";

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

                string query = "SELECT * FROM Order";

                return (await cnn.QueryAsync<Order>(query)).ToList();
            }
        }

        public async Task<Order> GetById(Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Order WHERE Id = @Id";

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
                = "UPDATE Order SET OrderDate = @OrderDate, UserId = @UserId";

                var parameters = new DynamicParameters();
                parameters.Add("OrderDate", Order.OrderDate, DbType.String);
                parameters.Add("UserId", Order.UserId, DbType.String);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }
    }
}
