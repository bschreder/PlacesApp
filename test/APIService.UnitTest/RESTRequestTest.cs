using APIService.RestApi;
using APIService.UnitTest.Mock;
using LibraryTest;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace APIService.UnitTest
{
    public class RESTRequestTest : IDisposable
    {
        private readonly ITestOutputHelper _output = default;

        private readonly HttpClient _httpClient = default;
        private readonly RESTRequest<OutputClass, InputClass> _restRequest = default;
        private readonly InputClass _testInputClass = default;
        private readonly OutputClass _testOutputClass = default;

        public RESTRequestTest(ITestOutputHelper output)
        {
            _output = output;

            _testInputClass = new InputClass() { Message = "test input" };
            _testOutputClass = new OutputClass() { Message = "test output" };

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_testOutputClass)) };
            var httpMessageHanderStub = new HttpMessageHandlerStub((request, cancellationToken) => Task.FromResult(httpResponseMessage));
            _httpClient = new HttpClient(httpMessageHanderStub);
            _restRequest = new RESTRequest<OutputClass, InputClass>(_httpClient);
        }

        [Fact]
        public async Task RESTRequestTest_GetNullServiceParams()
        {
            RESTRequestParameters parameters = null;

            var response = await _restRequest.Get(parameters);

            Assert.NotEmpty(response.Error);
            Assert.Contains(response.Error, e => e.Message.Contains("Service request parameters are null"));
            Assert.Contains(response.Error, e => e.ErrorLevel.Equals(LogLevel.Error));
        }

        [Fact]
        public async Task RESTRequestTest_GetRequest()
        {
            var headers = new Dictionary<string, string>() { { "OperationId", $"{Guid.NewGuid()}" } };
            var parameters = new RESTRequestParameters() { Url = "https://example.com", Headers = headers };

            var response = await _restRequest.Get(parameters);

            Assert.NotNull(response.Result);
            Assert.Empty(response.Error);
            Assert.Equal(_testOutputClass.Message, response.Result.Message);
        }

        [Fact]
        public async Task RESTRequestTest_GetRequestWithQueryString()
        {
            var headers = new Dictionary<string, string>() { { "OperationId", $"{Guid.NewGuid()}" } };
            var parameters = new RESTRequestParameters() { Url = "https://example.com", Headers = headers };
            var query = new Dictionary<string, string>() {
                { "input", "1234 street, city, state 12345" },
                { "key", "AFB139F" },
                { "sessiontoken", "12345"}
            };

            var response = await new RESTRequest<OutputClass, Dictionary<string, string>>(_httpClient).Get(parameters, query);

            Assert.NotNull(response.Result);
            Assert.Empty(response.Error);
            Assert.Equal(_testOutputClass.Message, response.Result.Message);
        }

        [Fact]
        public async Task RESTRequestTest_PostNullServiceParams()
        {
            RESTRequestParameters parameters = null;

            var response = await _restRequest.Post(parameters, null);

            Assert.NotEmpty(response.Error);
            Assert.Contains(response.Error, e => e.Message.Contains("Service request parameters are null"));
            Assert.Contains(response.Error, e => e.ErrorLevel.Equals(LogLevel.Error));
        }

        [Fact]
        public async Task RESTRequestTest_PostNullRequest()
        {
            var parameters = new RESTRequestParameters() { Url = "https://example.com" };

            var response = await _restRequest.Post(parameters, null);

            Assert.NotEmpty(response.Error);
            Assert.Contains(response.Error, e => e.Message.Contains("Invalid request object"));
            Assert.Contains(response.Error, e => e.ErrorLevel.Equals(LogLevel.Error));
        }

        [Fact]
        public async Task RESTRequestTest_PostRequest()
        {
            var headers = new Dictionary<string, string>() { { "OperationId", $"{Guid.NewGuid()}" } };
            var parameters = new RESTRequestParameters() { Url = "https://example.com", Headers = headers };
            var request = new InputClass() { Message = "test" };

            var response = await _restRequest.Post(parameters, request);

            Assert.NotNull(response.Result);
            Assert.Empty(response.Error);
            Assert.Equal(_testOutputClass.Message, response.Result.Message);
        }


        //  Dispose
        private bool isDisposed = false;
        public void Dispose()
        {
            if (!isDisposed)
                _httpClient.Dispose();
            isDisposed = true;
        }
    }
}
