using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Drug_Procurement.Context.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreateDbConnection();
}

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateDbConnection()
    {
        //return new SqlConnection(_configuration["ConnectionStrings:AccountingConnection"]);
        return new SqlConnection(_configuration.GetConnectionString("AccountingConnection"));
        //return new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:AccountingConnection"));
    }
}
