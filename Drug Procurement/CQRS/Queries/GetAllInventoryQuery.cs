using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllInventoryQuery : IRequest<IEnumerable<Inventory>>
    {
    }
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, IEnumerable<Inventory>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllInventoryQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Inventory.ToListAsync()).Where(x => x.IsDeleted == false).ToList();
        }
    }
}
