'use strict';

//  ApiResponse  object
function ApiResponse (result, error) {
    this.result = result;
    this.error = error;
};

Object.defineProperties(ApiResponse, {
    'result': { value: {}, enumerable: true, writable: true, },
    'error': { value: [], enumerable: true, writable: true, },
});