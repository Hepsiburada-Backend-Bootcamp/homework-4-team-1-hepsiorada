using Dapper;
using Hepsiorada.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Domain.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private string ConnectionString;
        public OrderDetailsRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
        }

        public async Task<OrderDetails> Add(OrderDetails orderDetails)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                    = $"INSERT INTO OrderDetails (ProductId, OrderId, ProductQuantity) VALUES (@ProductId, @OrderId, @ProductQuantity)";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", orderDetails.ProductId, DbType.String);
                parameters.Add("OrderId", orderDetails.OrderId, DbType.String);
                parameters.Add("ProductQuantity", orderDetails.ProductQuantity, DbType.String);

                await cnn.ExecuteAsync(query, parameters);
            }

            return orderDetails;
        }

        public async Task Delete(OrderDetails orderDetails)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "DELETE FROM OrderDetails WHERE ProductId = @ProductId AND OrderId = @OrderId";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", orderDetails.ProductId, DbType.Guid);
                parameters.Add("OrderId", orderDetails.OrderId, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<OrderDetails>> GetAll()
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM OrderDetails";

                return (await cnn.QueryAsync<OrderDetails>(query)).ToList();
            }
        }

        public async Task<OrderDetails> GetById(Guid ProductId, Guid OrderId)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM OrderDetails WHERE ProductId = @ProductId AND OrderId = @OrderId";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", ProductId, DbType.Guid);
                parameters.Add("OrderId", OrderId, DbType.Guid);

                return (await cnn.QueryAsync<OrderDetails>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task Update(OrderDetails orderDetails)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                = "UPDATE OrderDetails SET ProductId = @ProductId, OrderId = @OrderId, ProductQuantity = @ProductQuantity";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", orderDetails.ProductId, DbType.String);
                parameters.Add("OrderId", orderDetails.OrderId, DbType.String);
                parameters.Add("ProductQuantity", orderDetails.ProductQuantity, DbType.String);

                await cnn.ExecuteAsync(query, parameters);
            }
        }
    }
}
