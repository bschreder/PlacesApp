using APIService.RestApi;
using Library.Infrastructure;
using System.Threading.Tasks;

namespace APIService.Interfaces
{
    public interface IRESTRequest<TResult, TRequest>
        where TResult : class, new()
        where TRequest : class
    {
        Task<BusinessResult<TResult>> Get(RESTRequestParameters parameters, TRequest serviceRequest = null);
        Task<BusinessResult<TResult>> Post(RESTRequestParameters parameters, TRequest serviceRequest);
    }
}