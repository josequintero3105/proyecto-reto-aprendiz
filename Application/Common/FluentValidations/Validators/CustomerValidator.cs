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
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator() 
        {
            RuleFor(c => c.Name)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerNameCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerNameCannotBeEmpty))
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerNameCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerNameCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters));
            RuleFor(c => c.Email)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerEmailCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerEmailCannotBeEmpty))
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerEmailCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerEmailCannotBeEmpty));
            RuleFor(c => c.Phone)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerPhoneCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerPhoneCannotBeEmpty))
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.CustomerPhoneCannotBeEmpty).ToString())
                .WithMessage(nameof(GateWayBusinessException.CustomerPhoneCannotBeEmpty))
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithErrorCode(Convert.ToInt32(GateWayBusinessException.NotAllowSpecialCharacters).ToString())
                .WithMessage(nameof(GateWayBusinessException.NotAllowSpecialCharacters));
        }
    }
}
