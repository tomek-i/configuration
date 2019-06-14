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
            var fileStorage = new ConfigurationFileStorage(new JsonSerializer(), ".json", ConfigMode.Test);
            var cachedFileStorage = new ConfigurationStorageCache<ConfigurationFileStorage>(TimeSpan.FromSeconds(30), fileStorage);

            var sqlStorage = new SQLConfigStorage(new WarburnEstateDatabaseFactory().Create(), ConfigMode.Test);
            //IMPORTANT: the cached configuration store wont work properly with the SQL storage
            var cachedSqlStorage = new ConfigurationStorageCache<SQLConfigStorage>(TimeSpan.FromSeconds(30), sqlStorage);

            var manager = new ConfigurationManager<SQLConfigStorage>(sqlStorage);

            bool exist = false;
            if (!exist)
            {
                
                
                var item = manager.Storage.Get<SQLAppConfig>("L1PACK");
              
                var value = item.Get("FORMATS", ConfigMode.Live);
                var formats = manager.Storage.Get<SQLAppConfig>(value);
                var format = formats.Get("9", ConfigMode.Live);

             
                manager.Storage.Set(item);


            }

            Console.WriteLine($"Exist: {exist}");
        }
    }
}
