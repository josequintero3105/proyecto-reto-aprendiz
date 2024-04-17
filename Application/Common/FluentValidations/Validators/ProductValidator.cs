﻿using Application.DTOs;
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
                .WithMessage("The product name cannot be empty")
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithMessage("The product name should not has special characters")
                .MaximumLength(50)
                .WithMessage("The product contains more chracteres over the limit");
            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("The product price cannot be empty");
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .WithMessage("The product quantity cannot be empty");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("The product description cannot be empty")
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithMessage("The product description should not has special characters")
                .MaximumLength(200)
                .WithMessage("The description cannot be empty");
            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage("The product category cannot be empty")
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithMessage("The product category should not has special characters")
                .MaximumLength(50)
                .WithMessage("The product category cotains more characters over the limit");
        }
    }
}
