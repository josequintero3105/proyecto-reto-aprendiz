using System.Net.NetworkInformation;
using Application.DTOs.Entries;
using Common.Helpers.Exceptions;
using FluentValidation;

namespace Application.Common.FluentValidations.Validators
{
    public class ProductValidator : AbstractValidator<ProductInput>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductNameCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductNameCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductNameIsNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductNameIsNotValid))
                .MaximumLength(50)
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CharactersLenghtNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.CharactersLenghtNotValid));
            RuleFor(p => p.Price)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductPriceCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductPriceCannotBeNull));
            RuleFor(p => p.Quantity)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductQuantityCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductQuantityCannotBeNull));
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductDescriptionCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductDescriptionCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductDescriptionIsNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductDescriptionIsNotValid))
                .MaximumLength(200)
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CharactersLenghtNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.CharactersLenghtNotValid));
            RuleFor(p => p.Category)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductCategoryCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductCategoryCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductCategoryIsNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductCategoryIsNotValid))
                .MaximumLength(50)
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CharactersLenghtNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.CharactersLenghtNotValid));
        }
    }
}
