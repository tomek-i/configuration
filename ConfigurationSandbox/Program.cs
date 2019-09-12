using Configuration.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.Interfaces;
using TI.Serializer.Logic.Serializers;

namespace ConfigurationSandbox
{
    public class TestConfig : ConfigurationBase
    {
        public string ConfigA { get; set; }
        public int ConfigB { get; set; }
        public bool ConfigC { get; set; }

        public TestConfig():base(null)
        {

        }
        public TestConfig(string name):base(name)
        {

        }
        public override IConfiguration Default()
        {
            return new TestConfig("TestConfig")
            {
                ConfigA = "Test A",
                ConfigB = 2,
                ConfigC = true
            };
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Main2();
            Console.ReadKey();
        }

        public static void Main2()
        {
            var fileStorage = new ConfigurationFileStorage(new JsonSerializer(), ".json", ConfigMode.Test);
            var cachedFileStorage = new ConfigurationStorageCache<ConfigurationFileStorage>(TimeSpan.FromSeconds(30), fileStorage);

            var sqlStorage = new SQLConfigStorage(new WarburnEstateDatabaseFactory().Create(), ConfigMode.Test);
            //IMPORTANT: the cached configuration store wont work properly with the SQL storage
            var cachedSqlStorage = new ConfigurationStorageCache<SQLConfigStorage>(TimeSpan.FromSeconds(30), sqlStorage);

            var manager = new ConfigurationManager<SQLConfigStorage>(sqlStorage);
            var manager2 = new ConfigurationManager<ConfigurationFileStorage>(fileStorage);



            var sqlc = manager.Storage.Get<SQLAppConfig>("Line 1 Packaging");
            manager2.Storage.Set<SQLAppConfig>(sqlc);
           var abctest =  manager2.Storage.Get<SQLAppConfig>();
            bool exist = false;
            if (!exist)
            {

                var test = manager.Storage.Get<SQLAppConfig>("Line 1 Packaging");
                if (test == null)
                {
                    test = manager.Storage.Set(new SQLAppConfig("Line 1 Packaging"));
                   
                }
                test.Add(new SQLAppConfigSetting("test2", "3", ConfigMode.Default));
                manager.Storage.Save();


            }

            Console.WriteLine($"Exist: {exist}");
        }
    }
}
