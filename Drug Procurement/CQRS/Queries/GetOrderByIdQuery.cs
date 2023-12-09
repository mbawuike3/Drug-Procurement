using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepoDb;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int Id { get; set; }
    }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _connectionFactory;

        public GetOrderByIdQueryHandler(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            //var order = await _context.Order.Where(x => x.Id == request.Id && x.isdeleted == false).firstordefaultasync();
            //var order = await _context.Order.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            using var context = ConnectionHelper.GetConnection(_connectionFactory);
            List<QueryField> condition = new();
            condition.Add(new QueryField("Id", request.Id));
            condition.Add(new QueryField("IsDeleted", false));

            var orderFromDb = (await context.QueryAsync<Order>(
                tableName: "Order",
                where: condition,
                cancellationToken: cancellationToken
                )).FirstOrDefault();
            if (orderFromDb == null)
            {
                return null!;
            }
            return orderFromDb;
        }
    }
}
