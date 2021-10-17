using Hepsiorada.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Hepsiorada.Domain.Repository;

namespace Hepsiorada.Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private string ConnectionString;
        public ProductRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
        }

        public async Task<Product> Add(Product product)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query 
                    = $"INSERT INTO Product (ProductName, Brand, Description, Stock, Price) VALUES (@ProductName, @Brand, @Description, @Stock, @Price)";

                var parameters = new DynamicParameters();
                parameters.Add("ProductName", product.ProductName, DbType.String);
                parameters.Add("Brand", product.ProductName, DbType.String);
                parameters.Add("Description", product.Description, DbType.String);
                parameters.Add("Stock", product.Description, DbType.Int32);
                parameters.Add("Price", product.Price, DbType.Decimal);

                var output = await cnn.ExecuteAsync(query, parameters);
            }

            return product;
        }


        //TODO If exists?
        public async Task Delete(Product product)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "DELETE FROM Product WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", product.Id, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<Product>> GetAll()
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Product";

                return (await cnn.QueryAsync<Product>(query)).ToList();
            }
        }

        public async Task<Product> GetById(Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Product WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Guid);

                return (await cnn.QueryAsync<Product>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task Update(Product product)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                = "UPDATE Product SET ProductName = @ProductName, Brand = @Brand, Description = @Description, Stock = @Stock, Price = @Price WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("ProductName", product.ProductName, DbType.String);
                parameters.Add("Brand", product.ProductName, DbType.String);
                parameters.Add("Description", product.Description, DbType.String);
                parameters.Add("Stock", product.Description, DbType.Int32);
                parameters.Add("Price", product.Price, DbType.Decimal);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }
    }
}
