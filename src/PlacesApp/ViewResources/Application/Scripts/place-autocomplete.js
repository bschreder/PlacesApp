﻿'use strict';
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
                $(searchTerm).removeClass("ui-autocomplete-loading");
                if (/*Array.isArray(data.result.predictions) &&*/ data.result.predictions.length === 0) {
                    let msg = 'Invalid input: ' + data.result.status;
                    $(invalidInput).html(msg).show();
                }
                if (data.result.displayErrors && data.error.length > 0) {
                    let msg = invalidResult(data);
                    $(ajaxError).html(msg).show();
                }

                predictions = data.result.predictions;
                operationId = data.result.operationId;

                let b = data.result.predictions;
                let items = [];
                b.forEach(e => {
                    let item = [];
                    item.label = e.description;
                    item.value = e.description;
                    item.id = e.id;
                    items.push(item);
                });
                response(items);

            }).fail(function (xhr, status, error) {
                $(searchTerm).removeClass("ui-autocomplete-loading");
                let errorResult = [];
                errorResult.push('status - ' + status + ': statusText ' + xhr.statusText);
                errorResult.push('responseText: ' + xhr.responseText);
                $(invalidInput).html('ZERO_RESULTS returned').show();

                let apiResponse = new ApiResponse(null, errorResult);
                if (apiResponse.error.length > 0) {
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
                return o.id === ui.item.id;
            });
            displaySelectionResult(prediction[0]);

            $(searchTerm).val('');
            $(searchId).text('');

            return false;
        }
    });

    //  textarea and magnifying glass click event handler
    $('.pa-search').click(function (event, ui) {
        $(searchTerm).autocomplete("search", $(searchTerm).val());
    });

    //  display autocomplete selections in a row
    function displaySelectionResult(values) {
        $(description).text(values.description);
        $(distance).text(values.distanceMeters);
        $(id).text(values.id.slice(0, 15));
        $(types).text(values.types.join(', '))
        $(operationid).text(operationId);
    }

    function invalidResult(result) {
        let msg = 'Errors: <br />';
        result.error.forEach((e) => {
            msg += JSON.stringify(e) + '<br />';
        });
        return msg;
    }
})(jQuery);