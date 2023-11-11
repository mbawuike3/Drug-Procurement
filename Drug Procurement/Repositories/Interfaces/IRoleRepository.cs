using Drug_Procurement.Models;

namespace Drug_Procurement.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Roles>> GetRoles();
        Task<Roles> CreateRoles(Roles roles);
    }
}
