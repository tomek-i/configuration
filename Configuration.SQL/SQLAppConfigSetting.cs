namespace Configuration.SQL
{
    public class SQLAppConfigSetting
    {
        public string Key { get;  private set; }
        public string Value { get;  set; }
        public ConfigMode Mode { get; private set; }


        public string AppConfigId { get; set; }
        public SQLAppConfig Config { get; set; }

        internal SQLAppConfigSetting()
        {

        }

        public SQLAppConfigSetting(string key, string value, ConfigMode mode)
        {
            Key = key;
            Value = value;
            Mode = mode;
        }
    }
}
