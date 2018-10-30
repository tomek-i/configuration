using TI.Configuration.Logic.API;
using System.Threading.Tasks;
using System;

namespace TI.Configuration.Logic
{
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
            return Load<T>(name) != null;
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
            return (T)Storage.Get<T>(name);
        }

        //TODO: test?!
        public override Task<T> LoadAsync<T>(string name)
        {
            return (Task<T>)(object)Storage.GetAsync<T>(name);
        }
    }
}