using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Library.Errors
{
    public class BusinessException : Exception
    {
        [JsonProperty(PropertyName = "error", Required = Required.Always)]
        public BusinessError Error { get; private set; }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="errorMessage">  The message. </param>
        /// <param name="ex">            Actual exception. </param>
        /// <param name="operationId">   The unique operation identifier. </param>
        public BusinessException(string errorMessage, Exception ex, string operationId = null) : base(errorMessage, ex)
        {
            Error = new BusinessError( LogLevel.Critical, errorMessage, ex, operationId);
        }

    }
}
