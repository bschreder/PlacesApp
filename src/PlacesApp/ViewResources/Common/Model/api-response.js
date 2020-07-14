'use strict';

//  ApiResponse  object
function ApiResponse (result, error) {
    this.Result = result;
    this.Error = error;
};

Object.defineProperties(ApiResponse, {
    'Result': { value: {},  enumerable: true, writable: true, },
    'Error': { value: [], enumerable: true, writable: true, },
});