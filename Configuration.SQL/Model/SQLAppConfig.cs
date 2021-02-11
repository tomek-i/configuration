using System.Collections.Generic;
using System.Linq;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.Interfaces;

namespace Configuration.SQL
{
    public class SQLAppConfig : ConfigurationBase
    {
        public int Id { get; internal set; }
        //public new string Name { get; set; }
        //public string Code { get; set; }

        public ICollection<SQLAppConfigSetting> Settings { get; private set; }
        public SQLAppConfig() : this("SQLAppConfig")
        {
            Settings = new List<SQLAppConfigSetting>();
        }
        public SQLAppConfig(string name) : base(name)
        {
            Settings = new List<SQLAppConfigSetting>();
            //Name = name;
            //Code = Name.ToUpper().Replace(" ", "");
        }
        public void Add(SQLAppConfigSetting setting)
        {
            SQLAppConfigSetting existing = Settings.SingleOrDefault(x => x.Name == setting.Name && setting.Mode == x.Mode);
            if (existing != null)
            {
                existing.Value = setting.Value;
            }
            else
            {
                setting.AppConfigId = Id;
                Settings.Add(setting);
            }

        }

        public override IConfiguration Default()
        {
            return null;
        }

        public List<KeyValuePair<string, ConfigMode>> Get(string key)
        {
            return Settings.Where(x => x.Code == key.ToUpper()).Select(x => new KeyValuePair<string, ConfigMode>(x.Value, x.Mode)).ToList();
        }

        public string Get(string key, ConfigMode mode)
        {
            return Settings.Where(x => x.Code == key.ToUpper() && x.Mode == mode).SingleOrDefault()?.Value;
        }
    }
}
