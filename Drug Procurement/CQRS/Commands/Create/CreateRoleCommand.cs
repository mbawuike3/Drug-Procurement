using Drug_Procurement.Context;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public CreateRoleCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (userFromDb == null)
            {
                return "User does not exist";
            }
            if (userFromDb.RoleId != (int)RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            var role = new Roles
            {
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.Now
            };
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
            return "Role Created";
        }
    }
}
