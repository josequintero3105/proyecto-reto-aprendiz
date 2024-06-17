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
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator() 
        {
            RuleFor(i => i.ShoppingCartId)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdIsNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdIsNotValid))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters));
            RuleFor(i => i.CustomerId)
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerIdIsNotValid).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerIdIsNotValid))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters));
        }
    }
}
