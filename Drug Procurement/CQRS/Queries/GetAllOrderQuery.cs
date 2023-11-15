using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllOrderQuery : IRequest<IEnumerable<Order>>
    {
    }
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<Order>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllOrderQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Order.ToListAsync()).Where(x => x.IsDeleted == false).ToList();
        }
    }
}
