using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Utilities;
using Common.Helpers.Exceptions;

namespace Application.Common.Helpers.Exceptions
{
    public class CommonExceptions : Exception
    {
        private CommonExceptions(string message, string code) : base(message) => Code = code;

        public string Code { get; set; }

        public static CommonExceptions Throw(BusinessExceptionTypes exceptionType)
        {
            ServiceException serviceExceptionDefault = new()
            {
                Id = "DefaultException",
                Code = "409.500",
                Message = "Unknow error",
                Description = "Este error se genera cuando no existe la propiedad en el archivo de configuración del servicio (appsettings) que contiene el listado de códigos de error."
            };

            return new(serviceExceptionDefault.Message, serviceExceptionDefault.Code);
        }
    }
}
