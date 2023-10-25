using Drug_Procurement.DTOs;
using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(500)
                .Must(p => StringHelper.IsAlphaNumeric(p));

            
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsValidEmail(p));
           
        }
    }
}
