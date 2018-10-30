using System.Threading.Tasks;
using TI.Configuration.Logic.API;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Interface which defines a configuration store
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IConfigurationStorage<T> where T : class, IConfiguration
    {
        ConfigMode Mode { get; }

        T Get<TT>(string name) where TT : class, IConfiguration;
        Task<T> GetAsync<TT>(string name) where TT : class, IConfiguration;

        void Set(T instance); //where T : class, IConfiguration;
        Task SetAsync(T instance); //where T : class, IConfiguration;
    }
}