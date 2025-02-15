using System.Threading.Tasks;

namespace AspNetApi.Interfaces
{
    public interface IService<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    public interface IService<TResponse>
    {
        Task<TResponse> Handle();
    }
}
