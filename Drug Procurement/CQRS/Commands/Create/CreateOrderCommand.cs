using Drug_Procurement.Context;
using Drug_Procurement.DTOs;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using MediatR;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateOrderCommand : OrderDto, IRequest<int>
    {
        
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
                Email = request.Email,  
                DateCreated = DateTime.Now,
                Status = OrderStatusEnum.Created.ToString(),
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
