using Drug_Procurement.Context;
using Drug_Procurement.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Delete
{
    public class DeleteInventoryCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public DeleteInventoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null) return "User Not Found";
            if (user.RoleId != (int)RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can delete an inventory");
            }
            var userFromDb = await _context.Inventory.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if (userFromDb == null)
            {
                return "Inventory Not Found";
            }
            userFromDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return "Inventory deleted successfully";
        }
    }
}
