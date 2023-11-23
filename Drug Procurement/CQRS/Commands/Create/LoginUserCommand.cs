using Drug_Procurement.Context;
using Drug_Procurement.Models;
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
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IPasswordService passwordService, ApplicationDbContext context, IJwtAuth jwtAuth, IConfiguration configuration)
        {
            _passwordService = passwordService;
            _context = context;
            _jwtAuth = jwtAuth;
            _configuration = configuration;
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
                var tupleResponse = await CreateRefreshToken(userFromDB.Email);
                response.RefreshToken = tupleResponse.Item1;
                response.Expiration = tupleResponse.Item2;
                return response;
            }
            response.Message = "Invalid Credentials";
            return response;
        }
        private async Task<(string, DateTime)> CreateRefreshToken(string email)
        {
            var now = DateTime.Now;
            var refreshTokenValidityInDays = int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"]);
            var user = await _context.LoginRefreshTokens.Where(x => x.Email.ToLower().Equals(email.ToLower()) && x.IsActive).FirstOrDefaultAsync();
            string refreshToken = string.Empty;
            var expiryTime = now.AddDays(refreshTokenValidityInDays);
            if (user == null)
            {
                refreshToken = _jwtAuth.GenerateRefreshToken();
                
                var loginRefreshToken = new LoginRefreshToken
                {
                    Email = email,
                    IsActive = true,
                    RefreshToken = refreshToken,
                    DateCreated = now,
                    DateModified = now,
                    ExpiryTime = expiryTime
                };
                await _context.LoginRefreshTokens.AddAsync(loginRefreshToken);
                await _context.SaveChangesAsync();
                return (refreshToken, expiryTime);
            }
            refreshToken = user.RefreshToken;
            user.ExpiryTime = expiryTime;
            user.DateModified = now;
            await _context.SaveChangesAsync();
            return (refreshToken, expiryTime);
        }
    }
}
