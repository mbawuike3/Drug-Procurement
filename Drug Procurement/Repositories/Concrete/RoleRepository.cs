using Drug_Procurement.Context;
using Drug_Procurement.Models;
using Drug_Procurement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.Repositories.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Roles> CreateRoles(Roles roles)
        {
            await _context.Roles.AddAsync(roles);
            await _context.SaveChangesAsync();
            return roles;
        }
       
        public async Task<IEnumerable<Roles>> GetRoles()
        => await _context.Roles.ToListAsync();
    }
}
