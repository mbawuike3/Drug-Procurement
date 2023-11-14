using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetInventoryByIdQuery : IRequest<Inventory>
    {
        public int Id { get; set; }
    }
    public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, Inventory>
    {
        private readonly ApplicationDbContext _context;

        public GetInventoryByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (inventory == null)
                return default;
            return inventory;
        }
    }
}
