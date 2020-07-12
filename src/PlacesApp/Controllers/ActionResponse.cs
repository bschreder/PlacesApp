using Library.Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace PlacesApp.Controllers
{
    /// <summary>
    /// Controller response
    /// </summary>
    /// <typeparam name="T"> object to be return </typeparam>
    /// <remarks> if no object to be returned instantiate with object type </remarks>
    public class ActionResponse<T> : ActionResult
        where T : class, new()
    {
        //**  TODO:   need to add values to headers:  representation, last-modified, ....

        private readonly HttpStatusCode _httpStatusCode = default;
        private readonly string _httpContent = default;


        /// <summary>
        /// Return HttpResponse status and content
        /// </summary>
        /// <param name="result"> body of response </param>
        /// <returns> ActionResult </returns>
        public ActionResponse(BusinessResult<T> result)
        {
            //  generate http status code based on result.Error
            if (result.Error.Count == 0)
            {
                if (result.Result == null || result.Result == default)
                    _httpStatusCode = HttpStatusCode.NoContent;
                else
                    _httpStatusCode = HttpStatusCode.OK;
            }
            else if (result.Error.Any(e => e.ErrorLevel == LogLevel.Critical))
                _httpStatusCode = HttpStatusCode.InternalServerError;
            else
                _httpStatusCode = HttpStatusCode.BadRequest;

            //  generate return content based on result.Result 
            if (result.Result == null)
                result.Result = new T();

            _httpContent = JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// Return HttpResponse status and content
        /// </summary>
        /// <param name="result"> body of response </param>
        /// <returns>  </returns>
        public ActionResponse(BusinessResult result)
        {
            if (result.Error.Count == 0)
                _httpStatusCode = HttpStatusCode.NoContent;
            else if (result.Error.Any(e => e.ErrorLevel == LogLevel.Critical))
                _httpStatusCode = HttpStatusCode.InternalServerError;
            else
                _httpStatusCode = HttpStatusCode.BadRequest;

            _httpContent = JsonConvert.SerializeObject(result);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.HttpContext.Response.ContentType = $"{new MediaTypeHeaderValue("application/json")}";
            context.HttpContext.Response.StatusCode = (int)_httpStatusCode;

            if (!string.IsNullOrEmpty(_httpContent))
                context.HttpContext.Response.Write(_httpContent);
        }
    }
}
