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
            RuleFor(s => s._id)
                .NotNull()
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid))
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdIsNotValid).ToString())
                .NotEmpty()
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid))
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdIsNotValid).ToString())
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters));
        }
    }
}
