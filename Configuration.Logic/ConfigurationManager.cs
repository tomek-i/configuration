using System;
using System.IO;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using System.Collections.Generic;
using System.Windows.Forms;
using TI.Configuration.Logic.Abstracts;
using System.Linq;
using TI.Serializer.Logic.Serializers;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Manger class which takes care of read/write of configurations, 
    /// sending notifications to watchers/observers when cofnguration has changed
    /// and allows to register a display for configuration types.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        private static IDictionary<Type, ISet<INeedConfigUpdates>> watchers;
        private static readonly JsonSerializer Serializer;
        private static ConfigurationManager _instance;
        private static Dictionary<Type, Type> register;
        
        /// <summary>
        /// Default path where configuration files are being stored.
        /// </summary>
        public static string DefaultPath => _internals.Configs.MasterConfig.ConfigDirectory;

        /// <summary>
        /// Singel instance of Configuration Manager
        /// </summary>
        public static ConfigurationManager Instance => _instance ?? (_instance = new ConfigurationManager());

        /// <summary>
        /// Master Configuration file.
        /// </summary>
        public IConfiguration MasterConfig { get; private set; }

        #region ctor
        static ConfigurationManager()
        {
            watchers =  new Dictionary<Type, ISet<INeedConfigUpdates>>();
            Serializer = new JsonSerializer();
            register = new Dictionary<Type, Type>();
        }
        internal ConfigurationManager()
        {
            IConfiguration temp = new MasterConfig().Default();
            Refresh(ref temp);
            MasterConfig = temp;
        }
        #endregion

        /// <summary>
        /// Allows to add watchers to configuration types
        /// </summary>
        /// <typeparam name="T">The configuration to watch out for changes.</typeparam>
        /// <param name="watcher">the object which needs to be notified.</param>
        public void AddWatcher<T>(INeedConfigUpdates watcher)where T: IConfiguration
        {
            var t = typeof(T);
            if (!watchers.ContainsKey(t))
                watchers.Add(t, new HashSet<INeedConfigUpdates>());

            watchers[t].Add(watcher);
        }

        

       
      
        public T Read<T>(bool rewriteIfExists=true) where T : class, IConfiguration
        {
            T defaultInstance = Activator.CreateInstance<T>();
            return Read(defaultInstance.Default(), rewriteIfExists) as T;
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

        /// <summary>
        /// Rereads the Master Configuration file.
        /// </summary>
        public static void Reset()
        {
            IConfiguration temp = new MasterConfig();
            Instance.Refresh(ref temp);
            Instance.MasterConfig = temp;

        }

        /// <summary>
        /// Assigns the display type to a configuration type
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <typeparam name="TDisplay"></typeparam>
        public void MapToDisplay<TConfig,TDisplay>() 
            where TConfig : IConfiguration 
            where TDisplay:Control
        {
            register.Add(typeof(TConfig), typeof(TDisplay));
        }

        /// <summary>
        /// Gets the mapped display (control) for the configuraiton type.
        /// </summary>
        /// <typeparam name="T">The configuraiton type which will be displayed</typeparam>
        /// <returns>Instance of an control</returns>
        public Control GetMappedDisplay<T>() where T:IConfiguration
        {
            return Activator.CreateInstance(register[typeof(T)]) as Control;
        }


        private void Refresh(ref IConfiguration cfg)
        {
            cfg = (IConfiguration)Read(cfg);
        }
        private void ConfigChanged<T>(T instance) where T : ConfigurationBase
        {
            var key = watchers.Keys.Where(x => x.Name == instance.GetType().Name).Single();

            foreach (var item in watchers[instance.GetType()])
            {
                item.OnConfigurationUpdate(instance);
            }
        }
        private object Read(IConfiguration instance, bool rewriteIfExists = false)
        {
            var filepath = instance.GetFilePath();
            if (File.Exists(filepath))
            {
                var content = File.ReadAllText(filepath);
                var loadedCfg = (IConfiguration)Serializer.Deserialize(content, instance.GetType());
                var b = loadedCfg as ConfigurationBase;
                b.PropertyChanged += (sender, property) => {
                    ConfigChanged(b);
                };
                //forces to write new properties which havent been written to cfg yet
                if (rewriteIfExists)
                    Write(loadedCfg);

                return loadedCfg;
            }
            else
            {
                //TODO: file doesnt exist, the important part in 129 saying  b.PropertyChanged += (sender, property) => { is never executed which means the notification system doesnt work
            }

            if (!Write(instance))
                throw new Exception("Could not write configuration.");

            return instance;
        }

    }
}