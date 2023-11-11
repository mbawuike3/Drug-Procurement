using Drug_Procurement.Context;
using Drug_Procurement.Models;
using Drug_Procurement.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<Users>>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<Users>>
    {
        private readonly IUserRepository _repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Users>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsers();
            return users;
            
        }
    }

}
