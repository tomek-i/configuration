using TI.Configuration.Logic.API;
using System.Threading.Tasks;

namespace TI.Configuration.Logic
{
    public abstract class ConfigurationManagerBase<T> : IConfigurationManagerBase
        where T : class, IConfiguration
    {

        public ConfigurationStorage<T> Storage { get; }

        public abstract bool Exist<A>(string name) where A : class, IConfiguration;

        public abstract T Load<A>(string name) where A : class, IConfiguration;
        public abstract Task<T> LoadAsync<A>(string name) where A : class, IConfiguration;

        public abstract void Save(T instance) ;
        public abstract Task SaveAsync(T instance);

        protected  ConfigurationManagerBase(ConfigurationStorage<T> storage)
        {
            Storage = storage;
        }
    }
}