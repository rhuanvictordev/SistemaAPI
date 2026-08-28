using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Configuration;

namespace RestauranteAPI.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Mysql");
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}