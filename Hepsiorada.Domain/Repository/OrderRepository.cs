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
    public class OrderRepository
    {
        private string ConnectionString;
        public OrderRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
        }

        public async Task<Order> Add(Order Order)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                    = $"INSERT INTO Order (OrderDate, UserId) VALUES (@OrderDate, @UserId)";

                var parameters = new DynamicParameters();
                parameters.Add("OrderDate", Order.OrderDate, DbType.DateTimeOffset);
                parameters.Add("UserId", Order.UserId, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }

            return Order;
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
