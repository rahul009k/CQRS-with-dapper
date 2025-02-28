using Microsoft.Data.SqlClient;

namespace CQRS_with_dapper.DBconnection
{
    public class DBConnectionApp
    {
        public readonly string _connectionString;

        public DBConnectionApp(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")!;
        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
