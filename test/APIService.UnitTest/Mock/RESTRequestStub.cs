using APIService.Interfaces;
using APIService.RestApi;
using Library.Infrastructure;
using System.Threading.Tasks;

namespace APIService.UnitTest.Mock
{
    public class RESTRequestStub<TResult, TRequest> : IRESTRequest<TResult, TRequest>
        where TResult : class, new()
        where TRequest : class
    {
        public Task<BusinessResult<TResult>> Get(RESTRequestParameters parameters, TRequest serviceRequest)
        {
            return null;
        }

        public Task<BusinessResult<TResult>> Post(RESTRequestParameters parameters, TRequest serviceRequest)
        {
            return null;
        }
    }
}
