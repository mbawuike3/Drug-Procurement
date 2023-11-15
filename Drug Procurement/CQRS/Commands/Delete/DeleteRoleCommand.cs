using Drug_Procurement.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        public DeleteRoleCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null) return "User Not Found";
            if (user.RoleId != 1)
            {
                throw new InvalidOperationException("Only Admin can delete an order");
            }
            var userFromDb = await _context.Roles.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if (userFromDb == null)
            {
                return "Role Not Found";
            }
            userFromDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return "Role deleted successfully";
        }
    }
}
