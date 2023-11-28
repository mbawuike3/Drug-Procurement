using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateInventoryCommand : IRequest<string>
    { 
        public int UserId { get; set; }
        public string? Name { get; set; } 
        public string? ManufacturerName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public double Price { get; set; }
    }
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public CreateInventoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if(userFromDb == null)
            {
                return "User does not exist";
            }
            if(userFromDb.RoleId != 1 && userFromDb.RoleId != 2)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            var inventory = new Inventory
            {
                UserId = request.UserId,
                ManufacturerName = request.ManufacturerName,
                DateCreated = DateTime.Now,
                Name = request.Name,
                Price = request.Price,
                ExpiryDate = request.ExpiryDate,
                ManufactureDate = request.ManufactureDate
            };
            await _context.Inventory.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return "Inventory successfully created";
        }
    }
}
