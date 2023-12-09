using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetRolesByIdQuery : IRequest<Roles>
    {
        public int Id { get; set; }
    }
    public class GetRolesByIdQueryHandler : IRequestHandler<GetRolesByIdQuery, Roles>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _dapperConnection;

        public GetRolesByIdQueryHandler(ApplicationDbContext context, ISqlConnectionFactory dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;
        }

        public async Task<Roles> Handle(GetRolesByIdQuery request, CancellationToken cancellationToken)
        {
            //var role = await _context.Roles.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            using var context = _dapperConnection.CreatedDbConnection();
            if(context.State != ConnectionState.Open)
            {
                context.Open();
            }
            string sql = @"SELECT * FROM Roles WHERE IsDeleted=0 AND Id=@Id;";
            var role = await context.QueryFirstOrDefaultAsync<Roles>(sql, new { request.Id });
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return role;
        }
    }
}
