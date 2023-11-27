using Microsoft.Data.SqlClient;
using System.Data;

namespace Drug_Procurement.Context.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreatedDbConnection();
}

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreatedDbConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("AccountingConnection"));
    }
}
