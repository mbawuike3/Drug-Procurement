using Drug_Procurement.Context;
using Drug_Procurement.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public DeleteOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null) return "User Not Found";
            if ((RoleEnum)user.RoleId != RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can delete an order");
            }
            var userFromDb = await _context.Order.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if(userFromDb == null)
            {
                return "Order Not Found";
            }
            userFromDb.IsDeleted = true;
            await _context.SaveChangesAsync();
            return "Order deleted successfully";
        }
    }
}
