using System;
using System.IO;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using System.Collections.Generic;
using System.Windows.Forms;
using TI.Configuration.Logic.Abstracts;
using System.Linq;
using System.Threading;
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
        System.Threading.ReaderWriterLock rwl = new ReaderWriterLock();

        static readonly object _writeLock = new object();
        private static readonly IDictionary<Type, ISet<INeedConfigUpdates>> Watchers;
        private static readonly JsonSerializer Serializer;
        private static ConfigurationManager _instance;
        private static readonly Dictionary<Type, Type> Register;

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

            Watchers = new Dictionary<Type, ISet<INeedConfigUpdates>>();
            Serializer = new JsonSerializer();
            Register = new Dictionary<Type, Type>();
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
        public void AddWatcher<T>(INeedConfigUpdates watcher) where T : IConfiguration
        {
            var t = typeof(T);
            if (!Watchers.ContainsKey(t))
                Watchers.Add(t, new HashSet<INeedConfigUpdates>());

            Watchers[t].Add(watcher);
        }





        public T Read<T>() where T : class, IConfiguration
        {
            T defaultInstance = Activator.CreateInstance<T>();
            return Read(defaultInstance.Default()) as T;
        }
        public T Update<T>(Action<T> exp) where T : class, IConfiguration
        {
            var cfg = Read<T>();

            exp.Invoke(cfg);
            rwl.AcquireWriterLock(TimeSpan.FromSeconds(1));
            try
            {
                Write(cfg);
            }
            catch (Exception x)
            {

                throw x;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }

            return cfg;
        }
        public bool Write<T>(T instance) where T : IConfiguration
        {
            string filepath = instance.CreateDirectoryIfNotExist();

            var serialized = Serializer.Serialize(instance);


            lock (_writeLock)
            {
                int attempts = 1;
                rwl.AcquireWriterLock(TimeSpan.FromSeconds(1));

                RETRY:
                try
                {
                    using (TextWriter writer = new StreamWriter(File.Create(filepath)))
                    {
                        writer.Write(serialized);
                    }
                }
                catch (Exception exception)
                {
                    attempts++;
                    Thread.Sleep(50 * attempts);
                    if (attempts <= 5)
                    {
                        goto RETRY;
                    }
                    else
                    {
                        throw exception;
                    }

                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }
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
        public void MapToDisplay<TConfig, TDisplay>()
            where TConfig : IConfiguration
            where TDisplay : Control
        {
            Register.Add(typeof(TConfig), typeof(TDisplay));
        }

        /// <summary>
        /// Gets the mapped display (control) for the configuraiton type.
        /// </summary>
        /// <typeparam name="T">The configuraiton type which will be displayed</typeparam>
        /// <returns>Instance of an control</returns>
        public Control GetMappedDisplay<T>() where T : IConfiguration
        {
            return Activator.CreateInstance(Register[typeof(T)]) as Control;
        }


        private void Refresh(ref IConfiguration cfg)
        {
            cfg = (IConfiguration)Read(cfg);
        }
        private void ConfigChanged<T>(T instance) where T : ConfigurationBase
        {
            var key = Watchers.Keys.Single(x => x.Name == instance.GetType().Name);

            foreach (var item in Watchers[instance.GetType()])
            {
                item.OnConfigurationUpdate(instance);
            }
        }
        private object Read(IConfiguration instance)
        {
            var filepath = instance.GetFilePath();
            if (File.Exists(filepath))
            {
                var content = File.ReadAllText(filepath);
                var loadedCfg = (IConfiguration)Serializer.Deserialize(content, instance.GetType());
                var configurationBase = loadedCfg as ConfigurationBase;

                if (configurationBase != null)
                    configurationBase.PropertyChanged += (sender, property) =>
                    {
                        ConfigChanged(configurationBase);
                    };

                return loadedCfg;
            }
            else
            {
                instance = instance.Default();
                rwl.AcquireWriterLock(TimeSpan.FromSeconds(1));
                try
                {
                    Write(instance);
                }
                catch (Exception)
                {


                }
                finally
                {
                    rwl.ReleaseWriterLock();
                }
            }

            return instance;
        }

    }
}