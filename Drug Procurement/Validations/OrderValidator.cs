using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(1000)
                .Must(p => StringHelper.IsAlphaNumeric(p));

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .Must(p => StringHelper.IsNumbers(p));

            RuleFor(x => x.Price)   
                .NotEmpty()
                .NotNull()
                .Must(p => StringHelper.IsValidatePrice(p));  
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsValidEmail(p));
                

            

                
             
        }
    }
}
