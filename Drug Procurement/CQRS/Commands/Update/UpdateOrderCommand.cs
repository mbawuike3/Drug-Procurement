using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.Enums;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepoDb;

namespace Drug_Procurement.CQRS.Commands.Update
{
    public class UpdateOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Email { get; set; } = string.Empty;
    }
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _connectionFactory;

        public UpdateOrderCommandHandler(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0 || string.IsNullOrEmpty(request.Email))
            {
                throw new Exception("Invalid input parameters");
            }
            // var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            using var context = ConnectionHelper.GetConnection(_connectionFactory);
            List<QueryField> condition = new();
            condition.Add(new QueryField("Id", request.UserId));

            var user = (await context.QueryAsync<Users>(
                tableName: "Users",
                where: condition,
                cancellationToken: default
                )).FirstOrDefault();
            if (user == null)
            {
                return "User does not exist";
            }
            if ((RoleEnum)user.RoleId != RoleEnum.Admin && user.RoleId != (int)RoleEnum.Supplier)
            {
                throw new InvalidOperationException("Only Admin or Supplier can Update an order");
            }

            //var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == request.Id);
            condition.Clear();
            condition.Add(new QueryField("Id", request.Id));

            var order = (await context.QueryAsync<Order>(
                tableName: "Order",
                where: condition,
                cancellationToken: cancellationToken
                )).FirstOrDefault();
            if (order == null)
            {
                return "Order not found";
            }
            order.Price = request.Price;
            order.Quantity = request.Quantity;
            order.Description = request.Description;
            order.Email = request.Email;
            order.DateModified = DateTime.UtcNow;
            var updated = await context.UpdateAsync(
                tableName: "Order",
                entity: order,
                cancellationToken: cancellationToken
                );
            //await _context.SaveChangesAsync();
            return $"Order with Id {updated} Updated";
        }
    }
}
