using System;

namespace TI.Configuration.Logic
{
    /// <summary>
    /// Exception which is thrown when using FileStorage
    /// </summary>
    public sealed class ConfigurationFileStorageException : ApplicationException
    {
        public ConfigurationFileStorageException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}