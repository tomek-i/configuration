using System;
using System.IO;
using System.Linq.Expressions;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using TISerializer.Logic.Serializers;

namespace TI.Configuration.Logic
{
    public class ConfigurationManager : IConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        public IConfiguration MasterConfig { get; private set; }

        public static ConfigurationManager Instance => _instance ?? (_instance = new ConfigurationManager());

        public static string DefaultPath => _internals.Configs.MasterConfig.ConfigDirectory;


        internal ConfigurationManager()
        {
            IConfiguration temp = new MasterConfig();
            Refresh(ref temp);
            MasterConfig = temp;
            //Config = Activator.CreateInstance<MasterConfig>().Refresh<MasterConfig>();
            //Config = Read<MasterConfig>();
        }

        private void Refresh(ref IConfiguration cfg)
        {
            cfg = (IConfiguration) Read(cfg);
        }


        private object Read(IConfiguration instance, bool rewriteIfExists = false)
        {
            var filepath = instance.GetFilePath();
            if (File.Exists(filepath))
            {
                var content = File.ReadAllText(filepath);
                var loadedCfg = (IConfiguration) Serializer.Deserialize(content, instance.GetType());
                
                //forces to write new properties which havent been written to cfg yet
                if (rewriteIfExists)
                    Write(loadedCfg);

                return loadedCfg;
            }

            if (!Write(instance))
                throw new Exception("Could not write configuration.");

            return instance;
        }

        public T Read<T>(bool rewriteIfExists=true) where T : class, IConfiguration
        {
            T defaultInstance = Activator.CreateInstance<T>();
            return Read(defaultInstance, rewriteIfExists) as T;
        }

        
        public T Update<T>(Action<T> exp) where T : class, IConfiguration
        {
            var cfg = Read<T>();
        
            exp.Invoke(cfg);

            if (!Write(cfg))
                throw new Exception("Could not write configuration.");

            return cfg;
        }


        public bool Write<T>(T instance) where T : IConfiguration
        {
            string filepath = instance.CreateDirectoryIfNotExist();

            var serialized = Serializer.Serialize(instance);

            using (TextWriter writer = new StreamWriter(File.OpenWrite(filepath)))
                writer.Write(serialized);

            return true;
        }

        public static void Reset()
        {
            IConfiguration temp = new MasterConfig();
            Instance.Refresh(ref temp);
            Instance.MasterConfig = temp;

        }
    }
}