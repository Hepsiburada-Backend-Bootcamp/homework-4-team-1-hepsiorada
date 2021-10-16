using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.IO;

namespace Hepsiorada.Infrastructure.Context
{
    public class HepsiOradaDbDapperContext
    {
        private readonly IConfiguration Configuration;
        public HepsiOradaDbDapperContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public List<T> GetAll<T>(string fromTable, string whereCondition = "")
        {
            try
            {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                {
                    cnn.Open();

                    string query = $"SELECT * FROM {fromTable} ";

                    if(whereCondition != "")
                    {
                        query = query + $"WHERE {whereCondition} ";
                    }

                    var output = cnn.Query<T>(query);

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Write<T>(string tableName, T model, string whereCondition = "")
        {
            try
            {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                {
                    cnn.Open();

                    string query = $"INSERT INTO {tableName} (";

                    foreach(var property in model.GetType().GetProperties())
                    {
                        query = query + property.Name + ",";
                    }

                    query = query.TrimEnd(',');

                    query = query + ") VALUES (";

                    foreach(var property in model.GetType().GetProperties())
                    {
                        query = query + "@" + property.Name + ",";
                    }

                    query = query.TrimEnd(',');

                    query = query + ")";

                    if(whereCondition != "")
                    {
                        query = query + $" WHERE {whereCondition}";
                    }

                    var output = cnn.Execute(query, model);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetConnectionString()
        {
            var x = Configuration.GetConnectionString("HepsiOradaDbContext");
            var c = Directory.GetCurrentDirectory();
            return x;
        }
    }
}