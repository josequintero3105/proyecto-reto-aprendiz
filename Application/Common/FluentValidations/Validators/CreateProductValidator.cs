using Application.DTOs;
using FluentValidation;

namespace ProyectoBack.Validator
{
    public class CreateProductValidator : AbstractValidator<Product>
    {
        public CreateProductValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El campo del nombre no puede quedar vacio");
            RuleFor(p => p.Quantity).NotNull().WithMessage("Debe haber un numero valido");
            RuleFor(p => p.Description).NotEmpty().WithMessage("El campo no puede ser vacio");
            RuleFor(p => p.Category).NotEmpty().WithMessage("El campo no puede ser vacio");
        }
    }
}
