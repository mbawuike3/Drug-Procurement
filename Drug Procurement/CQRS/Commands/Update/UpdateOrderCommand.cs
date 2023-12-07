using Drug_Procurement.Context;
using Drug_Procurement.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Update
{
    public class UpdateOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Email { get; set; } = string.Empty;
    }
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public UpdateOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0 || string.IsNullOrEmpty(request.Email))
            {
                throw new Exception("Invalid input parameters");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                return "User does not exist";
            }
            if ((RoleEnum)user.RoleId != RoleEnum.Admin && user.RoleId != (int)RoleEnum.Supplier)
            {
                throw new InvalidOperationException("Only Admin or Supplier can Update an order");
            }
            
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (order == null)
            {
                return "Order not found";
            }
            order.Price = request.Price;
            order.Quantity = request.Quantity;
            order.Description = request.Description;
            order.Email = request.Email;
            order.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return "Order Updated";
        }
    }
}
