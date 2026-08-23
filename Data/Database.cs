using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RestauranteAPI.Framework;

namespace RestauranteAPI.Data
{
    public class Database
    {
        public static string connectionString;
        public static MySqlConnection Connection;

        public Database()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = configuration.GetConnectionString("Mysql");
            
        }

        public bool Load(string tableName, string id)
        {
            return false;
        }

        public MySqlCommand CreateCommand()
        { 
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn.CreateCommand();
        }
    }
}