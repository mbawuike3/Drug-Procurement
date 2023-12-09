using Drug_Procurement.Context.Dapper;
using Microsoft.AspNetCore.Connections;
using System.Data;

namespace Drug_Procurement.Helper
{
    public  class ConnectionHelper
    {
        public static IDbConnection GetConnection(ISqlConnectionFactory _connectionFactory)
        {
            var context = _connectionFactory.CreatedDbConnection();
            if (context.State != ConnectionState.Open)
            {
                context.Open();
            }
            return context;
        }
    }
}
