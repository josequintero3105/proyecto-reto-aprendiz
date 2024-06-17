using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.Common.Validations.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Common.FluentValidations.Extentions
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Validates the and throws asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValidator">The type of the validator.</typeparam>
        /// <param name="element">The element.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="BusinessException"></exception>
        public static async Task ValidateAndThrowsAsync<T, TValidator>(this T element)

            where TValidator : IValidator<T>
        {
            var validator = (TValidator)typeof(TValidator).New();
            ValidationResult validationResult = await validator.ValidateAsync(element);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.Errors[0].ErrorMessage, validationResult.Errors[0].ErrorCode);
            }
        }
    }
}
