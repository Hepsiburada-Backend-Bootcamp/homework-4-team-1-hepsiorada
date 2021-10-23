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
                    = $"INSERT INTO Products (Id, ProductName, Brand, Description, Stock, Price) VALUES (@Id, @ProductName, @Brand, @Description, @Stock, @Price)";

                var parameters = new DynamicParameters();
                parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
                parameters.Add("ProductName", product.ProductName, DbType.String);
                parameters.Add("Brand", product.ProductName, DbType.String);
                parameters.Add("Description", product.Description, DbType.String);
                parameters.Add("Stock", product.Stock, DbType.Int32);
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

                string query = "DELETE FROM Products WHERE Id = @Id";

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

                string query = "SELECT * FROM Products";

                return (await cnn.QueryAsync<Product>(query)).ToList();
            }
        }

        public async Task<List<Product>> GetAll(params string[] columns)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                StringBuilder querySB = new StringBuilder();
                querySB.Append("SELECT ");


                foreach (var column in columns)
                {
                    querySB.Append(column);
                    querySB.Append(",");
                }
                
                querySB.Remove(querySB.Length, 1);
                querySB.Append(" FROM Products");

                return (await cnn.QueryAsync<Product>(querySB.ToString())).ToList();
            }
        }

        public async Task<List<Product>> GetAll(string filter)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                StringBuilder querySB = new StringBuilder();
                querySB.Append("SELECT * FROM Products WHERE ");
                querySB.Append(filter);

                return (await cnn.QueryAsync<Product>(querySB.ToString())).ToList();
            }
        }

        public async Task<List<Product>> GetAll(string filter, params string[] columns)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                StringBuilder querySB = new StringBuilder();
                querySB.Append("SELECT ");


                foreach (var column in columns)
                {
                    querySB.Append(column);
                    querySB.Append(",");
                }

                querySB.Remove(querySB.Length, 1);
                querySB.Append(" FROM Products");
                querySB.Append(" WHERE ");
                querySB.Append(filter);

                return (await cnn.QueryAsync<Product>(querySB.ToString())).ToList();
            }
        }

        public async Task<Product> GetById(Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Products WHERE Id = @Id";

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
                = "UPDATE Products SET ProductName = @ProductName, Brand = @Brand, Description = @Description, Stock = @Stock, Price = @Price WHERE Id = @Id";

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
