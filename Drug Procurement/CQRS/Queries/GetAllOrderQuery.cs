using Drug_Procurement.Context;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllOrderQuery : IRequest<PagedResult<Order>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PagedResult<Order>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPagination _pagination;

        public GetAllOrderQueryHandler(ApplicationDbContext context, IPagination pagination)
        {
            _context = context;
            _pagination = pagination;
        }

        public async Task<PagedResult<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders =(await _context.Order.ToListAsync()).Where(x => x.IsDeleted == false).ToList();
            return _pagination.GetPaginatedResult(orders, request.PageNumber, request.PageSize);
        }
    }
}
