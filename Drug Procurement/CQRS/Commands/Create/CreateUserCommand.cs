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
        private readonly IUserRepository _repository;
        private readonly IPasswordService _passwordService;

        public CreateUserCommandHandler(IUserRepository repository, IPasswordService passwordService)
        {
            _repository = repository;
            _passwordService = passwordService;
            _repository = repository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Created salt, trim for white spaces and add password to the salt.
            var salt = Guid.NewGuid().ToString();
            request.Password = request.Password.Trim();
            request.Password += salt;

            var user = new Users
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Salt = salt,
                Password = _passwordService.Encoder(request.Password),
                RoleId = request.RoleId,
                DateCreated = DateTime.Now
            };
            user = await _repository.CreateUser(user);  
            return user.Id;
        }
    }
}
