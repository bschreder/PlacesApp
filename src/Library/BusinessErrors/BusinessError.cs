using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Library.BusinessErrors
{
    public class BusinessError
    {
        /// <summary>
        /// Error property
        /// </summary>
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        /// <summary>
        /// ErrorLevel property
        /// </summary>
        [JsonProperty(PropertyName = "errorLevel", Required = Required.Always)]
        public LogLevel ErrorLevel { get; set; }



        /// <summary>
        /// base CTOR
        /// </summary>
        /// <param name="level"></param>
        /// <param name="errorMessage"></param>
        /// <param name="ex"></param>
        /// <param name="operationId"></param>
        public BusinessError(LogLevel level, string errorMessage, Exception ex = null, string operationId = null)
        {
            ErrorLevel = (ex == null) ? level : LogLevel.Critical;
            Message = FormatMessage(level, $"Message: {errorMessage}", ex, operationId);
        }

        /// <summary>
        /// CTOR w/ Location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="level"></param>
        /// <param name="errorMessage"></param>
        /// <param name="ex"></param>
        /// <param name="operationId"></param>
        public BusinessError(string location, LogLevel level, string errorMessage, Exception ex = null, string operationId = null)
        {
            ErrorLevel = (ex == null) ? level : LogLevel.Critical;
            Message = FormatMessage(level, $"Location: {location}\nMessage: {errorMessage}", ex, operationId);
        }

        /// <summary>
        /// Format Error (or Exception) Message
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="level"></param>
        /// <param name="errMsg"></param>
        /// <param name="ex"></param>
        /// <param name="operationId"></param>
        /// <returns></returns>
        private string FormatMessage(LogLevel level, string errorMessage, Exception ex, string operationId = null)
        {
            var rtnVal = (ex != null)
                ? $"Level: {level}, {errorMessage}, OperationId: {operationId} \n Exception Message: {ex.Message} \n {ex.StackTrace}"
                : $"Level: {level}, {errorMessage}, OperationId: {operationId}";
            return rtnVal;
        }
    }
}
