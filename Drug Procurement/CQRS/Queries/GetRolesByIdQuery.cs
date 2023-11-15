using Drug_Procurement.Context;
using Drug_Procurement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetRolesByIdQuery : IRequest<Roles>
    {
        public int Id { get; set; }
    }
    public class GetRolesByIdQueryHandler : IRequestHandler<GetRolesByIdQuery, Roles>
    {
        private readonly ApplicationDbContext _context;

        public GetRolesByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Roles> Handle(GetRolesByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.Where(x => x.Id == request.Id && x.IsDeleted == false).FirstOrDefaultAsync();
            if (role == null)
            {
                return null!;
            }
            return role;
        }
    }
}
