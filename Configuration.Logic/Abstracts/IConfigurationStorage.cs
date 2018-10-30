using System.Threading.Tasks;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic
{
    public interface IConfigurationStorage<T>
        
    {
        ConfigMode Mode { get; }

        T Get<TT>(string name) where TT : class, IConfiguration;
        Task<T> GetAsync<TT>(string name) where TT : class, IConfiguration;

        void Set(T instance); //where T : class, IConfiguration;
        Task SetAsync(T instance); //where T : class, IConfiguration;
    }
}