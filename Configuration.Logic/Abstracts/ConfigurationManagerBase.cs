using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;

namespace TI.Configuration.Logic
{

    /// <summary>
    /// Abstract definition of a Configuration Manager base class
    /// </summary>
    public abstract class ConfigurationManagerBase : IConfigurationManagerBase
    {
        /// <summary>
        /// Storage for configurations
        /// </summary>
        public IConfigurationStorage<IConfiguration> Storage { get; }

        public abstract bool Exist<T>(string name) where T : class, IConfiguration;

        public abstract T Load<T>(string name) where T : class, IConfiguration;
        public abstract Task<T> LoadAsync<T>(string name) where T : class, IConfiguration;

        public abstract void Save<T>(T instance) where T : class, IConfiguration;
        public abstract Task SaveAsync<T>(T instance) where T : class, IConfiguration;

        protected ConfigurationManagerBase(IConfigurationStorage<IConfiguration> storage)
        {
            Storage = storage;
        }
    }
}