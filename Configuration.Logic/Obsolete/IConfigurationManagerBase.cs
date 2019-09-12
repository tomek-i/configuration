using System.Threading.Tasks;
using TI.Configuration.Logic.Interfaces;

namespace TI.Configuration.Obsolete
{
    public interface IConfigurationManagerBase
    {
        IConfigurationStorage<IConfiguration> Storage { get; }

        bool Exist<T>(string name) where T : class, IConfiguration;
        T Load<T>(string name) where T : class, IConfiguration;
        Task<T> LoadAsync<T>(string name) where T : class, IConfiguration;
        void Save<T>(T instance) where T : class, IConfiguration;
        Task SaveAsync<T>(T instance) where T : class, IConfiguration;
    }
}
