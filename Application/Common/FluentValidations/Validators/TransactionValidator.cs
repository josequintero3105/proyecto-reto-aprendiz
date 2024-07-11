using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;
using Common.Helpers.Exceptions;
using FluentValidation;

namespace Application.Common.FluentValidations.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionInput>
    {
        public TransactionValidator() 
        {
            RuleFor(t => t.Invoice)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull))
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.ShoppingCartIdCannotBeNull).ToString())
                .WithMessage(nameof(GateWayBusinessException.ShoppingCartIdCannotBeNull));
        }
    }
}
