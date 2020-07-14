using System;
using System.Collections.Generic;
using System.Text;

namespace DomainEntities.PlaceSearch
{
    public enum PlaceResponseStatus
    {
        NONE,
        OK,
        ZERO_RESULTS,
        OVER_QUERY_LIMIT,
        REQUEST_DENIED,
        INVALID_REQUEST,
        UNKNOWN_ERROR,
    }
}
