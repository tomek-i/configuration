using System;
using System.IO;
using System.Reflection;
using TI.Configuration.Logic._internals.Configs;

namespace TI.Configuration.Logic.API
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

        public static TPropertyType GetInternalConfigProperty<TPropertyType>(this IConfiguration config, Func<InternalConfigurationAttribute, TPropertyType> propertyAccess)
        {
            if (!config.IsInternalConfiguration())
            {
                throw new InvalidOperationException($"The object is not decorated with the {nameof(InternalConfigurationAttribute)} Attrbiute.");
            }
            return propertyAccess.Invoke(config.GetInternalConfig());
        }

        [Obsolete("Not required, can access property directly?")]
        public static TRet Get<T,TRet>(this T cfg, Func<T, TRet> action) where T : IConfiguration
        {
            return action.Invoke(cfg);
        }


        public static string CreateDirectoryIfNotExist(this IConfiguration cfg)
        {
            var path = cfg.GetFilePath();
            var directoryName = Path.GetDirectoryName(path);
            if (Directory.Exists(directoryName)) return Path.Combine(path);
            if (directoryName != null)
                Directory.CreateDirectory(directoryName);

            return path;
        }

        public static string GetFilePath(this IConfiguration cfg)
        {
            string path, filename;
            var config = cfg as MasterConfig;
            MasterConfig masterCfg = config ?? ConfigurationManager.Instance.Read<MasterConfig>(false);
            if (cfg.IsInternalConfiguration())
            {
                var internalCfg = cfg.GetInternalConfig();
                filename = $"{internalCfg.FilePrefix}{cfg.Name}.json";
                path = Path.Combine(MasterConfig.ConfigDirectory, internalCfg.Foldername);
            }
            else
            {
                filename = cfg.Name + ".json";
                path = Path.Combine(MasterConfig.ConfigDirectory, masterCfg.ModeName);
            }

            return Path.Combine(path, filename);

        }
    }
}