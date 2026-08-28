using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Configuration;

namespace RestauranteAPI.Data
{
    public static class Database
    {
        private static string _connectionString = "Server=localhost;Port=3306;Database=api;User ID=root;Password=root;";

        public static MySqlConnection Connect()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(_connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}