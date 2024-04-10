using Application.DTOs;
using Application.DTOs.PlantillaEntitys;
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
            RuleFor(transaction => transaction.Data)
                .NotNull()
                .WithErrorCode(Convert.ToInt32(DriverBusinessException.DataCannotBeNull).ToString())
                .WithMessage(nameof(DriverBusinessException.DataCannotBeNull))
                .NotEmpty()
                .WithErrorCode(Convert.ToInt32(DriverBusinessException.DataCannotBeEmpty).ToString())
                .WithMessage(nameof(DriverBusinessException.DataCannotBeEmpty))
                .ChildRules(Entity =>
                {
                    Entity.RuleFor(id => id.Id)
                    .NotEmpty()
                    .WithErrorCode(Convert.ToInt32(DriverBusinessException.TransactionIdCanNotBeEmpty).ToString())
                    .WithMessage(nameof(DriverBusinessException.TransactionIdCanNotBeEmpty))
                    .NotNull()
                    .WithErrorCode(Convert.ToInt32(DriverBusinessException.TransactionIdCanNotBeEmpty).ToString())
                    .WithMessage(nameof(DriverBusinessException.TransactionIdCanNotBeEmpty));
                });
        }
    }
}
