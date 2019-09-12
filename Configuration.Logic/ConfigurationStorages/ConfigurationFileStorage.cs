using System;
using System.IO;
using TI.Configuration.Logic.Interfaces;
using System.Threading.Tasks;
using System.Text;
using TI.Serializer.Logic.API;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// File I/O storage of configurations
    /// </summary>
    public sealed class ConfigurationFileStorage : DbContext
    {
        ISerializer Serializer;
        string Extension;
        ConfigMode Mode;
        string RootPath;

        public ConfigurationFileStorage(
            ISerializer serializer, string extension, ConfigMode mode, string path = "./config")
        {
            Serializer = serializer;
            Extension = extension.TrimStart('.');
            Mode = mode;
            RootPath = path;
        }
        public T Get<T>() where T : class, IConfiguration,new()
        {
            return Get<T>(new T().Name);
        }
        public T Get<T>(string name="") where T : class,IConfiguration
        {
            string path = GetPath(name);

            if (File.Exists(path))
            {
                StringBuilder sb = new StringBuilder();
                string content = null;
                using (StreamReader source = File.OpenText(path))
                {
                    content = source.ReadToEnd();
                }
                T config = Serializer.Deserialize<T>(content);
                //config.Name = name;
                return config;
            }
            return default(T);
        }

       
        public T Set<T>(T instance) where T:class,IConfiguration
        {
            string path = GetPath(instance.Name);

            var content = Serializer.Serialize(instance);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (TextWriter writer = new StreamWriter(File.Create(path)))
                {
                    writer.Write(content);
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationFileStorageException($"Failed to write configuration to: {path}", exception);
            }
            return instance;
        }

        private string GetPath(string name)
        {
            return Path.Combine(RootPath, Mode.ToString(), $"{name}.{Extension}");
        }

    }
}