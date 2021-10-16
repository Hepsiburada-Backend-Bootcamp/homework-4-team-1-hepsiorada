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

namespace Hepsiorada.Domain.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly IConfiguration Configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task<Product> Add(Product product)
        {
            using (IDbConnection cnn = new SqlConnection(Configuration.GetConnectionString("HepsiOradaDbContext")))
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

        public async Task Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
