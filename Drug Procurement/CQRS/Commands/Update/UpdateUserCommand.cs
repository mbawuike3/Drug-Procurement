﻿using Drug_Procurement.Context;
using Drug_Procurement.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Drug_Procurement.CQRS.Commands.Update
{
    public class UpdateUserCommand : UserUpdateDto, IRequest<int>
    {
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return default;
            }
            user.FirstName = request.FirstName;
            user.UserName = request.UserName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}