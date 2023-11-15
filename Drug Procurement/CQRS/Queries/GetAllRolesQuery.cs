using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllRolesQuery : IRequest<IEnumerable<Roles>>
    {
    }
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<Roles>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllRolesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roles>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return (await _context.Roles.ToListAsync()).Where(x => x.IsDeleted == false).ToList();
        }
    }

}
