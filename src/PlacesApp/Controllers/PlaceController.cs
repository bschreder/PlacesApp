using APIService.RestApi;
using DomainBusinessLogic.PlaceSearch;
using DomainEntities.Application;
using DomainEntities.Place;
using DomainEntities.PlaceSearch;
using Library.BusinessErrors;
using Library.Infrastructure;
using Microsoft.Extensions.Logging;
using PlacesApp.ViewModel.Place;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlacesApp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ILogger _logger = default;
        private bool _displayErrors = default;

        public PlaceController()
        {
            //  TODO:   wire in Real Logger
            _logger = new NullLogger();
            _displayErrors = Globals.Configuration.DisplayErrors ?? false;

        }

        /// <summary>
        /// Place Webpage Index 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var response = new BusinessResult<SearchPlacesResponse>() { Result = new SearchPlacesResponse() };
            response.Result.DisplayErrors = Globals.Configuration.DisplayErrors;

            return View(new PlaceIndexViewModel { PlaceIndexResponse = response });
        }

        /// <summary>
        /// Place Autocomplete Endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PlacesAutocomplete(SearchPlacesRequest request)
        {
            BusinessResult<SearchPlacesResponse> response = default;

            request.ApiKey = Globals.Credentials?.PlacesApiKey;
            request.PlaceBaseUrl = Globals.Configuration?.PlaceBaseUrl;

            using (var httpClient = new HttpClient())
            {
                var restService = new RESTRequest<PlacesResponse, Dictionary<string, string>>(httpClient);
                response = await new PlaceAutocompleteSearchProcessor(restService, _logger).ExecuteAsync(request);
            }

            return  new ActionResponse<SearchPlacesResponse>(response, _displayErrors, _logger);
        }
    }
}
