using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryTest
{
    /// <summary>
    /// Stubbed HttpMessageHandler used to support testing of HttpClient services
    /// </summary>
    public class HttpMessageHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        /// <summary>
        /// Default CTOR - doesn't return any content only a HttpStatus of '200'
        /// </summary>
        public HttpMessageHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        /// <summary>
        /// CTOR - takes HttpStatusCode that should be returned (used for error testing)
        /// </summary>
        /// <param name="httpStatusCode"> HttpStatusCode enum to be returned </param>
        public HttpMessageHandlerStub(HttpStatusCode httpStatusCode)
        {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(httpStatusCode));
        }

        /// <summary>
        /// CTOR - takes delegate
        /// </summary>
        /// <param name="handlerFunc"> 
        /// Delegate that takes a (request, cancellationToken) =&gt; HttpResponseMessage
        /// </param>
        /// <remarks> For an example, see ExscribeEhrApiServiceUnitTests CTOR </remarks>
        public HttpMessageHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        /// <summary>
        /// Faked handler SendAsync method
        /// </summary>
        /// <param name="request"> http message request </param>
        /// <param name="cancellationToken"> cancellation token </param>
        /// <returns> a valid HttpResponseMessage defined in the CTOR </returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}
