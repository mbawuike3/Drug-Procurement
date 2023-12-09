using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Enums;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepoDb;

namespace Drug_Procurement.CQRS.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _connectionFactory;

        public DeleteOrderCommandHandler(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<string> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            //var user = await _context.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync();
            using var context = ConnectionHelper.GetConnection(_connectionFactory);
            List<QueryField> condition = new();
            condition.Add(new QueryField("Id", request.UserId));

            var user = (await context.QueryAsync<Users>(
                tableName: "Users",
                where: condition,
                cancellationToken: default
                )).FirstOrDefault();
            if (user == null) return "User Not Found";
            if ((RoleEnum)user.RoleId != RoleEnum.Admin)
            {
                throw new InvalidOperationException("Only Admin can delete an order");
            }
            //var orderFromDb = await _context.Order.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            condition.Clear();
            condition.Add(new QueryField("Id", request.Id));
            condition.Add(new QueryField("IsDeleted", false));

            var orderFromDb = (await context.QueryAsync<Order>(
                tableName: "Order",
                where: condition,
                cancellationToken: cancellationToken
                )).FirstOrDefault();
            if (orderFromDb == null)
            {
                return "Order Not Found";
            }
            orderFromDb.IsDeleted = true;
            var updated = await context.UpdateAsync(
                tableName : "Order",
                entity: orderFromDb,
                cancellationToken : cancellationToken
                );
            //await _context.SaveChangesAsync();
            return "Order deleted successfully";
        }
    }
}
