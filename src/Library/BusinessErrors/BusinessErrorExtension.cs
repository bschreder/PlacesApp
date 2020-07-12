using Library.BusinessErrors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common_Infrastructure.BusinessErrors
{
    /// <summary>
    /// Extension method for BusinessError
    /// </summary>
    public static class BusinessErrorExtension
    {
        /// <summary>
        /// Write BusinessErrors to ILogger
        /// </summary>
        /// <param name="errors"> BusinessError </param>
        /// <param name="logger"> ILogger </param>
        /// <param name="operationId"> unique operation identifier </param>
        public static void ErrorLogger(this IEnumerable<BusinessError> errors, ILogger logger)
        {
            if (errors.Count() == 0)
                return;

            foreach (var error in errors)
                logger.Log(error.ErrorLevel, error.Message);
        }

        /// <summary>
        /// Throw exception with the aggregated errors
        /// </summary>
        /// <param name="errors"> BusinessError </param>
        /// <param name="logger"> ILogger </param>
        /// <param name="operationId"> unique operation identifier </param>
        public static void ThrowException(this IEnumerable<BusinessError> errors, ILogger logger = default)
        {
            if (errors.Count() == 0)
                return;

            if (logger != null)
                errors.ErrorLogger(logger);

            var sb = new StringBuilder();
            foreach (var error in errors)
                sb.AppendLine(error.Message);

            throw new Exception($"{sb}");
        }
    }

}
