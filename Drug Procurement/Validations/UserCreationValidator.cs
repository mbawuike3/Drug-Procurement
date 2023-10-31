using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.DTOs;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class UserCreationValidator : AbstractValidator<CreateUserCommand>
    {
        public UserCreationValidator() 
        { 
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .Must(p => StringHelper.IsAlphabet(p));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .Must(p => StringHelper.IsAlphabet(p));

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsValidEmail(p)); 

            RuleFor(x => x.Password)    
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .Must(p => StringHelper.IsAlphaNumeric(p));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .Must(p => StringHelper.IsAlphaNumeric(p));

        }
    }
}
