using Dapper;
using Drug_Procurement.Context;
using Drug_Procurement.Context.Dapper;
using Drug_Procurement.DTOs;
using Drug_Procurement.Enums;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepoDb;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateOrderCommand : IRequest<string>
    {
        public string? Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public int InventoryId { get; set; }
        public string? Email { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly ISqlConnectionFactory _connectionFactory;

        public CreateOrderCommandHandler(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0 || request.InventoryId <= 0 || string.IsNullOrEmpty(request.Email))
            {
                throw new Exception("Invalid input parameters");
            }

            // Ensure user and inventory exist (you may need to implement these checks)
            //var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
           using var context = ConnectionHelper.GetConnection(_connectionFactory);
            List<QueryField> condition = new();
            condition.Add(new QueryField("Id", request.UserId));

            var user = (await context.QueryAsync<Users>(
                tableName: "Users",
                where: condition,
                cancellationToken: default
                )).FirstOrDefault();
            //var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.InventoryId);
            condition.Clear();
            condition.Add(new QueryField("Id", request.InventoryId));

            var inventory = (await context.QueryAsync<Inventory>(
                tableName: "Inventory",
                where: condition,
                cancellationToken: cancellationToken
                )).FirstOrDefault();
            if (user == null || inventory == null)
            {
                throw new Exception("User or inventory not found");
            }  
            if ((RoleEnum)user.RoleId != RoleEnum.Admin && user.RoleId != (int)RoleEnum.Supplier)
            {
                throw new InvalidOperationException("Only Admin or Supplier can create an order");
            }

            var order = new Order
            {
                Description = request.Description!,
                Quantity = request.Quantity,
                Price = request.Price,
                Email = request.Email,  
                DateCreated = DateTime.Now,
                UserId = request.UserId,
                InventoryId = request.InventoryId,
                Status = OrderStatusEnum.Created.ToString(),
            };
            var created = await context.InsertAsync(
                tableName : "Order",
                entity : order,
                cancellationToken : cancellationToken
                );
            //await _context.Order.AddAsync(order);
            //await _context.SaveChangesAsync();
            return $"Order with Id {created} created successfully";
        }
    }
}
