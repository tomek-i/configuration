using System;

namespace TI.Configuration.Logic
{
    public sealed class ConfigurationFileStorageException : ApplicationException
    {
        public ConfigurationFileStorageException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}