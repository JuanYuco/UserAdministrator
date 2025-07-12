using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using UserAdministrator.Data.Interfaces;

namespace UserAdministrator.Data.DBContext
{
    public class AppContext : IAppContext
    {
        private readonly string _connectionString;
        public AppContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConecction") ?? "";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
