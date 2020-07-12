using APIService.Interfaces;
using Library.BusinessErrors;
using Library.Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APIService.RestApi
{
    public class RESTRequest<TResult, TRequest> : IRESTRequest<TResult, TRequest>
        where TResult : class, new()
        where TRequest : class
    {
        private readonly HttpClient _httpClient;

        public RESTRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BusinessResult<TResult>> Get(RESTRequestParameters parameters, TRequest serviceRequest = null)
        {
            var result = new BusinessResult<TResult>();

            if (parameters == null)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.Get", LogLevel.Error, $"Service request parameters are null; it must be of type ApiServiceRequestParameters"));
                return result;
            }

            if (string.IsNullOrEmpty(parameters.Url) || !Uri.IsWellFormedUriString(parameters.Url, UriKind.Absolute))
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.Get", LogLevel.Error, $"Invalid request Url:  {parameters?.Url} is not provided or malformed"));
                return result;
            }

            //  if TRequest is a dictionary then assume it contains query string values
            if ((serviceRequest is Dictionary<string, string> requestParameters) && (!parameters.Url.Contains("?")))
            {
                var queryString = string.Join("&", requestParameters.Select(kvp => kvp.Key + '=' + kvp.Value));
                var queryEncodedString = HttpUtility.UrlEncode(queryString, Encoding.UTF8);
                parameters.Url = string.Concat(parameters.Url, "?", queryEncodedString);
            }

            var serviceResult = await ExecuteRequestAsync(HttpMethod.Get, parameters, null);

            result.Error.AddRange(serviceResult.Error);
            result.Result = serviceResult.Result;
            return result;
        }

        public async Task<BusinessResult<TResult>> Post(RESTRequestParameters parameters, TRequest serviceRequest)
        {
            var result = new BusinessResult<TResult>();

            if (parameters == null)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.Post", LogLevel.Error, $"Service request parameters are null; it must be of type ApiServiceRequestParameters"));
                return result;
            }

            if (string.IsNullOrEmpty(parameters.Url) || !Uri.IsWellFormedUriString(parameters.Url, UriKind.Absolute))
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.Post", LogLevel.Error, $"Invalid request Url:  {parameters?.Url} is not provided or malformed"));
                return result;
            }

            if (serviceRequest == null)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.Post", LogLevel.Error, $"Invalid request object:  TRequest isn't of type {typeof(TRequest).Name}"));
                return result;
            }

            var serviceResult = await ExecuteRequestAsync(HttpMethod.Post, parameters, serviceRequest);

            result.Error.AddRange(serviceResult.Error);
            result.Result = serviceResult.Result;
            return result;
        }

        private async Task<BusinessResult<TResult>> ExecuteRequestAsync(HttpMethod httpMethod, RESTRequestParameters parameters, TRequest request)
        {
            var result = new BusinessResult<TResult>();
            HttpRequestMessage httpRequest;

            try
            {
                // Content
                if (httpMethod == HttpMethod.Get)
                    httpRequest = new HttpRequestMessage(httpMethod, parameters.Url);
                else if (httpMethod == HttpMethod.Post)
                    httpRequest = new HttpRequestMessage(httpMethod, parameters.Url)
                    { Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json") };
                else
                {
                    result.Error.Add(new BusinessError($"{this.GetType().FullName}.{httpMethod.Method}", LogLevel.Error, $"Invalid HttpMethod (HttpMethod.{httpMethod.Method})"));
                    return result;
                }

                // Headers
                if (parameters.Headers != null)
                {
                    foreach (KeyValuePair<string, string> kvp in parameters.Headers)
                        httpRequest.Headers.Add(kvp.Key, kvp.Value);
                }

                // Response
                HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    string errMsg = $"{httpMethod.Method} Request:  status code {(int)httpResponse.StatusCode}, reason {httpResponse.ReasonPhrase}";
                    result.Error.Add(new BusinessError($"{this.GetType().FullName}.{httpMethod.Method}", LogLevel.Error, errMsg));
                    return result;
                }

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<TResult>(response);
                }
            }
            catch (Exception ex)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.{httpMethod.Method}", LogLevel.Critical, $"{ex.Message}", ex));
            }

            return result;
        }
    }
}
