using DomainBusinessLogic.PlaceSearch;
using DomainEntities.Application;
using DomainEntities.PlaceSearch;
using Library.BusinessErrors;
using Library.Infrastructure;
using Microsoft.Extensions.Logging;
using PlacesApp.ViewModel.Place;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlacesApp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ILogger _logger = default;

        public PlaceController()
        {
            //  TODO:   wire in Real Logger
            _logger = new NullLogger();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var response = new BusinessResult<SearchPlacesResponse>() { Result = new SearchPlacesResponse() };
            response.Result.DisplayErrors = Globals.Configuration.DisplayErrors;

            return View(new PlaceIndexViewModel { PlaceIndexResponse = response });
        }

        [HttpPost]
        public async Task<JsonResult> PlacesAutocomplete(SearchPlacesRequest request)
        {
            //var response = new BusinessResult<SearchPlacesResponse>() { Result = new SearchPlacesResponse() };
            BusinessResult<SearchPlacesResponse> response = default;

            request.ApiKey = Globals.Credentials.PlacesApiKey;
            request.PlaceBaseUrl = Globals.Configuration.PlaceBaseUrl;

            using (var httpClient = new HttpClient())
                response = await new PlaceAutocompleteSearchProcessor(httpClient, _logger).ExecuteAsync(request);

            response.Result.DisplayErrors = Globals.Configuration.DisplayErrors;
            return Json(response, JsonRequestBehavior.AllowGet);
        }



    }
}
