using Drug_Procurement.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Drug_Procurement.CQRS.Commands.Delete
{
    public class DeleteUserCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if (userFromDb == null)
            {
                return "User not found";
            }
            userFromDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return "User Deleted Successfully";
        }
    }
}
