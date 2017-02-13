using System;
using System.IO;
using System.Reflection;

namespace TIConfiguration.Logic.API
{
    public static class ConfigurationExtensions
    {
        public static bool IsInternalConfiguration(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof(InternalConfigurationAttribute)) != null;
        }

        public static InternalConfigurationAttribute GetInternalConfig(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof (InternalConfigurationAttribute)) as InternalConfigurationAttribute;
        }
        public static TPropertyType GetInternalConfigProperty<TPropertyType>(this IConfiguration config,Func<InternalConfigurationAttribute, TPropertyType> propertyAccess)
        {
            if (!config.IsInternalConfiguration())
            {
                throw new InvalidOperationException($"The object is not decorated with the {nameof(InternalConfigurationAttribute)} Attrbiute.");
            }
            return propertyAccess.Invoke(config.GetInternalConfig());
        }
       

        public static bool Write(this IConfiguration instance)
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            string filepath;
            string filename;
            if (instance.IsInternalConfiguration())
            {
                var internalCfg = instance.GetInternalConfig();
                filename = $"{internalCfg.FilePrefix}{instance.Name}.json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, internalCfg.Foldername, filename);
            }
            else
            {
                filename = instance.Name + ".json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, ConfigurationManager.Config.ModeName, filename);
            }

            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                var subFolder = Path.GetDirectoryName(filepath);
                if (subFolder != null)
                    Directory.CreateDirectory(subFolder);
            }
            using (TextWriter writer = new StreamWriter(File.OpenWrite(filepath)))
            {
                writer.Write(ConfigurationManager.Serializer.Serialize(instance));
            }
            return true;


        }

        public static IConfiguration Refresh(this IConfiguration cfg)
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            string filename, filepath;

            var internalCfg = cfg.GetInternalConfig();
                
            if (internalCfg !=null)
            {
                filename = $"{internalCfg.FilePrefix}{cfg.Name}.json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, internalCfg.Foldername, filename);
            }
            else
            {
                filename = cfg.Name + ".json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, ConfigurationManager.Config.ModeName, filename);
            }

            if (File.Exists(filepath))
                cfg = ConfigurationManager.Serializer.Deserialize<IConfiguration>(File.ReadAllText(filepath)) ;

            return cfg;
        }
        public static void Update<T>(this T cfg, Func<T, object> exp) where T : IConfiguration
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            var value = exp.Invoke(cfg);

            if (!cfg.Write())
                throw new Exception();

            //return ocfg;
        }
    }
}