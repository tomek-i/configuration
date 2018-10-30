using TI.Configuration.Logic.API;
using System.Threading.Tasks;

namespace TI.Configuration.Logic
{
    public abstract class ConfigurationStorage<T> : IConfigurationStorage<T> where T : class, IConfiguration
    {
        public ConfigMode Mode { get; }

        protected ConfigurationStorage(ConfigMode mode)
        {
            Mode = mode;
        }

        public abstract Task<T> GetAsync<TT>(string name) where TT : class, IConfiguration;
        public abstract T Get<TT>(string name) where TT : class, IConfiguration;
        public abstract Task SetAsync(T instance);// where T : class, IConfiguration;
        public abstract void Set(T instance);// where T : class, IConfiguration;
    }
}