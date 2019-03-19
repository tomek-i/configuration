using System;
using System.IO;
using System.Reflection;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using System.Linq.Expressions;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Extension class with utility functions for Configurations
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="o"></param>
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public static string GetTooltip<T, TProp>(this T o, Expression<Func<T, TProp>> propertySelector) where T : Configuration.Logic.Abstracts.ConfigurationBase
        {
            string propertyName = ((MemberExpression)propertySelector.Body).Member.Name;
            return ((MemberExpression)propertySelector.Body).Expression.Type.GetProperty(propertyName).GetCustomAttribute<ConfigurationToolTipAttribute>()?.TooltipText;
        }

        public static string GetTooltip<T>(this T o, string propertyName) where T : Configuration.Logic.Abstracts.ConfigurationBase
        {
            return o.GetType().GetProperty(propertyName).GetCustomAttribute<ConfigurationToolTipAttribute>()?.TooltipText;
        }

        /// <summary>
        /// Determins if the current configuration instance is an internal (system) configuration or not
        /// </summary>
        /// <param name="config">the configuration instance to check</param>
        /// <returns>true if it is an internal configuration</returns>
        public static bool IsInternalConfiguration(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof(InternalConfigurationAttribute)) != null;
        }

        /// <summary>
        /// Retrives the interal configuration attribute of the configuration or null
        /// </summary>
        /// <param name="config">the configuration instance to get the internal confgiuration attribute from</param>
        /// <returns>returns the internalconfguration instance or null</returns>
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

        
        /// <summary>
        /// Creates the directory for the configuration if it doesn't exist
        /// </summary>
        /// <param name="cfg">the confguration instance to create the directory for</param>
        /// <returns>returns the crated path for the configuraiton</returns>
        public static string CreateDirectoryIfNotExist(this IConfiguration cfg)
        {
            var path = cfg.GetFilePath();
            var directoryName = Path.GetDirectoryName(path);
            if (Directory.Exists(directoryName)) return Path.Combine(path);
            if (directoryName != null)
                Directory.CreateDirectory(directoryName);

            return path;
        }
        /// <summary>
        /// Returns the path to store configuration files in
        /// </summary>
        /// <param name="cfg">the configuration to store</param>
        /// <returns>the path where the configuration will be stored</returns>
        public static string GetFilePath(this IConfiguration cfg)
        {
            string path, filename;
            var config = cfg as MasterConfig;
            MasterConfig masterCfg = config ?? ConfigurationManager.Instance.Read<MasterConfig>();
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