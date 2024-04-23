using Application.Common.Helpers.Exceptions;
using Application.Common.Helpers.Logger;
using Application.DTOs;
using Application.Interfaces.Common;
using Common.Helpers.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Helpers.Handle
{
    public class Handle : IHandle
    {
        private readonly ILogger<Handle> _logger;
        public Handle(ILogger<Handle> logger)
        {
            _logger = logger;
        }

        public async Task HandleRequestContextCatchException(Task useCaseFunction)
        {
            try
            {
                await useCaseFunction;
            }
            catch (BusinessException be)
            {
                LoggerMessageDefinition.BusinessException(_logger, be.Source, be.Code, be);
                throw;
            }
            catch (Exception ex)
            {
                LoggerMessageDefinition.ErrorNotControllerException(_logger, ex.Source, (int)BusinessExceptionTypes.NotControlledException, ex);
                throw;
            }
        }

        public async Task<Y> HandleRequestContextNotCatchException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity)
        {
            return await useCaseFunction(entity);
        }
    }
}
