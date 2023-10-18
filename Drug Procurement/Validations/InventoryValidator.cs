using Drug_Procurement.Helper;
using Drug_Procurement.Models;
using FluentValidation;

namespace Drug_Procurement.Validations
{
    public class InventoryValidator : AbstractValidator<Inventory>
    {
        public InventoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .Must(p => StringHelper.IsAlphabet(p));

            RuleFor(x => x.ManufacturerName)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(100)
                .Must(p => StringHelper.IsAlphaNumeric(p));

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .Must(p => StringHelper.IsValidatePrice(p));
        }
    }
}
