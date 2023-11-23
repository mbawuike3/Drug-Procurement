using Drug_Procurement.Context;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using Drug_Procurement.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Queries
{
    public class GetAllUsersQuery : IRequest<PagedResult<Users>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<Users>>
    {
        private readonly IUserRepository _repository;
        private readonly IPagination _pagination;

        public GetAllUsersQueryHandler(IUserRepository repository, IPagination pagination)
        {
            _repository = repository;
            _pagination = pagination;
        }

        public async Task<PagedResult<Users>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = (await _repository.GetAllUsers()).Where(x => x.IsDeleted == false).ToList();
            return _pagination.GetPaginatedResult(users, request.PageNumber, request.PageSize);
  
        }
    }

}
