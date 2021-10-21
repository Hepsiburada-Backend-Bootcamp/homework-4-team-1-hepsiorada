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
    public class UserRepository : IRepository<User>
    {
        private string ConnectionString;
        public UserRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("HepsiOradaDbContext");
        }

        public async Task<User> Add(User User)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                    = $"INSERT INTO Users (Id, FirstName, LastName, Email, Address, PhoneNumber) VALUES (@Id, @FirstName, @LastName, @Email, @Address, @PhoneNumber)";

                var parameters = new DynamicParameters();
                parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
                parameters.Add("FirstName", User.FirstName, DbType.String);
                parameters.Add("LastName", User.LastName, DbType.String);
                parameters.Add("Email", User.Email, DbType.String);
                parameters.Add("Address", User.Address, DbType.String);
                parameters.Add("PhoneNumber", User.PhoneNumber, DbType.String);

                var output = await cnn.ExecuteAsync(query, parameters);
            }

            return User;
        }


        //TODO If exists?
        public async Task Delete(User User)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "DELETE FROM Users WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", User.Id, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Users";

                return (await cnn.QueryAsync<User>(query)).ToList();
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query = "SELECT * FROM Users WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Guid);

                return (await cnn.QueryAsync<User>(query, parameters)).FirstOrDefault();
            }
        }

        public async Task Update(User User)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                string query
                = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Address = @Address, PhoneNumber = @PhoneNumber WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("FirstName", User.FirstName, DbType.String);
                parameters.Add("LastName", User.LastName, DbType.String);
                parameters.Add("Email", User.Email, DbType.String);
                parameters.Add("Address", User.Address, DbType.String);
                parameters.Add("PhoneNumber", User.PhoneNumber, DbType.String);
                parameters.Add("Id", User.Id, DbType.Guid);

                var output = await cnn.ExecuteAsync(query, parameters);
            }
        }
    }
}
