using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.Models;

namespace Drug_Procurement.Repositories.Interfaces
{
    public interface IUserRepository
    {
         Task<IEnumerable<Users>> GetUsers();
        Task<Users>CreateUsers(Users users);
    }
}
