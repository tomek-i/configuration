using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Wraps a storage around a cache 
    /// </summary>
    public sealed class ConfigurationStorageCache : IConfigStorage
    {
        private IConfigStorage Storage;
        private Dictionary<IConfiguration, DateTime> Cache;
        private TimeSpan CacheExpiry;

        public ConfigurationStorageCache(TimeSpan cacheExpiry, IConfigStorage storage)
        {
            CacheExpiry = cacheExpiry;
            Storage = storage;
            Cache = new Dictionary<IConfiguration, DateTime>();

        }
        public void Flush()
        {
            var expired = Cache.Where(x => DateTime.Now - x.Value > CacheExpiry).Select(x => x.Key);
            foreach (var item in expired)
            {
                Cache.Remove(item);
            }
        }
        private bool IsItemExpired(KeyValuePair<IConfiguration,DateTime> item)
        {
            return IsItemExpired(item.Value);
        }
        private bool IsItemExpired(DateTime item)
        {
            var age = DateTime.Now - item;
            return age > CacheExpiry;
        }

        public T Get<T>(string name) where T : class,IConfiguration
        {
            IConfiguration config = null;
            //check cache
            var item = Cache.Where(x => x.Key is T && x.Key.Name == name).SingleOrDefault();
            if (item.Key == null)
            {
                // did not exist in cache
                // read of storage
                config = Storage.Get<T>(name);
                //add to cache
                Add(config);
            }
            else
            {
                //existed in cache, check expiry
                
                if (IsItemExpired(item))
                {
                    
                    //remove expired item
                    Cache.Remove(item.Key);
                    //read of store
                    config = Storage.Get<T>(item.Key.Name);
                    //add to cache
                    Add(config);
                    //Cache.Add(config, DateTime.Now);
                }
                else
                {
                    config = item.Key;
                }
            }
            return (T)config;
        }
        public void Add<T>(T instance) where T : IConfiguration
        {
            Add(instance, DateTime.Now);
        }
        public void Add<T>(T instance, DateTime datetime) where T : IConfiguration
        {
            Cache.Add(instance, DateTime.Now);
        }

        public void Set<T>(T instance) where T : class,IConfiguration
        {
            var chached = Cache.Where(x => x.Key is T && x.Key.Name == instance.Name).Select(x => x.Key).SingleOrDefault();
            if (chached != null)
                Cache.Remove(chached);

            Storage.Set(instance);
            Add(instance);
        }
    }
}