using Drug_Procurement.Context;
using Drug_Procurement.DTOs;
using Drug_Procurement.Security.Hash;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Create
{
    public class ResetUserPasswordCommand : ResetUserPasswordDto, IRequest<string>
    {
        
    }
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public ResetUserPasswordCommandHandler(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<string> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == request.UserName!.ToLower() && x.Email.ToLower() == request.Email!.ToLower());
            if (userFromDb == null)
            {
                return "User Not Found";
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return "New password is required";
            }
            var salt = userFromDb.Salt;
            request.Password = request.Password.Trim() + salt;
            var newHashPassword = _passwordService.Encoder(request.Password);
            userFromDb.Password = newHashPassword;
            await _context.SaveChangesAsync();
            return "Password reset successful";
        }
    }
}
