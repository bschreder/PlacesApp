'use strict';

function SearchPlaceRequest(options) {
    let props = options || {};

    this.address = props.address || null;
    this.operationId = props.operationId || null;

    this.componentContries = props.componentContries || null;
    this.offset = props.offset || null;
    this.originPoint = props.originPoint || null;
    this.language = props.language || null;
    this.radius = props.radius || null;
    this.sessiontoken = props.sessiontoken || null;
    this.types = props.types || null;
};

Object.defineProperties(SearchPlaceRequest, {
    'address': { enumerable: true, writable: true, },
    'operationId': { enumerable: true, writable: true, },

    'componentContries': { value: {}, enumerable: true, writable: true, },
    'offset': { enumerable: true, writable: true, },
    'originPoint': { value: {}, enumerable: true, writable: true, },
    'language': { enumerable: true, writable: true, },
    'radius': { enumerable: true, writable: true, },
    'sessiontoken': { enumerable: true, writable: true, },
    'types': { enumerable: true, writable: true, },
});