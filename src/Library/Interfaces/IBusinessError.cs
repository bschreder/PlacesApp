using Microsoft.Extensions.Logging;

namespace Library.Interfaces
{
    public class IBusinessError
    {
        string Message { get; }
        LogLevel ErrorLevel { get; set; }
    }
}
