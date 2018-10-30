using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Configuration.SQL
{
    [Obsolete("use configuration manager with sql storage",true)]
    public class ConfigurationManagerSQL
    {

        private static ConfigurationManagerSQL _instance;
        private static Dictionary<Type, Type> Register;
        private static ConfigurationContext Context;
        public string Database => Context.Database.Connection.Database;
        public static ConfigurationManagerSQL Instance => _instance ?? (_instance = new ConfigurationManagerSQL(new WarburnEstateDatabaseFactory().Create()));
        public static void SetContext(ConfigurationContext context)
        {
            Context = context;
        }
        public ConfigurationManagerSQL(ConfigurationContext context)
        {
            Context = context;
            Register = new Dictionary<Type, Type>();
        }

        public bool Write(SQLAppConfig instance)
        {


            if (!Context.AppConfigs.Any(x => x.Name == instance.Name))
            {
                Context.AppConfigs.Add(instance);
            }


            return Context.SaveChanges() > 0;
        }

        public SQLAppConfig Read(string name)
        {
            return Context.AppConfigs.Include(x => x.Settings).Where(x => x.Name == name).SingleOrDefault();
        }

        public SQLAppConfig Get(string appConfig)
        {
            var instance = Context.Set<SQLAppConfig>().Include(x=>x.Settings).Where(x => x.Name == appConfig).SingleOrDefault();
            if (instance == null)
            {
                instance = new SQLAppConfig(appConfig);
                Context.AppConfigs.Add(instance);
            }

            return instance;
        }
    }
}
