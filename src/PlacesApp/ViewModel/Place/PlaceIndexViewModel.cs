using DomainEntities.PlaceSearch;
using Library.BusinessErrors;
using Library.Infrastructure;
using PlacesApp.ViewModel.Common;
using System.Collections.Generic;

namespace PlacesApp.ViewModel.Place
{
    public class PlaceIndexViewModel
    {
        public BusinessResult<SearchPlacesResponse> PlaceIndexResponse { get; set; }

        public ConstantsViewModel Constants => new ConstantsViewModel();

        //  Errors
        private bool _showErrors => PlaceIndexResponse.Result.DisplayErrors ?? false;
        public bool DisplayErrors => _showErrors && PlaceIndexResponse.Error?.Count != 0;
        public List<BusinessError> ErrorList => PlaceIndexResponse.Error;



    }
}