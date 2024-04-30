using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Common.Helpers.Exceptions;
using FluentValidation;

namespace Application.Common.FluentValidations.Validators
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductNameCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductNameCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters))
                .MaximumLength(50)
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CharactersLenghtNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.CharactersLenghtNotValid));
            RuleFor(s => s.Products)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductPriceCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductPriceCannotBeNull));
            RuleFor(s => s.PriceTotal)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductPriceCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ProductPriceCannotBeNull));
        }
    }
}
