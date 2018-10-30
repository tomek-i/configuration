using System.Collections.Generic;
using System.Linq;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace Configuration.SQL
{
    public sealed class SQLAppConfig : ConfigurationBase
    {
        public new string Name { get; private set; }

        internal ICollection<SQLAppConfigSetting> Settings { get; private set; }
        public ConfigMode Mode { get; set; }

        internal SQLAppConfig(string name) : this()
        {
            Name = name;
        }
        public void Add(SQLAppConfigSetting setting)
        {
            var existing = Settings.SingleOrDefault(x => x.Key == setting.Key && setting.Mode == x.Mode);
            if(existing!=null)
            {
                existing.Value = setting.Value;
            }
            else
            {
                setting.AppConfigId = Name;
                Settings.Add(setting);
            }

            


            
            
        }
        internal SQLAppConfig()
        {
            Settings = new List<SQLAppConfigSetting>();
        }
        public override IConfiguration Default()
        {
            return null;
        }

        public string Get(string key)
        {
            return Get(key, Mode);
        }

        public string Get(string key, ConfigMode mode)
        {
            return Settings.Where(x => x.Key == key && x.Mode == mode).SingleOrDefault()?.Value;
        }
    }
}
