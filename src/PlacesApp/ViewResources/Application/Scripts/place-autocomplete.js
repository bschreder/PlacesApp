'use strict';
/*
 *  Autocomplete  for Google Places
 */

(function ($) {
    const searchId = '#srch-guid';
    const searchTerm = '#srch-term';
    const ajaxError = '#ajax-errors';
    const searchWidget = "#srch-widget";
    const invalidInput = "#invalid-input";
    const description = "label.result-description";
    const distance = "label.result-distance";
    const id = "label.result-id";
    const operationid = "label.result-operationid";
    const types = "label.result-types";

    const applicationBaseUrl = $(location).attr('protocol') + '//' + $(location).attr('host');
    const path = 'Place';
    const endpoint = 'PlacesAutocomplete';
    const placesUrl = applicationBaseUrl + '/' + path + '/' + endpoint;

    let predictions = {};
    let operationId = '';

    //  Autocomplete search
    $(searchTerm).autocomplete({
        appendTo: searchWidget,
        position: { my: "right+75 top", at: "right bottom", collision: "fit" },
        source: function (request, response) {
            $(ajaxError).hide();
            $(invalidInput).hide();

            let placeRequest = new SearchPlaceRequest({
                address: request.term,
                operationId: uuidv4(),
            });

            $.ajax({
                url: placesUrl,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(placeRequest),
                success: function (data) {
                    response(data);
                }
            }).done(function (data) {
                if (data.Result.Predictions.length === 0) {
                    let msg = 'Invalid input: ' + data.Result.Status;
                    $(invalidInput).html(msg).show();
                }
                if (data.Error.length > 0) {
                    let msg = invalidResult(data);
                    $(ajaxError).html(msg).show();
                }

                predictions = data.Result.Predictions;
                operationId = data.Result.OperationId;

                let b = data.Result.Predictions;
                let items = [];
                b.forEach(e => {
                    let item = [];
                    item.label = e.Description;
                    item.value = e.Description;
                    item.id = e.Id;
                    items.push(item);
                });
                response(items);

            }).fail(function (xhr, status, error) {
                $(searchTerm).removeClass("ui-autocomplete-loading");
                let errorResult = [];
                errorResult.push('status - ' + status + ': statusText ' + xhr.statusText);
                errorResult.push('responseText: ' + xhr.responseText);

                let apiResponse = new ApiResponse(null, errorResult);
                if (apiResponse.Error.length > 0) {
                    let msg = invalidResult(apiResponse);
                    $(ajaxError).html(msg).show();
                }
                response('');
            });
        },
        delay: 250,
        minLength: 2,
        autoFocus: true,
        select: function (event, ui) {
            $(searchTerm).val(ui.item.value);
            $(searchId).text(ui.item.id);

            let prediction = predictions.filter(o => {
                return o.Id === ui.item.id;
            });
            displaySelectionResult(prediction[0]);

            $(searchTerm).val('');
            $(searchId).text('');
            $(searchTerm).removeClass("ui-autocomplete-loading");

            return false;
        }
    });

    //  textarea and magnifying glass click event handler
    $('.pa-search').click(function (event, ui) {
        $(searchTerm).autocomplete("search", $(searchTerm).val());
    });

    //  display autocomplete selections in a row
    function displaySelectionResult(values) {
        $(description).text(values.Description);
        $(distance).text(values.DistanceMeters);
        $(id).text(values.Id.slice(0, 15));
        $(types).text(values.Types.join(', '))
        $(operationid).text(operationId);
    }

    function invalidResult(result) {
        let msg = 'Errors: <br />';
        result.Error.forEach((e) => {
            msg += e.Message + '<br />';
        });
        return msg;
    }

})(jQuery);