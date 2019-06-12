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
    public sealed class ConfigurationFileStorage : ConfigurationStorage<IConfiguration>
    {
        private string _path;
        private string _fileExtension;
        private ISerializer _serializer;

        public ConfigurationFileStorage(ISerializer serializer, string fileExtension, ConfigMode mode, string path = "./config") : base(mode)
        {
            _serializer = serializer;
            _path = path;
            _fileExtension = fileExtension.TrimStart('.');

        }
        private string getFileLocation(string name)
        {
            return Path.Combine(_path, Mode.ToString(), $"{name}.{_fileExtension}");
        }

        public override async Task<IConfiguration> GetAsync<T>(string name)
        {
            var filepath = getFileLocation(name);

            if (File.Exists(filepath))
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader source = File.OpenText(filepath))
                {
                    char[] buffer = new char[0x1000];
                    int numRead;

                    while ((numRead = await source.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        sb.Append(buffer);
                    }
                }
                var content = sb.ToString();

                var config = _serializer.Deserialize<T>(content);

                return config;
            }
            else
            {
                //TODO: optionally create file with default?

                return null;
            }
        }
        public override IConfiguration Get<T>(string name)
        {
            var filepath = getFileLocation(name);

            if (File.Exists(filepath))
            {
                StringBuilder sb = new StringBuilder();
                string content = null;
                using (StreamReader source = File.OpenText(filepath))
                {
                    content = source.ReadToEnd();
                }

                //TODO: this is the bottleneck, wrapping in task caused more overhead
                T config = _serializer.Deserialize<T>(content); 
                //T config = Task.Factory.StartNew(() =>
                //{
                //    return _serializer.Deserialize<T>(content);
                //}).GetAwaiter().GetResult();


                return config;
            }
            else
            {
                //TODO: optionally create file with default?

                return null;
            }
        }
        public override async Task SetAsync(IConfiguration instance)
        {
            var filepath = getFileLocation(instance.Name);
            var content = _serializer.Serialize(instance);

            try
            {
                using (TextWriter writer = new StreamWriter(File.Create(filepath)))
                {
                    await writer.WriteAsync(content);
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationFileStorageException($"Failed to write configuration async to: {filepath}", exception);
            }
        }
        public override void Set(IConfiguration instance)
        {
            var filepath = getFileLocation(instance.Name);
            var content = _serializer.Serialize(instance);
            
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                using (TextWriter writer = new StreamWriter(File.Create(filepath)))
                {
                    writer.Write(content);
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationFileStorageException($"Failed to write configuration to: {filepath}", exception);
            }
        }

    }
}