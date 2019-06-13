using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Wraps a file storage around a cache 
    /// </summary>
    public sealed class ConfigurationStorageCache : IConfigurationStorage<IConfiguration>
    {
        private IConfigurationStorage<IConfiguration> Storage;
        private Dictionary<IConfiguration, DateTime> Cache;
        private TimeSpan CacheExpiry;

        public ConfigurationStorageCache(TimeSpan cacheExpiry, IConfigurationStorage<IConfiguration> storage)
        {
            CacheExpiry = cacheExpiry;
            Storage = storage;
            Cache = new Dictionary<IConfiguration, DateTime>();

        }
        public ConfigMode Mode => Storage.Mode;

        public void Set(IConfiguration instance)
        {
            var chached = Cache.Where(x => x.Key == instance).Select(x=>x.Key).SingleOrDefault();
            if(chached!=null)
                Cache.Remove(chached);

            Storage.Set(instance);
            Cache.Add(instance,DateTime.Now);
        }

        public Task SetAsync(IConfiguration instance)
        {
            //TODO: add/replace in cache?
            return Storage.SetAsync(instance);
        }

        IConfiguration IConfigurationStorage<IConfiguration>.Get<TT>(string name)
        {
            var item = Cache.Where(x => x.Key is TT && x.Key.Name == name).SingleOrDefault();

            if (item.Key == null) return null;

            var age = DateTime.Now - item.Value;
            if (age > CacheExpiry)
            {
                Cache.Remove(item.Key);
            }
            else
            {
                return item.Key;
            }

            var cfg = Storage.Get<TT>(name);
            Cache.Add(cfg, DateTime.Now);
            return cfg;
        }

        async Task<IConfiguration> IConfigurationStorage<IConfiguration>.GetAsync<TT>(string name)
        {
            var item = Cache.Where(x => x.Key is TT && x.Key.Name == name).SingleOrDefault();
            var age = DateTime.Now - item.Value;
            if (age > CacheExpiry)
            {
                Cache.Remove(item.Key);
            }
            else
            {
                return item.Key;
            }

            var cfg = await Storage.GetAsync<TT>(name);
            Cache.Add(cfg, DateTime.Now);
            return cfg;
        }
    }
}