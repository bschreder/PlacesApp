using APIService.RestApi;
using DomainEntities.Place;
using DomainEntities.PlaceSearch;
using Library.BusinessErrors;
using Library.Infrastructure;
using Library.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DomainBusinessLogic.PlaceSearch
{
    public class PlaceAutocompleteSearchProcessor : IBusinessProcessor<BusinessResult<SearchPlacesResponse>, SearchPlacesRequest>
    {
        private readonly HttpClient _httpClient = default;
        private readonly ILogger _logger = default;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public PlaceAutocompleteSearchProcessor(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Place Autocomplete search request and response processor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BusinessResult<SearchPlacesResponse>> ExecuteAsync(SearchPlacesRequest request)
        {
            var result = new BusinessResult<SearchPlacesResponse>() { Result = new SearchPlacesResponse() };

            if (request == null)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.ExecuteAsync", LogLevel.Error,
                        "Invalid request: request object is null", null, null));
                return result;
            }

            if (request.Address == null)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.ExecuteAsync", LogLevel.Error,
                        "Invalid request:  search address  is invalid", null, request?.OperationId));
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.OperationId))
                request.OperationId = $"{Guid.NewGuid()}";

            try
            {
                //  generate request
                var restServiceParameters = new RESTRequestParameters()
                {
                    Url = request.PlaceBaseUrl,
                    CancellationToken = request.CancellationToken,
                };
                var restRequest = CreateRequestDictionary(request);

                //  make service request
                var restService = await new RESTRequest<PlacesResponse, Dictionary<string, string>>(_httpClient).Get(restServiceParameters, restRequest.Result);
                result.Error.AddRange(restService.Error);

                //  check response status
                if (!string.IsNullOrEmpty(restService.Result?.Error_message))
                    result.Error.Add(new BusinessError(LogLevel.Error, restService.Result.Error_message, null, request.OperationId));
                if (!restService.Result?.Status.Equals($"{PlaceResponseStatus.OK}") ?? false)
                    result.Error.Add(new BusinessError(LogLevel.Error, $"Reponse Status: {restService.Result.Status}", null, request.OperationId));

                if (result.Error.Count == 0)
                {
                    //  process response
                    result.Result.Status = restService.Result.Status;
                    result.Result.SessionToken = request.SessionToken;
                    result.Result.OperationId = string.IsNullOrEmpty(result.Result.OperationId)
                                                    ? request.OperationId
                                                    : result.Result.OperationId;

                    result.Result.Predictions = new List<SearchPlacePrediction>();
                    foreach (var predicition in restService.Result.Predictions)
                    {
                        var searchPlacePredicition = new SearchPlacePrediction()
                        {
                            Description = predicition.Description,
                            Id = predicition.Id,
                            PlaceId = predicition.Place_id,
                            Reference = predicition.Reference,
                            Types = predicition.Types.ToList(),
                            DistanceMeters = predicition.Distance_meters,
                        };
                        result.Result.Predictions.Add(searchPlacePredicition);
                    }
                }

            }
            catch (Exception ex)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.CreateRequestDictionary", LogLevel.Critical, "Exception found", ex, request.OperationId));
            }

                result.Error.ErrorLogger(_logger);
            return result;
        }


        //  TODO:   use reflection
        private BusinessResult<Dictionary<string, string>> CreateRequestDictionary(SearchPlacesRequest request)
        {
            var result = new BusinessResult<Dictionary<string, string>>() { Result = new Dictionary<string, string>()};

            try
            {
                if (!string.IsNullOrEmpty(request.Address))
                    result.Result.Add("input", request.Address);
                else
                    result.Error.Add(new BusinessError(LogLevel.Error, "request is missing an address", null, request.OperationId));

                if (!string.IsNullOrEmpty(request.ApiKey))
                    result.Result.Add("key", request.ApiKey);
                else
                    result.Error.Add(new BusinessError(LogLevel.Error, "missing api key", null, request.OperationId));

                if (!string.IsNullOrEmpty(request.SessionToken))
                    result.Result.Add("sessiontoken", request.SessionToken);

                if (request.ComponentContries?.Components.Count > 0)
                {
                    var kvp = request.ComponentContries.ToKeyValuePair();
                    result.Result.Add(kvp.Key, kvp.Value);
                }

                if (request.Offset != null)
                    result.Result.Add("offset", $"{request.Offset}");

                if (request.OriginPoint != null)
                {
                    var kvp = request.OriginPoint.ToKeyValuePair();
                    result.Result.Add(kvp.Key, kvp.Value);
                }

                if (!string.IsNullOrEmpty(request.Language))
                    result.Result.Add("language", request.Language);

                if (request.Location != null)
                {
                    var kvp = request.Location.ToKeyValuePair();
                    result.Result.Add(kvp.Key, kvp.Value);
                }

                if (request.Radius != null)
                    result.Result.Add("radius", $"{request.Radius}");

                if (request.Strictbounds != null)
                    result.Result.Add("strictbounds", $"{request.Strictbounds}");

                if (request.Types != TypeOfResults.None)
                    result.Result.Add("types", $"{request.Types}");

            }
            catch (Exception ex)
            {
                result.Error.Add(new BusinessError($"{this.GetType().FullName}.CreateRequestDictionary", LogLevel.Critical, "Exception found", ex, request.OperationId));
            }

            return result;
        }
    }
}
