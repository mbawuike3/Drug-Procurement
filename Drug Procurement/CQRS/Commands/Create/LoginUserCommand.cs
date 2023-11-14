using Drug_Procurement.Context;
using Drug_Procurement.Security.Hash;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class LoginUserCommand : IRequest<string>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IPasswordService _passwordService;
        private readonly ApplicationDbContext _context;
        private readonly IJwtAuth _jwtAuth;

        public LoginUserCommandHandler(IPasswordService passwordService, ApplicationDbContext context, IJwtAuth jwtAuth)
        {
            _passwordService = passwordService;
            _context = context;
            _jwtAuth = jwtAuth;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userFromDB = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == request.UserName!.ToLower()); 
            if (userFromDB == null)
            {
                return "User not found";
            }
            var salt = userFromDB.Salt;
            request.Password = request.Password!.Trim();
            request.Password += salt;
            var hashedPassword = _passwordService.Encoder(request.Password);
            if (hashedPassword.Equals(userFromDB.Password))
            {
                return _jwtAuth.GenerateToken(userFromDB);
            }
            return "Invalid Credentials";
        }        
    }
}
