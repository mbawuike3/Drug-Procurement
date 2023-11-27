using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json.Serialization;

namespace Drug_Procurement.CQRS.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<string>
    {
        
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _dapperConnection;

        public DeleteRoleCommandHandler(ApplicationDbContext context, ISqlConnectionFactory dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;
        }

        public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            // var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
            using var context = _dapperConnection.CreatedDbConnection();
            if (context.State != ConnectionState.Open)
            {
                context.Open();
            }
            string userSql = @"SELECT * FROM Users WHERE Id=@UserId;";
            var user = await context.QueryFirstAsync<Users>(userSql, new { request.UserId });
            if (user == null) return "User Not Found";
            if (user.RoleId != (int)RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can delete a role");
            }
            //var userFromDb = await _context.Roles.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            string roleSql = @"SELECT * FROM Roles WHERE Id=@Id AND IsDeleted=0;";
            var roleFromDb = await context.QueryFirstAsync<Roles>(roleSql, new { request.Id });

            if (roleFromDb == null)
            {
                return "Role Not Found";
            }
            roleFromDb.IsDeleted = true;
            string updatedRoleSql = @"UPDATE Roles SET IsDeleted=@IsDeleted WHERE Id=@Id;";
            var rowsAffected = await context.ExecuteAsync(updatedRoleSql, roleFromDb);
            if(rowsAffected > 0)
            {
                return "Role deleted successfully";
            }
            else
            {
                return "Failed to delete Role";
            }
            
        }
    }
}
