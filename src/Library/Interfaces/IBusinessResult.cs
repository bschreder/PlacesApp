using Library.BusinessErrors;
using System.Collections.Generic;

namespace Library.Interfaces
{

    public interface IBusinessResult<TResult> : IBusinessResult where TResult : class
    {
        TResult Result { get; set; }
    }

    public interface IBusinessResult
    {
        List<BusinessError> Error { get; set; }


    }
}
