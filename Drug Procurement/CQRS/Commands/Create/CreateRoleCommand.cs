using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json.Serialization;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateRoleCommand : IRequest<string>
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[JsonIgnore]
        public int UserId { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _dapperConnection;

        public CreateRoleCommandHandler(ApplicationDbContext context, ISqlConnectionFactory dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;
        }

        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            //var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            using var context = _dapperConnection.CreatedDbConnection();
            if (context.State != ConnectionState.Open)
            {
                context.Open();
            }
            string userSql = @"SELECT * FROM Users WHERE Id=@UserId;";
            var user = await context.QueryFirstAsync<Users>(userSql, new { request.UserId });
            if (user == null)
            {
                return "User not found";
            }
            if ((RoleEnum)user.RoleId != RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            string insertRoleSql = @"INSERT INTO Roles (Name, Description, DateCreated, IsDeleted) 
                                    VALUES (@Name, @Description, @DateCreated, @IsDeleted);";
            var parameters = new Roles
            {
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            var rowAffected = await context.ExecuteAsync(insertRoleSql, parameters);
            if(rowAffected > 0)
            {
                return "Role Created";
            }
            else
            {
                return "Failed to create role";
            }
            
        }
    }
}
