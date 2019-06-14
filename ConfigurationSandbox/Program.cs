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

        public override IConfiguration Default()
        {
            return new TestConfig()
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
            var storage = new ConfigurationFileStorage(new JsonSerializer(), ".json", ConfigMode.Test);

            var sqlStorage = new ConfigurationSQLStorage<SQLAppConfig>(new WarburnEstateDatabaseFactory().Create(), ConfigMode.Test);

            //var cachedStore = new ConfigurationStorageCache(TimeSpan.FromSeconds(30), storage);
            ConfigurationManager.Create(storage);
            
            bool exist = ConfigurationManager.Instance.Exist<TestConfig>("TestConfig");
            if (!exist)
            {
                var item = ConfigurationManager.Instance.Load<TestConfig>("TestConfig");

                ConfigurationManager.Instance.Save(new TestConfig().Default());


            }

            Console.WriteLine($"Exist: {exist}");
        }
    }
}
