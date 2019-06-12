using TI.Configuration.Logic;

namespace Configuration.SQL
{
    public class SQLAppConfigSetting
    {
        public int Id { get; internal set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public ConfigMode Mode { get; set; }

        #region Navigation Property
        public int AppConfigId { get; set; }
        public SQLAppConfig Config { get; set; }
        #endregion

        internal SQLAppConfigSetting()
        {

        }

        public SQLAppConfigSetting(string name, string value, ConfigMode mode)
        {
            Name = name;
            Value = value;
            Mode = mode;
        }
    }
}
