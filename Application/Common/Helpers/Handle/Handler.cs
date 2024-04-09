using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Application.DTOs.Handle;
using Application.Interfaces.Common;
using Common.Helpers.Exceptions;

namespace Application.Common.Helpers.Handle
{
    public class Handler: IHandle
    {

        public async Task<Y> HandleRequestContextCatchException<T, Y>(Func<T, Task<Y>> useCaseFunction, T entity)
        {
            try
            {
                
                return await useCaseFunction(entity);
            }
            catch (BusinessException be)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
