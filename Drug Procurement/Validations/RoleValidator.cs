using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class RoleValidator : AbstractValidator<Roles>
    {
        public RoleValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsAlphabet(p));

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(1000);
        }
    }
}
