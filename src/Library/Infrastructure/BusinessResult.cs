using Library.Errors;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.Infrastructure
{
    /// <summary>
    /// BusinessResult that returns an object plus errors
    /// </summary>
    /// <typeparam name="TResult"> Result to return </typeparam>
    public class BusinessResult<TResult>
        where TResult : class, new()
    {
        /// <summary>
        /// The result of the operation
        /// </summary>
        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public TResult Result;

        /// <summary>
        /// List of errors from the operation
        /// </summary>
        [JsonProperty(PropertyName = "error", Required = Required.Always)]
        public List<BusinessError> Error = new List<BusinessError>();

        /// <summary>
        /// Casts to dynamic
        /// </summary>
        /// <param name="brT">  </param>
        public static implicit operator BusinessResult<dynamic>(BusinessResult<TResult> brT)
        {
            BusinessResult<dynamic> br = new BusinessResult<dynamic>() { Result = default };
            br.Error.AddRange(brT.Error);
            br.Result = brT.Result;
            return br;
        }

        /// <summary>
        /// Casts from dynamic
        /// </summary>
        /// <param name="brT">  </param>
        public static implicit operator BusinessResult<TResult>(BusinessResult<dynamic> brT)
        {
            BusinessResult<TResult> br = new BusinessResult<TResult>() { Result = new TResult() };
            br.Error.AddRange(brT.Error);
            br.Result = brT.Result as TResult;
            return br;
        }
    }



    /// <summary>
    /// BusinessResult that returns errors
    /// </summary>
    public class BusinessResult
    {
        /// <summary>
        /// List of errors from the operation
        /// </summary>
        [JsonProperty(PropertyName = "error", Required = Required.Always)]
        public List<BusinessError> Error = new List<BusinessError>();

        /// <summary>
        /// Casts from BusinessResult with dynamic result to BusinessResult
        /// </summary>
        /// <param name="brT">  </param>
        public static explicit operator BusinessResult(BusinessResult<dynamic> brT)
        {
            BusinessResult<dynamic> br = new BusinessResult();
            br.Error.AddRange(brT.Error);
            return (BusinessResult)br;
        }

        /// <summary>
        /// Casts to BusinessResult with dynamic result from BusinessResult
        /// </summary>
        /// <param name="brT">  </param>
        public static implicit operator BusinessResult<dynamic>(BusinessResult brT)
        {
            BusinessResult<dynamic> br = new BusinessResult<dynamic>() { Result = default };
            br.Error.AddRange(brT.Error);
            return br;
        }
    }
}
