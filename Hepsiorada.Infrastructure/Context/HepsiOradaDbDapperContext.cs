using Microsoft.Extensions.Configuration;
using Dapper;

namespace Hepsiorada.Infrastructure.Context
{
    public class HepsiOradaDbDapperContext
    {
        private readonly IConfiguration Configuration;
        public SQLiteService(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.LogService = logService;
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

                    LogService.Log(SharedData.LogMessageSelectSuccess);

                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return null;
            }
        }

        public bool Write<T>(string tableName, T model, string whereCondition = "")
        {
            try
            {
                using (IDbConnection cnn = new SqliteConnection(GetConnectionString()))
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

                    LogService.Log(SharedData.LogMessageInsertSuccess);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return false;
            }
        }

        private string GetConnectionString()
        {
            var x = Configuration.GetConnectionString("SQLite");
            var c = Directory.GetCurrentDirectory();
            return x;
        }
    }
}