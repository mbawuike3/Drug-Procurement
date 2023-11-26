using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllRolesQuery : IRequest<IEnumerable<Roles>>
    {
    }
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<Roles>>
    {
        private readonly ApplicationDbContext _context;
        readonly ISqlConnectionFactory _dapperConnection;

        public GetAllRolesQueryHandler(ApplicationDbContext context, ISqlConnectionFactory dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;
        }

        public async Task<IEnumerable<Roles>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            //return (await _context.Roles.ToListAsync()).Where(x => x.IsDeleted == false).ToList();

            using var context = _dapperConnection.CreateDbConnection();
            if (context.State != ConnectionState.Open)
                context.Open();

            string sql = @"
                              SELECT * FROM Roles
                              WHERE IsDeleted=0;
                               ";
            var roles = await context.QueryAsync<Roles>(sql);
            return roles;
        }
    }

}
