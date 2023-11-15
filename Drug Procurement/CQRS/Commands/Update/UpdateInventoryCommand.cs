using Drug_Procurement.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Update
{
    public class UpdateInventoryCommand : IRequest<string>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public double Price { get; set; }
    }
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public UpdateInventoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (userFromDb == null)
            {
                return "User does not exist";
            }
            if (userFromDb.RoleId != 1 && userFromDb.RoleId != 2)
            {
                throw new InvalidOperationException("Only Admin can create an inventory");
            }
            var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (inventory == null)
                return "No Inventory with Id found";
            inventory.Name = request.Name;
            inventory.ManufacturerName = request.ManufacturerName;
            inventory.Price = request.Price;
            inventory.DateModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return "Inventory Updated";
            
        }
    }
}
