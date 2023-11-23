using Drug_Procurement.Context;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllInventoryQuery : IRequest<PagedResult<Inventory>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, PagedResult<Inventory>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPagination _pagination;

        public GetAllInventoryQueryHandler(ApplicationDbContext context, IPagination pagination)
        {
            _context = context;
            _pagination = pagination;
        }

        public async Task<PagedResult<Inventory>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            var inventories = (await _context.Inventory.ToListAsync()).Where(x => x.IsDeleted == false).ToList();
            return _pagination.GetPaginatedResult(inventories, request.PageSize, request.PageNumber);
        }
    }
}
