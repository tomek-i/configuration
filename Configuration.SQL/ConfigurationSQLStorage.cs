using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using TI.Configuration.Logic;

namespace Configuration.SQL
{
    public sealed class ConfigurationSQLStorage : ConfigurationStorage<SQLAppConfig>
    {
        private ConfigurationContext _context;

        public ConfigurationSQLStorage(ConfigurationContext context,ConfigMode mode):base(mode)
        {
            _context = context;
        }

        public override SQLAppConfig Get<A>(string name)
        {
            var instance = _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Name == name).SingleOrDefault();
            if (instance == null)
            {
                instance = new SQLAppConfig(name);
                _context.AppConfigs.Add(instance);
            }
            return instance;
        }

        public override async Task<SQLAppConfig> GetAsync<A>(string name)
        {
            var instance = await _context.Set<SQLAppConfig>().Include(x => x.Settings).Where(x => x.Name == name).SingleOrDefaultAsync();
            if (instance == null)
            {
                instance = new SQLAppConfig(name);
                _context.AppConfigs.Add(instance);
            }
            return instance;
        }

        public override void Set(SQLAppConfig instance)
        {
            if (!_context.AppConfigs.Any(x => x.Name == instance.Name))
            {
                _context.AppConfigs.Add(instance);
            }
            _context.SaveChanges();
        }

        public override Task SetAsync(SQLAppConfig instance)
        {
            if (!_context.AppConfigs.Any(x => x.Name == instance.Name))
            {
                _context.AppConfigs.Add(instance);
            }
            return _context.SaveChangesAsync();
        }
    }
}
