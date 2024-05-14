using MySql.Data.MySqlClient;
using Dapper;

namespace ElasticsearchApi.Context.MySQL
{
    public class DatabaseService<T> where T : class
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetConnectionString("MySQLConnection");

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync()
        {
            using var connection = new MySqlConnection(ConnectionString);            
            string query = $"SELECT * FROM {typeof(T).Name}";
            return await connection.QueryAsync<T>(query);
        }
    }
}
