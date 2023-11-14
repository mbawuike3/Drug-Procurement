using Drug_Procurement.CQRS.Commands.Create;
using Drug_Procurement.DTOs;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class OrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(500);

            
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsValidEmail(p));
           
        }
    }
}
