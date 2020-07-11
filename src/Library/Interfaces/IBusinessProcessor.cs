using System.Threading.Tasks;

namespace Library.Interfaces
{
    /// <summary>
    /// Interface between controller and business processor
    /// </summary>
    /// <typeparam name="TResult"> return object </typeparam>
    /// <typeparam name="TInput"> input object </typeparam>
    public interface IBusinessProcessor<TResult, TInput>
    where TResult : class, new()
    where TInput : class
    {
        Task<TResult> ExecuteAsync(TInput input);
    }


    /// <summary>
    /// Interface between controller and business processor
    /// </summary>
    /// <typeparam name="TResult"> return object </typeparam>
    public interface IBusinessProcessor<TResult> where TResult : class, new()
    {
        Task<TResult> ExecuteAsync();
    }

    /// <summary>
    /// Interface between controller and business processor
    /// </summary>
    public interface IBusinessProcessor
    {
        Task ExecuteAsync();
    }
}
