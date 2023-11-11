using Drug_Procurement.Context;
using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.Models;
using Drug_Procurement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> CreateUser(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<Users>> GetUsers()
        => await _context.Users.ToListAsync();
    }
}
