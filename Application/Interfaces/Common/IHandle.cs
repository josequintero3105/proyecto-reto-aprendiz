using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Handle;

namespace Application.Interfaces.Common
{
    public interface IHandle
    {
        /// <summary>
        /// Handles the request context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="useCaseFunction">The use case function.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <returns></returns>
        Task<GenericResponse> HandleRequestContextGrpc<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity, Func<ValidationSettings, Task> validator, ValidationSettings validationSettings);

        /// <summary>
        /// Handles the request context HTTP.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="useCaseFunction">The use case function.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <returns></returns>
        Task<Y> HandleRequestContextCatchException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity, Func<ValidationSettings, Task> validator, ValidationSettings validationSettings);

        Task<Y> HandleRequestContextException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity);

        /// <summary>
        /// Handles the request context generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="useCaseFunction">The use case function.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <returns></returns>
        Task<Y> HandleRequestContextNotCatchException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity, Func<ValidationSettings, Task> validator, ValidationSettings validationSettings);
    }
}
