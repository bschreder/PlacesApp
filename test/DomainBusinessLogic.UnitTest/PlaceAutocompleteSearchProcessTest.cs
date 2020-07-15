using APIService.RestApi;
using DomainBusinessLogic.PlaceSearch;
using DomainEntities.Application;
using DomainEntities.Place;
using DomainEntities.PlaceSearch;
using Library.BusinessErrors;
using Library.Infrastructure;
using LibraryTest;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DomainBusinessLogic.UnitTest
{
    public class PlaceAutocompleteSearchProcessTest : IDisposable
    {
        private readonly ITestOutputHelper _output = default;
        private readonly HttpClient _httpClient = default;
        private readonly RESTRequest<PlacesResponse, Dictionary<string, string>> _restService = default;
        private readonly ILogger _logger = default;
        private readonly OutputClass _testOutputClass = default;

        private const string _credentialFile = "credentials.json";
        private const string _baseUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";

        public PlaceAutocompleteSearchProcessTest(ITestOutputHelper output)
        {
            _output = output;
            _logger = new NullLogger();
            _testOutputClass = new OutputClass { Message = new List<string> { "test output" } };

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(_testOutputClass)) };
            var httpMessageHanderStub = new HttpMessageHandlerStub((request, cancellationToken) => Task.FromResult(httpResponseMessage));
            _httpClient = new HttpClient(httpMessageHanderStub);
            _restService = new RESTRequest<PlacesResponse, Dictionary<string, string>>(_httpClient);

            var jsonFileHandler = new JsonFileHandler();
            Globals.Credentials = jsonFileHandler.ReadJson<Credentials>(_credentialFile);
        }

        [Fact]
        public async Task PlaceAutocompleteSearchProcessTest_WithRequiredValues()
        {
            var httpClient = new HttpClient();
            var restService = new RESTRequest<PlacesResponse, Dictionary<string, string>>(httpClient);
            BusinessResult<SearchPlacesResponse> result = default;

            try
            {
                var request = new SearchPlacesRequest()
                {
                    ApiKey = Globals.Credentials.PlacesApiKey,
                    //Address = "1600 Amphitheatre Pkwy, Mountain View, CA 94043",
                    Address = "1600 Amphitheatre",
                    PlaceBaseUrl = _baseUrl,
                    OperationId = $"{Guid.NewGuid()}",
                    CancellationToken = CancellationToken.None,
                };

                var searchProcessor = new PlaceAutocompleteSearchProcessor(restService, _logger);
                result = await searchProcessor.ExecuteAsync(request);
                _output.WriteLine(JsonConvert.SerializeObject(result));

                Assert.NotNull(result);
                Assert.NotNull(result.Result);
                Assert.IsType<SearchPlacesResponse>(result.Result);
                Assert.Empty(result.Error);
            }
            catch (Exception ex)
            {
                result.Error.Add(new BusinessError(LogLevel.Critical, "test exception", ex, result?.Result?.OperationId));
                _output.WriteLine($"{result.Error}");
                Assert.True(false);
            }
        }

        [Fact]
        public async Task PlaceAutocompleteSearchProcessTest_WithoutHttpClient()
        {
            BusinessResult<SearchPlacesResponse> result = default;

            var request = new SearchPlacesRequest()
            {
                ApiKey = Globals.Credentials.PlacesApiKey,
                Address = "1600 Amphitheatre",
                PlaceBaseUrl = _baseUrl,
                OperationId = $"{Guid.NewGuid()}",
                CancellationToken = CancellationToken.None,
            };

            var searchProcessor = new PlaceAutocompleteSearchProcessor(null, _logger);
            result = await searchProcessor.ExecuteAsync(request);
            _output.WriteLine(JsonConvert.SerializeObject(result));

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task PlaceAutocompleteSearchProcessTest_WithoutAddress()
        {
            BusinessResult<SearchPlacesResponse> result = default;

            var request = new SearchPlacesRequest()
            {
                ApiKey = Globals.Credentials.PlacesApiKey,
                //Address = "1600 Amphitheatre",
                PlaceBaseUrl = _baseUrl,
                OperationId = $"{Guid.NewGuid()}",
                CancellationToken = CancellationToken.None,
            };

            var searchProcessor = new PlaceAutocompleteSearchProcessor(_restService, _logger);
            result = await searchProcessor.ExecuteAsync(request);
            _output.WriteLine(JsonConvert.SerializeObject(result));

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Contains(result.Error, e => e.Message.Contains("search address"));
        }

        [Fact]
        public async Task PlaceAutocompleteSearchProcessTest_WithOutApiKey()
        {
            BusinessResult<SearchPlacesResponse> result = default;

            var request = new SearchPlacesRequest()
            {
                //ApiKey = "SWdaAjDTZ",
                Address = "1600 Amphitheatre",
                PlaceBaseUrl = _baseUrl,
                OperationId = $"{Guid.NewGuid()}",
                CancellationToken = CancellationToken.None,
            };

            var searchProcessor = new PlaceAutocompleteSearchProcessor(_restService, _logger);
            result = await searchProcessor.ExecuteAsync(request);
            _output.WriteLine(JsonConvert.SerializeObject(result));

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Contains(result.Error, e => e.Message.Contains("Invalid ApiKey"));
        }

        [Fact]
        public async Task PlaceAutocompleteSearchProcessTest_WithBadApiKey()
        {
            BusinessResult<SearchPlacesResponse> result = default;

            var request = new SearchPlacesRequest()
            {
                ApiKey = "SWdaAjDTZ",
                Address = "1600 Amphitheatre",
                PlaceBaseUrl = _baseUrl,
                OperationId = $"{Guid.NewGuid()}",
                CancellationToken = CancellationToken.None,
            };

            var searchProcessor = new PlaceAutocompleteSearchProcessor(_restService, _logger);
            result = await searchProcessor.ExecuteAsync(request);
            _output.WriteLine(JsonConvert.SerializeObject(result));

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Contains(result.Error, e => e.Message.Contains("Error"));
        }

        private bool isDisposed = false;
        public void Dispose()
        {
            if (!isDisposed)
                _httpClient.Dispose();
            isDisposed = true;
        }
    }
}
