using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.Interfaces;

namespace Configuration.SQL
{
    public sealed class SQLAppConfig : ConfigurationBase
    {
        public int Id { get; internal set; }
        public new string Name { get; set; }
        public string Code { get; set; }

        public ICollection<SQLAppConfigSetting> Settings { get; private set; }

        public SQLAppConfig(string name) : this()
        {
            Name = name;
        }
        public void Add(SQLAppConfigSetting setting)
        {
            var existing = Settings.SingleOrDefault(x => x.Name == setting.Name && setting.Mode == x.Mode);
            if(existing!=null)
            {
                existing.Value = setting.Value;
            }
            else
            {
                setting.AppConfigId = Id;
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
            return Get(key, ConfigMode.Default);
        }

        public string Get(string key, ConfigMode mode)
        {
            return Settings.Where(x => x.Name == key && x.Mode == mode).SingleOrDefault()?.Value;
        }
    }
}
