using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;
using System;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Configuration Manager, you have to call <c>ConfigurationManager.Create()</c> first.
    /// TODO: maybe there is a better solution to the usage of this
    /// </summary>
    public sealed class ConfigurationManager : ConfigurationManagerBase
    {
        public static ConfigurationManager Instance { get; private set; }

        public static ConfigurationManager Create(IConfigurationStorage<IConfiguration> storage)
        {
            var instance = new ConfigurationManager(storage);
            ConfigurationManager.Instance = instance;
            return Instance;
        }
        private ConfigurationManager(IConfigurationStorage<IConfiguration> storage):base(storage)
        {
           
        }
        public override bool Exist<T>(string name) 
        {
            return Storage.Get<T>(name) != null;
        }
        public override void Save<T>(T instance) 
        {
            Storage.Set(instance);
        }
        public override Task SaveAsync<T>(T instance)
        {
            return Storage.SetAsync(instance);
        }
        public override T Load<T>(string name) 
        {
            var cfg = (T)Storage.Get<T>(name);

            if(cfg==null)
            {
                cfg = Activator.CreateInstance<T>();
            }
            return (T)cfg.Default();
        }

        //TODO: test this?!
        public override Task<T> LoadAsync<T>(string name)
        {
            return (Task<T>)(object)Storage.GetAsync<T>(name);
        }
    }
}