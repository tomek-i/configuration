using System;
using System.IO;
using System.Reflection;

namespace TI.Configuration.Logic.API
{
    /// <summary>
    /// Colleciton of functions which extend on the <see cref="IConfiguration"/> interface.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Determines whether the configuration is decorated with the <see cref="InternalConfigurationAttribute"/> Attribute.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns><c>true</c> if its decorated, otherwise <c>false</c></returns>
        /// <seealso cref="IConfiguration"/>
        /// <seealso cref="InternalConfigurationAttribute"/>
        public static bool IsInternalConfiguration(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof(InternalConfigurationAttribute)) != null;
        }

        /// <summary>
        /// Gets the <see cref="InternalConfigurationAttribute"/> instance on the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>The instance of the <see cref="InternalConfigurationAttribute"/> attribute.</returns>
        /// <seealso cref="IConfiguration"/>
        /// <seealso cref="InternalConfigurationAttribute"/>
        public static InternalConfigurationAttribute GetInternalConfig(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof (InternalConfigurationAttribute)) as InternalConfigurationAttribute;
        }
        /// <summary>
        /// Gets the property on the <see cref="InternalConfigurationAttribute"/> instance.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="expression">The property expression.</param>
        /// <returns>The value of the property</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static TProperty GetInternalConfigProperty<TProperty>(this IConfiguration config,Func<InternalConfigurationAttribute, TProperty> expression)
        {
            if (!config.IsInternalConfiguration())
            {
                throw new InvalidOperationException($"The object is not decorated with the {nameof(InternalConfigurationAttribute)} Attrbiute.");
            }
            return expression.Invoke(config.GetInternalConfig());
        }

        /// <summary>
        /// Writes the configuration instance.
        /// </summary>
        /// <param name="instance">The configuration instance.</param>
        /// <returns><c>true</c> if it could write successfully otherwise <c>false</c></returns>
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

        /// <summary>
        /// Reloads the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>returns a new instance of the configuration if a confguration file exists, otherwise returns the same instance</returns>
        public static TConfigurationType Reload<TConfigurationType>(this IConfiguration configuration) where TConfigurationType:class,IConfiguration
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            string filename, filepath;

            var internalCfg = configuration.GetInternalConfig();
                
            if (internalCfg !=null)
            {
                filename = $"{internalCfg.FilePrefix}{configuration.Name}.json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, internalCfg.Foldername, filename);
            }
            else
            {
                filename = configuration.Name + ".json";
                filepath = Path.Combine(ConfigurationManager.Config.ConfigDirectory, ConfigurationManager.Config.ModeName, filename);
            }

            if (File.Exists(filepath))
                configuration = ConfigurationManager.Serializer.Deserialize(File.ReadAllText(filepath),configuration.GetType()) as IConfiguration ;

            return configuration as TConfigurationType;
        }

        public static void Update(this IConfiguration configuration, Func<IConfiguration, object> expression = null)
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            var value = expression?.Invoke(configuration);

            if (!configuration.Write())
                throw new Exception();

            //return ocfg;
        }
        public static void Update<TConfigurationType>(this IConfiguration configuration, Func<TConfigurationType, object> expression=null) where TConfigurationType: class, IConfiguration
        {
            //TODO: if this works, it can be refactored within the ConfigurationManager
            var value = expression?.Invoke(configuration as TConfigurationType);

            if (!configuration.Write())
                throw new Exception();
        }
    }
}