using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Helpers.Logger
{
    [ExcludeFromCodeCoverage]
    public static class LoggerMessageDefinition
    {
        internal static class Events
        {
            public static readonly EventId NotControllerException = new(1, "Not Controller Exception");
            public static readonly EventId _BusinessException = new(2, "Business Exception");
            public static readonly EventId StartedService = new(3, "Started service");
            public static readonly EventId FinalizedService = new(4, "Finalized service");
            public static readonly EventId ErrorService = new(5, "An error ocurred in an endpoint");
        }

        private static readonly Action<ILogger, string, int, Exception> _errorLoggerNotControllerException = LoggerMessage.Define<string, int>(
           LogLevel.Error,
           Events.NotControllerException,
           "Not controller exception, source: {Param1}, Exception id: {Param2}");

        private static readonly Action<ILogger, string, string, Exception> _businessException = LoggerMessage.Define<string, string>(
        LogLevel.Error,
        Events._BusinessException,
        "Business exception, source: {Param1}, Exception id: {Param2}");

        private static readonly Action<ILogger, string, string, Exception> _informationLoggerStartService = LoggerMessage.Define<string, string>(
            LogLevel.Information,
            Events.StartedService,
            "Service consumption starts: {Param1}, Request: {Param2}");

        private static readonly Action<ILogger, string, string, Exception> _informationLoggerFinalizedService = LoggerMessage.Define<string, string>(
            LogLevel.Information,
            Events.FinalizedService,
            "Service consumption finalized: {Param1}, Response: {Param2}");

        private static readonly Action<ILogger, string, string, Exception> _errorLoggerService = LoggerMessage.Define<string, string>(
            LogLevel.Error,
            Events.ErrorService,
            "Service consumption finalized whit error: {Param1}, Response: {Param2}");

        /// <summary>
        /// Errors the consuming log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="exception">The exception.</param>
        public static void ErrorNotControllerException(ILogger logger, string? value1, int value2, Exception exception) =>
            _errorLoggerNotControllerException(logger, value1, value2, exception);

        /// <summary>
        /// Businesses the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="exception">The exception.</param>
        public static void BusinessException(ILogger logger, string? value1, string? value2, BusinessException exception) =>
            _businessException(logger, value1, value2, exception);

        /// <summary>
        /// StaredServiceLog
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public static void StartedServiceLog(ILogger logger, string value1, string value2) =>
            _informationLoggerStartService(logger, value1, value2, null!);

        /// <summary>
        /// Finalizeds the service log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public static void FinalizedServiceLog(ILogger logger, string value1, string value2) =>
            _informationLoggerFinalizedService(logger, value1, value2, null!);

        /// <summary>
        /// Errors the service log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public static void ErrorServiceLog(ILogger logger, string value1, string value2) =>
            _errorLoggerService(logger, value1, value2, null!);
    }
}
