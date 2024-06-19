using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Common.Helpers.Exceptions;
using FluentValidation;
using MongoDB.Bson;

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
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters))
                .MaximumLength(24)
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid))
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdIsNotValid).ToString())
                .MaximumLength(24)
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid))
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdIsNotValid).ToString());
            RuleFor(s => s.ProductsInCart)
                .NotNull()
                .WithMessage(nameof(GateWayBusinessException.ProductListCannotBeNull))
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ProductListCannotBeNull).ToString());
        }
    }
}
