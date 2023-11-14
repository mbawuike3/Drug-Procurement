using Drug_Procurement.Context;
using Drug_Procurement.DTOs;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateOrderCommand : IRequest<string>
    {
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public int InventoryId { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public CreateOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0 || request.InventoryId <= 0 || string.IsNullOrEmpty(request.Email))
            {
                throw new Exception("Invalid input parameters");
            }

            // Ensure user and inventory exist (you may need to implement these checks)
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.InventoryId);

            if (user == null || inventory == null)
            {
                throw new Exception("User or inventory not found");
            }  
            if (user.RoleId != 1 && user.RoleId != 2)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            var order = new Order
            {
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
                Email = request.Email,  
                DateCreated = DateTime.Now,
                UserId = request.UserId,
                InventoryId = request.InventoryId,
                Status = OrderStatusEnum.Created.ToString(),
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return "Order created successfully";
        }
    }
}
