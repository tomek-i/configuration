using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Interfaces;

namespace Configuration.SQL
{
   
  
    public sealed class ConfigurationSQLStorage<T> : ConfigurationStorage<T> where T: class,IConfiguration
    {
        

        private ConfigurationContext _context;

        public ConfigurationSQLStorage(ConfigurationContext context,ConfigMode mode):base(mode)
        {
            
            _context = context;
        }


        public override T Get<TT>(string name)
        {
            var instance = _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Name == name).SingleOrDefault();
            if (instance == null)
            {
                instance = new SQLAppConfig(name);
                _context.AppConfigs.Add(instance);
            }
            return (T)(IConfiguration)instance;
        }

        public override async Task<T> GetAsync<TT>(string name)
        {
            
            var instance = await _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Name == name).SingleOrDefaultAsync();
            if (instance == null)
            {
                instance = new SQLAppConfig(name);
                _context.AppConfigs.Add(instance);
            }
            return (T)(IConfiguration)instance;
        }
        public override void Set(T instance)
        {
            if (!_context.AppConfigs.Any(x => x.Name == instance.Name))
            {
                _context.AppConfigs.Add((SQLAppConfig)(object)instance);
            }
            _context.SaveChanges();
        }

        public override Task SetAsync(T instance)
        {
            if (!_context.AppConfigs.Any(x => x.Name == instance.Name))
            {
                _context.AppConfigs.Add((SQLAppConfig)(object)instance);
            }
            return _context.SaveChangesAsync();
        }


    }
}
