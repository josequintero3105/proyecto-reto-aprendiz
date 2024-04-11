using Application.DTOs;
using Common.Helpers.Exceptions;
using FluentValidation;

namespace Application.Common.FluentValidations.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Quantity)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
        }
    }
}
