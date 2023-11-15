using Drug_Procurement.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        public UpdateRoleCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if(userFromDb == null)
            {
                return "User not found";
            }
            if (userFromDb.RoleId != 1)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (role == null) return "Role not Found";
            role.Name = request.Name;
            role.Description = request.Description;
            role.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return "Roles Updated";
        }
    }
}
