using Drug_Procurement.Models;

namespace Drug_Procurement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users>CreateUser(Users user);
    }
}
