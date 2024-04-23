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
    }
}
