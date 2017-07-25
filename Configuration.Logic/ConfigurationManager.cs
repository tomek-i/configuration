using System;
using System.IO;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;
using TISerializer.Logic.Serializers;
using System.Collections.Generic;
using System.Windows.Forms;
using TI.Configuration.Logic.Abstracts;
using System.Linq;

namespace TI.Configuration.Logic
{
    public interface INeedConfigUpdates
    {
        void OnConfigurationUpdate<T>(T instance) where T : ConfigurationBase;
    }
    //TODO: here we need
    public class ConfigurationManager : IConfigurationManager
    {
        private static ConfigurationManager _instance;
        private static readonly JsonSerializer Serializer = new JsonSerializer();
        private static Dictionary<Type, Type> register = new Dictionary<Type, Type>();

        public IConfiguration MasterConfig { get; private set; }
        public static ConfigurationManager Instance => _instance ?? (_instance = new ConfigurationManager());
        public static string DefaultPath => _internals.Configs.MasterConfig.ConfigDirectory;

        private static IDictionary<Type,ISet<INeedConfigUpdates>> watchers = new Dictionary<Type,ISet<INeedConfigUpdates>>();

        public void AddWatcher<T>(INeedConfigUpdates watcher)
        {
            var t = typeof(T);
            if (!watchers.ContainsKey(t))
                watchers.Add(t, new HashSet<INeedConfigUpdates>());

            watchers[t].Add(watcher);
        }
        public void ConfigChanged<T>(T instance) where T:ConfigurationBase
        {
            var key = watchers.Keys.Where(x => x.Name == instance.GetType().Name).Single();

            foreach (var item in watchers[instance.GetType()])
            {
                item.OnConfigurationUpdate(instance);
            }
        }

        internal ConfigurationManager()
        {
            IConfiguration temp = new MasterConfig();
            //MapToDisplay<MasterConfig, MasterConfigDisplay>();

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
                var b = loadedCfg as ConfigurationBase;
                b.PropertyChanged += (sender,property)=> {
                    ConfigChanged(b);
                };
                //forces to write new properties which havent been written to cfg yet
                if (rewriteIfExists)
                    Write(loadedCfg);

                return loadedCfg;
            }
            else
            {
                //TODO: file doesnt exist, the important part in 74 is never executed which means the notification system doesnt work
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

        public void MapToDisplay<TConfig,TDisplay>() 
            where TConfig : IConfiguration 
            where TDisplay:Control
        {
            register.Add(typeof(TConfig), typeof(TDisplay));
        }

        public Control GetMappedDisplay<T>() where T:IConfiguration
        {
            
            return Activator.CreateInstance(register[typeof(T)]) as Control;
        }

       
    }
}