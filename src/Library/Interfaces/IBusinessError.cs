using Microsoft.Extensions.Logging;

namespace Library.Interfaces
{
    public class IBusinessError
    {
        string Error { get; }
        LogLevel ErrorLevel { get; set; }
    }
}
