using Application.DTOs;

namespace Application.Interfaces.Common
{
    public interface IHandle
    {
        /// <summary>
        /// Handles the request context HTTP.
        /// </summary>
        /// <param name="useCaseFunction">The use case function.</param>
        /// <returns></returns>
        Task HandleRequestContextCatchException(Task useCaseFunction);
        /// <summary>
        /// Handles the request context HTTP.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="useCaseFunction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Y> HandleRequestContextException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity);
    }
}
