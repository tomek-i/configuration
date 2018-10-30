using TI.Configuration.Logic.API;
using System.Threading.Tasks;
using System;

namespace TI.Configuration.Logic
{
    public sealed class ConfigurationManager : ConfigurationManagerBase<IConfiguration>
    {
        public static ConfigurationManager Instance { get; private set; }

        public static ConfigurationManager Create(ConfigurationStorage<IConfiguration> storage)
        {
            var instance = new ConfigurationManager(storage);
            ConfigurationManager.Instance = instance;
            return Instance;
        }
        private ConfigurationManager(ConfigurationStorage<IConfiguration> storage):base(storage)
        {
           
        }
        //TODO: it should return the passed in type and A should be IConfiguration
        public override IConfiguration Load<A>(string name)
        {
            return Storage.Get<A>(name);
        }

        public override bool Exist<A>(string name)
        {
            return Load<A>(name) != null;
        }

        //TODO: it should return the passed in type and A should be IConfiguration
        public override Task<IConfiguration> LoadAsync<A>(string name)
        {
            return Storage.GetAsync<A>(name);
        }

        public override void Save(IConfiguration instance)
        {
            Storage.Set(instance);
        }

        public override Task SaveAsync(IConfiguration instance)
        {
            return Storage.SetAsync(instance);
        }
    }
}