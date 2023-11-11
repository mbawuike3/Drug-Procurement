using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.Helper;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class ResetUserPasswordValidator : AbstractValidator<ResetUserPasswordCommand>
    {
        public ResetUserPasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Must(p => StringHelper.IsAlphaNumeric(p));
        }
    }
}
