using Drug_Procurement.Context;
using Drug_Procurement.DTOs;
using Drug_Procurement.Enums;
using Drug_Procurement.Models;
using Drug_Procurement.Repositories.Interfaces;
using Drug_Procurement.Security.Hash;
using MediatR;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class CreateUserCommand : UserCreationDto, IRequest<int>
    {
        public int RoleId { get; set; } = 0;
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        
        private readonly IPasswordService _passwordService;

        IUserRepository _repository;

        public CreateUserCommandHandler(IPasswordService passwordService, IUserRepository repository)
        {
            _passwordService = passwordService;
            _repository = repository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = new Users
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = _passwordService.Encoder(request.Password),
                RoleId = request.RoleId == 0 ? (int)UserTypeEnum.HealthCareProvider : request.RoleId,
                DateCreated = DateTime.Now
            };
            user = await _repository.CreateUsers(user);  
            return user.Id;
        }
    }
}
