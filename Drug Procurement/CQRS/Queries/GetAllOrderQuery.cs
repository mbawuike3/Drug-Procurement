using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepoDb;
using System.Data;

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
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IPagination _pagination;

        public GetAllOrderQueryHandler(ApplicationDbContext context, ISqlConnectionFactory connectionFactory, IPagination pagination)
        {
            _context = context;
            _connectionFactory = connectionFactory;
            _pagination = pagination;
        }

        public async Task<PagedResult<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            //var orders2 =(await _context.Order.ToListAsync()).Where(x => x.IsDeleted == false).ToList();

            var context = ConnectionHelper.GetConnection(_connectionFactory);
            
            List<QueryField> condition = new();
            condition.Add(new QueryField("IsDeleted", false));

            var orders = (await context.QueryAsync<Order>(
                tableName : "Order",
                where : condition,
                cancellationToken : default
                )).ToList();
            return _pagination.GetPaginatedResult(orders, request.PageNumber, request.PageSize);
        }
    }
}
