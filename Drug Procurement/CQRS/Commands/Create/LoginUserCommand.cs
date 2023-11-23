using Drug_Procurement.Context;
using Drug_Procurement.Security.Hash;
using Drug_Procurement.Security.Jwt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class LoginUserCommand : IRequest<JwtAuthResponse>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JwtAuthResponse>
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

        public async Task<JwtAuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = new JwtAuthResponse();
            var userFromDB = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == request.UserName!.ToLower()); 
            if (userFromDB == null)
            {
                response.Message = "User not found";
                return response;
            }
            var salt = userFromDB.Salt;
            request.Password = request.Password!.Trim();
            request.Password += salt;
            var hashedPassword = _passwordService.Encoder(request.Password);
            if (hashedPassword.Equals(userFromDB.Password))
            {
                response.AccessToken = _jwtAuth.GenerateToken(userFromDB);
                return response;
            }
            response.Message = "Invalid Credentials";
            return response;
        }        
    }
}
