using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int Id { get; set; }
    }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly ApplicationDbContext _context;

        public GetOrderByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            //var order = await _context.Order.Where(x => x.Id == request.Id && x.isdeleted == false).firstordefaultasync();
            var order = await _context.Order.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if(order == null)
            {
                return null!;
            }
            return order;
        }
    }
}
