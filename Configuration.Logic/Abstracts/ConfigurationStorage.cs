using TI.Configuration.Logic.API;
using System.Threading.Tasks;

namespace TI.Configuration.Logic
{
    public abstract class ConfigurationStorage<T> where T : class, IConfiguration
    {
        public ConfigMode Mode { get;}

        protected ConfigurationStorage(ConfigMode mode)
        {
            Mode = mode;
        }
        public abstract Task<T> GetAsync<A>(string name) where A : class, IConfiguration;
        public abstract T Get<A>(string name) where A : class, IConfiguration;
        public abstract Task SetAsync(T instance);
        public abstract void Set(T instance);
    }
}