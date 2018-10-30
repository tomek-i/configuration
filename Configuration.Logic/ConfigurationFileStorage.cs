using System;
using System.IO;
using TI.Configuration.Logic.API;
using TI.Serializer.Logic.API;
using System.Threading.Tasks;
using System.Text;

namespace TI.Configuration.Logic
{

    public enum ConfigMode
    {
        Default = 1,
        Live = 1,
        Test = 2
    }

    public sealed class ConfigurationFileStorage : ConfigurationStorage<IConfiguration>
    {
        private string _path;
        private string _fileExtension;

        private ISerializer _serializer;

        public ConfigurationFileStorage(ISerializer serializer, string fileExtension,ConfigMode mode, string path = "./config"):base(mode)
        {
            _serializer = serializer;
            _path = path;
            _fileExtension = fileExtension.TrimStart('.');

        }

        public override async Task<IConfiguration> GetAsync<A>(string name)
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

                var config = _serializer.Deserialize<IConfiguration>(content);

                return config;
            }
            else
            {
                //TODO: optionally create file with default?

                return null;
            }
        }
        private string getFileLocation(string name)
        {
            return Path.Combine(_path, Mode.ToString(), $"{name}.{_fileExtension}");
        }


        public override IConfiguration Get<A>(string name)
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

                var config = _serializer.Deserialize<A>(content);

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