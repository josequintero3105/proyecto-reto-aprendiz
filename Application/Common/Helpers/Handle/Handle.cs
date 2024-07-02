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
        /// <summary>
        /// Declaring ILogger
        /// </summary>
        private readonly ILogger<Handle> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public Handle(ILogger<Handle> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// async task handleRequestContextHCatchException
        /// </summary>
        /// <param name="useCaseFunction"></param>
        /// <returns></returns>
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
        /// <summary>
        /// async task HandleRequestContextException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="useCaseFunction"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Y> HandleRequestContextException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity)
        {
            try
            {
                return await useCaseFunction(entity);
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
    }
}
