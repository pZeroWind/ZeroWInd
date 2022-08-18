using MySql.Data.MySqlClient;

namespace ZeroWindApi.Models
{
    public class DapperContext
    {
        private readonly MySqlConnection _conn;

        public DapperContext(IConfiguration config)
        {
            _conn = new MySqlConnection(config.GetConnectionString("conn"));
        }

        public MySqlConnection GetConnection()
        {
            return _conn;
        }
    }
}
