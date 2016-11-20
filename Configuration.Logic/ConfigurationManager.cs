using System;
using System.IO;
using System.Reflection;
using TIConfiguration.Logic.API;
using TIConfiguration.Logic._internals.Configs;
using TISerializer.Logic.Serializers;

namespace TIConfiguration.Logic
{

    //TODO: Split file system access from serialization
    public static class ConfigurationManager
    {
        internal static readonly MasterConfig Config;
        internal static readonly JsonSerializer Serializer;

        static ConfigurationManager()
        {
            Serializer = new JsonSerializer();

            Config = new MasterConfig
            {
#if DEBUG
                CurrentMode = ConfigurationMode.Debug
#else
                CurrentMode = ConfigurationMode.Release
#endif
            };

            Config = Read<MasterConfig>();
            
            if (Config.CurrentMode == ConfigurationMode.Custom && string.IsNullOrEmpty(Config.ModeName))
                throw new ArgumentNullException(nameof(Config.ModeName),
                    "The parameter cannot be null if the mode is set to custom.");
        }

        public static T Update<T>(Action<T> exp) where T : class,IConfiguration
        {
            var cfg = Read<T>();
            exp.Invoke(cfg);

            if (!Write(cfg))
                throw new Exception("Could not write configuration.");

            return cfg;
         }
      
        
        public static bool Write<T>(T instance) where T : IConfiguration
        {

            string filepath;
            string filename;
            if (instance.IsInternalConfiguration())
            {
                var internalCfg = instance.GetInternalConfig();
                filename = $"{internalCfg.FilePrefix}{instance.Name}.json";
                filepath = Path.Combine(Config.ConfigDirectory, internalCfg.Foldername, filename);
            }
            else
            {
                filename = instance.Name + ".json";
                filepath = Path.Combine(Config.ConfigDirectory, Config.ModeName, filename);
            }

            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                var subFolder = Path.GetDirectoryName(filepath);
                if (subFolder != null)
                    Directory.CreateDirectory(subFolder);
            }
            using (TextWriter writer = new StreamWriter(File.OpenWrite(filepath)))
            {
                writer.Write(Serializer.Serialize(instance));
            }
            return true;
        }

        public static T Read<T>() where T : class,IConfiguration
        {
            string filename, filepath;
            T defaultInstance = Activator.CreateInstance<T>();
            var internalCfg = typeof (T).GetCustomAttribute(typeof (InternalConfigurationAttribute)) as InternalConfigurationAttribute;
            if (internalCfg != null)
            {
                filename = $"{internalCfg.FilePrefix}{defaultInstance.Name}.json";
                filepath = Path.Combine(Config.ConfigDirectory, internalCfg.Foldername, filename);
            }
            else
            {
                filename = defaultInstance.Name + ".json";
                filepath = Path.Combine(Config.ConfigDirectory, Config.ModeName, filename);
            }

            if (File.Exists(filepath))
            {
                var content = File.ReadAllText(filepath);
                var loadedCfg = Serializer.Deserialize<T>(content);
                return loadedCfg;
            }

            if (!Write(defaultInstance))
                throw new Exception();

            return defaultInstance;
        }

    }
}