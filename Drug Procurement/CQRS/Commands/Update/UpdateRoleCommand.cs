using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Drug_Procurement.CQRS.Commands.Update
{
    public class UpdateRoleCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateModified { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _dapperConnection;

        public UpdateRoleCommandHandler(ApplicationDbContext context, ISqlConnectionFactory dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;
        }

        public async Task<string> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            //var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            using var context = _dapperConnection.CreatedDbConnection();
            if (context.State != ConnectionState.Open)
            {
                context.Open();
            }
            string userSql = @"SELECT * FROM Users WHERE Id=@UserId AND IsDeleted=0;";
            var user = await context.QueryFirstAsync<Users>(userSql, new { request.UserId });
            if (user == null)
            {
                return "User not found";
            }
            if ((RoleEnum)user.RoleId != RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can Update a role");
            }
            //var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id);
            string roleSql = @"SELECT * FROM Roles WHERE Id=@Id AND IsDeleted=0;";
            var roleFromDb = await context.QueryFirstAsync<Roles>(roleSql, new { request.Id });

            if (roleFromDb == null)
            {
                return "Role Not Found";
            }
            roleFromDb.Name = request.Name;
            roleFromDb.Description = request.Description;
            roleFromDb.DateModified = DateTime.Now;
            string updatedRoleSql = @"UPDATE Roles SET Name=@Name, Description=@Description, DateModified=@DateModified WHERE Id=@Id;";
            var rowAffected = await context.ExecuteAsync(updatedRoleSql, roleFromDb);
            if(rowAffected > 0)
            {
                return "Roles Updated";
            }
            else
            {
                return "Failed to Update Role";
            }
            
        }
    }
}
