using Application.DTOs;
using Common.Helpers.Exceptions;
using FluentValidation;

namespace Application.Common.FluentValidations.ProductValidators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(DriverBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(DriverBusinessException.JustNumberAllowInPhone.ToString())
                .WithMessage(nameof(DriverBusinessException.JustNumberAllowInPhone));
            RuleFor(p => p.Quantity)
                .NotNull()
                .NotEmpty()
                .WithErrorCode(DriverBusinessException.JustNumberAllowInPhone.ToString())
                .WithMessage(nameof(DriverBusinessException.JustNumberAllowInPhone));
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(DriverBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
            RuleFor(p => p.Category)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(DriverBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(DriverBusinessException.NotAllowSpecialCharacters));
        }
    }
}
