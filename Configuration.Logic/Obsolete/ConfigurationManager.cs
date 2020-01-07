using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.Interfaces;
using TI.Serializer.Logic.Serializers;

namespace TI.Configuration.Obsolete
{


    public static class ConfigurationExtensions
    {
        
        public static bool IsInternalConfiguration(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof(InternalConfigurationAttribute)) != null;
        }

        public static InternalConfigurationAttribute GetInternalConfig(this IConfiguration config)
        {
            return config.GetType().GetCustomAttribute(typeof(InternalConfigurationAttribute)) as InternalConfigurationAttribute;
        }

        public static TPropertyType GetInternalConfigProperty<TPropertyType>(this IConfiguration config, Func<InternalConfigurationAttribute, TPropertyType> propertyAccess)
        {
            if (!config.IsInternalConfiguration())
            {
                throw new InvalidOperationException($"The object is not decorated with the {nameof(InternalConfigurationAttribute)} Attrbiute.");
            }
            return propertyAccess.Invoke(config.GetInternalConfig());
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


    /// <summary>
    /// Attribute to decorate an internal configuration. These configurations are stored into a seperate folder
    /// and are loaded regardless of which mode the configuration manager is currently set to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class InternalConfigurationAttribute : Attribute
    {
        public string Foldername { get; }
        public string FilePrefix { get; }
        public InternalConfigurationAttribute(string foldername, string fileprefix)
        {
            Foldername = foldername;
            FilePrefix = fileprefix;
        }
        public InternalConfigurationAttribute() : this("_internals", "_")
        {
        }
    }
    [InternalConfiguration]
    public sealed class MasterConfig : ConfigurationBase
    {
        public const string ConfigDirectory = @".\config";

        private string _modeName;

        public MasterConfig():base(null)
        {

        }
        public string ModeName
        {
            get { return _modeName?.ToLowerInvariant(); }
            set
            {
                SetValue(nameof(ModeName), ref _modeName, ref value, () => { /*(De-)Serialize again? or call UI updates?*/ });
            }
        }



        public override IConfiguration Default()
        {
            string modename;
#if DEBUG
            modename = "debug";
#else
            modename = "live";
#endif
            return new MasterConfig() { ModeName = modename };
        }
    }
    public sealed class ConfigurationManager : IConfigurationManager//ConfigurationManagerBase
    {
        ReaderWriterLock rwl = new ReaderWriterLock();

        static readonly object _writeLock = new object();
        private static readonly IDictionary<Type, ISet<INeedConfigUpdates>> Watchers;
        private static readonly JsonSerializer Serializer;
        private static ConfigurationManager _instance;
        private static readonly Dictionary<Type, Type> Register;

        /// <summary>
        /// Default path where configuration files are being stored.
        /// </summary>
        public static string DefaultPath => MasterConfig.ConfigDirectory;

        /// <summary>
        /// Singel instance of Configuration Manager
        /// </summary>
        public static ConfigurationManager Instance => _instance ?? (_instance = new ConfigurationManager());

        /// <summary>
        /// Master Configuration file.
        /// </summary>
        //public IConfiguration MasterConfig { get; private set; }

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
            //MasterConfig = temp;
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
            //Instance.MasterConfig = temp;

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
            var key = Watchers.Keys.SingleOrDefault(x => x.Name == instance.GetType().Name);
            if (key == null) return;
            foreach (var item in Watchers[key])
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

